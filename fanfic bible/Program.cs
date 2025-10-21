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
using fanfic_bible.db;
using static fanfic_bible.db.dbMiddleMan;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;
using fanfic_bible.db;

<<<<<<< HEAD
/*public class AppDbContext : DbContext
{
    public DbSet<book> books { get; set; }
    public DbSet<reader> readers { get; set; }
    public DbSet<author> authors { get; set; }
    public DbSet<genre> genres { get; set; }
    public DbSet<issuance_key> issuance_keys { get; set; }

    // Данные нужно менять в зависимости от сервера на компе
    static string connectionString = "Data Source=SUPERPC228;Initial Catalog=DB_NAME;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True";
    //static string connectionString = "Data Source=192.168.9.203\\sqlexpress;Initial Catalog=DB_NAME;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True";
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
}*/// На память
=======
// Тут надо будет придумать как подключиться к бд

>>>>>>> Доделал пропущенные части middle man, теперб количество книг уменьшается и добавленна функция закрытия ключа.

// ==================================
// Программа
// ==================================
class Program
{
    static dbMiddleMan context = new dbMiddleMan();
    enum UserStatus
    {
            USER_DONT_EXIST = 0
        ,   USER_IS_AUTHOR  = 1
        ,   USER_IS_READER  = 2
    };

    static int CheckIfUserExists(string username)
=======
    static int CheckIfUserExists(string username, AppDbContext context, ref int user_id)
>>>>>>> Доделал пропущенные части middle man, теперб количество книг уменьшается и добавленна функция закрытия ключа.
    {
        int ret_code = (int)UserStatus.USER_DONT_EXIST;

        if (context.UserIsInAuthors(username))
        {
            ret_code = (int)UserStatus.USER_IS_AUTHOR;
        }

        if (context.UserIsInReaders(username))
        {
            ret_code = (int)UserStatus.USER_IS_READER;
        }
        return ret_code;
    }
// ----------------------------------
//      Main
// ----------------------------------
    static void Main()
    {
        fanfic.bible.tests.TestRunner.RunAllTests();
        Console.WriteLine("All tests passed. Starting the application.");
        Console.ReadKey();
        Console.Clear();
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

<<<<<<< HEAD
            user_status = CheckIfUserExists(name);
            if (user_status != (int)UserStatus.USER_DONT_EXIST)
            {
                logged_in = true;
            }
            Console.Clear();
        }

        Console.Clear();
        Console.WriteLine($"Добро пожаловать {name}!");
        Console.ReadKey();
        Console.Clear();

// ----------------------------------
//      Цикл программы
// ----------------------------------
        bool exit = false;
        if (user_status == (int)UserStatus.USER_IS_AUTHOR)
        {
            int user_id = context.GetAuthorIdByName(name);
            string user_input = "";
            while (!exit)
            {
                Console.WriteLine("Введите, что вы хотите сделать:");
                Console.WriteLine("1: Посмотреть свои книги");
                Console.WriteLine("2: Добавить книгу");
                Console.WriteLine("3: Изменить книгу");
                Console.WriteLine("4: Посмотреть жанры"); // Было решено добавить новую функцию иначе авторы бы тупо не знали названия жанров
                Console.WriteLine("0: Выйти");

                user_input = Console.ReadLine();
                switch (user_input)
                {
                    case ("1"):
                        context.PrintAuthorsBooks(user_id);
                        Console.ReadKey();
                        break;
                    case ("2"):
                        context.PrintAddBook(user_id);
                        Console.ReadKey();
                        break;
                    case ("3"):
                        context.PrintEditBook(user_id);
                        Console.ReadKey();
                        break;
                    case ("4"):
                        context.PrintGenres();
                        Console.ReadKey();
                        break;
                    case ("0"):
                        exit = true;
                        break;
                }
                Console.Clear();
            }
        }
        else if (user_status == (int)UserStatus.USER_IS_READER)
        {
            int user_id = context.GetReaderIdByName(name);
            string user_input = "";
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
                        context.PrintBooks();
                        Console.ReadKey();
                        break;
                    case ("2"):
                        context.PrintTakeBook(user_id);
                        Console.ReadKey();
                        break;
                    case ("3"):
                        // код
                        break;
                    case ("0"):
                        exit = true;
                        break;
                }
                Console.Clear();
            }
        }

// ==========================================
// ==========================================
=======
        man.issue_book(5, 2);


        //    AppDbContext context = new AppDbContext();

        //    //Console.WriteLine(context.bacpacFilePath);
>>>>>>> Доделал пропущенные части middle man, теперб количество книг уменьшается и добавленна функция закрытия ключа.

        //    context.Database.EnsureCreated();

        // ==========================================
        //      Интерфейс
        // ==========================================
        //    Console.WriteLine("Добро пожаловать в Библиотеку!");

        //    string name = "";
        //    string password = "";
        //    int user_id = -1;

        //    bool logged_in = false;
        //    int user_status = 0;
        //    while (!logged_in)
        //    {
        //        Console.WriteLine("Введите своё имя пользователя и пароль чтобы войти:");
        //        name = Console.ReadLine();
        //        password = Console.ReadLine();

        //        user_status = CheckIfUserExists(name, context, ref user_id);
        //        if (user_status != (int)UserStatus.USER_DONT_EXIST)
        //        {
        //            logged_in = true;
        //        }
        //        Console.Clear();
        //    }

        //    Console.Clear();
        //    Console.WriteLine($"Добро божаловать {name}!");
        //    Console.ReadKey();
        //    Console.Clear();
        //    // ----------------------------------
        //    // Цикл программы
        //    // ----------------------------------
        //    bool exit = false;
        //    string user_input = "";
        //    if (user_status == (int)UserStatus.USER_IS_AUTHOR) {
        //        while (!exit)
        //        {
        //            Console.WriteLine("Введите, что вы хотите сделать:");
        //            Console.WriteLine("1: Посмотреть свои книги");
        //            Console.WriteLine("2: Добавить книгу");
        //            Console.WriteLine("3: Изменить книгу");
        //            Console.WriteLine("0: Выйти");

        //            user_input = Console.ReadLine();
        //            switch (user_input)
        //            {
        //                case ("1"):
        //                    // код
        //                    break;
        //                case ("2"):
        //                    // код
        //                    break;
        //                case ("3"):
        //                    // код
        //                    break;
        //                case ("0"):
        //                    exit = true;
        //                    break;
        //            }
        //        }
        //    }
        //    else if (user_status == (int)UserStatus.USER_IS_READER)
        //    {
        //        while (!exit)
        //        {
        //            Console.WriteLine("Введите, что вы хотите сделать:");
        //            Console.WriteLine("1: Посмотреть книги");
        //            Console.WriteLine("2: Взать книгу");
        //            Console.WriteLine("3: Вернуть книгу");
        //            Console.WriteLine("0: Выйти");

        //            user_input = Console.ReadLine();
        //            switch (user_input)
        //            {
        //                case ("1"):
        //                    // код
        //                    break;
        //                case ("2"):
        //                    // код
        //                    break;
        //                case ("3"):
        //                    // код
        //                    break;
        //                case ("0"):
        //                    exit = true;
        //                    break;
        //            }
        //        }
        //    }
        //}
    }
}
