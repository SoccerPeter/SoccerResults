using System;
namespace SoccerResults.Models
{
    public class Events
    {
        public int Id { get; set; }
        public int? FixtureId { get; set; }
        public int? Elapsed { get; set; }
        public string Player { get; set; }
        public string Type { get; set; }
        public string Team { get; set; }
        public string Bild { get; set; }
    }
}
