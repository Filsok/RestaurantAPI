using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantsAPI.Entities;
using RestaurantsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantsAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        public IRestaurantService _restaurantService { get; }

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult ModifyById([FromRoute]int id, [FromBody] UpdateRestaurantDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (!_restaurantService.ModifyById(id,dto)) return NotFound();
            var ret = _restaurantService.GetById(id);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            if (!_restaurantService.Delete(id)) return NotFound();

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }


        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            if (restaurant == null) return NotFound();

            return Ok(restaurant);
        }
    }
}
