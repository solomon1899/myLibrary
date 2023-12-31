using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class LoginWindow : Window
    {
        public TextBox TxtAdminName => txtAdminName;
        public PasswordBox TxtPassword => txtPassword;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data source = DESKTOP-IBFNT2P\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True"))
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    String query = "select count(1) from LoginAdmin where AdminName=@AdminName and Password=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@AdminName", txtAdminName.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);

                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if (count == 1)
                    {
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ASIR T9***.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
