using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace brutTOnetto
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

        private void calc(object sender, RoutedEventArgs e)
        {
            if (brutto.Text != null)
            {
                double brutto_int = double.Parse(brutto.Text);

                double em_int = Math.Round(brutto_int * 0.0976); //emerytura
                em.Content = "Emerytalne (9,76%): " + em_int;
                double rent_int = Math.Round(brutto_int * 0.015); //renta
                rent.Content = "Rentowe (1,5%): " + rent_int;
                double chor_int = Math.Round(brutto_int * 0.0245); //chorobowe
                cho.Content = "Chorobowe (2,45%): " + chor_int;
                double zdr_int = Math.Round(brutto_int * 0.09); //zdrowotne
                zdr.Content = "Zdrowotne (9%): " + zdr_int;
                brutto_int = brutto_int - em_int - rent_int - chor_int - zdr_int;
                if (brutto_int < 2500)
                {
                    brutto_int = brutto_int;
                    double pit_int = Math.Round(brutto_int * 0);
                    pit.Content = "PIT (0%): " + pit_int;
                }
                else if (brutto_int > 2500 && brutto_int < 10000)
                {
                    double pit_int = Math.Round(brutto_int * 0.12);
                    brutto_int = brutto_int - pit_int;
                    pit.Content = "PIT (12%): " + pit_int;
                }
                else if (brutto_int > 10000)
                {
                    double pit_int = Math.Round(brutto_int * 0.32);
                    brutto_int = brutto_int - pit_int;
                    pit.Content = "PIT (32%): " + pit_int;
                }
                netto.Content = "Wynagrodzenie netto: " + brutto_int;
            }
        }
    }
}