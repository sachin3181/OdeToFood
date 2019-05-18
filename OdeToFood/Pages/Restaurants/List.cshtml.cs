using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        [TempData]
        public string DeleteMessage { get; set; }
        private readonly IRestaurantData _restaurantData;
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public string Message { get; set; }
        public IConfiguration Configuration { get; }
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration configuration, IRestaurantData restaurantData)
        {
            Configuration = configuration;
           _restaurantData = restaurantData;
        }
        public void OnGet()
        {
            
            Message = Configuration["Message"];
            Restaurants = _restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}