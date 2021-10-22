using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;

//DOTO - missing parts
/// <summary>
/// 1. Need to fix DateTime with the books
/// 2. Need to add a return method
/// 3. Need to add Unit tests
/// </summary>

namespace ConsoleApp1
{
    class Program
    {
        public static BookStore bookStore;
        static void Main(string[] args)
        {
            //before starting load all data to list from json file
            bookStore = new BookStore();
            string userName;
            string userInput = "";

            Console.WriteLine("Please enter your username.");
            userName = Console.ReadLine();
            while (string.IsNullOrEmpty(userName))
            {
                userName = Console.ReadLine();
                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("User must have username;");
                    Console.WriteLine("Please enter your username.");
                }
            }
            
            readFile("data.json");
            Console.WriteLine("Type !help for available actions");


            while (userInput != "!exit")
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "!help":
                        Console.WriteLine("Your available commands:\n" +
                            "1. To list all available books write !all\n" +
                            "2. To add new book type !addNew\n" +
                            "3. To take a book type !take\n" +
                            "4. To return a book type !return\n" +
                            "5. To delete a book type !delete\n" +
                            "6. To select filtering options type !filter\n" +
                            "7. To exit console type !exit\n");
                        break;
                    case "!exit":
                        Console.WriteLine("Application closing\n");
                        break;
                    case "!all":
                        Console.WriteLine("Listing all books...\n");
                        bookStore.PrintAllBooks();
                        break;
                    case "!addNew":
                        createNewBook();
                        break;
                    case "!take":
                        takeBook(userName);
                        break;
                    case "!return":
                        Console.WriteLine("TODO...");
                        break;
                    case "!delete":
                        deleteBook();
                        break;
                    case "!filter":
                        filteredList();
                        break;
                    default:
                        Console.WriteLine("There is no such available action...");
                        Console.WriteLine("To see more options type !help");
                        Console.WriteLine("To exit filtering type !exit");
                        break;
                }
                Console.WriteLine("\n");
            }
        }
        public static void readFile(String file)
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                bookStore = JsonConvert.DeserializeObject<BookStore>(json);
                r.Close(); //close after finished with it
            }
        }

        /// <summary>
        /// Creating new Book and add it to json and current list
        /// </summary>
        public static void createNewBook()
        {
            Console.WriteLine("Please enter all following information:\n");
            string Name, Author, Category, Language, ISBN, PublicationDate;
            Console.Write("Books name: ");
            Name = Console.ReadLine();

            Console.Write("Books Author: ");
            Author = Console.ReadLine();

            Console.Write("Books Category: ");
            Category = Console.ReadLine();

            Console.Write("Books Language: ");
            Language = Console.ReadLine();

            Console.Write("Books Publication Date (in format yyyy-mm-dd): ");
            PublicationDate = Console.ReadLine();

            Console.Write("Books ISBN: ");
            ISBN = Console.ReadLine();

            Book book = new Book(Name, Author, Category, Language, PublicationDate, ISBN);
            bookStore.AddNewBook(book);
            
            //add new list to json
            var jsonData = JsonConvert.SerializeObject(bookStore);
            File.WriteAllText("data.json", jsonData);

            Console.WriteLine("Book has been added to the library!\n");
        }

        /// <summary>
        /// Delete the book from library
        /// </summary>
        public static void deleteBook()
        {
            Console.WriteLine("Enter book ID which you want to delete:");
            string temp = Console.ReadLine();
            int bookId;
            bool isNumber = int.TryParse(temp, out bookId);
            //need to check if book is in the library first, if it is then can delete it
            if (bookStore.BookStatus(bookId) == 0)
            {
                bookStore.RemoveBook(bookId);
                //add new list to json
                var jsonData = JsonConvert.SerializeObject(bookStore);
                File.WriteAllText("data.json", jsonData);

                Console.WriteLine("Book has been deleted!");
            }
            else Console.WriteLine("Book is not in the library at the moment, cannot delete it!");
        }

        /// <summary>
        /// When user wants to take a book for himself
        /// 1. Need to check if user doesnt go over the limit of 3 books
        /// 2. Taking book for longer than two months is not allowed
        /// </summary>
        public static void takeBook(string user)
        {
            //1. Check how many books user holds
            int bookCounter = bookStore.UserBooks(user);
            if (bookCounter >= 3)
            {
                Console.WriteLine("You have allready taken 3 books you can not take more!");
                return;
            }

            Console.WriteLine("Enter book ID which you want to take:");
            int bookId;
            string temp = Console.ReadLine();
            bool isNumber = int.TryParse(temp, out bookId);
            if (isNumber == false)
            {
                Console.WriteLine("Input must be a number");
                return;
            }

            Console.WriteLine("How many days you plan to keep the book?");
            int days;
            temp = Console.ReadLine();
            isNumber = int.TryParse(temp, out days);
            if (isNumber == false)
            {
                Console.WriteLine("Input must be a number");
                return;
            }

            if (days > 60)
            {
                Console.WriteLine("You can not take a book for that long period of time...");
                return;
            }
            else
            {
                bookStore.UpdateBookHolder(bookId, user, days);
                //add new list to json
                var jsonData = JsonConvert.SerializeObject(bookStore);
                File.WriteAllText("data.json", jsonData);
                Console.WriteLine("Book has been assigned to you successfuly!");
            }
        }

        /// <summary>
        /// User returns a book to library
        /// </summary>
        public static void returnBook()
        {
            Console.WriteLine("What is your book ID that you want to return: ");
            int bookID;
            string temp = Console.ReadLine();
            bool isNumber = int.TryParse(temp, out bookID);
            if (isNumber == false)
            {
                Console.WriteLine("Input must be a number");
                return;
            }

            //check if book is not late for return

            bookStore.UpdateBookHolder(bookID, "Library", 0);
        }
        public static void filteredList()
        {
            string userInput = "";
            Console.WriteLine("Filtering options:\n" +
                "1.To filter books by author type !filtAuth\n" +
                "2.To filter books by category type !filtCate\n" +
                "3.To filter books by language type !filtLang\n" +
                "4.To filter books by ISBN type !filtISBN\n" +
                "5.To filter books by name type !filtName\n" +
                "6.To filter books by available or taken type !filtAvail\n");

            while (userInput != "!filterExit")
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "!filterHelp":
                        Console.WriteLine("Filtering options:\n" +
                            "1.To filter books by author type !filtAuth\n" +
                            "2.To filter books by category type !filtCate\n" +
                            "3.To filter books by language type !filtLang\n" +
                            "4.To filter books by ISBN type !filtISBN\n" +
                            "5.To filter books by name type !filtName\n" +
                            "6.To filter books by available or taken type !filtAvail\n");
                        break;
                    case "!filtAuth":
                        Console.WriteLine("Enter authors name:");
                        bookStore.FilterByAuthor(Console.ReadLine());
                        break;
                    case "!filtCate":
                        Console.WriteLine("Enter category:");
                        bookStore.FilterByCategory(Console.ReadLine());
                        break;
                    case "!filtLang":
                        Console.WriteLine("Enter language:");
                        bookStore.FilterByLanguage(Console.ReadLine());
                        break;
                    case "!filtISBN":
                        Console.WriteLine("Enter ISBN:");
                        bookStore.FilterByISBN(Console.ReadLine());
                        break;
                    case "!filtName":
                        Console.WriteLine("Enter books name:");
                        bookStore.FilterByName(Console.ReadLine());
                        break;
                    case "!filtAvail":
                        Console.WriteLine("Please enter one of the following:");
                        Console.WriteLine(" 0 - To see available books in the library\n 1 - to see take books from the library");
                        int avail;
                        string temp = Console.ReadLine();
                        bool isNumber = int.TryParse(temp, out avail);
                        if (isNumber == false)
                        {
                            Console.WriteLine("Input must be 0 or 1");
                            return;
                        }
                        bookStore.FilterByAvailability(avail);
                        break;
                    default:
                        Console.WriteLine("There is no such available filtering action...");
                        Console.WriteLine("To see filtering options type !filterHelp");
                        Console.WriteLine("To exit filtering type !filterExit");
                        break;
                }

            }
        }
    }
}
