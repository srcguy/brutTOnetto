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
                double result;
                if (double.TryParse(brutto.Text, out result))
                { 
                    double brutto_int = double.Parse(brutto.Text);

                    double em_int = Math.Round(brutto_int * 0.0976, 2); //emerytura
                    em.Content = "Emerytalne (9,76%): " + em_int;
                    double rent_int = Math.Round(brutto_int * 0.015, 2); //renta
                    rent.Content = "Rentowe (1,5%): " + rent_int;
                    double chor_int = Math.Round(brutto_int * 0.0245, 2); //chorobowe
                    cho.Content = "Chorobowe (2,45%): " + chor_int;
                    double zdr_int = Math.Round((brutto_int - em_int - rent_int - chor_int) * 0.09, 2); //zdrowotne
                    zdr.Content = "Zdrowotne (9%): " + zdr_int;

                    brutto_int = brutto_int - em_int - rent_int - chor_int; //do opodatkowania

                    double kup_int = Math.Round(double.Parse(kup.Text), 2);

                    if (ml_ulg.IsChecked == false)
                    {
                        double pit_int = Math.Round(Math.Round((brutto_int - kup_int) * 0.12) - 300.00, 2);
                        brutto_int = brutto_int - pit_int;

                        pit.Content = "PIT (12%): " + pit_int;
                    }
                    else 
                    {
                        pit.Content = "PIT (0%)";
                    }
                    brutto_int = Math.Round(brutto_int - zdr_int, 2);
                    netto.Content = "Wynagrodzenie netto: " + brutto_int;
                    help.Content = "?";
                }
                else
                {
                    netto.Content = "Niepoprawna wartość";
                }
            }
        }

        private void show(object sender, MouseEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void unshow(object sender, MouseEventArgs e)
        {
            popup.IsOpen = false;
        }

    }
}