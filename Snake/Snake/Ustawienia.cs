using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Kierunek
    {
        Left, Right, Up, Down
    }

   public  class Ustawienia
    {
        public int Szerokosc { get; set; }
        public int Wysokosc { get; set; }
        public int Szybkosc { get; set; }
        public int Wynik { get; set; }
        public int Punkty { get; set; }
        public bool GameOver { get; set; }
        public Kierunek kierunek { get; set; }

        public Ustawienia()  //domyślne ustawienia gry
        {
            Szerokosc = 16;
            Wysokosc = 16;
            Szybkosc = 15;
            Wynik = 0;
            Punkty = 10;
            GameOver = false;
            kierunek = Kierunek.Down; //domyślny kierunek rozpoczęcia gry
        }

    }
}


