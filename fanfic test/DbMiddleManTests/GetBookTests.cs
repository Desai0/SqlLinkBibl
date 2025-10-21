using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class GetBookTests
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

            _context.books.Add(new book { book_id = 1, book_name = "Test Book" });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void GetBook_ShouldReturnBook_WhenBookExists()
        {
            // Act
            var result = _dbMiddleMan.get_book(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Book", result.book_name);
        }

        [TestMethod]

        public void GetBook_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Act
            var result = _dbMiddleMan.get_book(2);

            // Assert
            Assert.IsNull(result);
        }
    }
}
