using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class GetAuthorsBooksTests
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

            _context.books.Add(new book { book_id = 1, book_name = "Book 1", book_author_id = 1 });
            _context.books.Add(new book { book_id = 2, book_name = "Book 2", book_author_id = 1 });
            _context.books.Add(new book { book_id = 3, book_name = "Book 3", book_author_id = 2 });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void GetAuthorsBooks_ShouldReturnCorrectBooks_WhenAuthorHasBooks()
        {
            // Act
            var result = _dbMiddleMan.GetAuthorsBooks(1);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(b => b.book_author_id == 1));
        }

        [TestMethod]
        public void GetAuthorsBooks_ShouldReturnEmptyList_WhenAuthorHasNoBooks()
        {
            // Act
            var result = _dbMiddleMan.GetAuthorsBooks(3);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
