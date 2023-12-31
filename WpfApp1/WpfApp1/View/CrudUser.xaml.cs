
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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1.View
{
    public partial class CrudUser : Window
    {
        public CrudUser()
        {
            InitializeComponent();
            // Récupérer les données du tableau à partir de la base de données
            DataTable dataTable = GetTableDataFromDatabase();

            // Lier les données au DataGrid
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private DataTable GetTableDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            // Chaîne de connexion à votre base de données SQL Server
            string connectionString = "Data source = DESKTOP-IBFNT2P\\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True";

            // Requête SQL pour récupérer les données du tableau
            string query = "SELECT * FROM Member";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    // Utiliser un DataAdapter pour remplir le DataTable avec les données du tableau
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}