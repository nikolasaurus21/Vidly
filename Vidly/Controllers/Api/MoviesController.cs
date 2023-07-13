using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
         private ApplicationDbContext _context;

        public MoviesController()
        {
            _context= new ApplicationDbContext();
        }


        //GET /api/movies
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies(string query = null) 
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        //GET /api/movies/1
        [HttpGet]
        public IHttpActionResult GetMovie(int id) 
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
               return NotFound();
            }

            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
                

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            //error popravljen na drugi nacin
            movie.DateAddedToDb = DateTime.Now;
            
            
            _context.Movies.Add(movie);
            
            _context.SaveChanges();
            

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }
        //PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id,MovieDto movieDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieDb = _context.Movies.SingleOrDefault(x => x.Id == id);

            if (movieDb == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movieDb);

            _context.SaveChanges();

            return Ok();
        }
        //DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieDb == null)
                return NotFound();

            _context.Movies.Remove(movieDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
