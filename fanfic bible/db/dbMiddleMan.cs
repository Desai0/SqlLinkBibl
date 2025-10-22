using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Data.SqlClient;

namespace fanfic_bible.db
{
    public class dbMiddleMan
    {
        AppDbContext db;

        public dbMiddleMan()
        {
            db = new AppDbContext();
            db.Database.EnsureCreated();
        }
        public dbMiddleMan(AppDbContext context) // Для тестов
        {
            db = context;
        }
        // ==================================
        //      Функции
        // ==================================
        // Тут Был Аким
        public bool UserIsInAuthors(string username)
        {
            var info = from author in db.authors
                       where author.author_name == username
                       select author;
            return info.Any();
        }
        public bool UserIsInReaders(string username)
        {
            var info = from reader in db.readers
                       where reader.reader_name == username
                       select reader;
            return info.Any();
        }

        public int GetAuthorIdByName(string username)
        {
            var info = from author in db.authors
                       where author.author_name == username
                       select author.author_id;
            return info.Sum();
        }
        public int GetReaderIdByName(string username)
        {
            var info = from reader in db.readers
                       where reader.reader_name == username
                       select reader.reader_id;
            return info.Sum();
        }
        // ----------------------------------
        //      Основные функции программы
        // ----------------------------------
        //      Автор
        // ------------------------------
        // 1. Посмотреть свои книги
        public List<book> GetAuthorsBooks(int user_id)
        {
            var info = from book in db.books
                       where book.book_author_id == user_id
                       select book;
            return info.ToList();
        }
        public void PrintAuthorsBooks(int user_id)
        {
            Console.WriteLine($"book id\tbook name\tgenre\tbook ammount");
            foreach (book? book in GetAuthorsBooks(user_id))
            {
                string genre = db.genres.Find(book.book_genre_id).genre_name;
                Console.WriteLine($"{book.book_id}\t{book.book_name}\t{genre}\t{book.book_amount}");
            }
        }

        // 2. Добавить книгу
        public bool AddBook(book new_book)
        {
            var info = from genre in db.genres
                       where genre.genre_id == new_book.book_genre_id
                       select genre;
            if (!info.Any())
            {
                return false;
            }
            else
            {
                db.books.Add(new_book);
                db.SaveChanges();
                return true;
            }
        }
        public void PrintAddBook(int author_id)
        {
            book new_book = new book();
            Console.WriteLine("Введите название новой книги:");
            new_book.book_name = Console.ReadLine();
            Console.WriteLine("Введите айди жанра новой книги:");
            int x = 0;
            if (Int32.TryParse(Console.ReadLine(), out x))
            {
                new_book.book_genre_id = x;
            }
            else
            {
                Console.WriteLine("Неверный ввод данных");
                return;
            }
            Console.WriteLine("Введите колличиство этих книг:");
            if (Int32.TryParse(Console.ReadLine(), out x))
            {
                new_book.book_amount = x;
            }
            else
            {
                Console.WriteLine("Неверный ввод данных");
                return;
            }
            new_book.book_author_id = author_id;
            if (AddBook(new_book))
            {
                Console.WriteLine("Книга добавлена");
            }
            else
            {
                Console.WriteLine("Id несуществующего жанра");
            }
        }

