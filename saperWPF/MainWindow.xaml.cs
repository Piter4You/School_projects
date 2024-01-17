using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace saperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Buttons[,] btn = new Buttons[10, 10];
        Random random = new Random();
        int flaga = 20, iloscruchow;
        bool pierwszy = false;
        public MainWindow()
        {
            InitializeComponent();
            iloscruchow = 100 - flaga;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    btn[i, j] = new Buttons();
                    plansza.Children.Add(btn[i, j]);
                    Grid.SetColumn(btn[i, j], j);
                    Grid.SetRow(btn[i, j], i);
                    btn[i, j].Click += Left_Click;
                    btn[i, j].MouseRightButtonDown += RightButtonDown;
                }
            }

           

        }

        private void RightButtonDown(object sender, MouseButtonEventArgs e)
        {
            int y = Grid.GetColumn((Buttons)sender);
            int x = Grid.GetRow((Buttons)sender);

            if (btn[x, y].flaga == false)
            {
                if (flaga > 0)
                {
                    btn[x, y].flaga = true;
                    btn[x, y].Content = "🚩";
                    flaga--;
                }       
            }
            else
            {
                btn[x, y].flaga = false;
                btn[x, y].Content = "";
                flaga++;
            }
           
        }

        private void Odkryj(int x, int y)
        {

            btn[x, y].Content = btn[x, y].Wartosc.ToString();
            btn[x, y].Odkryte = true;
            btn[x, y].IsEnabled = false;
            iloscruchow--;
            if(btn[x, y].flaga == true)
            {
                btn[x, y].flaga = false;
                flaga++;
            }
           
            if (btn[x, y].Wartosc == 0)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i <= 9 && j >= 0 && j <= 9)
                        {
                            if (!btn[i, j].Odkryte)
                            {
                                if (btn[i, j].Wartosc == 0)
                                {
                                    Odkryj(i, j);
                                }
                                else
                                {
                                    btn[i, j].Content = btn[i, j].Wartosc.ToString();
                                    btn[i, j].Odkryte = true;
                                    btn[i, j].IsEnabled = false;
                                    iloscruchow--;
                                    if (btn[i,j].flaga)
                                    {
                                        btn[i, j].flaga = false;
                                        flaga++;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            if (btn[x, y].Wartosc == 10)
            {
                odkryjPlansze(false);
            }
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            int y = Grid.GetColumn((Buttons)sender);
            int x = Grid.GetRow((Buttons)sender);
            if(pierwszy == false)
            {
                pierwszy = rozstawBomby(x,y);
            }
            if (btn[x, y].flaga == false)
            {
                Odkryj(x, y);
            }
            if (iloscruchow == 0)
            {
                odkryjPlansze(true);
            }
        }

        private bool rozstawBomby(int xx, int yy)
        {
            for (int i = xx - 1; i <= xx + 1; i++)
            {
                for (int j = yy - 1; j <= yy + 1; j++)
                {
                    if (i >= 0 && i <= 9 && j >= 0 && j <= 9)
                    {
                        btn[i, j].Wartosc = 100;
                    }
                }
            }

            int licznik = 0;
            while (licznik < flaga)
            {
                int x = random.Next(10);
                int y = random.Next(10);

                if (btn[x, y].Wartosc < 10)
                {
                    licznik++;
                    btn[x, y].Wartosc = 10;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (i >= 0 && i <= 9 && j >= 0 && j <= 9 && btn[i, j].Wartosc != 10)
                            {
                                btn[i, j].Wartosc++;
                            }
                        }
                    }
                }
            }

            for (int i = xx - 1; i <= xx + 1; i++)
            {
                for (int j = yy - 1; j <= yy + 1; j++)
                {
                    if (i >= 0 && i <= 9 && j >= 0 && j <= 9)
                    {
                        btn[i, j].Wartosc -= 100;
                    }
                }
            }
            return true;
        }

        private void odkryjPlansze(bool wygrana)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    btn[i, j].IsEnabled = false;
                    if (btn[i, j].Wartosc == 10)
                    {
                        if(btn[i, j].flaga == true)
                        {
                            btn[i, j].Content = "🚩";
                        }
                        else
                        {
                            btn[i, j].Content = "💣";
                        }
                    }
                    else
                    {
                        btn[i, j].Content = btn[i, j].Wartosc.ToString();
                    }
                }
            }
            if(wygrana)
            {
                MessageBox.Show("Wygrana");
            }
            else
            {
                MessageBox.Show("Przegrana");
            }
        }
    }
}
