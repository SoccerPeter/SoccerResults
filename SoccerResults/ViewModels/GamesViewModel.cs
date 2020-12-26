using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmHelpers;
using SoccerResults.Models;
using SoccerResults.Services;
using Xamarin.Forms;
using System.Timers;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;

namespace SoccerResults.ViewModels
{
    public class GamesViewModel: BaseViewModel
    {
        public ICommand GetTablesCommand { get; private set; }
        public Command GetGames { get; set; }
        public Command BackOneDay { get; set; }
        public Command ForwardOneDay { get; set; }
        private readonly ObservableRangeCollection<Grouping<string, Games>> _myItems = new ObservableRangeCollection<Grouping<string, Games>>();
        public ObservableCollection<Grouping<string, Games>> MyItems => _myItems;
        public string gameDate { get; set; }
        public string toDay { get; set; }
        public bool IsRefreshing { get; private set; }
        private Timer timer;

        public GamesViewModel()
        {
            Title = "SocerResults";
            DateTime D = DateTime.Now;
            toDay = D.ToString("yyyy-MM-dd");
            //SoccerDay = toDay;
            gameDate = D.ToString("yyyy-MM-dd");
            GetGames = new Command(getGames);
            GetTablesCommand = new Command<string>(GetTables);
            BackOneDay = new Command(ManuallyDateForward);
            ForwardOneDay = new Command(ManuallyDateBackward);

            timer = new Timer();
            timer.Interval = 60000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void ManuallyDateBackward(object obj)
        {
            DateTime D = DateTime.Parse(SoccerDay);
            DateTime DD = D.AddDays(-1);
            SoccerDay = DD.ToString("yyyy-MM-dd");
            _ = LoadGames(SoccerDay);
        }

        private void ManuallyDateForward()
        {
            DateTime D = DateTime.Parse(SoccerDay);
            DateTime DD = D.AddDays(1);
            SoccerDay = DD.ToString("yyyy-MM-dd");
            _ = LoadGames(SoccerDay);
        }

        private void GetTables(string value)
        {
            string s = value;
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (toDay == SoccerDay)
            {
                IsBusy = true;
                await LoadGames(gameDate);
                UpdatedTime = "Updated:" + DateTime.Now.ToString("HH:mm:ss");
                IsBusy = false;
            }

        }

        private string _updatedTime;

        public string UpdatedTime
        {
            get { return _updatedTime; }
            set { SetProperty(ref _updatedTime, value); }

        }


        private string _soccerDay;
        public string SoccerDay
        {
            get { return _soccerDay; }
            set
            {
                _soccerDay = value;
                SetProperty(ref _soccerDay, value);
                OnPropertyChanged(nameof(_soccerDay));
            }
        }

        public async Task LoadGames(string Datum)
        {

            var restService = new SoccerService();
            var GamesToDay = await restService.GetGames(Datum);
            var Fix = FixGames(GamesToDay);
            var sorted = from item in Fix
                         orderby item.league
                         group item by item.league.ToString().ToUpperInvariant() into itemGroup
                         select new Grouping<string, Games>(itemGroup.Key, itemGroup);

            _myItems.ReplaceRange(sorted);
            IsRefreshing = false;
            OnPropertyChanged("IsRefreshing");
            SoccerDay = Datum;
        }

        private void getGames(object obj)
        {
            LoadGames(SoccerDay);
        }

        private List<Games> FixGames(List<Games> G)
        {
            List<Games> fixedList = new List<Games>();
            TimeZone localZone = TimeZone.CurrentTimeZone;
            var timeDiff = localZone.GetUtcOffset(DateTime.Now);
            foreach (var game in G)
            {
                try
                {
                    Games g = new Games();
                    g.awayGoalDetails = String.IsNullOrEmpty(game.awayGoalDetails) ? string.Empty : game.awayGoalDetails.Trim();
                    g.awayGoals = game.awayGoals;
                    g.awayLineupDefense = String.IsNullOrEmpty(game.awayLineupDefense) ? string.Empty : game.awayLineupDefense.Trim();
                    g.awayLineupForward = String.IsNullOrEmpty(game.awayLineupForward) ? string.Empty : game.awayLineupForward.Trim();
                    g.awayLineupGoalkeeper = String.IsNullOrEmpty(game.homeLineupGoalkeeper) ? string.Empty : game.homeLineupGoalkeeper.Trim();
                    g.awayLineupMidfield = String.IsNullOrEmpty(game.awayLineupMidfield) ? string.Empty : game.awayLineupMidfield.Trim();
                    g.awayLineupSubstitutes = String.IsNullOrEmpty(game.awayLineupSubstitutes) ? string.Empty : game.awayLineupSubstitutes.Trim();
                    g.awayLineupSubstitutes = g.awayLineupSubstitutes.Replace(";", string.Empty);
                    g.awayTeam = String.IsNullOrEmpty(game.awayTeam) ? string.Empty : game.awayTeam.Trim();
                    g.awayTeamGoalLabel = g.awayTeam + " Scorers";
                    g.awayTeamId = game.awayTeamId;
                    g.awayTeamRedCardDetails = String.IsNullOrEmpty(game.awayTeamRedCardDetails) ? string.Empty : game.awayTeamRedCardDetails.Trim();
                    g.awayTeamYellowCardDetails = String.IsNullOrEmpty(game.awayTeamYellowCardDetails) ? string.Empty : game.awayTeamYellowCardDetails.Trim();
                    g.date = game.date;
                    g.gameStart = game.date.ToShortTimeString();
                    //g.gameStart = game.date.AddHours(timeDiff.Hours).ToShortTimeString();
                    g.homeGoalDetails = String.IsNullOrEmpty(game.homeGoalDetails) ? string.Empty : game.homeGoalDetails.Trim();

                    g.homeGoals = game.homeGoals;
                    g.homeLineupDefense = String.IsNullOrEmpty(game.homeLineupDefense) ? string.Empty : game.homeLineupDefense.Trim();
                    g.homeLineupForward = String.IsNullOrEmpty(game.homeLineupForward) ? string.Empty : game.homeLineupForward.Trim();
                    g.homeLineupGoalkeeper = String.IsNullOrEmpty(game.awayLineupGoalkeeper) ? string.Empty : game.awayLineupGoalkeeper.Trim();
                    g.homeLineupMidfield = String.IsNullOrEmpty(game.homeLineupMidfield) ? string.Empty : game.homeLineupMidfield.Trim();
                    g.homeLineupSubstitutes = String.IsNullOrEmpty(game.homeLineupSubstitutes) ? string.Empty : game.homeLineupSubstitutes.Trim();
                    g.homeTeam = game?.homeTeam.Trim();
                    g.homeTeamGoalLabel = g.homeTeam + " Scorers";
                    g.homeTeamId = game.homeTeamId;
                    g.homeTeamRedCardDetails = String.IsNullOrEmpty(game.homeTeamRedCardDetails) ? string.Empty : game.homeTeamRedCardDetails.Trim();
                    g.homeTeamYellowCardDetails = String.IsNullOrEmpty(game.homeTeamYellowCardDetails) ? string.Empty : game.homeTeamYellowCardDetails.Trim();
                    g.id = game.id;
                    g.idnr = game.idnr;
                    g.league = String.IsNullOrEmpty(game.league) ? string.Empty : game.league.Trim();
                    g.location = String.IsNullOrEmpty(game.location) ? string.Empty : game.location.Trim();
                    g.referee = String.IsNullOrEmpty(game.referee) ? string.Empty : game.referee.Trim();
                    g.round = String.IsNullOrEmpty(game.round) ? string.Empty : game.round.Trim();
                    g.time = String.IsNullOrEmpty(game.time) ? string.Empty : game.time.Trim();
                    fixedList.Add(g);
                }
                catch (Exception e)
                {
                    var ee = e;
                }
            }
            return fixedList;
        }
    }
}
