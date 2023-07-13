using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]

        public IHttpActionResult NewRental(RentalDTO rentalDTO)
        {
            var customer = _context.Customers.First(x => x.Id == rentalDTO.CustomerId);

            /*if (customer == null)
            {
                return BadRequest();
            }*/

            var movies = _context.Movies.Where(x => rentalDTO.MovieIds.Contains(x.Id)).ToList();

            /*if(movies.Count == 0)
            {
                return NotFound();
            }*/

            foreach (var movie in movies)
            {
                if(movie.NumberAvailable == 0)
                {
                    return BadRequest();
                }

                movie.NumberAvailable--;

                var rent = new Rental 
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented= DateTime.Now,
                };   

                _context.Rentals.Add(rent);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
