using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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

        private void FormWikiApplication_Load(object sender, EventArgs e)
        {
            InitialiseArray();
        }

        private void InitialiseArray()
        {
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++) {
                    ArrayWiki[x, y] = "";
                }
            }
            DisplayList();
        }

        // 9.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        private void ButtonAdd_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
            AddInformation();
        }

        private void AddInformation()
        {
            bool flag = false;
            
            for (int x = 0; x < row; x++)
            {
                if ((ArrayWiki[x, 0] == "") && !flag)
                {
                    ArrayWiki[x, 0] = textBoxDataStructureName.Text;
                    ArrayWiki[x, 1] = textBoxCategory.Text;
                    ArrayWiki[x, 2] = textBoxStructure.Text;
                    ArrayWiki[x, 3] = textBoxDefinition.Text;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                MessageBox.Show("You must delete a record before adding a new record.", "Too many records", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DisplayList();
        }

        // 9.3	Create an EDIT button that will allow the user to modify any information from the 4 text boxes into the 2D array,
        private void ButtonEdit_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
            EditInformation();
        }

        private void EditInformation()
        {
            int selectedIndex = -1;

            try
            {
                selectedIndex = listViewWiki.SelectedIndices[0];
                Trace.TraceInformation(selectedIndex.ToString());
                if (ArrayWiki[selectedIndex, 0] != "")
                {
                    textBoxDataStructureName.Text = ArrayWiki[selectedIndex, 0].ToString();
                    textBoxCategory.Text = ArrayWiki[selectedIndex, 1].ToString();
                    textBoxStructure.Text = ArrayWiki[selectedIndex, 2].ToString();
                    textBoxDefinition.Text = ArrayWiki[selectedIndex, 3].ToString();
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                statusStrip.Items.Add("Please select a valid record to edit");
            }
            DisplayList();
        }

        // 9.4	Create a DELETE button that removes all the information from a single entry of the array;
        // the user must be prompted before the final deletion occurs,
        private void ButtonDelete_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
        }

        private void DeleteInformation()
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
            listViewWiki.Items.Clear();
            for(int x = 0; x < row; x++)
            {
                ListViewItem lvi = new ListViewItem(ArrayWiki[x, 0]);   /* Name */
                lvi.SubItems.Add(ArrayWiki[x, 1].ToString());           /* Category */
                listViewWiki.Items.Add(lvi);
            }
        }

        // 9.9	Create a method so the user can select a definition (Name) from the ListView and all the information is displayed in the appropriate Textboxes,
        private void SelectDefinition()
        {

        }

        // 9.10	Create a SAVE button so the information from the 2D array can be written into a binary file called definitions.dat which is sorted by Name,
        // ensure the user has the option to select an alternative file. Use a file stream and BinaryWriter to create the file.
        private void ButtonSave_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void SaveData()
        {

        }

        // 9.11	Create a LOAD button that will read the information from a binary file called definitions.dat into the 2D array,
        // ensure the user has the option to select an alternative file.Use a file stream and BinaryReader to complete this task.
        private void ButtonLoad_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void LoadData()
        {

        }
    }
}
