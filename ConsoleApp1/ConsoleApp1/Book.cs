using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleApp1
{
    public class Book
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        [JsonProperty("Category")]
        public string Category { get; set; }
        [JsonProperty("Language")]
        public string Language { get; set; }
        [JsonProperty("PublicationDate")]
        public string PublicationDate { get; set; }
        [JsonProperty("ISBN")]
        public string ISBN { get; set; }
        //--------------------------------------------------------------Book additional data
        [JsonProperty("Status")]
        public int Status { get; set; } //is book available to take
        [JsonProperty("Holder")]
        public string Holder { get; set; } //who now has the book
        [JsonProperty("HoldingUntil")]
        public string HoldingUntil { get; set; } //who now has the book

        public Book()
        {
            //empty one
        }
        public Book(string name, string author, string category, string language, string publicationDate, string isbn)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            ISBN = isbn;
            Status = 0; //Book is available
            Holder = "Library"; // book is in the library
            HoldingUntil = "0000-00-00";//tam kartui stringas, bet cia reikia DateTime formato
        }


        /// <summary>
        /// when user takes the book, it is not available for others to take
        /// </summary>
        public void BookTaken(string holder)
        {
            Holder = holder; //update to new book holder
            Status = 1;
        }

        public String toString()
        {
            string temp = string.Format("ID: {0},   Name: {1},  Author: {2},     Category: {3},    Language: {4},      Publication Date: {5},      ISBN: {6}",
                ID, Name, Author, Category, Language, PublicationDate, ISBN);
            return temp;
        }
    }
}
