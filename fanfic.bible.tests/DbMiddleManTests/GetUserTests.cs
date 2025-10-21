using Microsoft.VisualStudio.TestTools.UnitTesting;
using fanfic_bible.db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace fanfic.bible.tests.DbMiddleManTests
{
    [TestClass]
    public class GetUserTests
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

            _context.readers.Add(new reader { reader_id = 1, reader_name = "Test Reader" });
            _context.issuance_keys.Add(new issuance_key { ik_id = 1, ik_reader_id = 1, ik_book_id = 1 });
            _context.issuance_keys.Add(new issuance_key { ik_id = 2, ik_reader_id = 1, ik_book_id = 2 });
            _context.SaveChanges();

            _dbMiddleMan = new dbMiddleMan(_context);
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUserModel()
        {
            // Act
            var result = _dbMiddleMan.get_user(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Reader", result.readerInfo.reader_name);
            Assert.AreEqual(2, result.issuances.Count);
            Assert.AreEqual(2, result.issuedBookIds.Count);
        }
    }
}
