using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Dac;

namespace fanfic_bible.db
{
    public class AppDbContext : DbContext
    {
        public DbSet<book> books { get; set; }
        public DbSet<reader> readers { get; set; }
        public DbSet<author> authors { get; set; }
        public DbSet<genre> genres { get; set; }
        public DbSet<issuance_key> issuance_keys { get; set; }

        // Данные нужно менять в зависимости от сервера на компе
        static string connectionString =
            "Data Source=SUPERPC228;" + // название локального сервера
            //"Initial Catalog=DB_NAME;" +
            //"Data Source=danems-thing\\SQLEXPRESS;" + // название локального сервера
            "Initial Catalog=womanko_reimagined;" + // 😎
            "Integrated Security=True;" +
            "Persist Security Info=false;" +
            "Pooling=False;" +
            "MultipleActiveResultSets=False;" + //Нафиг нам вот это? Вроде несколько запросов зв раз у нас не нужно держать? // Хз, пусть будет лол
            "Encrypt=True;" +
            "TrustServerCertificate=True";

        //static string connectionString = "Data Source=192.168.9.203\\sqlexpress;Initial Catalog=DB_NAME;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True";
        //static string bacpacFilePath = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\\b.bacpac";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
                //var dacServices = new DacServices(connectionString);
                //dacServices.ImportBacpac(BacPackage.Load(bacpacFilePath), "womanko_reimagined", new DacImportOptions());
            }
        }
    }
}
