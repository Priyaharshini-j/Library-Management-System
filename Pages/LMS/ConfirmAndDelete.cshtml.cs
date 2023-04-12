using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static NETCore.Pages.LMS.FetchBookModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Pages.LMS
{
    public class ConfirmAndDeleteModel : PageModel
    {
        public string bookCode = "";
        public BooksFetch book = new BooksFetch();
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
            bookCode = Request.Query["bookCode"];
            try
            {
                string connString = "Data Source=LocalHost;Encrypt=False;Initial Catalog=LMS_DB;Integrated Security=True;";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                Console.WriteLine(bookCode);
                cmd.CommandText = $"DELETE FROM lms_book_details WHERE book_code = '{bookCode}'";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
                if (rowsAffected > 0)
                {
                    Response.Redirect("/LMS/FetchBook");
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
    }
}