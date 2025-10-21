using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class GetGenresTests
    {
        private DbContextOptions<AppDbContext> _options;
        private AppDbContext _context;
        private dbMiddleMan _dbMiddleMan;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.genres.Add(new genre { genre_id = 1, genre_name = "Fantasy" });
            _context.genres.Add(new genre { genre_id = 2, genre_name = "Sci-Fi" });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void GetGenres_ShouldReturnAllGenres()
        {
            // Act
            var result = _dbMiddleMan.GetGenres();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
