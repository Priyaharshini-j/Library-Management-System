using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static NETCore.Pages.LMS.FetchBookModel;

namespace LibraryManagementSystem.Pages.LMS
{
    public class CreateBookModel : PageModel
    {
        public BooksFetch book = new BooksFetch();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {
            String CONN_STRING = "Data Source=LocalHost;Encrypt=False;Initial Catalog=LMS_DB;Integrated Security=True;";
            SqlConnection connect = new SqlConnection(CONN_STRING);
            connect.Open();
           // SqlCommand cmd = connect.CreateCommand();
            book.book_code= Request.Form["code"];
            book.bookTitle= Request.Form["title"];
            book.category= Request.Form["category"];
            book.author = Request.Form["author"];
            book.publication = Request.Form["publication"];
            book.publish_date = Convert.ToDateTime(Request.Form["publish_date"]);
            book.price = Convert.ToInt32(Request.Form["price"]);
            book.rack_number = Request.Form["rack_number"];
            try {
                string query= "INSERT INTO lms_book_details VALUES" +
                $" ('{book.book_code}','{book.bookTitle}','{book.category}','{book.author}','{book.publication}'," +
                $"'{book.publish_date}',7,{book.price},'{book.rack_number}','2011-05-04','S01')";
                SqlCommand cmd = new SqlCommand(query,connect);
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                successMessage = "Book added successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);

                errorMessage = ex.Message;

            }
            connect.Close();
        }
    }

}
