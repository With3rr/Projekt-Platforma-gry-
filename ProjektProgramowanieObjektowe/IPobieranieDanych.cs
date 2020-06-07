using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ProjektProgramowanieObjektowe
{
    /// <summary>
    /// Jest to interfejs który pełni rolę dostępu do relacji bazy danych.
    /// </summary>
    /// <remarks>
    /// Pozwala na wczytywanie kreślonych danych lub pobieranie zmiennych tak samo jak całych kolekcji obiektów.
    /// </remarks>
    public interface IPobieranieDanych
    {

        /// <summary>
        /// Metoda zwracająca wartość typu bolean która reprezentuje to czy użytkownik o podanych danych istnieje w bazie danych.
        /// </summary>
        /// <param name="a1">Jest to nazwa użytkownika</param>
        /// <param name="a2">Jest to hasło użytkownika</param>
        /// <returns>Wartość reprezentująca istnienie użytkownika o podanych danych.</returns>
        bool CzyJestUrzytkownikwBazie(string a1,string a2);




        /// <summary>
        /// Metoda zwracająca wartość typu bolean która reprezentuje to czy użytkownik o podanych danych istnieje w bazie danych.Wartość potrzebna do rejestracji.
        /// </summary>
        /// <param name="a1">Jest to nazwa użytkownika</param>
        /// <param name="a2">Jest to hasło użytkownika</param>
        /// <returns>Wartość reprezentująca istnienie użytkownika o podanych danych.</returns>
        bool CzyJestUrzytkownikwBazieRejestracja(string a1, string a2);



        /// <summary>
        /// Metoda odpowiedzialna za dodawanie nowego użtkownika o podanych danych do bazy danych.
        /// </summary>
        /// <param name="nick">Argument reprezentujący nick</param>
        /// <param name="email"> Argument reprezentujący email</param>
        /// <param name="haslo">Argument reprezentujący hasło użytkownika</param>
        /// <param name="pytanie">Argument który reprezentuje pytanie pomocnicze ustalane podczas rejestracji.</param>
        void DodajUzytkownika(string nick, string email, string haslo,string pytanie);


        /// <summary>
        /// Metoda odpowiedzialna za dodawanie nowego użtkownika o podanych danych do bazy danych.
        /// </summary>
        /// <param name="nick">Argument reprezentujący nick</param>
        /// <param name="email"> Argument reprezentujący email</param>
        /// <param name="haslo">Argument reprezentujący hasło użytkownika</param>
        void DodajUzytkownika(string nick, string email, string haslo);



        /// <value>
        /// Właściwość reprezentująca Fundusze użytkownika obecnie kożystającego z platformy.
        /// </value>
        float Fundusze { get; set; }
        int ZwrocId();




        /// <summary>
        /// Metoda sprawdzająca czy w bazie danych istnieje obiekt karty płatniczej o podanych danych,jeżeli tak następnie sprawdzane są środki na karcie.
        /// </summary>
        /// <param name="nr">Nr karty płatniczej.</param>
        /// <param name="kod">Kod dostępu do karty.</param>
        /// <param name="Imie">Imie posiadacza karty.</param>
        /// <param name="Nazwisko">Nazwisko posiadacza karty.</param>
        /// <param name="kwota">Kwota/środki na karcie</param>
        /// <returns>Zwraca wartość bolean która określa czy dana karta istnieje oraz czy środki są wystarczające</returns>
        bool CzyJestTakaKartaiSrodki(string nr,string kod,string Imie,string Nazwisko,float kwota);



        /// <summary>
        /// Metoda sprawdzająca czy w bazie danych istnieje obiekt którego właściwość kod zpełnia założenia.
        /// </summary>
        /// <param name="kod">Kod paysafe podawany przez użytkownika</param>
        /// <returns></returns>
        bool CzyJestTakiPaysafe(string kod);



        /// <summary>
        /// Metoda sprawdzająca  czy istnieje obiekt w bazie danych o podanym nr karty płatniczej.
        /// </summary>
        /// <param name="kod">Nr karty płatniczej podawany przez użytkownika</param>
        /// <returns>Zwraca wartość bolean która mówi czy takie konto istnieje.</returns>
        bool CzyJestTakeKontobankowe(string kod);



        /// <summary>
        /// Metoda która odpowiada za wysyłanie środków pieniężnych na określone konto bankowe.
        /// </summary>
        /// <param name="kwota">Kwota wypłaty.</param>
        /// <param name="nr_konta">Nr konta na które wpłacamy gotówę.</param>
        /// <param name="Opis">Opis wypłaty.</param>
        void WyplataPieniedzy(float kwota,string nr_konta,string Opis);


        /// <summary>
        /// Metoda zwracająca kolekcję wpłat użytkownika.
        /// </summary>
        /// <returns>Kolekcja obiektów wpłat</returns>
        
        ObservableCollection<Wplaty> PokazWplaty();

        /// <summary>
        /// Metoda zwracająca kolekcję wypłat użytkownika.
        /// </summary>
        /// <returns>Kolekcja obiektów wypłat</returns>
        ObservableCollection<Wyplaty> PokazWyplaty();



      
        bool czyOknoAktywne();


        /// <summary>
        /// Jest to metoda zapisująca nowy obiekt reprezentujący przeprowadzoną rozgrywkę w bazie danych.
        /// </summary>
        /// <param name="wielkoscZakladu">Argument reprezentujący wielkość zakładu.</param>
        /// <param name="WybranyKolor"> Argument reprezentujący wybrany przez użytkownika kolor.</param>
        /// <param name="Wygrana">Reprezentacja wygranej</param>
        /// <param name="CzasprzeprowadzeniaGry">Czas przeprowadzenia rozgrywki prze użytkownika</param>
        /// <param name="sekwencjaLiczb">Łańsuch znaków reprezentujący wylosowane przez użytkownika liczby.</param>
        void rozegranarozgrywka(int wielkoscZakladu, string WybranyKolor, int Wygrana, DateTime CzasprzeprowadzeniaGry, string sekwencjaLiczb);


        /// <summary>
        /// Metoda zwracająca obiekty rozgrywanych gier z bazy danych.
        /// </summary>
        /// <returns>Zwraca kolekcję bier rozegranych.</returns>
        ObservableCollection<RozegraneeGry> PokazGry();




        void uzupelnanieDanych(string imie, string nazwisko, string hobby, string oSobie,string pytanie);


        /// <summary>
        /// Metoda zwracająca kolekcję gier z bazy danych która zwraza gry rozegrane prze użytkownika.
        /// </summary>
        /// <returns>Kolekcja rozegranych gier.</returns>
        ObservableCollection<RozegraneeGry> PokazGryMoje();


        /// <summary>
        /// Zwraca kolekcję użytkowników z bazy danych.
        /// </summary>
        /// <returns>Lista użytkowników</returns>
        ObservableCollection<Konto> PokazUzytkownikow();

        /// <summary>
        /// Zwraca kolekcję obiektów reprezentujących wprowadzone zmiany
        /// </summary>
        /// <returns>Kolekcja obiektów zmian zasad gry.</returns>
        ObservableCollection<ZasadyGry> PokazZmianyy();


        /// <summary>
        /// Zwraca kolekcję obiektów reprezentujących wprowadzone zmiany
        /// </summary>
        /// <returns>Kolekcja obiektów zmian zasad gry.</returns>
        List<ZasadyGry> PokazZmianyyList();
        
            


        
        /// <summary>
        /// Metoda sprawdzająca czy urzytkownik ma uprawnienia administratora.
        /// </summary>
        /// <returns>Wartość bolean czy użytkownik ma takie uprawnienia czy nie ma.</returns>
        bool CzyMaster();



        /// <summary>
        /// Metoda usuwająca podanego użytkownika z bazy danych
        /// </summary>
        /// <param name="kontodoUsuniecia">Obiekt  do usunięcia</param>
        void Usuwanie_Usera(Konto kontodoUsuniecia);


        /// <summary>
        /// Zapisywanie zmian w bazie danych.
        /// </summary>
        void zapisanieZmiany();



        /// <value>
        /// Właściwość reprezentująca Imię użytkownika;
        /// </value>
        string Imie { get; set; }

        /// <value>
        /// Właściwość reprezentująca Nazwisko użytkownika;
        /// </value>
        string Nazwisko { get; set; }

        /// <value>
        /// Właściwość reprezentująca Hobby użytkownika;
        /// </value>
        string Hobby { get; set; }

        /// <value>
        /// Właściwość reprezentująca informacje o osobie użytkownika;
        /// </value>
        string Osobie { get; set; }

        /// <value>
        /// Właściwość reprezentująca pytanie pomocnicze użytkownika;
        /// </value>
        string PytaniePom { get; set; }


        /// <summary>
        /// Metoda która po podaniu odpowiednich danych podaje hasło użytkownika.
        /// </summary>
        /// <param name="pytanie">Pytanie pomocnicze.</param>
        /// <param name="login">Login użytkownika.</param>
        /// <returns>Zwraca hasło użytkownika.</returns>
        string Przypomnij_haslo(string pytanie,string login);


        /// <value>
        /// Właściwość reprezentująca obiekt ZasadGry.
        /// </value>
        ZasadyGry Mnozniki { get; set; }
       


    }
}
