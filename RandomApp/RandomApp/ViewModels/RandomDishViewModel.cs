using RandomApp.Helpers;
using RandomApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RandomApp.ViewModels
{
    public class RandomDishViewModel : INotifyPropertyChanged
    {
        private static RandomDishViewModel _instance = new RandomDishViewModel();
        public static RandomDishViewModel instance
        {
            get
            {
                return _instance;
            }
        }

        private List<Dish> dishList;
        public List<Dish> DishList
        {
            get
            {
                return dishList;
            }
            set
            {
                dishList = value;
                OnPropertyChanged();
            }
        }

        public RandomDishViewModel()
        {

            DishList = new List<Dish>();
            DishList = DatabaseHelper.Read();
        }

        public Dish PickRandomDish()
        {
            int selectedIndex = _random.Next(0, DishList.Count);
            return DishList[selectedIndex];
        }

        public void RefreshList()
        {
            DishList = DatabaseHelper.Read();
        }

        public Dish GetDishById(int DishId)
        {
            var index = DishList.FindIndex(x => x.Id == DishId);
            return DishList[index];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Random _random = new Random();
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
