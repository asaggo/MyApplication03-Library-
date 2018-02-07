using MyApplication03_Library_.Models;
using System.Data.Entity;

namespace MyApplication03_Library_.Context
{   //sql server와 데이터 연동을 돕는 클래스
    public class LibraryDB : DbContext
    {
        //                         Web.config 안에 tag name = DBCS인걸 찾아서 
        //                         그 tag 내용을 다 들고온다.
        public LibraryDB() : base("name=DBCS"){ } // <-constructor

        public DbSet<Book> Books { get; set; }
    }
}