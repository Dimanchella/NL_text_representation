using NL_text_representation.SemBuilding;
using NL_text_representation.SPARQL;
using nli_to_lod.Exceptinos;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NL_text_representation
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
                try
                {
                    SemBuilder semBuilder = new SemBuilder();
                    string repr = semBuilder.getSemReprRequest(nlRequest);

                    SparqlBuilder sparqlBuilder = new SparqlBuilder();
                    string request = sparqlBuilder.getSparql(repr);

                    SparqlRunner sparqlRunner = new SparqlRunner();

                    var result = sparqlRunner.runQuery(request);

                    results.ItemsSource = result;
                }
                catch (DBException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (IncorrectRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("Обработка одного или нескольких слов, использующихся в запросе, не поддерживается");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неизвестная ошибка");
                }
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
