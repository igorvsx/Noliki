using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Noliki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] btns = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int wins = 0;
        private bool player = true;
        private int sel;
        private int tag = 0;
        private bool isEnd = false;
        private int turn = 0;
        public MainWindow()
        {
            InitializeComponent();
            ButtonsDisable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string s = button.Name;
            var re = new Regex("button");
            sel = Convert.ToInt32(re.Replace(s, ""));

            if (btns[sel] != 'X' && btns[sel] != 'O' && player == true)
            {
                if (turn % 2 != 0)
                {
                    btns[sel] = 'X';
                    button.Content = 'X';
                }
                else
                {
                    btns[sel] = 'O';
                    button.Content = 'O';
                }
                player = false;

                tag = CheckWin();
                if (tag == 1)
                {
                    WhoWin.Text = "кожаный мешок победитель";
                    wins += 1;
                    Wins.Text = "Победы: " + wins.ToString();
                    ButtonsDisable();
                    isEnd = true;
                }
                else if (tag == -1)
                {
                    WhoWin.Text = "ничья";
                    ButtonsDisable();
                }

                else if (player == false)
                {
                    sel = RobotMove();
                    button = (Button)FindName("button" + sel);
                    if (turn % 2 == 0)
                    {
                        btns[sel] = 'X';
                        button.Content = 'X';
                    }
                    else
                    {
                        btns[sel] = 'O';
                        button.Content = 'O';
                    }
                    player = true;
                }
                if (isEnd == false)
                {
                    tag = CheckWin();
                    if (tag == 1)
                    {
                        WhoWin.Text = "ботик победил";
                        ButtonsDisable();
                    }
                    else if (tag == -1)
                    {
                        WhoWin.Text = "ничья";
                        ButtonsDisable();
                    }
                }
            }

        }
        private void buttonNewgame_Click(object sender, RoutedEventArgs e)
        {
            btns = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            NewGame();

            if (turn % 2 == 0)
            {
                Button button = (Button)sender;
                sel = RobotMove();
                button = (Button)FindName("button" + sel);
                btns[sel] = 'X';
                button.Content = 'X';
                player = true;
            }
        }
        private int CheckWin()
        {
            if (
                (btns[1] == btns[2] && btns[2] == btns[3]) ||
                (btns[4] == btns[5] && btns[5] == btns[6]) ||
                (btns[7] == btns[8] && btns[8] == btns[9]) ||
                (btns[1] == btns[4] && btns[4] == btns[7]) ||
                (btns[2] == btns[5] && btns[5] == btns[8]) ||
                (btns[3] == btns[6] && btns[6] == btns[9]) ||
                (btns[1] == btns[5] && btns[5] == btns[9]) ||
                (btns[3] == btns[5] && btns[5] == btns[7])
                )
            {
                return 1;
            }
            else if (btns[1] != 1 && btns[2] != 2 && btns[3] != 3 && btns[4] != 4 && btns[5] != 5 && btns[6] != 6 && btns[7] != 7 && btns[8] != 8 && btns[9] != 9)
            {
                return -1;
            }
            return 0;
        }
        private int RobotMove()
        {
            Random random = new Random();
            int step = random.Next(1, 10);
            while (btns[step] == 'X' || btns[step] == 'O')
            {
                step = random.Next(1, 10);
            }
            return step;
        }
        private void NewGame()
        {
            List<Button> buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (Button btn in buttons)
            {
                btn.IsEnabled = true;
                btn.Content = "";
            }
            WhoWin.Text = "";
            player = true;
            isEnd = false;
            tag = 0;
            turn++;
        }
        private void ButtonsDisable()
        {
            List<Button> buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (Button btn in buttons)
            {
                btn.IsEnabled = false;
            }
        }
    }
}
