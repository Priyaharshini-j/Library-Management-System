using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
namespace NETCore.Pages.LMS
{
    public class FetchBookModel : PageModel
    {
       public  List<BooksFetch> bookList = new List<BooksFetch>();
        public void OnGet()
        {
            try
            {
                String CONN_STRING = "Data Source=LocalHost;Encrypt=False;Initial Catalog=LMS_DB;Integrated Security=True;";
                SqlConnection connect = new SqlConnection(CONN_STRING);
                connect.Open();
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT book_code,book_title,author,category,publication from lms_book_details;";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BooksFetch book = new BooksFetch();
                    book.book_code = reader.GetString(0);
                    book.bookTitle = reader.GetString(1);
                    book.author = reader.GetString(2);
                    book.category = reader.GetString(3);
                    book.publication = (string)reader["publication"];

                    //we fetch the details of one book and store it in a list with properties of BooksFetch class
                    bookList.Add(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public class BooksFetch
        {
            public string book_code { get; set; }
            public string bookTitle { get; set; }
            public string author { get; set; }
            public string category { get; set; }
            public string publication { get; set; }
            public DateTime publish_date { get; set; }
            public string rack_number { get; set; }
            public int price { get; set; }
            public int book_edition { get; set; }
            public DateTime date_arrival { get; set; }
            public string supplier { get; set; }
            
        }
    }
}