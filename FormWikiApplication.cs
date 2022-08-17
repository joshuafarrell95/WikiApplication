using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WikiApplication
{
    public partial class FormWikiApplication : Form
    {
        public FormWikiApplication()
        {
            InitializeComponent();
        }

        // 9.1	Create a global 2D string array, use static variables for the dimensions (row, column),
        static int row = 12;                                    /* Number of records */
        static int col = 4;                                     /* Data Structure Name, Category, Structure and Definition */
        private string[,] ArrayWiki = new string[row, col];

        // 9.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        private void ButtonAdd_MouseClick(object sender, MouseEventArgs e)
        {

        }

        // 9.3	Create an EDIT button that will allow the user to modify any information from the 4 text boxes into the 2D array,
        private void ButtonEdit_MouseClick(object sender, MouseEventArgs e)
        {

        }

        // 9.4	Create a DELETE button that removes all the information from a single entry of the array;
        // the user must be prompted before the final deletion occurs,
        private void ButtonDelete_MouseClick(object sender, MouseEventArgs e)
        {

        }

        // 9.5	Create a CLEAR method to clear the four text boxes so a new definition can be added,
        private void ClearRecord()
        {

        }

        // 9.6	Write the code for a Bubble Sort method to sort the 2D array by Name ascending,
        // ensure you use a separate swap method that passes the array element to be swapped(do not use any built-in array methods),
        private void BubbleSort()
        {

        }

        // 9.7	Write the code for a Binary Search for the Name in the 2D array and display the information in the other textboxes when found,
        // add suitable feedback if the search in not successful and clear the search textbox (do not use any built-in array methods),
        private void BinarySearch()
        {

        }

        // 9.8	Create a display method that will show the following information in a ListView: Name and Category,
        private void DisplayList()
        {

        }
    }
}
