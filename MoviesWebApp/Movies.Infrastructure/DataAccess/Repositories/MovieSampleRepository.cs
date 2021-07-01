using Movies.Core.Entities;
using Movies.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.DataAccess.Repositories
{
    public class MovieSampleRepository : IMovieRepository
    {
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            var movies = new List<Movie>()
            {
                new Movie() { Id = 1, Title = "Titanic", Year = 1997 },
                new Movie() { Id = 2, Title = "Space Jam", Year = 1995 }
            };
            return movies;
        }
    }
}
