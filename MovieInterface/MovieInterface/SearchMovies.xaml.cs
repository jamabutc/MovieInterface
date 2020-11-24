using System;
using System.Collections.Generic;
using System.Data.OleDb;
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

namespace MovieInterface
{
    /// <summary>
    /// Interaction logic for SearchMovies.xaml
    /// </summary>
    public partial class SearchMovies : Page
    {
        private string searchType = "";
        public SearchMovies()
        {
            InitializeComponent();
        }

        // Help found at https://www.dotnetperls.com/combobox-wpf
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("Title");
            data.Add("Director");
            data.Add("Genre");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Home Page
            MovieInterfaceHome movieInterfaceHome = new MovieInterfaceHome();
            this.NavigationService.Navigate(movieInterfaceHome);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            string value = comboBox.SelectedItem as string;
            this.Title = "Selected: " + value;
            searchType = value;
        }

        private void SearchMovie(object sender, RoutedEventArgs e)
        {
            using (OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; 
            Data Source =|DataDirectory|\MartinMovies.accdb"))
            {
                string query = "";
                if (searchType == "Title")
                {
                    query = "select Titles, [Release Date], Description, Director, Genre from Titles where Titles LIKE '%' + @value + '%'";
                } else if (searchType == "Director")
                {
                    query = "select Titles, [Release Date], Description, Director, Genre from Titles where Director LIKE '%' + @value + '%'";
                } else
                {
                    query = "select Titles, [Release Date], Description, Director, Genre from Titles where Genre LIKE '%' + @value + '%'";
                }
                OleDbCommand cmd = new OleDbCommand(query, cn);
                OleDbParameter titleParam = new OleDbParameter();
                titleParam.OleDbType = OleDbType.VarChar;
                titleParam.ParameterName = "@value";
                titleParam.Value = SearchText.Text;

                cmd.Parameters.Add(titleParam);

                cn.Open();
                MovieInterfaceHome searchMovies = new MovieInterfaceHome();
                OleDbDataReader read = cmd.ExecuteReader();
                searchMovies.clearList();
                while (read.Read())
                {
                    List<string> items = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        items.Add(read[i].ToString());
                    }
                    searchMovies.addItemToList(items);
                }
                read.Close();


                this.NavigationService.Navigate(searchMovies);
            }
        }
    }
}
