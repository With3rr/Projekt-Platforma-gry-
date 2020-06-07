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
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Security;

namespace ProjektProgramowanieObjektowe
{
  
    
    /// <summary>
    /// Jest to główna klasa aplikacji w której znajduje się większość funkcjonalności.
    /// </summary>
    public partial class MainWindow : Window
    {

        private CollectionView widok = null;

        private CollectionView widok2 = null;


        public IPobieranieDanych dostepDoDanych = null;

        PrzeprowadzonaRozgrywka rozegranaGra = null;


        List<string> kolory = new List<string>();

        


        Random liczby_losowanie = new Random();

        List<TextBlock> listaCyfr = new List<TextBlock>();
        List<TextBlock> listaCyfrWybranych = new List<TextBlock>();
        
       
        private BazaGryHazardoweEntities bazadanych=null;

        /// <summary>
        /// Metoda main apikacji.W niej inicjalizowane są podstawowe komponenty do działania aplikacji.
        /// </summary>
        public MainWindow()
        {

            
            dostepDoDanych = new PobieranieDanych();
            bazadanych = new BazaGryHazardoweEntities();




            bazadanych.SaveChanges();

            this.MaxWidth = 1000;
            this.MinWidth = 1000;

            this.MinHeight = 600;
            this.MaxHeight = 600;


            InitializeComponent();





        }






