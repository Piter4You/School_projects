using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochody
{
    internal class Samochod
    {
        public string NrRejestracyjny { get; set; }
        public string Marka { get; set; }
        public int rokProdukcji { get; set; }
        public string Kolor { get; set; }
        public int liczbaPasazerow { get; set; }

        public Samochod() { }
        public Samochod(string numerRejestracyjny, string marka, int rokProdukcji, string kolor, int liczbaPasazerow)
        {
            NrRejestracyjny = numerRejestracyjny;
            Marka = marka;
            this.rokProdukcji = rokProdukcji;
            Kolor = kolor;
            this.liczbaPasazerow = liczbaPasazerow;
        }
    }
}
