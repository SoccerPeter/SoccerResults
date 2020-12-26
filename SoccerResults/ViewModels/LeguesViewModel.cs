using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using SoccerResults.Models;
using SoccerResults.Services;

namespace SoccerResults.ViewModels
{
    public class LeguesViewModel : BaseViewModel
    {
        private readonly ObservableRangeCollection<Grouping<string, Ligor>> _myItems = new ObservableRangeCollection<Grouping<string, Ligor>>();
        public ObservableCollection<Grouping<string, Ligor>> MyItems => _myItems;

        public LeguesViewModel()
        {
            Title = "Legues";
            LoadLigor();
        }

        public async Task LoadLigor()
        {

            var restService = new SoccerService();
            var L = await restService.GetLigor();
            var sorted = from item in L
                         orderby item.country
                         group item by item.country.ToString().ToUpperInvariant() into itemGroup
                         select new Grouping<string, Ligor>(itemGroup.Key, itemGroup);

            _myItems.ReplaceRange(sorted);
        }
    }
}
