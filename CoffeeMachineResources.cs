using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoffeeMachine
{
    abstract public class Resources
    {
        public abstract int Beans   { get; set; }
        public abstract int Milk    { get; set; }
        public abstract int Water   { get; set; }
        public abstract int Cups    { get; set; }
        public abstract int Balance { get; set; }
    }
    class CoffeeMachineResources : Resources, INotifyPropertyChanged
    {
        const int BEANS_MAX = 1000;
        const int WATER_MAX = 5000;
        const int MILK_MAX = 3000;
        const int CUPS_MAX = 200;
        
        private static int beans = BEANS_MAX;
        private static int milk = MILK_MAX;
        private static int water = WATER_MAX;
        private static int cups = CUPS_MAX;
        private static int balance = 0;

        public override int Beans { get => beans; set => beans = value; }
        public override int Milk { get => milk; set => milk = value; }
        public override int Water { get => water; set => water = value; }
        public override int Cups { get => cups; set => cups = value; }
        public override int Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged();
            }
        }
        public int GetBeansMax { get;} = BEANS_MAX;
        public int GetWaterMax { get;} = WATER_MAX;
        public int GetMilkMax { get;} = MILK_MAX;
        public int GetCupsMax { get;} = CUPS_MAX;

        public static int beansPic = 5;
        public static int waterPic = 5;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Coffee
    {
        private int waterCost;
        private int milkCost;
        private int beansCost;
        private int cupsCost;
        private int price;

        public Coffee(int waterCost, int milkCost, int beansCost, int cupsCost, int price)
        {
            this.waterCost = waterCost;
            this.milkCost = milkCost;
            this.beansCost = beansCost;
            this.cupsCost = cupsCost;
            this.price = price;
        }

        public int GetWaterCost { get => waterCost; }
        public int GetMilkCost { get => milkCost; }
        public int GetBeansCost { get => beansCost; }
        public int GetCupsCost { get => cupsCost; }
        public int GetPrice { get => price; }
    }

    public class CoffeeMachineClass
    {
        private Resources resources;
        public CoffeeMachineClass(Resources resources)
        {
            this.resources = resources;
        }

        public string BuyCoffee(Coffee coffee)
        {
            return MakeCoffee(coffee);
        }

        private string MakeCoffee(Coffee coffee)
        {
            string result = CheckProducts(coffee);

            if (result.Length == 0)
            {
                resources.Beans -= coffee.GetBeansCost;
                resources.Water -= coffee.GetWaterCost;
                resources.Milk -= coffee.GetMilkCost;
                resources.Cups -= coffee.GetCupsCost;
                resources.Balance += coffee.GetPrice;
            }
            return result;
        }

        private string CheckProducts(Coffee coffee)
        {
            string result = "";
            string[] empty_items;

            if (resources.Beans < coffee.GetBeansCost) { result += "Зерен"; }
            if (resources.Water < coffee.GetWaterCost) { result += " Воды"; }
            if (resources.Milk  < coffee.GetMilkCost)  { result += " Молока"; }
            if (resources.Cups  < coffee.GetCupsCost)  { result += " Стаканчиков"; }

            if (result.Length > 0)
            {
                empty_items = result.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (empty_items.Length == 2) { result = $"{empty_items[0]} и {empty_items[1]}"; }
                if (empty_items.Length == 3) { result = $"{empty_items[0]}, {empty_items[1]} и {empty_items[2]}"; }
                if (empty_items.Length == 4) { result = $"{empty_items[0]}, {empty_items[1]}, {empty_items[2]} и {empty_items[3]}"; }
                result = result.Insert(0, "Не достаточно: ");
            }

            return result;
        }
    }
}
