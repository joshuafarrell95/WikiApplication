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
    }
}
