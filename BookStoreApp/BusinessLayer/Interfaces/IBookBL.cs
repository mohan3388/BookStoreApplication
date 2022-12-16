using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);
        public List<BookPostModel> GetAllBooks();
        public BookPostModel UpdateBook(int BookID, BookPostModel bookPostModel);
        public int DeleteBook(int BookId);
    }
}
