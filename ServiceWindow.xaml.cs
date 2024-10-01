using System.Windows;
using System.Windows.Controls;

namespace CoffeeMachine
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private CoffeeMachineResources CMResources;
        private int waterValue;
        private int beansValue;
        private int milkValue;
        private int cupsValue;
        public ServiceWindow()
        {
            InitializeComponent();

            CMResources = new();

            sliderWater.Maximum = (CMResources.GetWaterMax - CMResources.Water);
            sliderBeans.Maximum = (CMResources.GetBeansMax - CMResources.Beans);
            sliderMilk.Maximum = (CMResources.GetMilkMax - CMResources.Milk);
            sliderCups.Maximum = (CMResources.GetCupsMax - CMResources.Cups);

            sliderBeans.Minimum = sliderMilk.Minimum = sliderCups.Minimum = sliderWater.Minimum = 0;
            sliderMilk.Value = sliderBeans.Value = sliderCups.Value = sliderWater.Value = 0;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            ((Slider)sender).Value = (int)((Slider)sender).Value;
            if (((Slider)sender).Name == sliderWater.Name) waterValue = (int)((Slider)sender).Value;
            if (((Slider)sender).Name == sliderBeans.Name) beansValue = (int)((Slider)sender).Value;
            if (((Slider)sender).Name == sliderMilk.Name) milkValue = (int)((Slider)sender).Value;
            if (((Slider)sender).Name == sliderCups.Name) cupsValue = (int)((Slider)sender).Value;
        }

        private void Ok_Button(object sender, RoutedEventArgs e)
        {
            CMResources.Water += waterValue;
            CMResources.Beans += beansValue;
            CMResources.Cups += cupsValue;
            CMResources.Milk += milkValue;

            this.DialogResult = true;
        }
        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
        }
        private void Withdrow_Button(object sender, RoutedEventArgs e)
        {
            CMResources.Balance = 0;
            withdraw_text.Text = CMResources.Balance.ToString() + " рублей";
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = new();
            checkBox = (CheckBox)sender;
            if(checkBox.Content.ToString() == "Вода")
            {
                sliderWater.IsEnabled = true;
            }
            if (checkBox.Content.ToString() == "Зерна")
            {
                sliderBeans.IsEnabled = true;
            }
            if (checkBox.Content.ToString() == "Молоко")
            {
                sliderMilk.IsEnabled = true;
            }
            if (checkBox.Content.ToString() == "Стаканчики")
            {
                sliderCups.IsEnabled = true;
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = new();
            checkBox = (CheckBox)sender;
            if (checkBox.Content.ToString() == "Вода")
            {
                sliderWater.IsEnabled = false;
            }
            if (checkBox.Content.ToString() == "Зерна")
            {
                sliderBeans.IsEnabled = false;
            }
            if (checkBox.Content.ToString() == "Молоко")
            {
                sliderMilk.IsEnabled = false;
            }
            if (checkBox.Content.ToString() == "Стаканчики")
            {
                sliderCups.IsEnabled = false;
            }
        }

    }
}
