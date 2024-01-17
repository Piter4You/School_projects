using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Snake
{
    public partial class MainPage : ContentPage
    {
        List<Snake> lista = new List<Snake>();
        List<int> kierunki = new List<int>();
        int ostatniKierunek = 0, w = 0;
        bool timer = true;
        decimal tempX, tempY;
        Random random = new Random();
        public MainPage()
        {
            InitializeComponent();
            lista.Add(new Snake(0.5M, 0.45M));
            lista.Add(new Snake(0.5M, 0.5M));
            lista.Add(new Snake(0.5M, 0.55M));
            lista.Add(new Snake(0.5M, 0.6M));

            for (int i = 0; i < lista.Count(); i++)
            {
                layout.Children.Add(lista[i]);
                AbsoluteLayout.SetLayoutBounds(lista[i], new Rect((double)lista[i].X, (double)lista[i].Y, 40, 40));
                AbsoluteLayout.SetLayoutFlags(lista[i], AbsoluteLayoutFlags.PositionProportional);
            }

            NoweJablko();

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300), () =>
            {
                if(timer)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (ostatniKierunek != 0) Ruch();
                        Zjedz();
                    });
                }
                else
                {
                    KoniecGry();
                }
                return timer; // runs again, or false to stop
            });
        }

        async void KoniecGry()
        {
            bool answer = await DisplayAlert("Koniec gry!", $"Twój wynik to {w} punktów. Czy chcesz zagrać ponownie?", "Tak", "Nie");
            if (answer) (Application.Current).MainPage = new MainPage();
            else System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void Zjedz()
        {
            if (lista[0].X == Apple.lista[0].X && lista[0].Y == Apple.lista[0].Y)
            {
                layout.Children.Remove(Apple.lista[0]);
                Apple.lista.RemoveAt(0);       
                lista.Add(new Snake(tempX, tempY));
                layout.Children.Add(lista.Last());
                AbsoluteLayout.SetLayoutBounds(lista.Last(), new Rect((double)lista.Last().X, (double)lista.Last().Y, 40, 40));
                AbsoluteLayout.SetLayoutFlags(lista.Last(), AbsoluteLayoutFlags.PositionProportional);
                NoweJablko();

                w++;
                Wynik.Text = w.ToString();
            }
            else
            {
                tempX = lista.Last().X;
                tempY = lista.Last().Y;
            }
        }

        public void NoweJablko()
        {
            decimal X, Y;
            bool ok;
            do
            {
                ok = true;
                X = random.Next(10) * 0.1M;
                Y = random.Next(20) * 0.05M;
                foreach(var snake in lista)
                {
                    if (snake.X == X && snake.Y == Y)
                        ok = false;
                }
            }while(!ok);

            Apple.lista.Add(new Apple(X, Y));
            layout.Children.Add(Apple.lista[0]);
            AbsoluteLayout.SetLayoutBounds(Apple.lista[0], new Rect((double)Apple.lista[0].X, (double)Apple.lista[0].Y, 40, 40));
            AbsoluteLayout.SetLayoutFlags(Apple.lista[0], AbsoluteLayoutFlags.PositionProportional);
        }

        public void Ruch()
        { 
            for(int i = lista.Count-1; i>0; i--)
            {
                lista[i].X = lista[i - 1].X;
                lista[i].Y = lista[i - 1].Y;

            }
            if(kierunki.Count() >0)
            {
                ostatniKierunek = kierunki[0];
                kierunki.RemoveAt(0);
            }
            switch(ostatniKierunek)
            {
                case 1:
                    lista[0].Y -= 0.05M;
                    break;
                case 2:
                    lista[0].Y += 0.05M;
                    break;
                case 3:
                    lista[0].X -= 0.1M;
                    break;
                case 4:
                    lista[0].X += 0.1M;
                    break;
            }

            if (lista[0].X >= 0 && lista[0].X <= 1 && lista[0].Y >= 0 && lista[0].Y <= 1)
            {
                for (int i = 0; i < lista.Count(); i++)
                {
                    AbsoluteLayout.SetLayoutBounds(lista[i], new Rect((double)lista[i].X, (double)lista[i].Y, 40, 40));
                }
                if(timer)
                    for (int i = 1; i < lista.Count; i++)
                    {
                        if (lista[0].X == lista[i].X && lista[0].Y == lista[i].Y)
                        {
                            timer = false;
                            foreach (var snake in lista)
                            {
                                snake.BackgroundColor = Color.Black;
                            }
                            break;
                        }
                    }
            }
            else
            {
                foreach (var snake in lista)
                {
                    snake.BackgroundColor = Color.Black;
                }
                timer = false;
            }
                
        }

        public void nowyKierunek(int k)
        {
            if(ostatniKierunek == 0)
            {
                if (k != 2)
                    ostatniKierunek = k;
            }
            else if(k != ostatniKierunek)
            {
                if (kierunki.Count > 0)
                {
                    if (kierunki.Last() != k)
                    {
                        kierunki.Add(k);
                    }
                }
                else
                    kierunki.Add(k);
            }
        }

        private void Up_Swiped(object sender, SwipedEventArgs e)
        {
            if(ostatniKierunek != 2)
                nowyKierunek(1);
        }
        private void Down_Swiped(object sender, SwipedEventArgs e)
        {
            if (ostatniKierunek != 1)
                nowyKierunek(2);
        }
        private void Left_Swiped(object sender, SwipedEventArgs e)
        {
            if (ostatniKierunek != 4)
                nowyKierunek(3);
        }
        private void Right_Swiped(object sender, SwipedEventArgs e)
        {
            if (ostatniKierunek != 3)
                nowyKierunek(4);
        }
    }
}
