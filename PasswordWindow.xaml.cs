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
using System.Windows.Shapes;

namespace CoffeeMachine
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        private string? checkPassword;
        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Button button = new Button();
            button = (Button)sender;
            TextBlock textBlock = new TextBlock();
            textBlock = (TextBlock)button.Content;
            checkPassword += textBlock.Text;
            main.textCappuccino.Text = checkPassword;
        }
        private void Ok_Button(object sender, RoutedEventArgs e)
        {
            string password = "123";
            if(checkPassword == password)
            {
                this.DialogResult = true;
                checkPassword = "";
            }
            else
            {
                messageBlock.Text = "Неправильный пароль";
                checkPassword = "";
            }
        }
        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
        }

    }
}