        /// <summary>
        /// Przycisk przełączania pomiędzy logowaniem a rejestracją(zmiana właściwości visibility).
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Dologowania_Click(object sender, RoutedEventArgs e)
        {
            Logowanie.Visibility = Visibility.Visible;
            Rejestracja.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Przycisk przełączania pomiędzy logowaniem a rejestracją(zmiana właściwości visibility).
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void ClickToRegister_Click(object sender, RoutedEventArgs e)
        {
            Logowanie.Visibility = Visibility.Collapsed;
            Rejestracja.Visibility = Visibility.Visible;

        }

       /// <summary>
       /// Metoda ta odpowiada za przywracanie kontrolką text block ich początkowych wartości właściwości(resetowanie).
       /// </summary>
       /// <remarks>
       /// Ustawianie tych zawartości ma pozwolić na resetowanie wyświetlanych liczb w celu ponownego wykorzystania kontrolek w następnym podejściu do gry.
       /// </remarks>
        public void Clr_CyfrDoLosowaniaWyswietlanie()
        {
            for (int i = 0; i < listaCyfr.Count; i++)
            {
                listaCyfr[i].Text = string.Empty;
                listaCyfr[i].Foreground = Brushes.Black;
                listaCyfr[i].Background = null;


            }
            listaCyfr.Clear();


        }

        /// <summary>
        /// Metoda ta odpowiada za przywracanie kontrolką text block ich początkowych wartości właściwości(resetowanie).
        /// </summary>
        /// <remarks>
        /// Ustawianie tych zawartości ma pozwolić na resetowanie wyświetlanych liczb w celu ponownego wykorzystania kontrolek w następnym podejściu do gry.
        /// </remarks> 
        public void Clr_WYbranychLiczbWyswietlanie()
        {
            for (int i = 0; i < listaCyfrWybranych.Count; i++)
            {
                listaCyfrWybranych[i].Text = string.Empty;
                listaCyfrWybranych[i].Foreground = Brushes.Black;


            }
            listaCyfrWybranych.Clear();



        }

        /// <summary>
        /// Dodawanie kontrolek Text Block do kolekcji oraz losowanie dla nich zawartości jak i koloru które reprezentują.
        /// </summary>
        /// <remarks>
        /// Jest to mechanizm który ustawia zawartość kolekcji danych danymi wolosowanych prze program losowymi liczbami.Liczby te posłuża w dalszej częsci do porównywania z wylosowanymi przez użytkownika.
        /// </remarks>
        public void funkcjadodawaniaDoListyCyfr()
        {
            listaCyfr.Clear();
            listaCyfr.Add(Z1tb1);
            listaCyfr.Add(Z1tb2);
            listaCyfr.Add(Z1tb3);
            listaCyfr.Add(Z1tb4);
            listaCyfr.Add(Z1tb5);
            listaCyfr.Add(Z1tb6);
            listaCyfr.Add(Z1tb7);
            listaCyfr.Add(Z1tb8);
            listaCyfr.Add(Z1tb9);
            listaCyfr.Add(Z1tb10);
            listaCyfr.Add(Z1tb11);
            listaCyfr.Add(Z1tb12);
            listaCyfr.Add(Z1tb13);
            listaCyfr.Add(Z1tb14);
            listaCyfr.Add(Z1tb15);
            listaCyfr.Add(Z1tb16);

            listaCyfr.Add(Z2tb1);
            listaCyfr.Add(Z2tb2);
            listaCyfr.Add(Z2tb3);
            listaCyfr.Add(Z2tb4);
            listaCyfr.Add(Z2tb5);
            listaCyfr.Add(Z2tb6);
            listaCyfr.Add(Z2tb7);
            listaCyfr.Add(Z2tb8);
            listaCyfr.Add(Z2tb9);
            listaCyfr.Add(Z2tb10);
            listaCyfr.Add(Z2tb11);
            listaCyfr.Add(Z2tb12);
            listaCyfr.Add(Z2tb13);
            listaCyfr.Add(Z2tb14);
            listaCyfr.Add(Z2tb15);
            listaCyfr.Add(Z2tb16);


            bool czy = true;
            for (int i = 0; i < listaCyfr.Count; i++)
            {
                czy = true;
                int a = (liczby_losowanie.Next(0, 50));
                for (int j = 0; j <i ; j++)
                {
                    if(listaCyfr[j].Text==a.ToString())
                    {
                        czy = false;
                    }

                }
                if(czy==true)
                {
                    listaCyfr[i].Text = a.ToString();

                }
                else
                {
                    i--;
                }


            }
            kolory = new List<string>();
            for (int i = 0; i < listaCyfr.Count; i++)
            {
                int kolor= liczby_losowanie.Next(0, 4);
                if(kolor==0)
                {
                    listaCyfr[i].Foreground = Brushes.Red;
                    kolory.Add("czerwony");




                }
                if (kolor == 1)
                {
                    listaCyfr[i].Foreground = Brushes.Blue;
                    kolory.Add("niebieski");




                }
                if (kolor == 2)
                {
                    listaCyfr[i].Foreground = Brushes.Green;
                    kolory.Add("zielony");




                }
                if (kolor == 3)
                {
                    listaCyfr[i].Foreground = Brushes.Brown;
                    kolory.Add("brazowy");




                }
                if (kolor == 4)
                {
                    listaCyfr[i].Foreground = Brushes.Orange;
                    kolory.Add("pomaranczowy");




                }




            }


            


        }



        /// <summary>
        /// Metoda która obsługuje operacje weryfikacji danych logowania użytkownika.
        /// </summary>
        /// <remarks>
        /// Metoda sprawdza czy wszystkie dane podane przez użytkownika do założenia konta spełniają określone założenia takie jak długość czy spujnosć.
        /// Jeżeli dane są poprawne konto zostaje utwożone,w przeciwnym przypadku zostają wyświetlone odpowiednie komunikaty.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Rejestrowanie_Click(object sender, RoutedEventArgs e)
        {
            if (passwordRejestracja.Text == string.Empty || LoginRejestracja.Text == string.Empty|| pytanieRejestracja.Text == string.Empty || emailRejestracja.Text == string.Empty || CzyDaneSpelniajaZalozenia(passwordRejestracja.Text)|| CzyDaneSpelniajaZalozenia(LoginRejestracja.Text)  || CzyDaneSpelniajaZalozenia(emailRejestracja.Text) || CzyDaneSpelniajaZalozenia(pytanieRejestracja.Text) )
            {
                bladRejestracji.Visibility = Visibility.Visible;

                if (passwordRejestracja.Text == string.Empty || CzyDaneSpelniajaZalozenia(passwordRejestracja.Text))
                {
                    passwordRejestracja.BorderBrush = Brushes.Red;
                }
                else
                {
                    passwordRejestracja.BorderBrush = Brushes.Black;

                }
                if (LoginRejestracja.Text == string.Empty || CzyDaneSpelniajaZalozenia(LoginRejestracja.Text))
                {
                    LoginRejestracja.BorderBrush = Brushes.Red;
                }
                else
                {
                    LoginRejestracja.BorderBrush = Brushes.Black;

                }
                if (emailRejestracja.Text == string.Empty || CzyDaneSpelniajaZalozenia(emailRejestracja.Text)) 
                {
                    emailRejestracja.BorderBrush = Brushes.Red;
                }
                else
                {
                    emailRejestracja.BorderBrush = Brushes.Black;

                }
                if (pytanieRejestracja.Text == string.Empty || CzyDaneSpelniajaZalozenia(pytanieRejestracja.Text))
                {
                    pytanieRejestracja.BorderBrush = Brushes.Red;
                }
                else
                {
                    pytanieRejestracja.BorderBrush = Brushes.Black;

                }


            }
            else
            {
                bladRejestracji.Visibility = Visibility.Visible;
                passwordRejestracja.BorderBrush = Brushes.Black;
                LoginRejestracja.BorderBrush = Brushes.Black;
                emailRejestracja.BorderBrush = Brushes.Black;
                pytanieRejestracja.BorderBrush = Brushes.Black;

                string Nick = LoginRejestracja.Text;
                string Email = emailRejestracja.Text;
                string haslo = passwordRejestracja.Text;
                string pytanko = pytanieRejestracja.Text;
                if (dostepDoDanych.CzyJestUrzytkownikwBazieRejestracja(Nick, Email) == true)
                {
                    bladRejestracji.Visibility = Visibility.Visible;


                }
                else
                {
                    bladRejestracji.Visibility = Visibility.Collapsed;
                    dostepDoDanych.DodajUzytkownika(Nick,Email,haslo,pytanko);

                    potwierdzenieRejestracji.Visibility = Visibility.Visible;
                    Rejestracja.Visibility = Visibility.Collapsed;
                    Logowanie.Visibility = Visibility.Collapsed;

                    LoginRejestracja.Text = String.Empty;
                    emailRejestracja.Text = String.Empty;
                    passwordRejestracja.Text = String.Empty;
                    pytanieRejestracja.Text = String.Empty;



                }


            }

        }


        /// <summary>
        /// Metoda obsługująca operacje logowania.
        /// </summary>
        /// <remarks>
        /// Działanie tej metody jest nieco podobne do tej odpowiedzialnej za rejestrację tj. spawdzane są dane pod względem poprawności z bazą danych a jezeli tak jest następuje zalogwanie i przełączenie pomiędzy panelami.
        /// Metoda ta dodatkowo sprawdza czy użytkownik logujący się nie posiada uprawnień administratora,jeżeli tak następuje przełączenie pomiędzy panelem odpowiadającym za operacje administracyjne.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void LogInNow_Click(object sender, RoutedEventArgs e)
        {
            string loginLogowanie = string.Empty;
            string PasswordLogowanie = string.Empty;
            loginLogowanie = emailusernameLogin.Text;
            PasswordLogowanie = passwordLogin.Password;

            if (loginLogowanie == string.Empty || PasswordLogowanie == string.Empty)
            {
                bladLogowania.Visibility = Visibility.Visible;
                emailusernameLogin.BorderBrush = Brushes.Red;
                passwordLogin.BorderBrush = Brushes.Red;



            }
            else
            {
                emailusernameLogin.BorderBrush = Brushes.Black;
                passwordLogin.BorderBrush = Brushes.Black;
                bladLogowania.Visibility = Visibility.Collapsed;


                

                

                if (dostepDoDanych.CzyJestUrzytkownikwBazie(loginLogowanie, PasswordLogowanie)==false)
                {
                    brakUsera.Visibility = Visibility.Visible;
                }

                else
                {
                    if(dostepDoDanych.CzyMaster()==true)
                    {
                        main3.Visibility = Visibility.Visible;

                        ObservableCollection<Konto> aaaaa = dostepDoDanych.PokazUzytkownikow();
                        ListaUzytkownikowDG.ItemsSource = dostepDoDanych.PokazUzytkownikow();

                        listaZmianZasad.ItemsSource = dostepDoDanych.PokazZmianyy();

                        zmianaMnoznika1.Text = dostepDoDanych.Mnozniki.Mnoznik_Kolory_dodatnie;
                        zmianaMnoznika2.Text = dostepDoDanych.Mnozniki.Mnoznik_Kolory_ujemne;
                        zmianaMnoznika3.Text = dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie;
                        zmianaMnoznika4.Text = dostepDoDanych.Mnozniki.Mnoznik_Gry_ujemne;




                    }
                    else
                    {
                        WyswietlKase.DataContext = dostepDoDanych;
                        main1.Visibility = Visibility.Collapsed;
                        main2.Visibility = Visibility.Visible;
                        MojeRozegraneGryWszystkich.ItemsSource = dostepDoDanych.PokazGry();
                        MojeRozegraneGry.ItemsSource = dostepDoDanych.PokazGryMoje();

                        widok = ((CollectionView)CollectionViewSource.GetDefaultView(MojeRozegraneGryWszystkich.ItemsSource));
                        widok.SortDescriptions.Add(new SortDescription("WartoscWygranej", ListSortDirection.Descending));

                        widok2 = ((CollectionView)CollectionViewSource.GetDefaultView(MojeRozegraneGry.ItemsSource));
                        widok2.Filter = Filtr;

                        ImieDane.Text = dostepDoDanych.Imie;
                        NazwiskoDane.Text = dostepDoDanych.Nazwisko;
                        HobbyDane.Text = dostepDoDanych.Hobby;
                        OsobieDane.Text = dostepDoDanych.Osobie;
                        PytPom.Text = dostepDoDanych.PytaniePom;

                        MNDodatniuz.Text = dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie;
                        MNUjemnyuz.Text= dostepDoDanych.Mnozniki.Mnoznik_Gry_ujemne;
                        KolorDodatniuz.Text= dostepDoDanych.Mnozniki.Mnoznik_Kolory_dodatnie;
                        KolorUjemnyuz.Text= dostepDoDanych.Mnozniki.Mnoznik_Kolory_ujemne;


                    }










                }






            }
        }

        /// <summary>
        /// Metoda sprawdza czy podany obiekt spełnia określone założenia a następnie zwraca odpowiedni do tego komunikat.
        /// </summary>
        /// <param name="item"> Objekt typu RozegraneeGry który poddawany będzie filtracji</param>
        /// <returns> Wartość typu boolean określająca czy podany obiekt spełnia założenia czy też nie.</returns>
        private bool Filtr(object item)
        {
            if((item as RozegraneeGry).Id_rozgrywajacego==dostepDoDanych.ZwrocId())
            {
                return true;

            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Metoda przełączająca pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            potwierdzenieRejestracji.Visibility = Visibility.Collapsed;
            Logowanie.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Metoda twożąca obiekt w którym przechowywane będą dane o przeprowadzonej rozgrywce.
        /// </summary>
        /// <remarks>
        /// W początkowym etapie do obiektu dodawane o wielkości zakładu oraz wybranym kolorze.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            int zaklad = 0;
            
            funkcjadodawaniaDoListyCyfr();
            if(wybranyZaklad.SelectedIndex==0)
            {
                zaklad = 2;

            }
            if (wybranyZaklad.SelectedIndex == 1)
            {
                zaklad = 5;
            }
            if (wybranyZaklad.SelectedIndex == 2)
            {
                zaklad = 10;
            }
            if (wybranyZaklad.SelectedIndex == 3)
            {
                zaklad = 20;
            }
            if(dostepDoDanych.Fundusze>=zaklad)
            {
                dostepDoDanych.Fundusze-=zaklad;
                

                Gra.Visibility = Visibility.Visible;
                startGry.Visibility = Visibility.Collapsed;
                rozegranaGra = new PrzeprowadzonaRozgrywka() { CzasprzeprowadzeniaGry= DateTime.Today, wielkoscZakladu=zaklad , WybranyKolor=WybieranieKoloru() , Wygrana=0};
                
                kolorekwybrany.Content= WybieranieKoloru();
            }
            

        }



        /// <summary>
        /// Metoda która określa wybrany kolor oraz zwraca jego nazwę.
        /// </summary>
        /// <returns>Zwraca łańcuch tekstowy reprezentujący kolor.</returns>
        public string WybieranieKoloru()
        {
            if(rb1.IsChecked==true)
            {
                return "Czerwony";

            }
            else if (rb2.IsChecked == true)
            {
                return "Niebieski";

            }
            else if (rb3.IsChecked == true)
            {
                return "Zielony";

            }
            else if (rb4.IsChecked == true)
            {
                return "Brązowy";

            }
            else 
            {
                return "Pomarańczowy";


            }
           
        }

        /// <summary>
        /// Metoda odpowiedzialna za dodawaniu do kolekcji kontrolek typu text block.
        /// </summary>
       
        public void LosowanieLiczbWybranych()
        {
            listaCyfrWybranych.Clear();
            listaCyfrWybranych.Add(l1);
            listaCyfrWybranych.Add(l2);
            listaCyfrWybranych.Add(l3);
            listaCyfrWybranych.Add(l4);
            listaCyfrWybranych.Add(l5);
            listaCyfrWybranych.Add(l6);
            listaCyfrWybranych.Add(l7);
            listaCyfrWybranych.Add(l8);
            listaCyfrWybranych.Add(l9);
            listaCyfrWybranych.Add(l10);
            listaCyfrWybranych.Add(l11);
            listaCyfrWybranych.Add(l12);
            listaCyfrWybranych.Add(l13);
            listaCyfrWybranych.Add(l14);
            listaCyfrWybranych.Add(l15);
            listaCyfrWybranych.Add(l16);
            listaCyfrWybranych.Add(l17);

            listaCyfrWybranych.Add(l18);

        }
        



        public void Dodawaj(int [] tablica,int ktora)
        {
            Task.Delay(1000);
           

           


        }


        /// <summary>
        ///  Metoda odpowiadająca za asynchronicznie określaniu liczb które reprezentują każdą kontrolkę
        /// </summary>
        /// <remarks>
        /// Działanie tej metody polega na przypisywaniu wylosowanych wartości liczb do poszczególnych obiektów kolekcji oraz w dalszej częsci oznaczanie 
        /// (poprzez zmianę właściwości forreground) tych liczb które znajdują się w jednych z dwóch tabel wylosowanych liczb gry.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            los.Visibility = Visibility.Collapsed;
            LosowanieLiczbWybranych();
            int[] tablica = new int[listaCyfrWybranych.Count];
            
            bool czy = true;
            for (int i = 0; i < listaCyfrWybranych.Count; i++)
            {
                czy = true;
                int a = (liczby_losowanie.Next(0, 50));
                for (int j = 0; j < i; j++)
                {
                    if (tablica[j] == a)
                    {
                        czy = false;
                    }

                }
                if (czy == true)
                {
                    tablica[i] = a;

                }
                else
                {
                    i--;
                }
               






            }
            
            for (int i = 0; i < listaCyfrWybranych.Count; i++)
            {
                listaCyfrWybranych[i].Text = await Calculate(tablica[i], listaCyfr);
                rozegranaGra.sekwencjaLiczb += listaCyfrWybranych[i].Text + " ";



            }
            Thread.Sleep(100);
            bool czy_jestTam = false;
            for (int i = 0; i < listaCyfrWybranych.Count; i++)
            {
                czy_jestTam = false;
                for (int j = 0; j < listaCyfr.Count; j++)
                {
                    if (listaCyfrWybranych[i].Text == listaCyfr[j].Text)
                    {
                        czy_jestTam = true;
                    }

                }
                if (czy_jestTam == true)
                {
                    listaCyfrWybranych[i].Foreground = await CosTam();

                }

            }

            for (int i = 0; i < listaCyfr.Count; i++)
            {
                for (int j = 0; j < listaCyfrWybranych.Count; j++)
                {
                    if(listaCyfr[i].Text==listaCyfrWybranych[j].Text)
                    {
                        listaCyfr[i].Background = Brushes.DarkGray;

                    }

                }
            }


            los.Visibility = Visibility.Collapsed;
            pods1.Visibility = Visibility.Visible;




        }
        


        /// <summary>
        /// Metoda odpowiadająca za asynchroniczne przeczekiwanie danego interwału czasowego oraz zwracaniu wartoci string.
        /// </summary>
        /// <param name="number12"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        private async Task<string> Calculate(int number12,List<TextBlock> a)
        {
            return await Task.Run(() =>
            {
                
                Thread.Sleep(500);
               
                return number12.ToString();
                
            });
        }



        /// <summary>
        /// Metoda odpowiadająca za asynchroniczne przeczekiwanie danego interwału czasowego oraz zwracaniu wartoci string.
        /// </summary>
        private async Task<SolidColorBrush> CosTam()
        {
            return await Task.Run(() =>
            {

                Thread.Sleep(500);

                return Brushes.Red;

            });
        }



        /// <summary>
        /// Metoda sprawdzająca wybrane liczby oraz dodawanie oraz odejmowanie konkretnych wartości od sumy wygranej przez użytkownika.
        /// </summary>
        /// <remarks>
        /// Sprawdzane jest również to czy dany kolor który liczba reprezentuje pasuje do tego wybranego przez użytkwonika na początku gry,jeżeli tak to wartość jest mnożona przed odpowiedni mnożnik jeśli nie dzielona.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void pods1_Click(object sender, RoutedEventArgs e)
        {
            pods1.Visibility = Visibility.Collapsed;
            wynikiGry.Visibility = Visibility.Visible;
            for (int i = 0; i < listaCyfrWybranych.Count; i++)
            {
                for (int j = 0; j < listaCyfr.Count; j++)
                {
                    if(listaCyfrWybranych[i].Text==listaCyfr[j].Text)
                    {
                        if(j<16)
                        {
                           

                            if (kolory[j] == rozegranaGra.WybranyKolor)
                            {
                                rozegranaGra.Wygrana += (((Convert.ToInt32(listaCyfr[j].Text) * Int32.Parse(dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie)) *Int32.Parse(dostepDoDanych.Mnozniki.Mnoznik_Kolory_dodatnie))*rozegranaGra.wielkoscZakladu);

                            }
                            else
                            {
                                rozegranaGra.Wygrana += ((Convert.ToInt32(listaCyfr[j].Text) * Int32.Parse(dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie))* rozegranaGra.wielkoscZakladu);
                            }
                           

                        }
                        else
                        {
                            if (kolory[j] == rozegranaGra.WybranyKolor)
                            {
                                rozegranaGra.Wygrana -= ((Convert.ToInt32((listaCyfr[j].Text)) * (Int32.Parse(dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie))) * Int32.Parse(dostepDoDanych.Mnozniki.Mnoznik_Kolory_ujemne));

                            }
                            else
                            {
                                rozegranaGra.Wygrana -= ((Convert.ToInt32((listaCyfr[j].Text)) * (Int32.Parse(dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie))));
                            }
                        }
                    }

                }
                

            }
            
            if(rozegranaGra.Wygrana>0)
            {
                wynikwygranej.Text = rozegranaGra.Wygrana.ToString() +"PLN";
                dostepDoDanych.Fundusze += rozegranaGra.Wygrana;
                dostepDoDanych.rozegranarozgrywka(rozegranaGra.wielkoscZakladu, rozegranaGra.WybranyKolor, rozegranaGra.Wygrana, rozegranaGra.CzasprzeprowadzeniaGry, rozegranaGra.sekwencjaLiczb);


            }
            else
            {
                dostepDoDanych.rozegranarozgrywka(rozegranaGra.wielkoscZakladu, rozegranaGra.WybranyKolor,0, rozegranaGra.CzasprzeprowadzeniaGry, rozegranaGra.sekwencjaLiczb);
                wynikwygranej.Text = "0" +"PLN";
            }
             
            
            
            


        }


        /// <summary>
        /// Resetowanie danych rozgrywki oraz przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            los.Visibility = Visibility.Visible;
            pods1.Visibility = Visibility.Collapsed;
            Clr_WYbranychLiczbWyswietlanie();
            Clr_CyfrDoLosowaniaWyswietlanie();
            wybranyZaklad.SelectedIndex = 0;
            wynikiGry.Visibility = Visibility.Collapsed;
            startGry.Visibility = Visibility.Visible;
            Gra.Visibility = Visibility.Collapsed;

        }



        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Gra.Visibility == Visibility.Visible)
            {

            }
            else
            {
                startGry.Visibility = Visibility.Visible;

                HistoriaGierAll.Visibility = Visibility.Collapsed;
                HistoriaGier.Visibility = Visibility.Collapsed;
                Profil.Visibility = Visibility.Collapsed;
                Zasadygry.Visibility = Visibility.Collapsed;
            }

            

        }


        /// <summary>
        /// Otwieranie nowego okna.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            if(OtwieranieOkna.otwarteOkno==false)
            {
                Portfel secwindow = new Portfel(dostepDoDanych);
                secwindow.Show();

            }
           
           
            
            
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (Gra.Visibility == Visibility.Visible)
            {

            }
            else
            {
                HistoriaGierAll.Visibility = Visibility.Visible;
                HistoriaGier.Visibility = Visibility.Collapsed;
                startGry.Visibility = Visibility.Collapsed;
                Profil.Visibility = Visibility.Collapsed;
                Zasadygry.Visibility = Visibility.Collapsed;

            }
           
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (Gra.Visibility == Visibility.Visible)
            {

            }
            else
            {
                HistoriaGierAll.Visibility = Visibility.Collapsed;
                HistoriaGier.Visibility = Visibility.Visible;
                startGry.Visibility = Visibility.Collapsed;
                Profil.Visibility = Visibility.Collapsed;
                Zasadygry.Visibility = Visibility.Collapsed;
                

            }    
           

        }



        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (Gra.Visibility == Visibility.Visible)
            {
                
            }
            else
            {
                startGry.Visibility = Visibility.Collapsed;
                HistoriaGier.Visibility = Visibility.Collapsed;
                Profil.Visibility = Visibility.Collapsed;
                HistoriaGierAll.Visibility = Visibility.Collapsed;
                Zasadygry.Visibility = Visibility.Visible;

            }
            
        }



        /// <summary>
        /// Jest to metoda odpowiedzialna za zmianę danych osobowych użytkownika.
        /// </summary>
        /// <remarks>
        /// Jezeli podane dane spełniają założenia to do bazy zostają zapisane poprawione dane.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if((PytPom.Text).Length>=5 && ImieDane.Text.Length<15&& NazwiskoDane.Text.Length < 15)
            {
                dostepDoDanych.uzupelnanieDanych(ImieDane.Text, NazwiskoDane.Text, HobbyDane.Text, OsobieDane.Text,PytPom.Text);
               
                potwierdzeniezmiany.Visibility = Visibility.Visible;
                bladPytania.Visibility = Visibility.Collapsed;
            }
            else
            {
                bladPytania.Visibility = Visibility.Visible;

            }
           
        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            potwierdzeniezmiany.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if(Gra.Visibility==Visibility.Visible)
            {

            }
            else
            {
                Profil.Visibility = Visibility.Visible;
                startGry.Visibility = Visibility.Collapsed;
                HistoriaGierAll.Visibility = Visibility.Collapsed;
                HistoriaGier.Visibility = Visibility.Collapsed;
                Zasadygry.Visibility = Visibility.Collapsed;
            }
            

        }



        /// <summary>
        /// Metoda odpowiedzialna za przypominanie hasła logowania.
        /// </summary>
        /// <remarks>
        /// Pobierane są dane z kontrolek textBox i w przypadku gdy podane dane są prawidłowe to hasło zostaje wyświetlone.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            string tymczasowe= string.Empty;
            if ((tymczasowe=dostepDoDanych.Przypomnij_haslo(odpPrzypomnieniehasla.Text, LoginPrzypomnienieHasła.Text))!="brak")
            {
                odpPrzypomnieniehasla.Text = string.Empty;
                LoginPrzypomnienieHasła.Text = string.Empty;
                PanelPrzypominania.Visibility = Visibility.Visible;
                Przypomnianehaslo.Text = tymczasowe;
                bladPrzypomnieniahasla.Visibility = Visibility.Collapsed;
                

            }
            else
            {
                PanelPrzypominania.Visibility = Visibility.Hidden;
                bladPrzypomnieniahasla.Visibility = Visibility.Visible;
            }
            
        }




        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            PanelPrzypominania.Visibility = Visibility.Collapsed;
            Przypomnianehaslo.Text = string.Empty;
            bladPrzypomnieniahasla.Visibility = Visibility.Collapsed;

            Przypomnieniehasla.Visibility = Visibility.Collapsed;
            Logowanie.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void PrzypomnijHaslo_Click(object sender, RoutedEventArgs e)
        {
            Przypomnieniehasla.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Zamykanie aplikacji.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }




        /// <summary>
        /// Zapisywanie zmian dokonwyanych w aplikacji.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
          
            dostepDoDanych.zapisanieZmiany();
        }



        /// <summary>
        /// Metoda odpowiedzialna za usuwanie urzytkowników z poziomu panelu adminitratora.
        /// </summary>
        /// <remarks>
        /// Jeżeli wybrany jest jakiś użytkownik usuwany zostaje on wraz z wszelkimi jego tranzakcjami oraz rozgrywkami.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
           
            
            if(ListaUzytkownikowDG.SelectedItem != null)
            {
                dostepDoDanych.Usuwanie_Usera(ListaUzytkownikowDG.SelectedItem as Konto);

            }
            else
            {
                MessageBox.Show("Proszę wybrać użytkownika do usunięcia");
            }



            
        }



        /// <summary>
        /// Metoda sprawdzająca poprawność podanego łańcucha znaków
        /// </summary>
        /// <param name="tekst"> Podany łańcuch znakowy do sprawdzenia</param>
        /// <returns>Wartość bolean która reprezentuje czy dane spełniają bądź nie spełniają wymogów</returns>
        public static bool CzyDaneSpelniajaZalozenia(string tekst)
        {

            bool czyPrawda = true;

            try
            {

                if (tekst.Length < 8)
                {
                    czyPrawda = true;
                }
                else if (tekst.Contains(",") || tekst.Contains("/") || tekst.Contains("'") || tekst.Contains(";") || tekst.Contains("[") || tekst.Contains("]") || tekst.Contains("{") || tekst.Contains("}") || tekst.Contains("|") || tekst.Contains(":") || tekst.Contains(@"""") || tekst.Contains("<") || tekst.Contains(">") || tekst.Contains("!") || tekst.Contains(")") || tekst.Contains("(") || tekst.Contains("*"))
                {
                    czyPrawda = true;

                }
                else if (tekst == "")
                {
                    czyPrawda = true;
                }
                else
                {
                    czyPrawda = false;
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


            return czyPrawda;





        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            dodawanieUseraAdmin.Visibility = Visibility.Collapsed;
            bladDodawaniaUseraAdmin.Visibility = Visibility.Collapsed;

        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            dodawanieUseraAdmin.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Metoda odpowiedzialna za dodawanie nowego użytkownika do zbazy danych z poziomu panelu administratora wraz z sprawdzeniem poprawności danych.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            bladDodawaniaUseraAdmin.Visibility = Visibility.Collapsed;


            if (PassworddodawanieAdmin.Text == string.Empty || NickdodawanieAdmin.Text == string.Empty || EmaildodawanieAdmin.Text == string.Empty || CzyDaneSpelniajaZalozenia(PassworddodawanieAdmin.Text) || CzyDaneSpelniajaZalozenia(NickdodawanieAdmin.Text) || CzyDaneSpelniajaZalozenia(EmaildodawanieAdmin.Text))
            {
                bladDodawaniaUseraAdmin.Visibility = Visibility.Visible;

                


            }
            else
            {

               
                if (dostepDoDanych.CzyJestUrzytkownikwBazieRejestracja(NickdodawanieAdmin.Text, EmaildodawanieAdmin.Text) == true)
                {
                    bladDodawaniaUseraAdmin.Visibility = Visibility.Visible;



                }
                else
                {
                    
                    dostepDoDanych.DodajUzytkownika(EmaildodawanieAdmin.Text, NickdodawanieAdmin.Text, PassworddodawanieAdmin.Text);
                    bladDodawaniaUseraAdmin.Visibility = Visibility.Collapsed;
                    PassworddodawanieAdmin.Text = string.Empty;
                    NickdodawanieAdmin.Text = string.Empty;
                    EmaildodawanieAdmin.Text = string.Empty;
                    MessageBox.Show("Pomyślnie dodano użytkownika");



                }


            }


        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            ListaUzytkownikow.Visibility = Visibility.Visible;
            DanedzialaniaGry.Visibility = Visibility.Collapsed;
            ListaZmian.Visibility = Visibility.Collapsed;

        }




        /// <summary>
        /// Sprawdzanie czy podany łańcuch znaków można przekonwertować w liczbę typu int.
        /// </summary>
        /// <param name="liczba"></param>
        /// <returns> Zwraca Wartość typu bolean reprezentującą czy dane są poprawne czy nie.</returns>
        public static bool sprawdzanieDanych(string liczba)
        {
            



            int local;
            if((Int32.TryParse(liczba,out local))==true)
            {
                return true;

            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// Ustawianie pustych wartości kontrolką TB.
        /// </summary>
        public void ZerowanieZmian()
        {
            zmianaat1.Text = string.Empty;
            zmianaat2.Text = string.Empty;
            zmianaat3.Text = string.Empty;
            zmianaat4.Text = string.Empty;
            zmianaaOpis.Text= string.Empty; 

        }


        /// <summary>
        ///  Zmiana parametrów bonusów ustalane z poziomu panelu użytkownika.
        /// </summary>
        /// <remarks>
        /// Jeżeli podane przez administratora dane są poprawne(spełniają wymogi) aktualne dane gry zostają zmienione.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_21(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sprawdzanieDanych(zmianaat1.Text) && sprawdzanieDanych(zmianaat2.Text) && sprawdzanieDanych(zmianaat2.Text) && sprawdzanieDanych(zmianaat2.Text))
                {
                    niepoprawneDaneZmiany.Visibility = Visibility.Collapsed;
                    dostepDoDanych.Mnozniki = (new ZasadyGry() { Data_zmiany = DateTime.Today, Mnoznik_Kolory_dodatnie = zmianaat1.Text, Mnoznik_Kolory_ujemne = zmianaat2.Text, Mnoznik_Gry_dodatnie = zmianaat2.Text, Mnoznik_Gry_ujemne = zmianaat2.Text, Opis_zmiany = zmianaaOpis.Text, Id_pracownika_zmieniajacego = dostepDoDanych.ZwrocId() });
                    MessageBox.Show("Pomyślnie zmieniono dane");
                    ZerowanieZmian();

                    UstawienieZasad();




                }
                else
                {
                    niepoprawneDaneZmiany.Visibility = Visibility.Visible;

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
        /// Ustalanie danych gry.
        /// </summary>
        private void UstawienieZasad()
        {
            zmianaMnoznika1.Text = dostepDoDanych.Mnozniki.Mnoznik_Kolory_dodatnie;
            zmianaMnoznika2.Text = dostepDoDanych.Mnozniki.Mnoznik_Kolory_ujemne;
            zmianaMnoznika3.Text = dostepDoDanych.Mnozniki.Mnoznik_Gry_dodatnie;
            zmianaMnoznika4.Text = dostepDoDanych.Mnozniki.Mnoznik_Gry_ujemne;

        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            DanedzialaniaGry.Visibility = Visibility.Visible;
            ListaUzytkownikow.Visibility = Visibility.Collapsed;
            ListaZmian.Visibility = Visibility.Collapsed;
        }



        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            ListaZmian.Visibility = Visibility.Visible;
            DanedzialaniaGry.Visibility = Visibility.Collapsed;
            ListaUzytkownikow.Visibility = Visibility.Collapsed;
        }




        /// <summary>
        /// Metoda wywołująca funkcję zerującą parametry.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void PrzyciskModyfikacjiDanych_Unchecked(object sender, RoutedEventArgs e)
        {
            ZerowanieZmian();

        }




        /// <summary>
        /// Metoda która odpowiada za otwieranie okna OpenFileDialog oraz wczystanie danych rozgrywek z pliku o formacie csv.
        /// </summary>
        /// <remarks>
        /// Jeżeli dane wczytane z pliku są poprawne do bazy danych zostaje dodany nowy objekt a samo wyświetlanie akutalnych danych zostaje odświeżone.Przechwytywane są równiez wyjątki które mogą wystąpić.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {



            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Pliki csv|*.csv";
            dialog.DefaultExt = "*.csv";
            dialog.ShowDialog();
          

            ZasadyGry nowaZasada = new ZasadyGry();
            try
            {
                
                

                using (var streamReader = File.OpenText(dialog.FileName))
                {
                    var reader = new CsvReader(streamReader, new CultureInfo(1));

                    reader.Read();
                    reader.Configuration.RegisterClassMap<UM>();

                    nowaZasada = reader.GetRecord<ZasadyGry>();

                    try
                    {
                        Int32.Parse(nowaZasada.Mnoznik_Kolory_dodatnie);
                        Int32.Parse(nowaZasada.Mnoznik_Kolory_ujemne);
                        Int32.Parse(nowaZasada.Mnoznik_Gry_dodatnie);
                        Int32.Parse(nowaZasada.Mnoznik_Gry_ujemne);

                        nowaZasada.Data_zmiany = DateTime.Today;
                        nowaZasada.Id_pracownika_zmieniajacego = dostepDoDanych.ZwrocId();
                        dostepDoDanych.Mnozniki = nowaZasada;
                        UstawienieZasad();


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Podane dane są nieprawidłowe","Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
                       
                        
                    }


                   








                }

            }

            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Nie podano ścieżki dostępu do pliku. " + ex.Message, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Nieprawidłowa ścieżka dostępu do pliku. " + ex.Message, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Nie znaleziono pliku! " + ex.Message, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Ścieżka dostępu do pliku jest nieprawidłowa. " + ex.Message, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Ścieżka dostępu do pliku ma nieprawidłowy format. " + ex.Message, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }




            
            
            
           
        }


        /// <summary>
        /// Klasa odpowiedzialna za mapowanie kolumn w formacie csv na rekordy. 
        /// </summary>
        public sealed class UM : ClassMap<ZasadyGry>
        {
            public UM()
            {
                Map(m => m.Mnoznik_Kolory_dodatnie).Index(0);
                Map(m => m.Mnoznik_Kolory_ujemne).Index(1);
                Map(m => m.Mnoznik_Gry_dodatnie).Index(2);
                Map(m => m.Mnoznik_Gry_ujemne).Index(3);
                Map(m => m.Opis_zmiany).Index(4);

            }
        }


        /// <summary>
        /// Metoda która odpowiada za otwieranie okna SabeFileDialog oraz zapisywanie danych rozgrywek w formacie csv.
        /// </summary>
        /// <remarks>
        /// Po podaniu odpowiedniej ścieżki  dane (zasad gry) w okreslonym miejscu zostaną zapisane.
        /// </remarks>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>
        private void Button_Click_24(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Pliki csv|*.csv;";
            dialog.ShowDialog();
           

            dialog.AddExtension = true;
            
           
           

            try
            {
                using (var streamWriter = File.CreateText(dialog.FileName))
                {
                    using (var writer = new CsvWriter(streamWriter, new CultureInfo(1)))
                    {
                        writer.Configuration.RegisterClassMap<UM>();

                        writer.WriteRecords(dostepDoDanych.PokazZmianyyList());
                        MessageBox.Show("Pomyślnie zapisano dane do pliku! ", "Pomyślny zapis", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                    

                   
                    

                }

            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Nie podano ścieżki dostępu do pliku. " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Nieprawidłowa ścieżka dostępu do pliku. " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Nie można uzyskać praw dostępu! " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SecurityException ex)
            {
                MessageBox.Show("Nie można uzyskać praw dostępu! " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Ścieżka dostępu do pliku jest nieprawidłowa. " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (PathTooLongException ex)
            {
                MessageBox.Show("Ścieżka dostępu do pliku jest nieprawidłowa. " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Ścieżka dostępu do pliku ma nieprawidłowy format. " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Błąd zapisu pliku", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           


          

        }


        /// <summary>
        /// Przełączanie pomiędzy panelami.
        /// </summary>
        /// <param name="sender">określa obiekt który wywołał dany event</param>
        /// <param name="e">Zawiera informacje o stanie i dane zdarzenia powiązane ze zdarzeniem kierowanym.</param>>

        private void PrzypomnijHaslo_Click_1(object sender, RoutedEventArgs e)
        {
            
            Logowanie.Visibility = Visibility.Collapsed;
            Przypomnieniehasla.Visibility = Visibility.Visible;
        }
    }
}
