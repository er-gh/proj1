using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace CoffeeMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoffeeMachineResources CMResources;
        private CoffeeMachineClass coffeeMachine;
        private Coffee americano, latte, cappuccino;
        public MainWindow()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(Closing_Main);
            CMResources = new();
            coffeeMachine = new(CMResources);

            americano  = new(250, 0, 16, 1, 4);
            latte      = new(350, 75, 20, 1, 7);
            cappuccino = new(200, 100, 12, 1, 6);
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

            string str = CMResources.Balance.ToString()[^1..];
            serviceWindow.withdraw_text.Text = str switch
            {
                "0" or "5" or "6" or "7" or "8" or "9" => CMResources.Balance + " рублей",
                "1" => CMResources.Balance + " рубль",
                "2" or "3" or "4" => CMResources.Balance + " рубля",
                _ => CMResources.Balance + " рублей" + str,
            };

            passwordWindow.ShowDialog();
            if (passwordWindow.DialogResult == true) serviceWindow.ShowDialog();
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
            buyWindow.buyInfo.Visibility = Visibility.Visible;

            buyWindow.Owner = this;
            buyWindow.Background = this.Background;

            string check;
            if (button.Name == "buttonAmericano")
            {
                if((check = coffeeMachine.BuyCoffee(americano)) == "")
                {
                    buyWindow.coffeeInfo.Text = "Американо";
                    buyWindow.buyInfo.Text = "Стоимость " + americano.GetPrice.ToString();
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
                    buyWindow.coffeeInfo.Text = check;
                    buyWindow.buyInfo.Visibility = Visibility.Hidden;
                    buyWindow.buyButton.IsEnabled = false;
                    buyWindow.ShowDialog();
                }
            }
            else this.Show();
            if(button.Name == "buttonCappuccino")
            {
                if ((check = coffeeMachine.BuyCoffee(cappuccino)) == "")
                {
                    buyWindow.coffeeInfo.Text = "Капучино";
                    buyWindow.buyInfo.Text = "Стоимость " + cappuccino.GetPrice.ToString();
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
                    buyWindow.coffeeInfo.Text = check;
                    buyWindow.buyInfo.Visibility = Visibility.Hidden;
                    buyWindow.buyButton.IsEnabled = false;
                    buyWindow.ShowDialog();
                }
            }
            else this.Show();
            if (button.Name == "buttonLatte")
            {
                if ((check = coffeeMachine.BuyCoffee(latte)) == "")
                {
                    buyWindow.coffeeInfo.Text = "Латте";
                    buyWindow.buyInfo.Text = "Стоимость " + latte.GetPrice.ToString();
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
                    buyWindow.coffeeInfo.Text = check;
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
            CoffeeMachineResources.waterPic = CMResources.Water / 1000;
            CoffeeMachineResources.beansPic = CMResources.Beans / 200;
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
