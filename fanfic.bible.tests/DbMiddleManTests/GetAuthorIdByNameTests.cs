using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class GetAuthorIdByNameTests
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

            _context.authors.Add(new author { author_id = 1, author_name = "TestAuthor" });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void GetAuthorIdByName_ShouldReturnCorrectId_WhenAuthorExists()
        {
            // Act
            var result = _dbMiddleMan.GetAuthorIdByName("TestAuthor");

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetAuthorIdByName_ShouldReturnZero_WhenAuthorDoesNotExist()
        {
            // Act
            var result = _dbMiddleMan.GetAuthorIdByName("NonExistentAuthor");

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
