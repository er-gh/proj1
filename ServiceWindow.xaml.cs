using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();
            CoffeeMachineResources coffeeRes = new();
            sliderWater.Maximum = (coffeeRes.WaterMax - CoffeeMachineResources.Water);
            sliderBeans.Maximum = (coffeeRes.BeansMax - CoffeeMachineResources.Beans);
            sliderMilk.Maximum = (coffeeRes.MilkMax - CoffeeMachineResources.Milk);
            sliderCups.Maximum = (coffeeRes.CupsMax - CoffeeMachineResources.Cups);
            sliderBeans.Minimum = sliderMilk.Minimum = sliderCups.Minimum = sliderWater.Minimum = 0;
            sliderMilk.Value = sliderBeans.Value = sliderCups.Value = sliderWater.Value = 0;
        }
        private int waterValue;
        private int beansValue;
        private int milkValue;
        private int cupsValue;

        


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
            CoffeeMachineResources.Water += waterValue;
            CoffeeMachineResources.Beans += beansValue;
            CoffeeMachineResources.Cups += cupsValue;
            CoffeeMachineResources.Milk += milkValue;

            this.DialogResult = true;
        }
        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
        }
        private void Withdrow_Button(object sender, RoutedEventArgs e)
        {
            CoffeeMachineResources coffee = new()
            {
                Money = 0
            };
            withdraw_text.Text = coffee.Money.ToString() + " рублей";
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
