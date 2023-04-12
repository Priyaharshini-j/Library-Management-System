using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static NETCore.Pages.LMS.FetchBookModel;

namespace LibraryManagementSystem.Pages.LMS
{
    public class UpdateBookModel : PageModel
    {
        public BooksFetch book = new BooksFetch();
        public string message = "";
        public string bookCode = "";


        public void OnGet()
        {
            bookCode = Request.Query["bookCode"];
            try
            {
                string bookCodetoUpdate = Request.Query["BookCode"];
                string connString = "Data Source=LocalHost;Encrypt=False;Initial Catalog=LMS_DB;Integrated Security=True;";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select * FROM lms_book_details WHERE BOOK_CODE = '{bookCode}'";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    book.book_code = reader.GetString(0);
                    book.bookTitle = reader.GetString(1);
                    book.author = reader.GetString(3);
                    book.category = reader.GetString(2);
                    book.publication = (string)reader["publication"];
                    book.publish_date = Convert.ToDateTime(reader["publish_date"]);
                    book.book_edition = (int)reader["book_edition"];
                    book.price = (int)(reader["price"]);
                    book.rack_number = (string)reader["rack_number"];
                    book.supplier = (string)reader["supplier_id"];
                }
                reader.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("sql exception : " + se);
            }
        }
        public void OnPost()
        {
            message = "";
            bookCode = Request.Query["BookCode"];
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=LocalHost;Encrypt=False;Initial Catalog=LMS_DB;Integrated Security=True;");
                connection.Open();

                book.bookTitle = Request.Form["BookTitle"];
                book.author = Request.Form["Author"];
                book.category = Request.Form["Category"];
                book.publication = Request.Form["Publication"];
                book.publish_date = Convert.ToDateTime(Request.Form["PublicDate"]);
                book.book_edition = Convert.ToInt32(Request.Form["BookEdition"]);
                book.price = Convert.ToInt32(Request.Form["Price"]);

                SqlCommand cmd = connection.CreateCommand();

                Console.WriteLine(book.category);
                Console.WriteLine(bookCode);

                try
                {
                    cmd.CommandText = $"UPDATE lms_book_details SET book_title ='{book.bookTitle}'," +
                        $"author = '{book.author}',category = '{book.category}', publication = '{book.publication}'," +
                        $"publish_date = '{book.publish_date}', book_edition= {book.book_edition}, price = {book.price} WHERE " +
                        $"book_code = '{bookCode}';";

                    cmd.ExecuteNonQuery();

                    message = "Book Updated Successfully";

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                Console.WriteLine(ex.Message);
            }

        }
    }
}
