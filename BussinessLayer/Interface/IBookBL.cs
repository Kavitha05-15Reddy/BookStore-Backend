using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IBookBL
    {
        public object AddBook(BookModel model);
        public object GetAllBooks();
        public object UpdateBook(int bookId, BookModel model);
        public object DeleteBook(int bookId);
        public object GetBookByBookId(int bookId);
        public object GetBookByName(string bookName, string authorName);
    }
}
