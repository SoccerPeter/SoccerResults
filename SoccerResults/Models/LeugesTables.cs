using System;
namespace SoccerResults.Models
{
    public class LeugesTables
    {
        public int id { get; set; }
        public string liga { get; set; }
        public string team { get; set; }
        public int played { get; set; }
        public int playedAtHome { get; set; }
        public int playedAway { get; set; }
        public int won { get; set; }
        public int draw { get; set; }
        public int lost { get; set; }
        public int goalsFor { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
        public int points { get; set; }
    }
}
