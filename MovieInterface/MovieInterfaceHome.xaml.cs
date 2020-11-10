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
    /// Interaction logic for MovieInterfaceHome.xaml
    /// </summary>
    public partial class MovieInterfaceHome : Page
    {
        public MovieInterfaceHome()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // View Movie
            MovieInformation movieInformation = new MovieInformation();
            this.NavigationService.Navigate(movieInformation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Search Movies
            SearchMovies searchMovies = new SearchMovies();
            this.NavigationService.Navigate(searchMovies);
        }
    }
}
