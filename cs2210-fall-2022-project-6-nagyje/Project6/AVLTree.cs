using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/////////////////////////////////////////
// 
// Author: PrinciRaj1992, geeksforgeeks.com / Joe Nagy, nagyje@etsu.edu
// Course CSCI 2210-001 - Data Structures
// Assignment: Project 6 - Library Kiosk System
//
/////////////////////////////////////////
namespace Project_6
{

    // C# program for insertion in AVL Tree
    using System;

    /// <summary>
    /// The Node and AVLTree classes were created by PrinciRaj1992.
    /// I used them as a baseline to make the program function for
    /// Book objects as the node data instead of integers. 
    /// max, height, rotations, getbalance, and preorder were fully written
    /// by PrinciRaj1992, the others were adjusted to fit my program.
    /// </summary>
    public class Node
    {
        public Book book;
        public String key;
        public int height;
        public Node left, right;

        public Node(Book d, int setting)
        {
            book = d;
            if (setting == 1)
            {
                key = d.Title;
            } else if (setting == 2)
            {
                key = d.Author;
            } else if (setting == 3)
            {
                key = d.Publisher;
            }
            height = 1;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AVLTree
    {

        public Node root;

        // A utility function to get
        // the height of the tree
        int height(Node N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        // A utility function to get
        // maximum of two integers
        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // A utility function to right
        // rotate subtree rooted with y
        // See the diagram given above.
        Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation
            x.right = y;
            y.left = T2;

            // Update heights
            y.height = max(height(y.left),
                        height(y.right)) + 1;
            x.height = max(height(x.left),
                        height(x.right)) + 1;

            // Return new root
            return x;
        }

        // A utility function to left
        // rotate subtree rooted with x
        // See the diagram given above.
        Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            // Perform rotation
            y.left = x;
            x.right = T2;

            // Update heights
            x.height = max(height(x.left),
                        height(x.right)) + 1;
            y.height = max(height(y.left),
                        height(y.right)) + 1;

            // Return new root
            return y;
        }

        // Get Balance factor of node N
        int getBalance(Node N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
        }

        /// <summary>
        /// Adds a new node to the tree based on which sort
        /// key is currently in use.
        /// </summary>
        /// <param name="node">
        /// root node of tree
        /// </param>
        /// <param name="d">
        /// book to be added
        /// </param>
        /// <param name="setting">
        /// current sort key
        /// </param>
        /// <returns>
        /// new root node of the tree
        /// </returns>
        public Node insert(Node node, Book d, int setting)
        {
            String key = "";
            switch (setting)
            {
                case 1:
                    key = d.Title;
                    break;
                case 2:
                    key = d.Author;
                    break;
                case 3:
                    key = d.Publisher;
                    break;
            }
            /* 1. Perform the normal BST insertion */
            if (node == null)
                return (new Node(d, setting));

            if (-1 == string.Compare(key, node.key))
                node.left = insert(node.left, d, setting);
            else if (1 == string.Compare(key, node.key))
                node.right = insert(node.right, d, setting);
            else // Duplicate keys not allowed
                return node;

            /* 2. Update height of this ancestor node */
            node.height = 1 + max(height(node.left),
                                height(node.right));

            /* 3. Get the balance factor of this ancestor
                node to check whether this node became
                unbalanced */
            int balance = getBalance(node);

            // If this node becomes unbalanced, then there
            // are 4 cases Left Left Case
            if (balance > 1 && -1 == string.Compare(key, node.left.key))
                return rightRotate(node);

            // Right Right Case
            if (balance < -1 && 1 == string.Compare(key, node.right.key))
                return leftRotate(node);

            // Left Right Case
            if (balance > 1 && 1 == string.Compare(key, node.left.key))
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && -1 == string.Compare(key, node.right.key))
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            /* return the (unchanged) node pointer */
            return node;
        }
        Node minValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the leftmost leaf */
            while (current.left != null)
                current = current.left;

            return current;
        }

        /// <summary>
        /// Deletes the specified node from the tree and rebalances
        /// as necessary
        /// </summary>
        /// <param name="root">
        /// root node of tree
        /// </param>
        /// <param name="key">
        /// specific key to delete node for
        /// </param>
        /// <returns>
        /// updated root node of tree
        /// </returns>
        public Node deleteNode(Node root, String key)
        {
            // STEP 1: PERFORM STANDARD BST DELETE 
            if (root == null)
                return root;

            // If the key to be deleted is smaller than 
            // the root's key, then it lies in left subtree 
            if (-1 == string.Compare(key, root.key))
                root.left = deleteNode(root.left, key);

            // If the key to be deleted is greater than the 
            // root's key, then it lies in right subtree 
            else if (1 == string.Compare(key, root.key))
                root.right = deleteNode(root.right, key);

            // if key is same as root's key, then this is the node 
            // to be deleted 
            else
            {

                // node with only one child or no child 
                if ((root.left == null) || (root.right == null))
                {
                    Node temp = null;
                    if (temp == root.left)
                        temp = root.right;
                    else
                        temp = root.left;

                    // No child case 
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else // One child case 
                        root = temp; // Copy the contents of 
                                     // the non-empty child 
                }
                else
                {

                    // node with two children: Get the inorder 
                    // successor (smallest in the right subtree) 
                    Node temp = minValueNode(root.right);

                    // Copy the inorder successor's data to this node 
                    root.key = temp.key;

                    // Delete the inorder successor 
                    root.right = deleteNode(root.right, temp.key);
                }
            }

            // If the tree had only one node then return 
            if (root == null)
                return root;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE 
            root.height = max(height(root.left),
                        height(root.right)) + 1;

            // STEP 3: GET THE BALANCE FACTOR
            // OF THIS NODE (to check whether 
            // this node became unbalanced) 
            int balance = getBalance(root);

            // If this node becomes unbalanced, 
            // then there are 4 cases 
            // Left Left Case 
            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            // Left Right Case 
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case 
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case 
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
        }
        // A utility function to print preorder traversal
        // of the tree.
        // The function also prints height of every node
        public void preOrder(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.key);
                preOrder(node.left);
                preOrder(node.right);
            }
        }

        // This code has been contributed
        // by PrinciRaj1992


        /// <summary>
        /// Changes the key the tree nodes are sorted on by sending 
        /// all the current node info to a list and reorganizing the tree around
        /// the new parameters
        /// </summary>
        /// <param name="node">
        /// root node of the tree
        /// </param>
        /// <param name="userInput">
        /// user choice for new key
        /// </param>
        /// <returns>
        /// updated tree
        /// </returns>
        public AVLTree changeKey(Node node, int userInput)
        {
            AVLTree newtree = new AVLTree();
            List<Book> books = new List<Book>();
            books = newBooks(books, node, userInput);
            for (int i = 0; i < books.Count; i++)
            {
                 newtree.root = newtree.insert(newtree.root, books[i], userInput);
            }
            return newtree;
        }

        /// <summary>
        /// Recursively adds the books to a list using a tweaked
        /// preorder transversal logic base
        /// </summary>
        /// <param name="books">
        /// premade list for books to be stored in
        /// </param>
        /// <param name="node">
        /// root node of tree being copied over
        /// </param>
        /// <param name="userInput">
        /// key on which the tree will be sorted
        /// </param>
        /// <returns>
        /// list of books that were in the tree
        /// </returns>
        public List<Book> newBooks(List<Book> books, Node node, int userInput)
        {
            if (node != null)
            {
                books.Add(node.book);
                newBooks(books, node.left, userInput);
                newBooks(books, node.right, userInput);
            }
            return books;
        }
    }
}
