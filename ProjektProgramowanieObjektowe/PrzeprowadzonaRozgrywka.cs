using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanieObjektowe
{
    /// <summary>
    /// Klasa która reprezentować będzie obiekty stanowiące rozgrywaną rozgrywkę.
    /// </summary>
    public class PrzeprowadzonaRozgrywka
    {


        
        /// <value>
        /// Opisuje wielkość zakładu postawionego przez użytkownika(2,10 itd.)
        /// </value>
        public int wielkoscZakladu { get; set; }




        /// <value>
        /// Właściwość reprezentująca wybrany kolor.
        /// </value>
        public string WybranyKolor { get; set; }




        /// <value>
        /// Właściwość określająca wielkość kwoty wygranej prze użytkownika.
        /// </value>
        public int Wygrana { get; set; }



        /// <value>
        /// Reprezentacja czasu w którym użytkownik przeprowadził rozgrywkę.
        /// </value>
        public DateTime CzasprzeprowadzeniaGry { get; set; }




        /// <value>
        /// Łańcuch znaków który odpowiadał będzie wylosowanym liczbą przez użytkownika.
        /// </value>
        public string sekwencjaLiczb { get; set; }


    }
}
