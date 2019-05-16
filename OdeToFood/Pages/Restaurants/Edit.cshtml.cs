using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
        }
        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = _restaurantData.GetByid(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
                
            }
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                _restaurantData.Update(Restaurant);
                TempData["Message"] = "Restaurant updated!";
            }
            else
            {
                _restaurantData.Add(Restaurant);
                TempData["Message"] = "Restaurant added!";
            }
            
            _restaurantData.Commit();
           
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });


        }
    }
}