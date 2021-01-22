using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Obiekty> Snake = new List<Obiekty>(); //utworzenie listy dla węża głowa i ciało
        private Obiekty jedzenie = new Obiekty();        //utworzenie obiektu jedzenie
        public Ustawienia _ustawienia = new Ustawienia();

        public Form1()
        {
            InitializeComponent();

            new Ustawienia();
            tCzasGry.Interval = 1000 / _ustawienia.Szybkosc;
            tCzasGry.Tick += Ekran; //do zmiany (powiązanie funkcji Screen do czasu)
            tCzasGry.Start();
            startGry();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Ekran(object sender, EventArgs e)
        {
            if (_ustawienia.GameOver == true) // GameOver zwraca true i zostanie naciśnięty przycisk enter zostanie uruchomina funkcja startu gry
            {
                if (Klawiatura.KeyPress(Keys.Enter))
                {
                    startGry();
                }
            }
            else
            {
                if (Klawiatura.KeyPress(Keys.Right) && _ustawienia.kierunek != Kierunek.Left) // przyciśnięcie przycisku w prawo oraz ustawienie kierunku nie jest w lewo, kierunek zmieni się w prawo
                {
                    _ustawienia.kierunek = Kierunek.Right;
                }

                else if (Klawiatura.KeyPress(Keys.Left) && _ustawienia.kierunek != Kierunek.Right)
                {
                    _ustawienia.kierunek = Kierunek.Left;
                }

                else if (Klawiatura.KeyPress(Keys.Up) && _ustawienia.kierunek != Kierunek.Down)
                {
                    _ustawienia.kierunek = Kierunek.Up;
                }

                else if (Klawiatura.KeyPress(Keys.Down) && _ustawienia.kierunek != Kierunek.Up)
                {
                    _ustawienia.kierunek = Kierunek.Down;
                }

                Ruch();

            }
            pbPlansza.Invalidate(); //ponowne załadownie planszy


        }
        private void wdol(object sender, KeyEventArgs e) 
        {
            Klawiatura.changeState(e.KeyCode, true);
        }

        private void wgore(object sender, KeyEventArgs e)
        {
            Klawiatura.changeState(e.KeyCode, false);
        }

        private void rysowanieWeza(object sender, PaintEventArgs e)
        {
            Graphics plansza = e.Graphics;
            
            if (_ustawienia.GameOver == false)
            {
                Brush wazKolor;

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        {
                            wazKolor = Brushes.Green;
                        }
                    else
                        {
                            wazKolor = Brushes.DarkGreen; 
                        }


                    plansza.FillRectangle(wazKolor, new Rectangle(
                                                                   Snake[i].X * _ustawienia.Szerokosc,
                                                                   Snake[i].Y * _ustawienia.Wysokosc,
                                                                   _ustawienia.Szerokosc, _ustawienia.Wysokosc
                                                                  ));
                                       
                    plansza.FillRectangle(Brushes.Red, new Rectangle(
                                                                jedzenie.X * _ustawienia.Szerokosc,
                                                                jedzenie.Y * _ustawienia.Wysokosc,
                                                                _ustawienia.Szerokosc, _ustawienia.Wysokosc
                                                                ));
                }
            }
            else
            {
                string gameOver = "Game Over \n" + "Twój wynik to: " + _ustawienia.Wynik + "\n  Wciśnij Enter aby zagrać ponownie \n";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }

        private void startGry()
        {
            label3.Visible = false;
            
            Snake.Clear();
            Obiekty glowa = new Obiekty { X = 10, Y = 10 };
            Snake.Add(glowa);

            label2.Text = _ustawienia.Wynik.ToString();

            generowanieJedzenia();
        }

        private void Ruch()
        {
            for (int i = Snake.Count - 1; i >=0; i--)
            {
                if (i == 0)
                {
                    switch (_ustawienia.kierunek)
                    {
                        case Kierunek.Right:
                            Snake[i].X++;
                            break;

                        case Kierunek.Left:
                            Snake[i].X++;
                            break;

                        case Kierunek.Up:
                            Snake[i].X++;
                            break;

                        case Kierunek.Down:
                            Snake[i].X++;
                            break;
                    }

                    int maxXpos = pbPlansza.Size.Width / _ustawienia.Szerokosc; 
                    int maxYpos = pbPlansza.Size.Height / _ustawienia.Wysokosc;

                    if (Snake[i].X < 0 || Snake[i].Y <0 || Snake[i].X > maxXpos || Snake[i].Y > maxYpos)
                    {
                        Smierc(); 
                    }

                    for (int j = 1; j < Snake.Count; j++) // sprawdzenie czy głowa węża nie dotknęła ogona
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Smierc();
                        }
                    }
                    if (Snake[0].X == jedzenie.X && Snake[0].Y == jedzenie.Y) 
                    {
                        Zjedzenie(); // uruchomie funkcji zjedzenie po "kolizji" głowy z jedzeniem
                    }
                }
                else // brak "kolizji" z planszą, ciałem i jedzeniem - wąż porusza się dalej
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void generowanieJedzenia()
        {
            int maxXpos = pbPlansza.Size.Width / _ustawienia.Szerokosc;
            int maxYpos = pbPlansza.Size.Height / _ustawienia.Wysokosc;

            Random rnd = new Random();
            jedzenie = new Obiekty
            {
                X = rnd.Next(0, maxXpos),
                Y = rnd.Next(0, maxYpos)
            };
        }

        private void Zjedzenie()
        {
            Obiekty ogon = new Obiekty
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y,
            };

            Snake.Add(ogon);
            _ustawienia.Wynik += _ustawienia.Punkty;
            label2.Text = _ustawienia.Wynik.ToString();
            generowanieJedzenia();
        }

        private void Smierc ()
        {
            _ustawienia.GameOver = true;
        }
    }

}
