using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace fanfic_bible
{
    [Table(Name = "BOOKS")]
    public class book
    {
        [Key]
        [Column(IsPrimaryKey = true, IsIdentity = true, Name = "book_id", SkipOnInsert = true)]
        public int book_id { get;  set; }
        [Column(Name = "book_name")]
        public string book_name { get; set; }
        [Column(Name = "book_amount")]
        public int book_amount { get; set; }
        [Column(Name = "book_author_id")]
        public int book_author_id { get; set; }
        [Column(Name = "book_genre_id")]
        public int book_genre_id { get; set; }
    }

    [Table(Name = "READERS")]
    public class reader
    {
        //public reader(int id, string name, string birth, bool active)
        //{
        //    this.reader_id = id;
        //    this.reader_birth_date = birth;
        //    this.reader_active = active;
        //    this.reader_name = name;
        //}
        // Лучше не использовать

        [Key]
        [Column(IsPrimaryKey = true, IsIdentity = true, Name = "reader_id", SkipOnInsert = true)]
        public int reader_id { get;  set; }
        [Column(Name = "reader_name")]
        public string reader_name { get; set; }
        [Column(Name = "reader_birth_date")]
        public DateOnly reader_birth_date { get; set; } // Unsure about the typing
        [Column(Name = "reader_active")]
        public bool reader_active { get; set; }

    }

    [Table(Name = "AUTHORS")]
    public class author
    {
        [Key]
        [Column(IsPrimaryKey = true, IsIdentity = true, Name = "author_id", SkipOnInsert = true)]
        public int author_id { get;  set; }
        [Column(Name = "author_name")]
        public string author_name { get; set; }
        [Column(Name = "author_birth_date")]
        public DateOnly author_birth_date { get; set; }
    }

    [Table(Name = "GENRES")]
    public class genre
    {
        [Key]
        [Column(IsPrimaryKey = true, IsIdentity = true, Name = "genre_id", SkipOnInsert = true)]
        public int genre_id { get;  set; }
        [Column(Name = "genre_name")]
        public string genre_name { get; set; }
    }

    [Table(Name = "ISSUANCE_KEY")]
    public class issuance_key
    {
        [Key]
        [Column(IsPrimaryKey = true, IsIdentity = true, Name = "ik_id", SkipOnInsert = true)]
        public int ik_id { get;  set; }
        [Column(Name = "ik_date")]
        public DateOnly ik_date { get; set; }
        [Column(Name = "ik_closed")]
        public bool ik_closed { get; set; }
        [Column(Name = "ik_reader_id")]
        public int ik_reader_id { get; set; }
        [Column(Name = "ik_book_id")]
        public int ik_book_id { get; set; }


    }
}
