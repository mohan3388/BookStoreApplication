using CommonLayer.BookModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL:IBookRL
    {
        private readonly string connetionString;
        public BookRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }
        string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True";
        public BookPostModel AddBook(BookPostModel bookPostModel)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                SqlCommand cmd = new SqlCommand("spAddBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookName", bookPostModel.BookName);
                cmd.Parameters.AddWithValue("@Description", bookPostModel.Description);
                cmd.Parameters.AddWithValue("@AuthorName", bookPostModel.AuthorName);
                cmd.Parameters.AddWithValue("@ActualPrice", bookPostModel.ActualPrice);
                cmd.Parameters.AddWithValue("@DiscountedPrice", bookPostModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@Quantity", bookPostModel.Quantity);
                cmd.Parameters.AddWithValue("@BookImage", bookPostModel.BookImage);
                cmd.Parameters.AddWithValue("@TotalRating", bookPostModel.TotalRating);
                cmd.Parameters.AddWithValue("@CountRating", bookPostModel.RatingCount);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return bookPostModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BookPostModel> GetAllBooks()
        {
            List<BookPostModel> bookPostModels = new List<BookPostModel>();
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAllBooks", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BookPostModel getBook = new BookPostModel();

                    getBook.BookId = Convert.ToInt32(rd["BookId"]);
                    getBook.BookName = Convert.ToString(rd["BookName"]);
                    getBook.AuthorName = Convert.ToString(rd["AuthorName"]);
                    getBook.Description = Convert.ToString(rd["Description"]);
                    getBook.ActualPrice = Convert.ToDouble(rd["ActualPrice"]);
                    getBook.DiscountedPrice = Convert.ToDouble(rd["DiscountedPrice"]);
                    getBook.Quantity = Convert.ToInt32(rd["Quantity"]);
                    getBook.BookImage = Convert.ToString(rd["BookImage"]);
                    getBook.TotalRating = Convert.ToInt32(rd["TotalRating"]);
                    getBook.RatingCount = Convert.ToInt32(rd["CountRating"]);
                    bookPostModels.Add(getBook);
                }
                connection.Close();
                if (bookPostModels.Count != 0)
                {
                    return bookPostModels;
                }
                else
                {
                    return null;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookPostModel UpdateBook(int BookID, BookPostModel bookPostModel)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", BookID);
                cmd.Parameters.AddWithValue("@BookName", bookPostModel.BookName);
                cmd.Parameters.AddWithValue("@Description", bookPostModel.Description);
                cmd.Parameters.AddWithValue("@AuthorName", bookPostModel.AuthorName);
                cmd.Parameters.AddWithValue("@ActualPrice", bookPostModel.ActualPrice);
                cmd.Parameters.AddWithValue("@DiscountedPrice", bookPostModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@BookImage", bookPostModel.BookImage);
                cmd.Parameters.AddWithValue("@Quantity", bookPostModel.Quantity);
                cmd.Parameters.AddWithValue("@TotalRating", bookPostModel.TotalRating);
                cmd.Parameters.AddWithValue("@CountRating", bookPostModel.RatingCount);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return bookPostModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteBook(int BookId)
        {
            var result = 0;
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteBooks", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", BookId);
                connection.Open();
                result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
