using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class AddBookTests
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
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void AddBook_ShouldAddBook_WhenGenreExists()
        {
            // Arrange
            var newBook = new book { book_name = "New Book", book_genre_id = 1, book_author_id = 1, book_amount = 1 };

            // Act
            _dbMiddleMan.AddBook(newBook);
            
            // Assert
            Assert.AreEqual(1, _context.books.Count());
            Assert.AreEqual("New Book", _context.books.Single().book_name);
        }

        [TestMethod]
        public void AddBook_ShouldNotAddBook_WhenGenreDoesNotExist()
        {
            // Arrange
            var newBook = new book { book_name = "Another Book", book_genre_id = 2, book_author_id = 1, book_amount = 1 };

            // Act
            _dbMiddleMan.AddBook(newBook);

            // Assert
            Assert.AreEqual(0, _context.books.Count());
        }
    }
}
