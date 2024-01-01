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
using System.Windows.Shapes;
using System.Windows;
using WpfApp1.View;

namespace WpfApp1
{
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void Button_CrudUser(object sender, RoutedEventArgs e)
        {
            CrudUser crudUserWindow = new CrudUser();
            crudUserWindow.Show();
            this.Hide();
        }

        private void Button_CRUDBOOK(object sender, RoutedEventArgs e)
        {
            CrudBook CrudBookWindow = new CrudBook();
            CrudBookWindow.Show();
            this.Hide();
        }
    }
}