using NL_text_representation.SemBuilding;
using NL_text_representation.SPARQL;
using nli_to_lod.Sparql;
using System.Windows;
using System.Windows.Controls;

namespace nli_to_lod
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nlRequest = inputRequest.Text;
            if (nlRequest.Length > 0)
            {
                SemBuilder semBuilder = new SemBuilder();
                string repr = semBuilder.getSemReprRequest(nlRequest);

                SparqlBuilder sparqlBuilder = new SparqlBuilder();
                string request = sparqlBuilder.getSparql(repr);

                SparqlRunner sparqlRunner = new SparqlRunner();

                var result = sparqlRunner.runQuery(request);
                               
                results.ItemsSource = result;
            }
        }

        private void results_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (results.SelectedItem != null)
            {
                string destinationurl = ((ResultValue)results.SelectedItem).Value;
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
                results.UnselectAll();
            }
        }
    }
}
