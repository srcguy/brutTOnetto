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

namespace brutTOnetto
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    /// 
    public partial class Sheet : Window
    {
        public List<TextBox> inputBoxes = new();
        public List<Label> LabelList = new List<Label>();

        public Sheet()
        {
            InitializeComponent();

            policz.Click += (s, e) => calc(s, e, LabelList);

            for (int i = 1; i < 13; i++)
            {
                TextBox tb = new TextBox { Name = "input" + i, Text = "0" };
                Grid.SetRow(tb, i);
                Grid.SetColumn(tb, 1);
                Label pit = new Label { Content = "0" , Name = "pit" + i};
                Grid.SetRow(pit, i);
                Grid.SetColumn(pit, 2);
                Label eme = new Label { Content = "0", Name = "eme" + i };
                Grid.SetRow(eme, i);
                Grid.SetColumn(eme, 3);
                Label ren = new Label { Content = "0", Name = "ren" + i };
                Grid.SetRow(ren, i);
                Grid.SetColumn(ren, 4);
                Label cho = new Label { Content = "0", Name = "cho" + i };
                Grid.SetRow(cho, i);
                Grid.SetColumn(cho, 5);
                Label zdr = new Label { Content = "0", Name = "zdr" + i };
                Grid.SetRow(zdr, i);
                Grid.SetColumn(zdr, 6);
                Label kup = new Label { Content = "0", Name = "kup" + i };
                Grid.SetRow(kup, i);
                Grid.SetColumn(kup, 7);
                Label nar = new Label { Content = "0", Name = "nar" + i };
                Grid.SetRow(nar, i);
                Grid.SetColumn(nar, 8);
                Label netto = new Label { Content = "0", Name = "netto" + i };
                Grid.SetRow(netto, i);
                Grid.SetColumn(netto, 9);
                MainGrid.Children.Add(tb);
                MainGrid.Children.Add(pit);
                MainGrid.Children.Add(eme);
                MainGrid.Children.Add(ren);
                MainGrid.Children.Add(cho);
                MainGrid.Children.Add(zdr);
                MainGrid.Children.Add(kup);
                MainGrid.Children.Add(nar);
                MainGrid.Children.Add(netto);
                inputBoxes.Add(tb);
                LabelList.Add(eme);
                LabelList.Add(ren);
                LabelList.Add(cho);
                LabelList.Add(zdr);
                LabelList.Add(kup);
                LabelList.Add(pit);
                LabelList.Add(netto);
                LabelList.Add(nar);
            }
        }

        private void calc(object sender, RoutedEventArgs e, List<Label> LabelList)
        {
            int n = 0;
            decimal narastajace = 0m;

            for (int i = 0; i < 12; i++)
            {
                if (decimal.TryParse(inputBoxes[i].Text, out decimal brutto_int))
                {

                    decimal procent = 0.0976m;
                    decimal em_int = Math.Round(Decimal.Multiply(brutto_int, procent), 2); //emerytura
                    LabelList[n].Content = em_int;
                    procent = 0.015m;
                    decimal rent_int = Math.Round(Decimal.Multiply(brutto_int, procent), 2); //renta
                    LabelList[n+1].Content = rent_int;
                    procent = 0.0245m;
                    decimal chor_int = Math.Round(Decimal.Multiply(brutto_int, procent), 2); //chorobowe
                    LabelList[n+2].Content = chor_int;
                    procent = 0.09m;
                    decimal zdr_int = Math.Round(Decimal.Multiply(brutto_int - em_int - rent_int - chor_int, procent), 2); //zdrowotne
                    LabelList[n+3].Content = zdr_int;

                    decimal kup_int = Math.Round(decimal.Parse(kup.Text), 2);

                    brutto_int = brutto_int - em_int - rent_int - chor_int; //do opodatkowania

                    decimal do_opodatkowania;

                    if (brutto_int <= 0)
                    {
                        do_opodatkowania = 0m;
                    }
                    else
                    {
                        do_opodatkowania = brutto_int - kup_int;
                    }

                    decimal nowe_narastajace = narastajace + do_opodatkowania;

                    decimal kwota12 = 0m;
                    decimal kwota32 = 0m;

                    if (ml_ulg.IsChecked == false)
                    {
                        if (nowe_narastajace <= 120000m)
                        {
                            kwota12 = do_opodatkowania;
                        }
                        else
                        {
                            if (narastajace < 120000m)
                            {
                                kwota12 = 120000m - narastajace;
                                kwota32 = do_opodatkowania - kwota12;
                            }
                            else
                            {
                                kwota32 = do_opodatkowania;
                            }
                        }
                        decimal procent12 = 0.12m;
                        decimal procent32 = 0.32m;
                        narastajace = narastajace + do_opodatkowania;
                        LabelList[n + 7].Content = narastajace;
                        LabelList[n + 4].Content = kup_int;
                        decimal podatek = (kwota12 * procent12) + (kwota32 * procent32);
                        decimal pit_int = 0m;
                        if (podatek > 300m)
                        {
                            pit_int = Math.Round(Math.Round((kwota12 * procent12) + (kwota32 * procent32) - 300m, 2));
                        }
                        brutto_int = brutto_int - pit_int;
                    
                        LabelList[n+5].Content = pit_int;
                    }
                    else
                    {
                        procent = 0m;
                        LabelList[n + 4].Content = 0;
                        decimal pit_int = 0m;
                        brutto_int = brutto_int - pit_int;

                        LabelList[n + 5].Content = pit_int;
                    }
                    brutto_int = Math.Round(brutto_int - zdr_int, 2);
                    LabelList[n+6].Content = brutto_int;
                    n = n + 8;
                }
            }

        }

        private void UDM_info(object sender, MouseEventArgs e)
        {
            UDM_popup.IsOpen = true;
        }
        private void UDM_info_hide(object sender, MouseEventArgs e)
        {
            UDM_popup.IsOpen = false;
        }

    }
}
