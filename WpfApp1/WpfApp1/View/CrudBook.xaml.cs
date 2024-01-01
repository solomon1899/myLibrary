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
using System.Net;
using System.Xml.Linq;

namespace WpfApp1.View
{
    public partial class CrudBook : Window
    {
        private SqlDataAdapter sqlDataAdapter;

        public SqlConnection Con { get; private set; }

        public CrudBook()
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
            string query = "SELECT * FROM Book";

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
                    SqlCommand command = new SqlCommand("INSERT INTO Book (Title, Author, Subject, NumberOfCopied, Description, Category, LendingStatus) VALUES (@Title, @Author, @Subject, @NumberOfCopied, @Description, @Category, @LendingStatus)", con);                   
                    command.Parameters.AddWithValue("@Title", textBox1.Text);
                    command.Parameters.AddWithValue("@Author", textBox2.Text);
                    command.Parameters.AddWithValue("@Subject", textBox6.Text);
                    command.Parameters.AddWithValue("@NumberOfCopied", textBox3.Text);
                    command.Parameters.AddWithValue("@Description", textBox4.Text);
                    command.Parameters.AddWithValue("@Category", textBox5.Text); 
                    command.Parameters.AddWithValue("@LendingStatus", ComboBox1.Text);

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
                int BookId = Convert.ToInt32(selectedRow["BookId"]);

                // Récupérer les nouvelles valeurs des champs FullName, PhoneNumber et MembershipStatus
                string Title = textBox1.Text;
                string Author = textBox2.Text;
                string Subject = textBox6.Text;
                string NumberOfCopied = textBox3.Text;
                string Description = textBox4.Text;
                string Category = textBox5.Text;               
                string LendingStatus = ComboBox1.Text;

                // Mettre à jour les données dans la base de données
                UpdateBook(BookId, Title, Author, Subject, NumberOfCopied, Description, Category, LendingStatus);

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
        private void UpdateBook(int BookId, string Title, string Subject, string Author, string NumberOfCopied, string Description, string Category, string LendingStatus)
        {
            string connectionString = "Data source=DESKTOP-IBFNT2P\\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction(); // Début de la transaction

                try
                {
                   
                    SqlCommand command = new SqlCommand("UPDATE Book SET Title = @Title, Author = @Author, Subject = @Subject, NumberOfCopied = @NumberOfCopied, Description = @Description, Category = @Category, LendingStatus = @LendingStatus WHERE BookId = @BookId", con);
                    command.Parameters.AddWithValue("@Title", Title);
                    command.Parameters.AddWithValue("@Author", Author);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.Parameters.AddWithValue("@NumberOfCopied", NumberOfCopied);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@Category", Category);
                    command.Parameters.AddWithValue("@LendingStatus", LendingStatus);                   
                    command.Parameters.AddWithValue("@BookId", BookId);

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
                int BookId = Convert.ToInt32(selectedRow["BookId"]);

                // Demander une confirmation avant de supprimer la ligne
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Supprimer la ligne de la base de données
                    DeleteBook(BookId);

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

        private void DeleteBook(int BookId)
        {
            string connectionString = "Data source=DESKTOP-IBFNT2P\\SQLEXPRESS; Initial Catalog=LibraryDB; Integrated Security=true; TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction(); // Début de la transaction

                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Book WHERE BookId = @BookId", con);
                    command.Parameters.AddWithValue("@BookId", BookId);

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
        dataView.RowFilter = $"Title LIKE '%{searchValue}%' OR Author LIKE '%{searchValue}%' OR BookId = '{searchValue}'";

        // Vérifier si des résultats de recherche sont disponibles
        if (dataView.Count > 0)
        {
            // Mettre à jour le DataGrid avec les résultats de recherche
            dataGrid.ItemsSource = dataView;
        }
        else
        {
            MessageBox.Show("Aucune correspondance trouvée. Veuillez réessayer avec un autre titre, auteur ou ID de livre.");
        }
    }
    else
    {
        // Si aucune valeur de recherche n'est fournie, rétablir l'affichage de toutes les lignes
        DataTable dataTable = GetTableDataFromDatabase();
        dataGrid.ItemsSource = dataTable.DefaultView;
    }
       
        }

        private void Button_CrudUser(object sender, RoutedEventArgs e)
        {
            CrudUser crudUserWindow = new CrudUser();
            crudUserWindow.Show();
            this.Hide();
        }

    }


}


