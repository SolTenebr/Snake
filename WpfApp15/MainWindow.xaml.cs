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
using System.Windows.Threading;

namespace WpfApp15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Ellipse> snakeBody = new List<Ellipse>();
        private int speed = 20;
        private int diraction = 1;
        private int score = 0;
        private int index = 0;
        private int mone = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        //Тут создаются новые элементы змейки
        public void Increase()
        {
            index++;
            snakeBody.Add(new Ellipse());
            snakeBody[index].Visibility = Visibility.Visible;
            snakeBody[index].Fill = new SolidColorBrush(Colors.LightGreen);
            snakeBody[index].Width = 19;
            snakeBody[index].Height = 19;
            canvas.Children.Add(snakeBody[index]);

        }
        //Реализация тиков (для движения)
        public void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = snakeBody.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (diraction == 1)
                    {
                        snakeBody[i].Margin = new Thickness { Left = snakeBody[i].Margin.Left + this.speed, Top = snakeBody[i].Margin.Top };
                    }
                    if (diraction == 2)
                    {
                        snakeBody[i].Margin = new Thickness { Top = snakeBody[i].Margin.Top + this.speed, Left = snakeBody[i].Margin.Left };
                    }
                    if (diraction == 3)
                    {
                        snakeBody[i].Margin = new Thickness { Left = snakeBody[i].Margin.Left - this.speed, Top = snakeBody[i].Margin.Top };
                    }
                    if (diraction == 4)
                    {
                        snakeBody[i].Margin = new Thickness { Top = snakeBody[i].Margin.Top - this.speed, Left = snakeBody[i].Margin.Left };
                    }
                }
                else
                {
                    snakeBody[i].Margin = new Thickness { Top = snakeBody[i - 1].Margin.Top, Left = snakeBody[i - 1].Margin.Left };

                }
            }
        }
        //Управление
        public void gr_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Up) && diraction != 2 && !Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Right))

            {
                diraction = 4;
            }
            if (Keyboard.IsKeyDown(Key.Left) && diraction != 1 && !Keyboard.IsKeyDown(Key.Up) && !Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Right))

            {
                diraction = 3;
            }
            if (Keyboard.IsKeyDown(Key.Down) && diraction != 4 && !Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Up) && !Keyboard.IsKeyDown(Key.Right))
            {
                diraction = 2;
            }
            if (Keyboard.IsKeyDown(Key.Right) && diraction != 3 && !Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Up))
            {
                diraction = 1;
            }
        }
        //Начало операций
        public void StartTime()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(25);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Tick += new EventHandler(GameOver);
            timer.Tick += new EventHandler(Eat);
            timer.Start();
        }

        //условие по поеданию яблок
        public void Eat(object sender, EventArgs e)
        {
            if ((food.Margin.Top < elip.Margin.Top + 10 && food.Margin.Top > elip.Margin.Top - 10) && (food.Margin.Left < elip.Margin.Left + 10 && food.Margin.Left > elip.Margin.Left - 10))

            {
                score++;
                Score.Content = "Счёт: " + score;
                Random rnd = new Random();
                int MaxH = rnd.Next(10, 285);
                int MaxW = rnd.Next(10, 475);
                food.Margin = new Thickness { Left = MaxW, Top = MaxH };
                Increase();
            }
        }

        //Начало игры
        public void Start(object sender, RoutedEventArgs e)
        {
            elip.Margin = new Thickness(150, 150, 0, 0);
            this.score = 0;
            this.speed = 5;
            this.diraction = 1;
            this.index = 0;

            elip.Fill = new SolidColorBrush(Colors.Green);
            elip.Visibility = Visibility.Visible;
            food.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri( @"C:\Users\User\source\repos\WpfApp15\WpfApp15\bin\Debug\apple.png")) };
           // food.Fill = new SolidColorBrush(Colors.Red);
            food.Visibility = Visibility.Visible;
            sg.Margin = new Thickness { Left = 1500, Top = 1500 };
            //sg.Visibility = Visibility.Hidden;
            int MaxH = 0, MaxW = 0;
            Random rnd = new Random();
            MaxH = rnd.Next(10, 285);
            MaxW = rnd.Next(10, 475);
            food.Margin = new Thickness { Left = MaxW, Top = MaxH };

            snakeBody.Add(elip);
            if (this.mone == 0)
            {
                StartTime();
                this.mone++;
            }
        }


        //Конец игры
        public void GameOver(object sender, EventArgs e)
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                if (elip.Margin.Left == snakeBody[i].Margin.Left && elip.Margin.Top == snakeBody[i].Margin.Top)
                {
                    restart();
                    break;
                }
            }
            if (elip.Margin.Top > 290 || elip.Margin.Top < 10 || elip.Margin.Left > 480 || elip.Margin.Left < 10)
            {
                restart();
            }

        }

        //Рестарт
        
        public void restart()
        {
            elip.Visibility = Visibility.Hidden;
            food.Visibility = Visibility.Hidden;

            sg.Margin = new Thickness { Left = 20, Top = 20 };
            sg.Height = 270;
            sg.Width = 300;
            sg.Content = "   Змiй убився \n  Начать снова? \n      Рекорд:" + this.score;
            for (int i = 0; i < snakeBody.Count; i++)
            {
                snakeBody[i].Visibility = Visibility.Hidden;
            }
            snakeBody.RemoveRange(0, snakeBody.Count);

        }

    }
}
