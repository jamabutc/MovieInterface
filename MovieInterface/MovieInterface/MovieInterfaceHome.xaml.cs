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
using System.Data.OleDb;

namespace MovieInterface
{
    /// <summary>
    /// Interaction logic for MovieInterfaceHome.xaml
    /// </summary>
    public partial class MovieInterfaceHome : Page
    {
        private List<List<string>> moviesShown = new List<List<string>>();
        public MovieInterfaceHome()
        {
            InitializeComponent();
            using (OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; 
            Data Source =|DataDirectory|\MartinMovies.accdb"))
            {
                string query = "select Titles, [Release Date], Description, Director, Genre from Titles";
                OleDbCommand cmd = new OleDbCommand(query, cn);
                cn.Open();
                OleDbDataReader read = cmd.ExecuteReader();
                clearList();
                while (read.Read())
                {
                    List<string> items = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        items.Add(read[i].ToString());
                    }
                    addItemToList(items);
                }
                read.Close();
            }
            ViewBtn.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // View Movie
            MovieInformation movieInformation = new MovieInformation();
            int index = movie_titleListBox.SelectedIndex;
            movieInformation.setInfo(moviesShown[index][0], moviesShown[index][4], moviesShown[index][3]);
            this.NavigationService.Navigate(movieInformation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Search Movies
            SearchMovies searchMovies = new SearchMovies();
            this.NavigationService.Navigate(searchMovies);
        }

        public void addItemToList(List<string> items)
        {
            if (items.Count == 5)
            {
                moviesShown.Add(items);
                movie_titleListBox.Items.Add(items[0]);
            }
        }

        public void clearList()
        {
            ViewBtn.IsEnabled = false;
            moviesShown.Clear();
            movie_titleListBox.Items.Clear();
        }

        private void movie_titleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewBtn.IsEnabled = true;
        }
    }
}
