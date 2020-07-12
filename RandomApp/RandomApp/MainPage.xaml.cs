using RandomApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RandomApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private RandomDishViewModel DishVM;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = RandomDishViewModel.instance;
            DishVM = ((RandomDishViewModel)BindingContext);
        }

        private void GenerateDish(object sender, EventArgs e)
        {
            if (DishVM.DishList.Count <= 0)
            {
                DisplayAlert("No Dish Found", "Please add atleast 1 dish.", "Okay");
                Navigation.PushAsync(new ListPage());
            }

            var loopCount = 10;
            var waitCount = 20;
            string[] knownColor = { "#000000", "#32a848", "#327da8", "#5d32a8", "#a8329c", "#a83259", "#a83232" };
            Task.Run(() =>
            {
                AnimateDishImage();
                for (var i = 1; i <= loopCount; i++) {
                    var selectedDish = DishVM.PickRandomDish();
                    Random randomGen = new Random();
                    int randomIndex = randomGen.Next(0, knownColor.Length);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        label_dish.Text = selectedDish.Name;
                        label_dish.TextColor = Color.FromHex(knownColor[randomIndex]);
                    });
                    Task.Delay(waitCount).Wait();
                }
            });
        }

        async void AnimateDishImage()
        {
            uint timeout = 50;

            await image_dish.TranslateTo(-15, 0, timeout);

            await image_dish.RotateTo(10, timeout);

            await image_dish.TranslateTo(15, 0, timeout);

            await image_dish.RotateTo(-10, timeout);

            await image_dish.TranslateTo(-10, 0, timeout);

            await image_dish.RotateTo(5, timeout);

            await image_dish.TranslateTo(10, 0, timeout);

            await image_dish.RotateTo(-5, timeout);

            await image_dish.TranslateTo(-5, 0, timeout);

            await image_dish.RotateTo(2, timeout);

            await image_dish.TranslateTo(5, 0, timeout);

            await image_dish.RotateTo(-2, timeout);

            image_dish.TranslationX = 0;
            image_dish.Rotation = 0;
        }

        private void GoToDishList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Accelerometer.ShakeDetected += GenerateDish;
            Accelerometer.Start(SensorSpeed.Game);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Accelerometer.ShakeDetected -= GenerateDish;
            Accelerometer.Stop();
        }
    }
}
