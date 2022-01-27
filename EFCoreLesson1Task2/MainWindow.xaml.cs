using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EFCoreLesson1Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void showBtn_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = await GetUsers();
            dgrid.ItemsSource = users;
        }

        private void generateBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateUsers();
            MessageBox.Show("Values were generated");
        }


        private async Task<List<User>> GetUsers()
        {
            return await Task.Run(() =>
            {
                using (AppContext db = new AppContext())
                {
                    List<User> users = db.Users.ToList();
                    return users;
                }
            }
            );
        }

        private async void CreateUsers()
        {
            User u1 = new User() { UserName = "Pavel", BirthDate = new DateTime(1999, 01, 01) };
            User u2 = new User() { UserName = "Vladimir", BirthDate = new DateTime(1999, 01, 01) };
            User u3 = new User() { UserName = "Georgiy", BirthDate = new DateTime(1999, 01, 01) };
            await Task.Run(() =>
            {
                using (AppContext db = new AppContext())
                {
                    db.Users.AddRange(u1, u2, u3);
                    db.SaveChanges();
                }
            }
            );
        }

        private void dropDB_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                db.Database.EnsureDeleted();
                MessageBox.Show("DB was deleted");
                showBtn_Click(sender, e);
            }
        }
    }
}
