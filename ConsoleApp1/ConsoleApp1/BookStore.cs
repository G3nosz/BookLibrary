using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.Web;

namespace ConsoleApp1
{
    public class BookStore
    {
        [JsonProperty("bookList")]
        private List<Book> bookList { get; set; }

        public BookStore()
        {
            bookList = new List<Book>();
        }

        /// <summary>
        /// Just to add correct id to the book
        /// Lets just say ID increase by one at the time
        /// </summary>
        /// <returns></returns>
        private int bookLastID()
        {
            int id = 0;
            foreach (Book b in bookList)
            {
                if (b.ID > id) id = b.ID;
            }
            return id + 1;
        }

        /// <summary>
        /// Print all books that are in the system
        /// </summary>
        public void PrintAllBooks()
        {
            foreach (Book b in bookList)
            {
                Console.WriteLine(b.toString());
            }
        }

        /// <summary>
        /// Add new book
        /// </summary>
        /// <param name="newBook"> New Book data </param>
        /// <returns></returns>
        public int AddNewBook(Book newBook)
        {
            Console.WriteLine("Adding book to list");
            int newID = bookLastID();
            newBook.ID = newID;
            bookList.Add(newBook);
            return 1;//return 1 on success
        }

        /// <summary>
        /// Update who now has that book
        /// </summary>
        /// <param name="ID"> Book ID </param>
        /// <param name="Holder"> Person name </param>
        /// <param name="days"> How long he will have it </param>
        public void UpdateBookHolder(int ID, string Holder, int days)
        {
            foreach (Book b in bookList)
            {
                if (b.ID == ID)
                {
                    b.Holder = Holder;
                    //and append dateTime here
                    if (Holder == "Library") b.Status = 0;
                    else b.Status = 1;
                }
            }
        }
        /// <summary>
        /// Remove book
        /// </summary>
        /// <param name="ID"> Book ID </param>
        public void RemoveBook(int ID)
        {
            List<Book> temp = new List<Book>();
            foreach (Book b in bookList)
            {
                if (b.ID != ID)
                {
                    temp.Add(b);
                }
            }
            bookList = temp;//list without previous book
        }

        /// <summary>
        /// If book status is 1 - book is taken
        /// if book status is 0 - book is in the library
        /// </summary>
        /// <param name="ID"> Book ID </param>
        /// <returns></returns>
        public int BookStatus(int ID)
        {
            foreach (Book b in bookList)
            {
                if (b.ID == ID) return b.Status;
            }
            return -1; //book wasnt found at all
        }

        /// <summary>
        /// Check how many books does user allready have
        /// </summary>
        /// <param name="user"> Current user name </param>
        /// <returns></returns>
        public int UserBooks(string user)
        {
            int counter = 0;
            foreach (Book b in bookList)
            {
                if (b.Holder == user) counter++;
            }
            return counter;
        }


        //Filter Methods----------------------------------------------------------------------------------------------------
        public void FilterByAuthor(string author)
        {
            List<Book> filtered = bookList.FindAll(e => e.Author == author);
            foreach (Book b in filtered)
            {
                Console.WriteLine(b.toString());
            }
        }
        public void FilterByCategory(string category)
        {
            List<Book> filtered = bookList.FindAll(e => e.Category == category);
            foreach (Book b in filtered)
            {
                Console.WriteLine(b.toString());
            }
        }
        public void FilterByLanguage(string language)
        {
            List<Book> filtered = bookList.FindAll(e => e.Language == language);
            foreach (Book b in filtered)
            {
                Console.WriteLine(b.toString());
            }
        }
        public void FilterByISBN(string isbn)
        {
            List<Book> filtered = bookList.FindAll(e => e.ISBN == isbn);
            foreach (Book b in filtered)
            {
                Console.WriteLine(b.toString());
            }
        }
        public void FilterByName(string name)
        {
            List<Book> filtered = bookList.FindAll(e => e.Name == name);
            foreach (Book b in filtered)
            {
                Console.WriteLine(b.toString());
            }
        }

        public void FilterByAvailability(int avail)
        {
            List<Book> filtered = bookList.FindAll(e => e.Status == avail);
            foreach (Book b in filtered)
            {
                Console.WriteLine(b.toString());
            }
        }
    }
}
