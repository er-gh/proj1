using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Threading;
using System.Diagnostics;

namespace CoffeeMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(Closing_Main);
        }
        private void Service_Button(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new();
            ServiceWindow serviceWindow = new();
            passwordWindow.Owner = this;
            serviceWindow.Owner = this;
            foreach (Window window in this.OwnedWindows)
            {
                window.Background = this.Background;
            }
            this.Hide();
            CoffeeMachineResources coffee = new();
            string str = coffee.Money.ToString()[^1..];
            serviceWindow.withdraw_text.Text = str switch
            {
                "0" or "5" or "6" or "7" or "8" or "9" => coffee.Money + " рублей",
                "1" => coffee.Money + " рубль",
                "2" or "3" or "4" => coffee.Money + " рубля",
                _ => coffee.Money + " рублей" + str,
            };
            passwordWindow.ShowDialog();
            if(passwordWindow.DialogResult == true) serviceWindow.ShowDialog();
            if (serviceWindow.DialogResult == true)
            {
                Update_Pictures();
                Show();
            }
        }
        private void Buy_Coffee(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            BuyCoffeeWindow buyWindow = new();
            CoffeeMachineResources coffeeRes = new();
            buyWindow.buyInfo.Visibility = Visibility.Visible;

            buyWindow.Owner = this;
            buyWindow.Background = this.Background;

            if (button.Name == "buttonAmericano")
            {
                if(coffeeRes.AmericanoBuy() == "")
                {
                    buyWindow.coffeeInfo.Text = "Американо";
                    buyWindow.buyInfo.Text = "Стоимость " + coffeeRes.Americano.ToString();
                    this.Hide();
                    buyWindow.ShowDialog();
                    if (buyWindow.DialogResult == true)
                    {
                        Update_Pictures();
                        this.Show();
                    }
                }
                else
                {
                    buyWindow.coffeeInfo.Text = coffeeRes.AmericanoBuy();
                    buyWindow.buyInfo.Visibility = Visibility.Hidden;
                    buyWindow.buyButton.IsEnabled = false;
                    buyWindow.ShowDialog();
                }
            }
            else this.Show();
            if(button.Name == "buttonCappuccino")
            {
                if (coffeeRes.CappuccinoBuy() == "")
                {
                    buyWindow.coffeeInfo.Text = "Капучино";
                    buyWindow.buyInfo.Text = "Стоимость " + coffeeRes.Cappuccino.ToString();
                    this.Hide();
                    buyWindow.ShowDialog();
                    if (buyWindow.DialogResult == true)
                    {
                        Update_Pictures();
                        this.Show();
                    }
                }
                else
                {
                    buyWindow.coffeeInfo.Text = coffeeRes.CappuccinoBuy();
                    buyWindow.buyInfo.Visibility = Visibility.Hidden;
                    buyWindow.buyButton.IsEnabled = false;
                    buyWindow.ShowDialog();
                }
            }
            else this.Show();
            if (button.Name == "buttonLatte")
            {
                if (coffeeRes.LatteBuy() == "")
                {
                    buyWindow.coffeeInfo.Text = "Латте";
                    buyWindow.buyInfo.Text = "Стоимость " + coffeeRes.Latte.ToString();
                    this.Hide();
                    buyWindow.ShowDialog();
                    if (buyWindow.DialogResult == true)
                    {
                        Update_Pictures();
                        this.Show();
                    }
                }
                else
                {
                    buyWindow.coffeeInfo.Text = coffeeRes.LatteBuy();
                    buyWindow.buyInfo.Visibility = Visibility.Hidden;
                    buyWindow.buyButton.IsEnabled = false;
                    buyWindow.ShowDialog();
                }
            }
            else this.Show();
        }
        private void Closing_Main(object? sender, CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
        public void Update_Pictures()
        {
            CoffeeMachineResources.waterPic = CoffeeMachineResources.Water / 1000;
            CoffeeMachineResources.beansPic = CoffeeMachineResources.Beans / 200;
            for (int i = 0; i < 5; i++)
            {
                Button beans = (Button)beansGrid.Children[i];
                Button water = (Button)waterGrid.Children[i];
                beans.SetResourceReference(Button.ContentProperty, "Bean_full");
                water.SetResourceReference(Button.ContentProperty, "Waterdrop_full");
            }
            for (int i = 0; i < (5 - CoffeeMachineResources.beansPic); i++)
            {
                Button button = (Button)beansGrid.Children[i];
                button.SetResourceReference(Button.ContentProperty, "Bean_empty");
            }
            for (int i = 0; i < (5 - CoffeeMachineResources.waterPic); i++)
            {
                Button button = (Button)waterGrid.Children[i];
                button.SetResourceReference(Button.ContentProperty, "Waterdrop_empty");
            }
            
        }
    }
}
