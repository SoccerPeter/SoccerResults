using System;
using System.Collections.Generic;
using SoccerResults.Models;
using SoccerResults.Services;
using SoccerResults.ViewModels;
using Xamarin.Forms;

namespace SoccerResults.Views
{
    public partial class GamesPage : ContentPage
    {
        GamesViewModel viewMNodel;

        public GamesPage()
        {
            BindingContext = viewMNodel = new GamesViewModel();
            InitializeComponent();
        }

        public void Handle_Date(object sender, EventArgs e)
        {
            //datePicker.Focus();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var v = e.Item as Games;
            //await Navigation.PushModalAsync(new NavigationPage(new GamesDetailPage(v)));
            var restService = new SoccerService();
            var E = await restService.GetEvents(v.id);
            var EE = fixEvents(E);
            await Navigation.PushAsync(new NavigationPage(new EventsPage(EE)));
            ((ListView)sender).SelectedItem = null;
        }

        private List<Events> fixEvents(List<Events> E)
        {
            foreach (var item in E)
            {
                if (item.Type.Trim() == "Goal")
                {
                    item.Bild = "ball_goal";
                }
                if (item.Type.Trim() == "Card")
                {
                    item.Bild = "yellow_card";
                }
                if (item.Type.Trim() == "subst")
                {
                    item.Bild = "right";
                }
            }

            return E;
        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            string v = (sender as Button).Text;
            int langd = v.Length;
            int start = v.IndexOf(":");
            int slut = langd - start;
            string liga = v.Substring(start + 1, slut - 1);
            var restService = new SoccerService();
            var T = await restService.GetTable(liga);

            //await DisplayAlert("Alert", T.ToString(), "OK");
            //await Navigation.PushModalAsync(new NavigationPage(new TablesPage(T)));
        }

        public async void getGames(object sender, DateChangedEventArgs e)
        {
            var d = datePicker.Date.ToString("yyyy-MM-dd");
            viewMNodel.gameDate = d;
            await viewMNodel.LoadGames(d);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (viewMNodel.MyItems.Count == 0)
            {
                viewMNodel.IsBusy = true;
                DateTime D = DateTime.Now;
                datePicker.Date = D;
                viewMNodel.SoccerDay = D.ToString("yyyy-MM-dd");
                await viewMNodel.LoadGames(viewMNodel.SoccerDay);
                viewMNodel.IsBusy = false;
            }

        }

        private async void MenuItem1_Clicked(System.Object sender, System.EventArgs e)
        {
            viewMNodel.IsBusy = true;
            DateTime D = DateTime.Parse(viewMNodel.SoccerDay);
            DateTime DD = D.AddDays(-1);
            datePicker.Date = DD;
            viewMNodel.SoccerDay = datePicker.Date.ToString("yyyy-MM-dd");
            await viewMNodel.LoadGames(viewMNodel.SoccerDay);
            Datum.Text = viewMNodel.SoccerDay;
            viewMNodel.IsBusy = false;
        }

        private async void MenuItem2_Clicked(System.Object sender, System.EventArgs e)
        {
            viewMNodel.IsBusy = true;
            DateTime D = DateTime.Parse(viewMNodel.SoccerDay);
            DateTime DD = D.AddDays(1);
            datePicker.Date = DD;
            viewMNodel.SoccerDay = datePicker.Date.ToString("yyyy-MM-dd");
            await viewMNodel.LoadGames(viewMNodel.SoccerDay);
            Datum.Text = viewMNodel.SoccerDay;
            viewMNodel.IsBusy = false;
        }
    }
}
