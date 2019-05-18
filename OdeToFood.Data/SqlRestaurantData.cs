using System;
using System.Collections.Generic;
using System.Text;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
           return _db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetByid(id);
            if (restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetByid(int id)
        {
            return _db.Restaurants.Find(id);
        }

        public int GetCount()
        {
            return _db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in _db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
