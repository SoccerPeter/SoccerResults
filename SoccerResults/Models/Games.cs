using System;
namespace SoccerResults.Models
{
    public class Games
    {
        public int idnr { get; set; }
        public int id { get; set; }
        public DateTime date { get; set; }
        public string league { get; set; }
        public string round { get; set; }
        public string homeTeam { get; set; }
        public int homeTeamId { get; set; }
        public string awayTeam { get; set; }
        public string awayTeamId { get; set; }
        public string location { get; set; }
        public int homeGoals { get; set; }
        public int awayGoals { get; set; }
        public string time { get; set; }
        public string homeTeamYellowCardDetails { get; set; }
        public string awayTeamYellowCardDetails { get; set; }
        public string homeTeamRedCardDetails { get; set; }
        public string awayTeamRedCardDetails { get; set; }
        public string homeLineupGoalkeeper { get; set; }
        public string awayLineupGoalkeeper { get; set; }
        public string homeLineupDefense { get; set; }
        public string awayLineupDefense { get; set; }
        public string homeLineupMidfield { get; set; }
        public string awayLineupMidfield { get; set; }
        public string homeLineupForward { get; set; }
        public string awayLineupForward { get; set; }
        public string homeLineupSubstitutes { get; set; }
        public string awayLineupSubstitutes { get; set; }
        public string homeGoalDetails { get; set; }
        public string awayGoalDetails { get; set; }
        public string referee { get; set; }
        public string gameStart { get; set; }
        public string homeTeamGoalLabel { get; set; }
        public string awayTeamGoalLabel { get; set; }
    }
}
