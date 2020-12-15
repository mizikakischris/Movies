using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie.Repository;
using Movie.Repository.Data;

namespace Movie.UnitTests
{
    [TestClass]
    public class MovieTest
    {
        private readonly Depedencies _depedencies;

        public MovieTest()
        {
            _depedencies = new Depedencies();
            _depedencies.Setup();

        }
        [TestMethod]
        public void GetAllMovies_Test()
        {
            using (var appDbContext = new AppDbContext(_depedencies.Options)) // <-- Pass the options here
            {
                MovieModelRepository modelRepo = new MovieModelRepository(appDbContext);
                var movies = modelRepo.GetMovies();

                Assert.IsNotNull(movies);
                Assert.IsTrue(movies.Count > 0);
            }

        }

    }
}
