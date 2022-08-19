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

        Random random = new Random();

        private void FormWikiApplication_Load(object sender, EventArgs e)
        {
            InitialiseArray();
        }

        private void InitialiseArray()
        {
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++) {
                    //ArrayWiki[x, y] = "";                                 /* Production code */
                    ArrayWiki[x, 0] = random.Next(1, row).ToString();       /* Testing code */
                    ArrayWiki[x, 1] = random.Next(1, row).ToString();
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
                MessageBox.Show("You must delete a record before adding a new record.",
                    "Too many records", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                int selectedIndex = listViewWiki.SelectedIndices[0];
                if (ArrayWiki[selectedIndex, 0] != "")
                {
                    ArrayWiki[selectedIndex, 0] = textBoxDataStructureName.Text;
                    ArrayWiki[selectedIndex, 1] = textBoxCategory.Text;
                    ArrayWiki[selectedIndex, 2] = textBoxStructure.Text;
                    ArrayWiki[selectedIndex, 3] = textBoxDefinition.Text;
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
            DeleteInformation();
        }

        private void DeleteInformation()
        {
            try
            {
                int selectedIndex = listViewWiki.SelectedIndices[0];

                if (ArrayWiki[selectedIndex, 0] != "")
                {
                    var userDecision = MessageBox.Show("Are you sure you want to delete the selected record " + ArrayWiki[selectedIndex, 0] + "?", 
                        "Confirm record deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (userDecision == DialogResult.OK)
                    {
                        ArrayWiki[selectedIndex, 0] = "";       /* Data Structure Text */
                        ArrayWiki[selectedIndex, 1] = "";       /* Category Text */
                        ArrayWiki[selectedIndex, 2] = "";       /* Structure Text */
                        ArrayWiki[selectedIndex, 3] = "";       /* Description Text */
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                statusStrip.Items.Add("Please select a valid record to delete");
            }
            DisplayList();
        }

        // 9.5	Create a CLEAR method to clear the four text boxes so a new definition can be added,
        private void TextBoxDataStructureName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            statusStrip.Items.Clear();

            textBoxDataStructureName.Clear();
            textBoxCategory.Clear();
            textBoxStructure.Clear();
            textBoxDefinition.Clear();

            if (ArrayWiki[11, 0] == "")             /* Check if row 12 has a record and give correct feedback */
            {
                statusStrip.Items.Add("Text boxes cleared, you may now add a new definition");
            }
            else
            {
                statusStrip.Items.Add("Text boxes cleared, you must delete a definition before adding another");
            }
        }

        // 9.6	Write the code for a Bubble Sort method to sort the 2D array by Name ascending,
        // ensure you use a separate swap method that passes the array element to be swapped(do not use any built-in array methods),
        private void BubbleSort()
        {
            bool flag = true;
            for (int x = 0; (x <= (row - 1)); x++)
            {
                flag = false;
                for (int xy = 0; xy < (row - 1); xy++)
                {
                    if (ArrayWiki[x, 0].CompareTo(ArrayWiki[xy, 0]) > 0)
                    {
                        for (int y = 0; y < col; y++)
                        {
                            flag = Swap(x, y);
                        }
                    }
                }
            }
        }

        private bool Swap(int indx, int indy)
        {
            try
            {     
                String temp = ArrayWiki[indx, indy];

                ArrayWiki[indx, indy] = ArrayWiki[indx + 1, indy];

                ArrayWiki[indx + 1, indy] = temp;
            }
            catch (IndexOutOfRangeException ex)
            {
                return false;
            }
            return true;
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
            //BubbleSort();
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

        private void TempButtonSort_MouseClick(object sender, MouseEventArgs e)
        {
            BubbleSort();
        }
    }
}
