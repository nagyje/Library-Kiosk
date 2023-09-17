using System.Text.RegularExpressions;
using System;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.VisualBasic.FileIO;
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
    /// The Program class includes the driver for the kiosk system
    /// and the functions for menu logic: mainMenu(), addBook(), 
    /// and writeMenu()
    /// </summary>
    public class Program
    {
        public static int setting { get; set; }
        /// <summary>
        /// The driver for the program reads the provided books.csv file
        /// and creates a book for each entry. An AVL tree is then created
        /// using the list of books, automatically balancing itself as the books
        /// are added to the tree. 
        /// A menu loop is then run, and the user can choose from 5 options 
        /// to execute functions of the tree.
        /// </summary>
        /// <param name="args">
        /// Default parameters
        /// </param>
        public static void Main(String[] args)
        {
            setting = 1;
            String textFile = "C:\\Users\\User\\source\\repos\\Project 6\\Project 6\\bin\\Debug\\net6.0\\books.csv";
            List<Book> booklist = new List<Book>();
            // Parse CSV file and create a Book object for each row
            using (TextFieldParser parser = new TextFieldParser(textFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    booklist.Add(new Book(fields[0], fields[1], Int32.Parse(fields[2]), fields[3]));
                }
            }

            // create tree and add books as nodes
            AVLTree tree = new AVLTree();
            
            for (int i = 0; i < booklist.Count; i++)
            {
                tree.root = tree.insert(tree.root, booklist[i], setting);
            }

            // run kiosk loop
            tree = mainMenu(tree);

        }
        /// <summary>
        /// mainMenu writes the kiosk functions for the user
        /// and runs the user's choice through the corresponding logic.
        /// The menu options are for adding a book to the kiosk, checking
        /// out books, changing the sort key for the books, and displaying a 
        /// preorder transversal.
        /// </summary>
        /// <param name="tree">
        /// AVLTree object that the logic runs from
        /// </param>
        /// <returns>
        /// Updated AVLTree 
        /// </returns>
        public static AVLTree mainMenu(AVLTree tree)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("\n~~~~ MENU ~~~~");
                Console.WriteLine("1.   Add Book");
                Console.WriteLine("2.   Check Out Book");
                Console.WriteLine("3.   Change Sort Key");
                Console.WriteLine("4.   Preorder Transversal");
                Console.WriteLine("0.   Exit");
                Console.WriteLine("~~~~~~~~~~~~~~");
                Console.Write("\nChoose a menu option: ");
                String inputString = Console.ReadLine();
                int inputInt = int.Parse(inputString);
                switch (inputInt)
                {
                    case 1:
                        Book book = addBook();
                        tree.root = tree.insert(tree.root, book, setting);
                        break;
                    case 2:
                        Console.WriteLine("\n~~~ Check Out Book ~~~");
                        Console.Write("Enter key of book: ");
                        String userInput = Console.ReadLine();
                        tree.deleteNode(tree.root, userInput);
                        break;
                    case 3:
                        Console.WriteLine("\n~~~ Change Key ~~~");
                        setting = writeMenu();
                        tree = tree.changeKey(tree.root, setting);
                        break;
                    case 4:
                        Console.WriteLine("\n~~~ Preorder Transversal ~~~");
                        tree.preOrder(tree.root);
                        break;
                    case 0:
                        Console.WriteLine("\n~~~ Have a good day! ~~~");
                        run = false;
                        break;
                    default:
                        Console.WriteLine("\nNot a valid option");
                        break;
                }
            }
            
            return tree;
        }
        /// <summary>
        /// Asks the user for the title, author, 
        /// pages, and publisher. These inputs are then
        /// put into a new Book object and returned.
        /// </summary>
        /// <returns>
        /// Book object with user inputs
        /// </returns>
        public static Book addBook()
        {
            Console.WriteLine("\n~~~ Add Book ~~~");
            Console.Write("Enter title: ");
            String title = Console.ReadLine();
            Console.Write("Enter author: ");
            String author = Console.ReadLine();
            Console.Write("Enter page count: ");
            String pages = Console.ReadLine();
            int pagesint = int.Parse(pages);
            Console.Write("Enter publisher: ");
            String publisher = Console.ReadLine();
            Book newbook = new Book(title,author,pagesint,publisher);
            return newbook;
        }
        /// <summary>
        /// Prints the menu options and gathers user's choice
        /// </summary>
        /// <returns>
        /// integer value of user's choice
        /// </returns>
        public static int writeMenu()
        {
            Console.WriteLine("\nChange key to...");
            Console.WriteLine("----------");
            Console.WriteLine("1.   Title");
            Console.WriteLine("2.   Author");
            Console.WriteLine("3.   Publisher\n");
            Console.Write("Enter a number: ");
            String input = Console.ReadLine();
            return int.Parse(input);
        }
    }
}