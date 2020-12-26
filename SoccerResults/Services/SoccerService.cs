using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SoccerResults.Models;

namespace SoccerResults.Services
{
    public class SoccerService
    {
        public async Task<List<Games>> GetGames(string Datum)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync("https://apisoccerresults.azurewebsites.net/api/Games?Datum=" + Datum);
            return JsonConvert.DeserializeObject<List<Games>>(response);
        }

        public async Task<List<LeugesTables>> GetTable(string Liga)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync("https://coresoccerapi.azurewebsites.net/api/Table?Liga=" + Liga);
            return JsonConvert.DeserializeObject<List<LeugesTables>>(response);
        }

        public async Task<List<Ligor>> GetLigor()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync("https://apisoccerresults.azurewebsites.net/api/Ligor");
            return JsonConvert.DeserializeObject<List<Ligor>>(response);
        }

        public async Task<List<Events>> GetEvents(int id)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync("https://apisoccerresults.azurewebsites.net/api/Event/" + id.ToString());
            return JsonConvert.DeserializeObject<List<Events>>(response);
        }
    }
}
