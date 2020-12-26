using System;
using System.Collections.Generic;
using SoccerResults.Models;
using Xamarin.Forms;

namespace SoccerResults.Views
{
    public partial class EventsPage : ContentPage
    {
        public EventsPage(List<Events> listaEvents)
        {
            InitializeComponent();
            lst.ItemsSource = listaEvents;
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
