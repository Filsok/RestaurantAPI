using RestaurantsAPI.Entities;
using RestaurantsAPI.Models;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RestaurantsAPI
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool Delete(int id);
        bool ModifyById(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDBContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public bool ModifyById(int id, UpdateRestaurantDto dto)
        {
            _logger.LogInformation($"ModifyById() : Restaurant with id {id} modified.");
            var restaurant = _dbContext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if(restaurant is null) return false;

            restaurant.Name = String.IsNullOrEmpty(dto.Name) ? restaurant.Name : dto.Name;
            restaurant.Description = String.IsNullOrEmpty(dto.Description) ? restaurant.Description : dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            _logger.LogWarning($"Delete() : Restaurant with id: {id} DELETE action invoked");
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;

            _dbContext.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }

        public RestaurantDto GetById(int id)
        {
            _logger.LogTrace($"GetById() : Client asked about Restaurant with id {id}.");

            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return null;

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {

            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            _logger.LogTrace($"GetAll() : All {restaurantsDtos.Count} restaurants listed.");

            return restaurantsDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {

            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            _logger.LogInformation($"Create() : New Restaurant with id {restaurant.Id} created in database.");
            return restaurant.Id;
        }

    }
}