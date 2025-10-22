using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class UnIssueBookTests
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

            _context.books.Add(new book { book_id = 1, book_name = "Test Book", book_amount = 0 });
            _context.issuance_keys.Add(new issuance_key { ik_id = 1, ik_book_id = 1, ik_closed = false });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void UnIssueBook_ShouldReturnTrueAndIncreaseAmount_WhenKeyExists()
        {
            // Act
            var result = _dbMiddleMan.un_issue_book(1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _context.books.Find(1).book_amount);
            Assert.IsTrue(_context.issuance_keys.Find(1).ik_closed);
        }

        [TestMethod]
        public void UnIssueBook_ShouldReturnFalse_WhenKeyDoesNotExist()
        {
            // Act
            var result = _dbMiddleMan.un_issue_book(2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
