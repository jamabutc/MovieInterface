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

namespace MovieInterface
{
    /// <summary>
    /// Interaction logic for MovieInformation.xaml
    /// </summary>
    public partial class MovieInformation : Page
    {
        private List<string> directors { get; }
        public MovieInformation()
        {
            InitializeComponent();
            directors = new List<string>();
            DirectorGrid.ItemsSource = this.directors;
        }

        public void setInfo(string title, string genre, string directors)
        {
            TitleText.Content = title;
            GenreText.Content = genre;
            string[] allDirectors = directors.Split(';');
            foreach (string director in allDirectors)
            {
                this.directors.Add(director.Trim());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TitleText.Content = "";
            GenreText.Content = "";
            this.directors.Clear();
            // Home Page
            MovieInterfaceHome movieInterfaceHome = new MovieInterfaceHome();
            this.NavigationService.Navigate(movieInterfaceHome);
        }
    }
}
