using Microsoft.EntityFrameworkCore;
using Movies.Core.Constants;
using Movies.Core.Entities;
using Movies.Infrastructure.DataAccess;
using Movies.Infrastructure.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Movies.UnitTests.Infrastructure.Repositories
{
    public class MovieRepositoryOperations
    {
        private readonly DbContextOptions<ApplicationDbContext> options = 
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieDbInMemory")
            .Options;

        private ApplicationDbContext dbContext { get; set; }

        [Fact]
        public async Task GetAllMoviesTest()
        {
            //Arrange
            dbContext = new ApplicationDbContext(options);

            dbContext.Movies.Add(new Movie { Id = 1, Title = "Movie 1", Year = 2018, Rating = 20, Satisfaction = SatisfactionEnum.Terrible });
            dbContext.Movies.Add(new Movie { Id = 2, Title = "Movie 2", Year = 2019, Rating = 60, Satisfaction = SatisfactionEnum.Normal });
            dbContext.Movies.Add(new Movie { Id = 3, Title = "Movie 3", Year = 2020, Rating = 100, Satisfaction = SatisfactionEnum.Excelente });
            dbContext.SaveChanges();

            MovieRepository movieRepository = new MovieRepository(dbContext);

            //Act
            var movies = await movieRepository.GetMoviesAsync(null);

            //Assert
            Assert.Equal(3, movies.ToList().Count);
        }
    }
}
