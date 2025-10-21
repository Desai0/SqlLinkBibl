using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class GetBooksTests
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

            _context.books.Add(new book { book_id = 1, book_name = "Book 1" });
            _context.books.Add(new book { book_id = 2, book_name = "Book 2" });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void GetBooks_ShouldReturnAllBooks()
        {
            // Act
            var result = _dbMiddleMan.get_books();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
