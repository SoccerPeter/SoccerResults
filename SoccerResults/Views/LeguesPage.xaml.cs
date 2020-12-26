using System;
using System.Collections.Generic;
using SoccerResults.ViewModels;
using Xamarin.Forms;

namespace SoccerResults.Views
{
    public partial class LeguesPage : ContentPage
    {
        private LeguesViewModel _leguesViewModel;

        public LeguesPage()
        {
            InitializeComponent();
            BindingContext = _leguesViewModel = new LeguesViewModel();
        }
    }
}
