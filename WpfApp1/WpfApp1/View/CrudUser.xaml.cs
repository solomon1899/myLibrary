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
        private SqlDataAdapter sqlDataAdapter;

        public SqlConnection Con { get; private set; }

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

        //Boton Insert
        private void Button_Insert(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data source=DESKTOP-IBFNT2P\\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction(); // Début de la transaction

                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Member (FullName, PhoneNumber, MembershipStatus) VALUES (@FullName, @PhoneNumber, @MembershipStatus)", con);
                    command.Parameters.AddWithValue("@FullName", textBox1.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", textBox2.Text);
                    command.Parameters.AddWithValue("@MembershipStatus", ComboBox1.Text);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    transaction.Commit(); // Validation de la transaction

                    MessageBox.Show("Successfully Inserted.");
                    // Actualiser les données du DataGrid
                    DataTable dataTable = GetTableDataFromDatabase();
                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Annulation de la transaction en cas d'erreur
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        //Boton update 
        private void Button_Update(object sender, RoutedEventArgs e)
        {
            // Récupérer la ligne sélectionnée dans le DataGrid
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                // Récupérer l'ID du membre à partir de la colonne "MemberID"
                int memberId = Convert.ToInt32(selectedRow["MemberID"]);

                // Récupérer les nouvelles valeurs des champs FullName, PhoneNumber et MembershipStatus
                string fullName = textBox1.Text;
                string phoneNumber = textBox2.Text;
                string membershipStatus = ComboBox1.Text;

                // Mettre à jour les données dans la base de données
                UpdateMember(memberId, fullName, phoneNumber, membershipStatus);

                // Actualiser les données du DataGrid
                DataTable dataTable = GetTableDataFromDatabase();
                dataGrid.ItemsSource = dataTable.DefaultView;

                MessageBox.Show("Successfully Updated.");
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        //methode pour update tablaux
        private void UpdateMember(int memberId, string fullName, string phoneNumber, string membershipStatus)
        {
            string connectionString = "Data source=DESKTOP-IBFNT2P\\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction(); // Début de la transaction

                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Member SET FullName = @FullName, PhoneNumber = @PhoneNumber, MembershipStatus = @MembershipStatus WHERE MemberID = @MemberID", con);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@MembershipStatus", membershipStatus);
                    command.Parameters.AddWithValue("@MemberID", memberId);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    transaction.Commit(); // Validation de la transaction
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Annulation de la transaction en cas d'erreur
                    MessageBox.Show("Error occurred during update: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //boton DELETE
        private void Button_DELETE(object sender, RoutedEventArgs e)
        {
            // Vérifiez si une ligne est sélectionnée dans le DataGrid
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                // Récupérer l'ID du membre à partir de la colonne "MemberID"
                int memberId = Convert.ToInt32(selectedRow["MemberID"]);

                // Demander une confirmation avant de supprimer la ligne
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Supprimer la ligne de la base de données
                    DeleteMember(memberId);

                    // Actualiser les données du DataGrid
                    DataTable dataTable = GetTableDataFromDatabase();
                    dataGrid.ItemsSource = dataTable.DefaultView;

                    MessageBox.Show("Successfully Deleted.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }
        //methode delete 

        private void DeleteMember(int memberId)
        {
            string connectionString = "Data source=DESKTOP-IBFNT2P\\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction(); // Début de la transaction

                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Member WHERE MemberID = @MemberID", con);
                    command.Parameters.AddWithValue("@MemberID", memberId);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    transaction.Commit(); // Validation de la transaction
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Annulation de la transaction en cas d'erreur
                    MessageBox.Show("Error occurred during delete: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            // Récupérer la valeur de recherche à partir d'une zone de texte
            string searchValue = textBoxSearch.Text;

            // Vérifier si une valeur de recherche est fournie
            if (!string.IsNullOrEmpty(searchValue))
            {
                // Effectuer la recherche dans le DataTable
                DataTable dataTable = GetTableDataFromDatabase();
                DataView dataView = dataTable.DefaultView;
                dataView.RowFilter = $"FullName LIKE '%{searchValue}%' OR MemberID = '{searchValue}'";

                // Mettre à jour le DataGrid avec les résultats de recherche
                dataGrid.ItemsSource = dataView;
            }
            else
            {
                // Si aucune valeur de recherche n'est fournie, rétablir l'affichage de toutes les lignes
                DataTable dataTable = GetTableDataFromDatabase();
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        // les boton de menu
        private void Button_CRUDBOOK(object sender, RoutedEventArgs e)
        {
            CrudBook CrudBookWindow = new CrudBook();
            CrudBookWindow.Show();
            this.Hide();
        }
    }


    
    

}