        // 3. Изменить книгу
        public void PrintEditBook(int author_id) // Большая функция TwT
        {
            Console.WriteLine("Введите Id книги которую хотите изменить:");
            int x;
            book? edit_book;
            if (Int32.TryParse(Console.ReadLine(), out x))
            {
                edit_book = db.books.Find(x);
                if (edit_book == null || edit_book.book_author_id != author_id)
                {
                    Console.WriteLine("У вас нет книги с таким id");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод данных");
                return;
            }
            Console.WriteLine($"Хотите поменять название книги ({edit_book.book_name})?[y/N]");
            string usr_choise = Console.ReadLine();
            if (usr_choise == "y")
            {
                Console.WriteLine("Введите новое название книги:");
                edit_book.book_name = Console.ReadLine();
            }
            Console.WriteLine($"Хотите поменять жанр книги ({edit_book.book_genre_id})?[y/N]");
            usr_choise = Console.ReadLine();
            if (usr_choise == "y")
            {
                Console.WriteLine("Введите новое id жанра книги:");
                int y;
                if (Int32.TryParse(Console.ReadLine(), out y))
                {
                    edit_book.book_genre_id = y;
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных");
                }
            }
            Console.WriteLine($"Хотите поменять кол-во книги ({edit_book.book_amount})?[y/N]");
            usr_choise = Console.ReadLine();
            if (usr_choise == "y")
            {
                Console.WriteLine("Введите новое кол-во книги:");
                int y;
                if (Int32.TryParse(Console.ReadLine(), out y))
                {
                    edit_book.book_amount = y;
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных");
                }
            } // Этого поидее достаточно, если верить https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/how-to-update-rows-in-the-database
            Console.WriteLine("Книга изменена");
        }

        // 4. Посмотреть жанры
        public List<genre> GetGenres()
        {
            var info = from genre in db.genres
                       select genre;
            return info.ToList();
        }
        public void PrintGenres()
        {
            Console.WriteLine($"genre id\tgenre name");
            foreach (genre? genre in GetGenres())
            {
                Console.WriteLine($"{genre.genre_id}\t{genre.genre_name}");
            }
        }


        // ------------------------------
        //      Читатель
        // ------------------------------
        // 1. Посмотреть книги
        // Лист Книг
        public List<book> get_books()
        {
            var info = from book in db.books
                       select book;
            return info.ToList();
        }
        public void PrintBooks()
        {
            Console.WriteLine($"book id\tbook name\tauthor\tgenre\tbook ammount");
            foreach (book? book in get_books())
            {
                string author = db.authors.Find(book.book_author_id).author_name;
                string genre = db.genres.Find(book.book_genre_id).genre_name;
                Console.WriteLine($"{book.book_id}\t{book.book_name}\t{author}\t{genre}\t{book.book_amount}");
            }
        }

        // 2. Взать книгу
        // Получение одной книги, возвращяет null если нет запрашиваемого id
        public book? get_book(int book_id)
        {
            book? bookInfo = db.books.Find(book_id);
            // ОКЕЙ ВОЗМОЖНЫ ЭТО ВСЁ ЖЕ БЫЛИ КОСТЫЛИ, НО КОММЕНТАРИИ СЛИШКОМ СМЕШНЫЕ ЧТОБЫ ИХ УДАЛЯТЬ
            // Фиксить лень

            return bookInfo;
        }

        // Выдача Книги
        public bool issue_book(int reader_id, int book_id)
        {
            //book issuedBook = db.books
            //    .Where(id => id.book_id == book_id)
            //    .ToList()[0]; //Видимо это всё же не костыль, но get_user комент слишко смешной чтобы его удалять

            // Заменяет верхнее потому что https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/how-to-update-rows-in-the-database
            book? issuedBook = (from book in db.books
                              where book.book_id == book_id
                              select book).FirstOrDefault(); //для работы теста

            if (issuedBook == null || issuedBook.book_amount <= 0)
            {
                return false;
            }
            issuedBook.book_amount--;

            issuance_key newKey = new issuance_key();

            newKey.ik_date = DateOnly.FromDateTime(DateTime.Now);
            newKey.ik_book_id = book_id;
            newKey.ik_reader_id = reader_id;
            newKey.ik_closed = false;

            db.issuance_keys.Add(newKey);
            db.SaveChanges();
            // BTW В linq to sql это должно выглядеть как:
            //db.issuance_keys.InsertOnSubmit(newKey);
            //db.SubmitChanges();
            //
            //Мы литтерали используем Entity Framework

            return true;

        }
        public void PrintTakeBook(int reader_id)
        {
            Console.WriteLine("Введите id книги, которую хотите взять:");
            int input_book_id;
            if (Int32.TryParse(Console.ReadLine(), out input_book_id))
            {
                book? takened_book = get_book(input_book_id); //инглиш
                if (takened_book == null)
                {
                    Console.WriteLine("Книга с таким id не найдена");
                }
                else
                {
                    if (issue_book(reader_id, input_book_id))
                    {
                        Console.WriteLine("Книга успешно взята");
                    }
                    {
                        Console.WriteLine("Не удалось взять книгу");
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод данных");
            }
        }

        // 3. Вернуть книгу
        // Возврат Книги (Закрытие Ключа)
        // Возвращает true при удачном закрытии, false иначе
        public bool un_issue_book(int ik_reader_id, int ik_book_id)
        {
            issuance_key? key = (from issuance_key in db.issuance_keys
                                 where issuance_key.ik_reader_id == ik_reader_id && issuance_key.ik_book_id == ik_book_id
                                 select issuance_key).FirstOrDefault();
            if (key == null)
            {
                return false;
            }

            book? bookInfo = db.books.Find(key.ik_book_id);

            if (bookInfo == null)
            {
                return false;
            }

            key.ik_closed = true;
            bookInfo.book_amount += 1;

            db.SaveChanges();
            return true;
        }
        public void PrintUnIssueBook(int ik_reader_id)
        {
            Console.WriteLine("Введи id книги которую нахрен хочешь сволочь?");
            int x;
            if (Int32.TryParse(Console.ReadLine(), out x))
            {
                if (un_issue_book(ik_reader_id, x) == true)
                {
                    Console.WriteLine("Книга была возвращена");
                }
                else
                {
                    Console.WriteLine("У вас нет такой книги");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод данных");
            }
        }

        // 4. Посмотреть взятые книги
        public void PrintTakenBooks(int ik_reader_id)
        {
            UserModel user = get_user(ik_reader_id);
            Console.WriteLine($"date\tis closed\tbook id");
            foreach (issuance_key? ik in user.issuances)
            {
                Console.WriteLine($"{ik.ik_id}\t{ik.ik_closed}\t{ik.ik_book_id}");
            }
            Console.WriteLine("Неверный ввод данных");
        }

        // Получение UserModel читателя
        public UserModel get_user(int user_id)
        {
            UserModel model = new UserModel();

            model.readerInfo = db.readers
                .Where(i => i.reader_id == user_id)
                .ToList()[0]; // ВОТ ЭТО КОСТЫЛЬ, а не эта всякая акимовская фигня

            model.issuances = db.issuance_keys
                .Where(ik => ik.ik_reader_id == user_id)
                .ToList();

            model.issuedBookIds = model.issuances
                .Select(i => i.ik_book_id)
                .ToList();

            return model;
        }
    }
}