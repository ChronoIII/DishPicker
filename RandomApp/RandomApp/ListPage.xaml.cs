using RandomApp.Helpers;
using RandomApp.Models;
using RandomApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RandomApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private RandomDishViewModel DishVM;
        public ListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = RandomDishViewModel.instance;
            DishVM = ((RandomDishViewModel)BindingContext);
        }

        private async void PushDishToList(object sender, EventArgs e)
        {
            Dish dish = new Dish()
            {
                Name = entry_dish.Text.Trim()
            };
            if (DatabaseHelper.Insert(ref dish))
            {
                await DisplayAlert("New Dish", "You successfully added a dish.", "Thank you");
                entry_dish.Text = "";
                DishVM.RefreshList();
            }
        }

        private async void RemoveDishFromList(object sender, EventArgs e)
        {
            // Dish selectedDish = (Dish)lv_dishes.SelectedItem;
            var selectedId = Int32.Parse(((Image)sender).ClassId);
            Dish selectedDish = DishVM.GetDishById(selectedId);
            bool confirm = await DisplayAlert("Remove Dish", "Do you wish to delete \"" + selectedDish.Name + "\" ?", "Yes", "No");
            if (confirm)
            {
                if (DatabaseHelper.Remove(selectedId))
                {
                    await DisplayAlert("Dish Removed", "\"" + selectedDish.Name + "\" was successfully deleted", "Ok");
                    DishVM.RefreshList();
                }
            }
        }

        private void BackToMain(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}