using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

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
        
        // Лист Книг
        public List<book> get_books()
        {
            var info = from book in db.books
                       select book;
            return info.ToList();
        }

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
            book issuedBook = db.books
                .Where(id => id.book_id == book_id)
                .ToList()[0]; //Видимо это всё же не костыль, но get_user комент слишко смешной чтобы его удалять

            if (issuedBook == null || issuedBook.book_amount <= 0)
            {
                return false;
            }

            issuance_key newKey = new issuance_key();
            
            newKey.ik_date = DateOnly.FromDateTime(DateTime.Now);
            newKey.ik_book_id = book_id;
            newKey.ik_reader_id= reader_id;
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
