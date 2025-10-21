using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class IssueBookTests
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

            _context.books.Add(new book { book_id = 1, book_name = "Test Book", book_amount = 1 });
            _context.books.Add(new book { book_id = 2, book_name = "Empty Book", book_amount = 0 });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void IssueBook_ShouldReturnTrueAndDecreaseAmount_WhenBookIsAvailable()
        {
            // Act
            var result = _dbMiddleMan.issue_book(1, 1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.books.Find(1).book_amount);
            Assert.AreEqual(1, _context.issuance_keys.Count());
        }

        [TestMethod]
        public void IssueBook_ShouldReturnFalse_WhenBookIsNotAvailable()
        {
            // Act
            var result = _dbMiddleMan.issue_book(1, 2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IssueBook_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Act
            var result = _dbMiddleMan.issue_book(1, 3);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
