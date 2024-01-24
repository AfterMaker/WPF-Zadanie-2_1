using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calkulator
{
    public partial class MainWindow : Window
    {
        double first_num = 0;
        bool first_num_is_enter = false;
        double second_num = 0;
        int action;
        bool is_frac = false;
        bool update_sec_num = true;
        bool error = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void clear(bool clear_display)
        {
            first_num = 0;
            second_num = 0;
            first_num_is_enter = false;
            is_frac = false;
            action = 0;
            if (clear_display)
                Display.Text = "0";
        }
        private void ActButClick(object sender, RoutedEventArgs e)
        {
            if (error)
                return;
            first_num = Convert.ToDouble(Display.Text);
            first_num_is_enter = true;
            switch ((e.Source as Button).Content.ToString())
            {
                case "%":
                    action = 1;
                    break;
                case "÷":
                    action = 2;
                    break;
                case "×":
                    action = 3;
                    break;
                case "-":
                    action = 4;
                    break;
                case "+":
                    action = 5;
                    break;
            }
            Display.Text = "0";
            is_frac = false;
            update_sec_num = true;
        }
        private void CButClick(object sender, RoutedEventArgs e)
        {
            if (first_num_is_enter)
            {
                second_num = 0;
            }
            else
            {
                first_num = 0;
            }
            Display.Text = "0";
            is_frac = false;
            error = false;
        }
        private void CEButClick(object sender, RoutedEventArgs e)
        {
            clear(true);
            error = false;
        }
        private void DelButClick(object sender, RoutedEventArgs e)
        {
            int last_index = Display.Text.Length - 1;
            if (last_index == 0 || error)
            {
                Display.Text = "0";
                error = false;
            }
            else
            {
                if (Display.Text[last_index] == ',')
                {
                    is_frac = false;
                }

                Display.Text = Display.Text[..^1];
            }

        }
        private void FracButClick(object sender, RoutedEventArgs e)
        {
            if (error)
                return;
            double num = Convert.ToDouble(Display.Text);
            if (num != 0)
            {
                Display.Text = $"{1 / num}";
            }
            clear(false);
        }
        private void DegrButClick(object sender, RoutedEventArgs e)
        {
            if (error)
                return;
            double num = Convert.ToDouble(Display.Text);
            Display.Text = $"{num * num}";
            clear(false);
        }
        private void RootClick(object sender, RoutedEventArgs e)
        {
            if (error)
                return;
            double num = Convert.ToDouble(Display.Text);

            Display.Text = $"{Math.Sqrt(num)}";
            clear(false);
        }
        private void ChangeSignButClick(object sender, RoutedEventArgs e)
        {
            if (error)
                return;
            if (Display.Text[0] == '-')
            {
                Display.Text = Display.Text.Substring(1, Display.Text.Length - 1);
            }
            else
            {
                if (Display.Text == "0")
                    Display.Text = "-";
                else
                    Display.Text = $"-{Display.Text}";
            }

        }
        private void DotButClick(object sender, RoutedEventArgs e)
        {
            if (error)
                return;
            if (!is_frac)
            {
                Display.Text = $"{Display.Text},";
                is_frac = true;
            }

        }
        private void ResButClick(object sender, RoutedEventArgs e)
        {
            if (first_num_is_enter)
            {
                double res = 0;
                if (update_sec_num)
                    second_num = Convert.ToDouble(Display.Text);
                bool er = false;
                switch (action)
                {
                    case 1:
                        if (second_num != 0)
                            res = first_num % second_num;
                        else
                            er = true;
                        break;
                    case 2:
                        if (second_num != 0)
                            res = first_num / second_num;
                        else
                            er = true;
                        break;
                    case 3:
                        res = first_num * second_num;
                        break;
                    case 4:
                        res = first_num - second_num;
                        break;
                    case 5:
                        res = first_num + second_num;
                        break;
                }
                if (!er)
                {
                    Display.Text = $"{res}";
                    foreach (char c in Display.Text)
                    {
                        if (c == ',')
                        {
                            is_frac = true;
                            break;
                        }
                    }
                    update_sec_num = false;
                    first_num = res;
                }
                else
                {
                    Display.Text = "E";
                    error = true;
                    clear(false);
                }
            }
        }
        private void NumButClick(object sender, RoutedEventArgs e)
        {

            String nums = (e.Source as Button).Content.ToString();

            if (Display.Text == "0" || error)
            {
                Display.Text = "";
                error = false;
            }

            Display.Text = Display.Text + nums;

        }
        private void KeyboardListener(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}