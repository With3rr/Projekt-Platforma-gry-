using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektProgramowanieObjektowe
{

    /// <summary>
    /// Klasa która zapewnia dostęp do bazy danych.
    /// </summary>
    class PobieranieDanych:IPobieranieDanych,INotifyPropertyChanged
    {

        /// <summary>
        /// Obiekt karty Visa.
        /// </summary>
        Visa kartaVisa = null;


        /// <summary>
        /// Obiekt konta użytkownika obecnie kożystającego z aplikacji.
        /// </summary>
        Konto kontoObecnegoTypa = null;


        /// <summary>
        /// Obiekt bazy danych dzięki któremu będzie możliwy dostęp do poszczególnych relacjiw bazie.
        /// </summary>
        BazaGryHazardoweEntities baza = new BazaGryHazardoweEntities();

        /// <summary>
        /// Zmienna reprezenutująca obiekt paysafe. 
        /// </summary>
        PaySafe kartaPayS = null;


        /// <summary>
        /// Zmienna określająca czy konto jest aktywne.
        /// </summary>
        private bool aktywneKonto = false;

        /// <summary>
        /// Kolekcja obiektów wpłat.
        /// </summary>
        private ObservableCollection<Wplaty> wplaty = new ObservableCollection<Wplaty>();



        /// <summary>
        /// Kolekcja obiektów wypłat.
        /// </summary>
        private ObservableCollection<Wyplaty> wyplaty = new ObservableCollection<Wyplaty>();



        /// <summary>
        /// Kolekcja obiektów zagranych gier wszystkich użytkowników.
        /// </summary>
        private ObservableCollection<RozegraneeGry> gryZagraneAll=new ObservableCollection<RozegraneeGry>();

        /// <summary>
        /// Kolekcja obiektów zagranych gier.
        /// </summary>
        private ObservableCollection<RozegraneeGry> gryZagranemoje = new ObservableCollection<RozegraneeGry>();



        /// <summary>
        /// Kolekcja obiektów reprezentujących użytkowników platformy.
        /// </summary>

        private ObservableCollection<Konto> uzytownicyPlatformy = new ObservableCollection<Konto>();



        /// <summary>
        /// Kolekcja obiektów reprezentujących listy zmian w zasadach gier platfromy.
        /// </summary>
        private ObservableCollection<ZasadyGry> listaDodanzmian = new ObservableCollection<ZasadyGry>();




        /// <summary>
        /// Obiekt konta bankowego.
        /// </summary>
        KontaBankowe kontoWBanku = null;


        /// <summary>
        /// Zwraca kolekcję obiektów reprezentujących wprowadzone zmiany
        /// </summary>
        /// <returns>Kolekcja obiektów zmian zasad gry.</returns>
        public List<ZasadyGry> PokazZmianyyList()
        {
            return baza.ZasadyGry.ToList();
        }




        /// <summary>
        /// Metoda pozwala na usuwanie odniesień do obiektów z kolekcji a następnie znów przypisywanie obiektów które zostały poszeżone lub zmniejszone o obiekt.
        /// </summary>
        private void RefreshZmiandy()
        {
            listaDodanzmian.Clear();

            List<ZasadyGry> lista5 = baza.ZasadyGry.ToList();
            for (int i = 0; i < lista5.Count; i++)
            {
                listaDodanzmian.Add(lista5[i]);

            }

        }



        /// <summary>
        /// Zwraca kolekcję obiektów reprezentujących wprowadzone zmiany
        /// </summary>
        /// <returns>Kolekcja obiektów zmian zasad gry.</returns>
        public ObservableCollection<ZasadyGry> PokazZmianyy()
        {
            RefreshZmiandy();
            return listaDodanzmian;
        }



        /// <value>
        /// Właściwość reprezentująca obiekt ZasadGry.Zwraca ostatnio dodany obiekt zasad gry a w przypadku dodania nowego odświeża kolekcję.
        /// </value>
        public ZasadyGry Mnozniki 
        {
            get
            {
             




                    return baza.ZasadyGry.OrderByDescending(i => i.Id).First();
                

            }

            set
            {
                baza.ZasadyGry.Add(value);
                baza.SaveChanges();
                RefreshZmiandy();




            }
        }



        /// <summary>
        /// Metoda która po podaniu odpowiednich danych podaje hasło użytkownika.
        /// </summary>
        /// <param name="pytanie">Pytanie pomocnicze.</param>
        /// <param name="login">Login użytkownika.</param>
        /// <returns>Zwraca hasło użytkownika.</returns>
        public string Przypomnij_haslo(string pytanie,string login)
        {
            Konto kontoSprawdzenie = baza.Konto.FirstOrDefault(n=>n.Login==login &&n.PytPomocnicze==pytanie);
            if(kontoSprawdzenie!=null)
            {
                return kontoSprawdzenie.Password;
            }
            else
            {
                return "brak";
            }


        }



        /// <summary>
        /// Metoda pozwalająca na zapisywanie do określonego obiektu w bazie danych dodatkowych danych osobowych.
        /// </summary>
        /// <param name="imie">Imie użytkownika.</param>
        /// <param name="nazwisko">Nazwisko użytkownika.</param>
        /// <param name="hobby">Hobby użytkownika.</param>
        /// <param name="oSobie">Informacje o użytkowniku</param>
        /// <param name="pytanie">Pytanie pomocnicze.</param>
        public void uzupelnanieDanych(string imie, string nazwisko, string hobby, string oSobie,string pyt)
        {
            


            try
            {
                if(imie.Length>30||nazwisko.Length>30||hobby.Length>30||oSobie.Length>100||pyt.Length>30)
                {
                    throw new Exception("Dane nie prawidłowe bądź zbyt długie.");
                }
                kontoObecnegoTypa.Imie = imie;
                kontoObecnegoTypa.Nazwisko = nazwisko;
                kontoObecnegoTypa.Hobby = hobby;
                kontoObecnegoTypa.O_sobie = oSobie;
                kontoObecnegoTypa.PytPomocnicze = pyt;
                baza.SaveChanges();

            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                
            }


        }


        /// <summary>
        /// Metoda zwracająca kolekcję gier z bazy danych która zwraza gry rozegrane prze użytkownika.
        /// </summary>
        /// <returns>Kolekcja rozegranych gier.</returns>
        public ObservableCollection<RozegraneeGry> PokazGryMoje()
        {

            return gryZagranemoje;
        }


        /// <summary>
        /// Zwraca kolekcję użytkowników z bazy danych.
        /// </summary>
        /// <returns>Lista użytkowników</returns>
        public ObservableCollection<Konto> PokazUzytkownikow()
        {
            refreshZnaj();
            return uzytownicyPlatformy;


        }



        /// <summary>
        /// Metoda usuwająca podanego użytkownika z bazy danych.Metoda usuwa również wszystkie obiekty z pozostałych relacji które odnoszą się do użytkownika.
        /// </summary>
        /// <param name="kontodoUsuniecia">Obiekt  do usunięcia</param>
        public void Usuwanie_Usera(Konto kontodoUsuniecia)
        {
            
            baza.Wyplaty.RemoveRange(baza.Wyplaty.Where(n => n.Id_wyplacajacego == kontodoUsuniecia.Id));
            baza.Wplaty.RemoveRange(baza.Wplaty.Where(n => n.ID_wplacajocego == kontodoUsuniecia.Id));
            baza.RozegraneeGry.RemoveRange(baza.RozegraneeGry.Where(n => n.Id_rozgrywajacego == kontodoUsuniecia.Id));
            baza.SaveChanges();



            baza.Konto.Remove(baza.Konto.FirstOrDefault(n => n.Id == kontodoUsuniecia.Id));
            baza.SaveChanges();
            refreshZnaj();

        }



        /// <summary>
        /// Zapisywanie zmian w bazie danych.
        /// </summary>
        public void zapisanieZmiany()
        {
            try
            {
                baza.SaveChanges();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK);
            }
            
        }


        /// <summary>
        /// Metoda pozwala na usuwanie odniesień do obiektów z kolekcji a następnie znów przypisywanie obiektów które zostały poszeżone lub zmniejszone o obiekt.
        /// </summary>
        private void refreshZnaj()
        {
            uzytownicyPlatformy.Clear();
            List<Konto> lista4 = baza.Konto.ToList();
            for (int i = 0; i < lista4.Count; i++)
            {
                if(lista4[i].Uprawnienia!="Admin")
                {
                    uzytownicyPlatformy.Add(lista4[i]);

                }
               

            }



        }


        /// <summary>
        /// Metoda zwracająca obiekty rozgrywanych gier z bazy danych.
        /// </summary>
        /// <returns>Zwraca kolekcję bier rozegranych.</returns>
        public ObservableCollection<RozegraneeGry> PokazGry()
        {

            return gryZagraneAll;
        }



        /// <summary>
        /// Metoda sprawdzająca czy urzytkownik ma uprawnienia administratora.
        /// </summary>
        /// <returns>Wartość bolean czy użytkownik ma takie uprawnienia czy nie ma.</returns>
        public bool CzyMaster()
        {
            if(kontoObecnegoTypa.Uprawnienia=="Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Metoda zwracająca wartość typu bolean która reprezentuje to czy użytkownik o podanych danych istnieje w bazie danych.
        /// </summary>
        /// <param name="nick">Jest to nazwa użytkownika</param>
        /// <param name="haslo">Jest to hasło użytkownika</param>
        /// <returns>Wartość reprezentująca istnienie użytkownika o podanych danych.</returns>
        public bool CzyJestUrzytkownikwBazie(string nick, string haslo)
        {
            if (baza.Konto.FirstOrDefault(s => s.Login == nick && s.Password == haslo) != null)
            {
                kontoObecnegoTypa = baza.Konto.FirstOrDefault(s => s.Login == nick && s.Password == haslo);
                refreshWplat();
                refreshWyplat();
                refreshGier();


                return true;

            }
            else
            {
                return false;
            }



        }




        /// <summary>
        /// Metoda pozwala na usuwanie odniesień do obiektów z kolekcji a następnie znów przypisywanie obiektów które zostały poszeżone lub zmniejszone o obiekt.
        /// </summary>
        private void refreshGier()
        {
            gryZagraneAll.Clear();
            List<RozegraneeGry> lista3 = baza.RozegraneeGry.ToList();
            for (int i = 0; i < lista3.Count; i++)
            {

                if(lista3[i].WartoscWygranej>=50)
                {
                    gryZagraneAll.Add(lista3[i]);
                }
                

            }


            gryZagranemoje.Clear();
            List<RozegraneeGry> lista4 = baza.RozegraneeGry.Where(n => n.Id_rozgrywajacego == kontoObecnegoTypa.Id).ToList();
            for (int i = 0; i < lista4.Count; i++)
            {
                gryZagranemoje.Add(lista4[i]);

            }
        }

        /// <summary>
        /// Metoda pozwala na usuwanie odniesień do obiektów z kolekcji a następnie znów przypisywanie obiektów które zostały poszeżone lub zmniejszone o obiekt.
        /// </summary>
        private void  refreshWplat()
        {
            wplaty.Clear();
            List<Wplaty> lista = baza.Wplaty.Where(n => n.ID_wplacajocego == kontoObecnegoTypa.Id).ToList();
            for (int i = 0; i < lista.Count; i++)
            {
                wplaty.Add(lista[i]);

            }
           

        }

        /// <summary>
        /// Metoda pozwala na usuwanie odniesień do obiektów z kolekcji a następnie znów przypisywanie obiektów które zostały poszeżone lub zmniejszone o obiekt.
        /// </summary>
        private void refreshWyplat()
        {
            wyplaty.Clear();
            List<Wyplaty> lista2 = baza.Wyplaty.Where(n => n.Id_wyplacajacego == kontoObecnegoTypa.Id).ToList();
            for (int i = 0; i < lista2.Count; i++)
            {
                wyplaty.Add(lista2[i]);

            }

        }


        /// <summary>
        /// Metoda określająca aktywność konta.
        /// </summary>
        /// <returns></returns>
        public bool czyOknoAktywne()
        {

            return aktywneKonto;

        }




        /// <summary>
        /// Jest to metoda zapisująca nowy obiekt reprezentujący przeprowadzoną rozgrywkę w bazie danych.
        /// </summary>
        /// <param name="wielkoscZakladu">Argument reprezentujący wielkość zakładu.</param>
        /// <param name="WybranyKolor"> Argument reprezentujący wybrany przez użytkownika kolor.</param>
        /// <param name="Wygrana">Reprezentacja wygranej</param>
        /// <param name="CzasprzeprowadzeniaGry">Czas przeprowadzenia rozgrywki prze użytkownika</param>
        /// <param name="sekwencjaLiczb">Łańsuch znaków reprezentujący wylosowane przez użytkownika liczby.</param>
        public void rozegranarozgrywka(int wielkoscZakladu,string WybranyKolor,int Wygrana,DateTime CzasprzeprowadzeniaGry, string sekwencjaLiczb)
        {
            baza.RozegraneeGry.Add(new RozegraneeGry() { CzasRozegrania = CzasprzeprowadzeniaGry, Id_rozgrywajacego = kontoObecnegoTypa.Id, Imie = kontoObecnegoTypa.Imie, Nazwisko = kontoObecnegoTypa.Nazwisko, PostawionaGotowka = wielkoscZakladu, TrafioneLiczby = sekwencjaLiczb, WartoscWygranej = Wygrana});
            baza.SaveChanges();
            refreshGier();
        }




        /// <summary>
        /// Metoda zwracająca kolekcję wpłat użytkownika.
        /// </summary>
        /// <returns>Kolekcja obiektów wpłat</returns>
        
        public ObservableCollection<Wplaty> PokazWplaty()
        {
            return wplaty;

        }


        /// <summary>
        /// Metoda zwracająca kolekcję wypłat użytkownika.
        /// </summary>
        /// <returns>Kolekcja obiektów wypłat</returns>
        public ObservableCollection<Wyplaty> PokazWyplaty()
        {
            return wyplaty;
        }


        /// <summary>
        /// Metoda która odpowiada za wysyłanie środków pieniężnych na określone konto bankowe.
        /// </summary>
        /// <param name="kwota">Kwota wypłaty.</param>
        /// <param name="nr_konta">Nr konta na które wpłacamy gotówę.</param>
        /// <param name="Opis">Opis wypłaty.</param>
        public void WyplataPieniedzy(float kwota,string nrkonta,string opis)
        {
            kontoWBanku.Stan_Konta += kwota;
            baza.Wyplaty.Add(new Wyplaty { Wielkosc_wyplaty=kwota, Id_wyplacajacego=kontoObecnegoTypa.Id, Nr_konta= nrkonta, Opis=opis, Nazwa_banku=" ", Data_wyplaty=DateTime.Today});
            Fundusze -= kwota;
            baza.SaveChanges();
            refreshWyplat();
        }


        /// <summary>
        /// Metoda sprawdzająca  czy istnieje obiekt w bazie danych o podanym nr karty płatniczej.
        /// </summary>
        /// <param name="kod">Nr karty płatniczej podawany przez użytkownika</param>
        /// <returns>Zwraca wartość bolean która mówi czy takie konto istnieje.</returns>
        public bool CzyJestTakeKontobankowe(string kod)
        {
            if((kontoWBanku=baza.KontaBankowe.FirstOrDefault(s=> s.Nr_konta==kod))!=null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        /// <summary>
        /// Metoda sprawdzająca czy w bazie danych istnieje obiekt karty płatniczej o podanych danych,jeżeli tak następnie sprawdzane są środki na karcie.
        /// </summary>
        /// <param name="nr">Nr karty płatniczej.</param>
        /// <param name="kod">Kod dostępu do karty.</param>
        /// <param name="Imie">Imie posiadacza karty.</param>
        /// <param name="Nazwisko">Nazwisko posiadacza karty.</param>
        /// <param name="kwota">Kwota/środki na karcie</param>
        /// <returns>Zwraca wartość bolean która określa czy dana karta istnieje oraz czy środki są wystarczające</returns>
        public bool CzyJestTakaKartaiSrodki(string nr, string kod, string Imie, string Nazwisko,float kwota)
        {
            if((kartaVisa=baza.Visa.FirstOrDefault(s => s.Nr_karty == nr && s.Kod== kod && s.Imie==Imie && s.Nazwisko==Nazwisko && s.Stan_konta>=kwota)) != null)
            {
                Fundusze += kwota;
                kartaVisa.Stan_konta -= kwota;
                baza.Wplaty.Add(new Wplaty() { Data_wpłaty=DateTime.Today, ID_wplacajocego=kontoObecnegoTypa.Id, Kwota_wplaty=kwota, Sposob_platnosci="Visa"});
                baza.SaveChanges();
                refreshWplat();
                return true;
                


            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Metoda sprawdzająca czy w bazie danych istnieje obiekt którego właściwość kod zpełnia założenia.
        /// </summary>
        /// <param name="kod">Kod paysafe podawany przez użytkownika</param>
        /// <returns></returns>
        public bool CzyJestTakiPaysafe(string kod)
        {
            if((kartaPayS = baza.PaySafe.FirstOrDefault(s => s.Kod_aktywujacy==kod)) != null)
            {
                Fundusze += (float)kartaPayS.Wielkosc_doladowania;
                baza.Wplaty.Add(new Wplaty() { Data_wpłaty = DateTime.Today, ID_wplacajocego = kontoObecnegoTypa.Id, Kwota_wplaty = (float)kartaPayS.Wielkosc_doladowania, Sposob_platnosci = "PaySafe" });
                baza.PaySafe.Remove(kartaPayS);
                baza.SaveChanges();
                refreshWplat();
                return true;

            }
            else
            {
                return false;
            }
        }




        /// <summary>
        /// Metoda zwracająca wartość typu bolean która reprezentuje to czy użytkownik o podanych danych istnieje w bazie danych.Wartość potrzebna do rejestracji.
        /// </summary>
        /// <param name="a1">Jest to nazwa użytkownika</param>
        /// <param name="a2">Jest to hasło użytkownika</param>
        /// <returns>Wartość reprezentująca istnienie użytkownika o podanych danych.</returns>
        public bool CzyJestUrzytkownikwBazieRejestracja(string a1, string a2)
        {
            if (baza.Konto.FirstOrDefault(s => s.Login == a1) !=null)
            {
                return true;
            }
            else if(baza.Konto.FirstOrDefault(s => s.Email == a2) != null)
            {
                return true;

            }
            else
            {
                return false;
            }



        }


        /// <summary>
        /// Metoda odpowiedzialna za dodawanie nowego użtkownika o podanych danych do bazy danych.
        /// </summary>
        /// <param name="nick">Argument reprezentujący nick</param>
        /// <param name="email"> Argument reprezentujący email</param>
        /// <param name="haslo">Argument reprezentujący hasło użytkownika</param>
        public void DodajUzytkownika(string nick, string email, string haslo)
        {
            baza.Konto.Add(new Konto { Login = nick, Funds = 0, Password = haslo, Email = email, Uprawnienia = "Użytkownik" });
            baza.SaveChanges();
            refreshZnaj();

        }



        /// <summary>
        /// Metoda odpowiedzialna za dodawanie nowego użtkownika o podanych danych do bazy danych.
        /// </summary>
        /// <param name="nick">Argument reprezentujący nick</param>
        /// <param name="email"> Argument reprezentujący email</param>
        /// <param name="haslo">Argument reprezentujący hasło użytkownika</param>
        /// <param name="pytanie">Argument który reprezentuje pytanie pomocnicze ustalane podczas rejestracji.</param>
        public void DodajUzytkownika(string nick, string email, string haslo,string pytanie)
        {
            baza.Konto.Add(new Konto { Login = nick, Funds = 0, Password = haslo, Email = email, PytPomocnicze=pytanie, Uprawnienia="Użytkownik"});
            baza.SaveChanges();
            refreshZnaj();

        }


        /// <value>
        /// Właściwość reprezentująca Fundusze użytkownika obecnie kożystającego z platformy.
        /// </value>
        public float Fundusze { 
            
            get
            {
                return (float)kontoObecnegoTypa.Funds;

            }
            
            set
            {

                kontoObecnegoTypa.Funds=value;
                baza.SaveChanges();
                PropertyChanged(this, new PropertyChangedEventArgs("Fundusze"));




            }
        }


        /// <summary>
        /// Jest to event pozwalający na powiadamianie apliakcji o zmianie  wartości włąściwości.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public int ZwrocId()
        {
            return kontoObecnegoTypa.Id;

        }


        /// <value>
        /// Właściwość reprezentująca Imię użytkownika;
        /// </value>
        public string Imie
        {
            get
            {
                return kontoObecnegoTypa.Imie;

            }
            set
            {
                kontoObecnegoTypa.Imie = value;
                baza.SaveChanges();

            }
        }


        /// <value>
        /// Właściwość reprezentująca Nazwisko użytkownika;
        /// </value>
        public string Nazwisko
        {
            get
            {
                return kontoObecnegoTypa.Nazwisko;

            }
            set
            {
                kontoObecnegoTypa.Nazwisko = value;
                baza.SaveChanges();

            } 
        }


        /// <value>
        /// Właściwość reprezentująca Hobby użytkownika;
        /// </value>
        public string Hobby
        {
            get
            {
                return kontoObecnegoTypa.Hobby;
            }
            set
            {
                kontoObecnegoTypa.Hobby = value;

            }
        }


        /// <value>
        /// Właściwość reprezentująca informacje o osobie użytkownika;
        /// </value>
        public string Osobie
        { 
            get
            {
                return kontoObecnegoTypa.O_sobie;
            }
            set
            {
                kontoObecnegoTypa.O_sobie = value;
            }
        }

        /// <value>
        /// Właściwość reprezentująca pytanie pomocnicze użytkownika;
        /// </value>
        public string PytaniePom {
            get
            {
                return kontoObecnegoTypa.PytPomocnicze;
            }
            set
            {
                kontoObecnegoTypa.PytPomocnicze = value;

            }
        }



    }
}
