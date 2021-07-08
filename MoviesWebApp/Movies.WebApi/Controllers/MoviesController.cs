using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Infrastructure.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync(string title)
        {
            Movie entity = null;
            if (!string.IsNullOrEmpty(title))
            {
                entity = new Movie
                {
                    Title = title,
                };
            }
            var movies = await _movieRepository.GetMoviesAsync(entity);
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMoviesAsync(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync(Movie movie)
        {
            if (movie == null || string.IsNullOrEmpty(movie.Title))
            {
                return BadRequest("Movie object is null");
            }
            var result = await _movieRepository.AddMovieAsync(movie);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("Movie Id is different");
            }
            var result = await _movieRepository.UpdateMovieAsync(movie);
            if (result == null)
            {
                return NotFound("");
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var result = await _movieRepository.DeleteMovieAsync(id);
            if (result == null)
            {
                return NotFound($"Movie with Id = {id} not found");
            }
            return Ok($"Movie with Id = {id} was deleted");
        }
    }
}
