using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/////////////////////////////////////////
// 
// Author: Joe Nagy, nagyje@etsu.edu
// Course CSCI 2210-001 - Data Structures
// Assignment: Project 6 - Library Kiosk System
//
/////////////////////////////////////////
namespace Project_6
{
    /// <summary>
    /// The Book class holds four variables that are used
    /// to identify and give info about the book. 
    /// There are Strings Title, Author, and Publisher, and
    /// an integer for page count.
    /// </summary>
    public class Book
    {
        public String Title { get; set; }
        public String Author { get; set; }
        int Pages { get; set; }
        public String Publisher { get; set; }
        
        /// <summary>
        /// Constructor for Book objects
        /// </summary>
        /// <param name="title">
        /// Title of book
        /// </param>
        /// <param name="author">
        /// Author of book
        /// </param>
        /// <param name="pages">
        /// Number of pages in book
        /// </param>
        /// <param name="publisher">
        /// Publisher of book
        /// </param>
        public Book(string title, string author, int pages, string publisher)
        {
            Title = title;
            Author = author;
            Pages = pages;
            Publisher = publisher;
        }

        /// <summary>
        /// Prints book info
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("Pages: " + Pages);
            Console.WriteLine("Publisher: " + Publisher);
        }
    }
    
}
