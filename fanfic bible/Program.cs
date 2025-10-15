// Если что либо подчёркивается, то просто скачай через нугет эту фигню
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Dac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using fanfic_bible;

// Тут надо будет придумать как подключиться к бд
public class AppDbContext : DbContext
{
    public DbSet<book> books { get; set; }
    public DbSet<reader> readers { get; set; }
    public DbSet<author> authors { get; set; }
    public DbSet<genre> genres { get; set; }
    public DbSet<issuance_key> issuance_keys { get; set; }

    // Данные нужно менять в зависимости от сервера на компе
    static string connectionString = "Data Source=SUPERPC228;Initial Catalog=DB_NAME;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True";
    static string bacpacFilePath = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\\b.bacpac";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString);
            //var dacServices = new DacServices(connectionString);
            //dacServices.ImportBacpac(BacPackage.Load(bacpacFilePath), "DB_NAME", new DacImportOptions());
        }
    }
}

// ==================================
// Программа
// ==================================
class Program
{
    enum UserStatus
    {
            USER_DONT_EXIST = 0
        ,   USER_IS_AUTHOR  = 1
        ,   USER_IS_READER  = 2
    };
    static int CheckIfUserExists(string username, AppDbContext context)
    {
        if (context.authors
                    .Where(author => author.author_name == username)
                    .Select(author => author.author_name)
                    .Any())
        {
            return (int)UserStatus.USER_IS_AUTHOR;
        }
        if (context.readers
                    .Where(reader => reader.reader_name == username)
                    .Select(reader => reader.reader_name)
                    .Any())
        {
            return (int)UserStatus.USER_IS_READER;
        }
        return (int)UserStatus.USER_DONT_EXIST;
    }
    static void Main()
    {
        AppDbContext context = new AppDbContext();

        //Console.WriteLine(context.bacpacFilePath);

        context.Database.EnsureCreated();

// ==========================================
//      Интерфейс
// ==========================================
        Console.WriteLine("Добро пожаловать в Библиотеку!");

        string name = "";
        string password = "";

        bool logged_in = false;
        int user_status = 0;
        while (!logged_in)
        {
            Console.WriteLine("Введите своё имя пользователя и пароль чтобы войти:");
            name = Console.ReadLine();
            password = Console.ReadLine();

            user_status = CheckIfUserExists(name, context);
            if (user_status != (int)UserStatus.USER_DONT_EXIST)
            {
                logged_in = true;
            }
            Console.Clear();
        }

        Console.Clear();
        Console.WriteLine($"Добро божаловать {name}!");
        Console.ReadKey();
        Console.Clear();
        // ----------------------------------
        // Цикл программы
        // ----------------------------------
        bool exit = false;
        string user_input = "";
        if (user_status == (int)UserStatus.USER_IS_AUTHOR) {
            while (!exit)
            {
                Console.WriteLine("Введите, что вы хотите сделать:");
                Console.WriteLine("1: Посмотреть свои книги");
                Console.WriteLine("2: Добавить книгу");
                Console.WriteLine("3: Изменить книгу");
                Console.WriteLine("0: Выйти");

                user_input = Console.ReadLine();
                switch (user_input)
                {
                    case ("1"):
                        // код
                        break;
                    case ("2"):
                        // код
                        break;
                    case ("3"):
                        // код
                        break;
                    case ("0"):
                        exit = true;
                        break;
                }
            }
        }
        else if (user_status == (int)UserStatus.USER_IS_READER)
        {
            while (!exit)
            {
                Console.WriteLine("Введите, что вы хотите сделать:");
                Console.WriteLine("1: Посмотреть книги");
                Console.WriteLine("2: Взать книгу");
                Console.WriteLine("3: Вернуть книгу");
                Console.WriteLine("0: Выйти");

                user_input = Console.ReadLine();
                switch (user_input)
                {
                    case ("1"):
                        // код
                        break;
                    case ("2"):
                        // код
                        break;
                    case ("3"):
                        // код
                        break;
                    case ("0"):
                        exit = true;
                        break;
                }
            }
        }

        // ==========================================
        // ==========================================


        // ==========================================
        //       ПРИМЕРЫ
        // ==========================================
        // Заполнение таблиц данными
        // ----------------------------------
        /*
        context.Products.AddRange(
                new Product { Id = 1, Name = "Phone", Price = 800 },
                new Product { Id = 2, Name = "Laptop", Price = 1200 },
                new Product { Id = 3, Name = "Tablet", Price = 600 }
            );

        context.SaveChanges();

        // ----------------------------------
        // Добавление фигни
        // ----------------------------------
        Console.WriteLine("Продукты добавлены.");

        Console.WriteLine("Все продукты:");
        var products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Нахождение фигни
        // ----------------------------------
        Console.WriteLine("\nПоиск продукта по имени:");

        var product = context.Products.FirstOrDefault(p => p.Name == "Phone");

        if (product != null)
        {
            Console.WriteLine($"Найден продукт: {product.Name} - {product.Price}");
        }
        else
        {
            Console.WriteLine("Продукт не найден.");
        }

        // ----------------------------------
        // Фильтрация фигни
        // ----------------------------------
        Console.WriteLine("\nПродукты дороже 700:");

        var filteredProducts = context.Products.Where(p => p.Price > 700).ToList();

        foreach (var pr in filteredProducts)
        {
            Console.WriteLine($"{pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Обновление фигни
        // ----------------------------------
        Console.WriteLine("\nОбновление цены продукта:");

        product = context.Products.Find(1);

        if (product != null)
        {
            product.Price = 900;

            context.SaveChanges();

            Console.WriteLine($"Цена продукта {product.Name} обновлена до {900}.");
        }
        else
        {
            Console.WriteLine("Продукт для обновления не найден.");
        }

        products = context.Products.ToList();

        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Удаление фигни
        // ----------------------------------
        Console.WriteLine("\nУдаление продукта:");

        product = context.Products.Find(2);

        if (product != null)
        {
            context.Products.Remove(product);

            context.SaveChanges();

            Console.WriteLine($"Продукт {product.Name} удален.");
        }
        else
        {
            Console.WriteLine("Продукт для удаления не найден.");
        }

        products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Агрегатная фигня
        // ----------------------------------
        Console.WriteLine("\nСредняя цена всех продуктов:");

        var averagePrice = context.Products.Average(p => p.Price);
        Console.WriteLine($"Средняя цена: {averagePrice:F2}");

        // ----------------------------------
        // Отчистка фигни
        // ----------------------------------
        Console.WriteLine("\nОчистка таблицы:");

        context.Products.RemoveRange(context.Products);

        context.SaveChanges();

        Console.WriteLine("Таблица очищена.");

        products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        int[] nums = { 1, 2, 3, 4, 5 };

        var evenNums = nums.Where(x => x % 2 == 0);

        var evenNums2 = from x in nums
                        where x % 2 == 0
                        select x;

        string[] names = { "Loh", "Loh21", "Loh3" };

// ==========================================
//      БОЛЬШЕ ПРИМЕРОВ
// ==========================================
        var sortedNames = names.OrderBy(x => x.Length);

        var sortedNames3 = names.OrderByDescending(x => x.Length);

        foreach (var name in sortedNames)
        {
            Console.WriteLine(name);
        }

        var sortedNames2 = from name in names
                           orderby name.Length
                           select name;

        // ----------------------------------
        var squares = nums.Select(x => x * x);

        var squares2 = from x in squares
                       select x;

        // ----------------------------------
        int count = nums.Count();
        int sum = nums.Sum();
        int min = nums.Min();
        int max = nums.Max();
        double average = nums.Average();

        var result = nums
            .Where(x => x % 2 != 0)
            .OrderByDescending(x => x)
            .Select(x => x * x);

        // ----------------------------------
        var people = new List<Person>()
        {
            new Person("Loh", 25),
            new Person("Loh21", 21),
            new Person("Loh3", 69),
            new Person("Loh404", 25)
        };

        var groupedPeople = people.GroupBy(x => x.Age);

        foreach (var grouped in groupedPeople)
        {
            Console.WriteLine($"Years: {grouped.Key}");
            foreach (var person in grouped)
            {
                Console.WriteLine(person.Name);
            }
        }

        // ----------------------------------
        var departaments = new List<Departament>
        {
            new Departament(1, "HR"),
            new Departament(2, "IT")
        };

        var employees = new List<Employee>
        {
            new Employee(1, "Loh3"),
            new Employee(2, "Loh"),
            new Employee(1, "Loh21")
        };

        var employeeDepartaments = departaments.Join(
            employees,
            d => d.Id,
            e => e.DepartamentId,
            (d, e) => new { DepartamentName = d.Name, EmployeeName = e.Name }
            );

        foreach (var item in employeeDepartaments)
        {
            Console.WriteLine($"{item.EmployeeName} works in {item.DepartamentName}");
        }

        // ----------------------------------
        int[] nums1 = { 1, 2, 3, 4, 5 };
        int[] nums2 = { 4, 5, 6, 7, 8 };

        var nums3 = nums1.Concat(nums2);

        foreach (var i in nums3)
        {
            Console.WriteLine(i);
        }

        var nums4 = nums3.Distinct();

        foreach (var i in nums4)
        {
            Console.WriteLine(i);
        }

        var nums5 = nums1.Union(nums2);
        var nums6 = nums1.Intersect(nums2);
        var nums7 = nums1.Except(nums2);

        var nums8 = nums1.Skip(2).Take(2);
        var nums9 = nums1.SkipWhile(x => x < 2).TakeWhile(x => x < 4);
        */
    }
}
