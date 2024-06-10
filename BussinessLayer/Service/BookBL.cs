using BussinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL ibookRL;
        public BookBL(IBookRL ibookRL)
        {
            this.ibookRL = ibookRL;
        }

        //AddBook
        public object AddBook(BookModel model)
        {
            return ibookRL.AddBook(model);
        }

        //GetAllBooks
        public object GetAllBooks()
        {
            return ibookRL.GetAllBooks();
        }

        //UpdateBook
        public object UpdateBook(int bookId, BookModel model)
        {
            return ibookRL.UpdateBook(bookId, model);
        }

        //DeleteBook
        public object DeleteBook(int bookId)
        {
            return ibookRL.DeleteBook(bookId);
        }

        //GetBookByBookId
        public object GetBookByBookId(int bookId)
        {
            return ibookRL.GetBookByBookId(bookId);
        }

        //GetBookByName
        public object GetBookByName(string bookName, string authorName)
        {
            return ibookRL.GetBookByName(bookName, authorName);
        }
    }
}
