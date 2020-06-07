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

namespace ProjektProgramowanieObjektowe
{
    /// <summary>
    /// Jest to klasa odpowiadająca za logiczne operacje do Portfel.xaml.Klasa ta odpowiada za pozwala na obsługiwanie wszystkich operacji które implementacje mają w code behind.
    /// </summary>
    public partial class Portfel : Window
    {
        IPobieranieDanych pobieraniezBazy = null;
        public Portfel(IPobieranieDanych konto)
        {
            pobieraniezBazy = konto;
            this.MaxHeight = 400;
            this.MinHeight = 400;
            this.MaxWidth = 500;
            this.MinWidth = 500;
            

            InitializeComponent();

            OtwieranieOkna.otwarteOkno = true;

            WyswietlKase.DataContext = pobieraniezBazy;
            Hwplat.ItemsSource = pobieraniezBazy.PokazWplaty();
            Hwyplat.ItemsSource = pobieraniezBazy.PokazWyplaty();
            //WyswietlKase.DataContext = konto;

        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StanKonta.Visibility = Visibility.Visible;
            HistoriaWplat.Visibility = Visibility.Collapsed;
            HistoriaWyplat.Visibility = Visibility.Collapsed;
            WyplataPieniedzy.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility).
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Wplacanie.Visibility = Visibility.Collapsed;
            Visa.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Metoda odpowiedzialna za sprawdzanie czy podane dane do karty płatniczej są poprawne.
        /// </summary>
        /// <remarks>
        /// Metoda sprawdza czy Środki na karcie są wystarczające oraz czy taka karta istnieje,jeżeli tak fundusze konta zostają odpowiednio zmniejszone do podanej prze użytkownika kwoty.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pobieraniezBazy.CzyJestTakaKartaiSrodki(Nrkarta.Text, KodKarta.Text, ImieKarta.Text, NazwiskoKarta.Text, float.Parse(Kwota.Text)) == true)
                {
                    Visa.Visibility = Visibility.Collapsed;
                    niepoprawnakarta.Visibility = Visibility.Collapsed;
                    PotwiedzeniePlatnosci.Visibility = Visibility.Visible;
                    Zerowanie();
                }
                else
                {
                    niepoprawnakarta.Visibility = Visibility.Visible;

                }

            }
            catch (OverflowException error1)
            {
                MessageBox.Show(error1.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (FormatException error2)
            {
                MessageBox.Show(error2.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception error3)
            {

                MessageBox.Show(error3.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);


            }


        }


        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility).
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StanKonta.Visibility = Visibility.Visible;
            PotwiedzeniePlatnosci.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) oraz ustawianie właściwości Text kontrolek na puste.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Visa.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Visible;
            Zerowanie();

        }

        /// <summary>
        /// Metoda odpowiedzialna za ustawianie właściwości Text kontrolek na puste.
        /// </summary>
        private void Zerowanie()
        {
            ImieKarta.Text = string.Empty;
            NazwiskoKarta.Text = string.Empty;
            Nrkarta.Text = string.Empty;
            KodKarta.Text = string.Empty;
            Kwota.Text = string.Empty;
        }




        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) oraz dokonywanie płatnoći.
        /// </summary>
        /// <remarks>
        /// Metoda odwołuje się do metody klasy zapewniającej dostęp do bazy danych oraz sprawdza czy istnieje karta PaySafe która pozwoli na zasilenie konta użytkownika,jeżeli tak konto zostaje doładowane(zmiana wartości atrybutu relacji średkow na końcie)
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(pobieraniezBazy.CzyJestTakiPaysafe(wpisywanieKoduPaysafe.Text)==true)
            {
                PotwiedzeniePlatnosci.Visibility = Visibility.Visible;

                wpisywanieKoduPaysafe.Text = string.Empty;

                PaySafe.Visibility = Visibility.Collapsed;
            }
            else
            {
                niepprawnyPaysafe.Visibility = Visibility.Collapsed;
            }
        }


        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility).
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            StanKonta.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Collapsed;
            Visa.Visibility = Visibility.Collapsed;
            PotwiedzeniePlatnosci.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility).
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            WyplataPieniedzy.Visibility = Visibility.Visible;
            Visa.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Collapsed;
            HistoriaWplat.Visibility = Visibility.Collapsed;
            StanKonta.Visibility = Visibility.Collapsed;
            HistoriaWyplat.Visibility = Visibility.Collapsed;
            PaySafe.Visibility = Visibility.Collapsed;
        }




        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) oraz ustawianie zawartości kontrolek.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            WyplataPieniedzy.Visibility = Visibility.Collapsed;
            StanKonta.Visibility = Visibility.Visible;
            nrKonta.Text = string.Empty;
            Kwotawyplaty.Text = string.Empty;
            opisWyplaty.Text = string.Empty;
        }



        /// <summary>
        /// Metoda odpowiedzialna za wypłacanie funduszy na podane prze użytkownika konto.
        /// </summary>
        /// <remarks>
        /// Metoda ta odpowiada za sprawdzenie czy istnieje podane konto z konkretnymi danymi,jeżeli tak (i jeżeli obecna wartość funduszy jest większa bądź równa od tych które użytkownik chce wypłacić) następuje odjęcie środków z konta platformy oraz dodanie ich do konta w bazie danych( zwiększenie wartości właściwości która określa fundusze).
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if(pobieraniezBazy.CzyJestTakeKontobankowe(nrKonta.Text)==false)
            {
                niepoprawneDaneWyplaty.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    if (float.Parse(Kwotawyplaty.Text) <= pobieraniezBazy.Fundusze)
                    {
                        pobieraniezBazy.WyplataPieniedzy(float.Parse(Kwotawyplaty.Text),nrKonta.Text, opisWyplaty.Text);
                        niepoprawneDaneWyplaty.Visibility = Visibility.Collapsed;
                        PotwiedzeniePlatnosci.Visibility = Visibility.Visible;
                        WyplataPieniedzy.Visibility = Visibility.Collapsed;
                        nrKonta.Text = string.Empty;
                        Kwotawyplaty.Text = string.Empty;
                        opisWyplaty.Text = string.Empty;




                    }

                }
                catch (OverflowException error1)
                {
                    MessageBox.Show(error1.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (FormatException error2)
                {
                    MessageBox.Show(error2.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (Exception error3)
                {

                    MessageBox.Show(error3.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);


                }

            }
            
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) .
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HistoriaWyplat.Visibility = Visibility.Visible;
            HistoriaWplat.Visibility = Visibility.Collapsed;
            WyplataPieniedzy.Visibility = Visibility.Collapsed;
            StanKonta.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Collapsed;
            Visa.Visibility = Visibility.Collapsed;
            PaySafe.Visibility = Visibility.Collapsed;
            PotwiedzeniePlatnosci.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) .
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            HistoriaWplat.Visibility = Visibility.Visible;

            HistoriaWyplat.Visibility = Visibility.Collapsed;
            WyplataPieniedzy.Visibility = Visibility.Collapsed;
            StanKonta.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Collapsed;
            Visa.Visibility = Visibility.Collapsed;
            PaySafe.Visibility = Visibility.Collapsed;
            PotwiedzeniePlatnosci.Visibility = Visibility.Collapsed;

        }



        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) .
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Wplacanie.Visibility = Visibility.Visible;
            HistoriaWplat.Visibility = Visibility.Collapsed;
            StanKonta.Visibility = Visibility.Collapsed;
            HistoriaWyplat.Visibility = Visibility.Collapsed;
            Visa.Visibility = Visibility.Collapsed;
            WyplataPieniedzy.Visibility = Visibility.Collapsed;

        }


        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) .
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            PaySafe.Visibility = Visibility.Collapsed;
            Wplacanie.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Przełączanie pomiędzy panelami(zmina właściwości Visibility) .
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            PaySafe.Visibility = Visibility.Visible;
            Wplacanie.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// Metoda która sygnalizuje że drugie okno zostaje zamknięte.
        /// </summary>
        /// <remarks>
        /// Pozwala to zapobiegnięciu otworzenia jednego okna kilka razy w tym samym czasie.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OtwieranieOkna.otwarteOkno = false;
        }
    }
}
