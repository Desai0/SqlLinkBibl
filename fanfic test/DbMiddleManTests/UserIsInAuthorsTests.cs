using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class UserIsInAuthorsTests
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

            _context.authors.Add(new author { author_name = "TestAuthor" });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void UserIsInAuthors_ShouldReturnTrue_WhenAuthorExists()
        {
            // Act
            var result = _dbMiddleMan.UserIsInAuthors("TestAuthor");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserIsInAuthors_ShouldReturnFalse_WhenAuthorDoesNotExist()
        {
            // Act
            var result = _dbMiddleMan.UserIsInAuthors("NonExistentAuthor");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
