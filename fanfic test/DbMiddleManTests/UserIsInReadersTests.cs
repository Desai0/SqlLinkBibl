using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using fanfic_bible;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class UserIsInReadersTests
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

            _context.readers.Add(new reader { reader_name = "TestReader" });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void UserIsInReaders_ShouldReturnTrue_WhenReaderExists()
        {
            // Act
            var result = _dbMiddleMan.UserIsInReaders("TestReader");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserIsInReaders_ShouldReturnFalse_WhenReaderDoesNotExist()
        {
            // Act
            var result = _dbMiddleMan.UserIsInReaders("NonExistentReader");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
