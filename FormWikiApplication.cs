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
using System.IO;

namespace WikiApplication
{
    public partial class FormWikiApplication : Form
    {
        public FormWikiApplication()
        {
            InitializeComponent();
        }

        // 9.1	Create a global 2D string array, use static variables for the dimensions (row, column),
        #region 9.1
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
        #endregion

        // 9.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        #region 9.2
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
        #endregion

        // 9.3	Create an EDIT button that will allow the user to modify any information from the 4 text boxes into the 2D array,
        #region 9.3
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
        #endregion

        // 9.4	Create a DELETE button that removes all the information from a single entry of the array;
        // the user must be prompted before the final deletion occurs,
        #region 9.4
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
        #endregion

        // 9.5	Create a CLEAR method to clear the four text boxes so a new definition can be added,
        #region 9.5
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
        #endregion

        // 9.6	Write the code for a Bubble Sort method to sort the 2D array by Name ascending,
        // ensure you use a separate swap method that passes the array element to be swapped(do not use any built-in array methods),
        #region 9.6
        private void BubbleSort()
        {
            for (int xa = 0; xa < row; xa++)
            {
                for (int xb = 0; xb < (row - 1); xb++)
                {
                    if (String.Compare(ArrayWiki[xa, 0], ArrayWiki[xb, 0]) < 0)
                    {
                        for (int y = 0; y < col; y++)
                        {
                            Swap(xa, xb, y);
                        }
                    }
                }
            }
        }

        private void Swap(int indxa, int indxb, int loop)
        {
            try
            {     
                string temp = ArrayWiki[indxa, loop];

                ArrayWiki[indxa, loop] = ArrayWiki[indxb, loop];

                ArrayWiki[indxb, loop] = temp;
            }
            catch (IndexOutOfRangeException ex)
            {
                
            }
            
        }
        #endregion

        // 9.7	Write the code for a Binary Search for the Name in the 2D array and display the information in the other textboxes when found,
        // add suitable feedback if the search in not successful and clear the search textbox (do not use any built-in array methods),
        #region 9.7
        private void ButtonSearch_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
            if (textBoxSearch.Text != "")
            {
                BinarySearch(textBoxSearch.Text);
            }
            else
            {
                statusStrip.Items.Add("Please enter a search string in the search textbox");
                textBoxSearch.Focus();
            }
        }

        private void BinarySearch(string searchString)
        {
            bool isItemFound = false;
            int min = 0;
            int max = row - 1;

            while (min <= max)
            {
                int mid = ((min + max) / 2);
                if (searchString.CompareTo(ArrayWiki[mid, 0]) == 0)
                {
                    isItemFound = true;
                    HighlightRecord(mid);
                    break;
                }
                else if (searchString.CompareTo(ArrayWiki[mid, 0]) < 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            if(!isItemFound)
            {
                MessageBox.Show("Record " + searchString + " not found.", "Search unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                statusStrip.Items.Add("Record " + searchString + " found and highlighted.");
            }
            textBoxSearch.Clear();
        }

        private void HighlightRecord(int recordIndx)
        {
            listViewWiki.Refresh();
            listViewWiki.Items[recordIndx].Selected = true;
            listViewWiki.Select();
        }
        #endregion

        // 9.8	Create a display method that will show the following information in a ListView: Name and Category,
        #region 9.8
        private void DisplayList()
        {
            BubbleSort();
            listViewWiki.Items.Clear();
            for(int x = 0; x < row; x++)
            {
                ListViewItem lvi = new ListViewItem(ArrayWiki[x, 0]);   /* Name */
                lvi.SubItems.Add(ArrayWiki[x, 1].ToString());           /* Category */
                listViewWiki.Items.Add(lvi);
            }
        }
        #endregion

        // 9.9	Create a method so the user can select a definition (Name) from the ListView and all the information is displayed in the appropriate Textboxes,
        private void SelectDefinition()
        {
            string selectedItem = listViewWiki.SelectedItems[0].ToString();
            int selectedIndex = listViewWiki.FindItemWithText(selectedItem);
        }


        // 9.10	Create a SAVE button so the information from the 2D array can be written into a binary file called definitions.dat which is sorted by Name,
        // ensure the user has the option to select an alternative file. Use a file stream and BinaryWriter to create the file.
        #region 9.10
        const string DEFAULT_FILE_NAME = "definitions.dat";

        private void ButtonSave_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "dat files (*.dat)|*.dat";
            saveFileDialog.Title = "Save a DAT file";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.DefaultExt = "dat";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                if (saveFileDialog.FileName != "")
                {
                    SaveData(fileName);
                }
                else
                {
                    SaveData(DEFAULT_FILE_NAME);
                }
            }
        }

        private void SaveData(string saveFileName)
        {
            try
            {
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        for (int x = 0; x < row; x++)
                        {
                            for (int y = 0; y < col; y++)
                            {
                                writer.Write(ArrayWiki[x, y]);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("File " + saveFileName + " was unable to be saved due to an IO Error. Please try again.", "Save IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // 9.11	Create a LOAD button that will read the information from a binary file called definitions.dat into the 2D array,
        // ensure the user has the option to select an alternative file.Use a file stream and BinaryReader to complete this task.
        #region 9.11
        private void ButtonLoad_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "dat files (*.dat)|*.dat";
            openFileDialog.Title = "Open a DAT file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadData(openFileDialog.FileName);
            }
        }

        private void LoadData(string loadFileName)
        {
            try
            {
                using (Stream stream = File.Open(loadFileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        {
                            int x = 0;
                            Array.Clear(ArrayWiki, 0, ArrayWiki.Length);
                            while (stream.Position < stream.Length)
                            {
                                for (int y = 0; y < col; y++)
                                {
                                    ArrayWiki[x, y] = reader.ReadString();
                                }
                                x++;
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("File " + loadFileName + " was unable to be loaded due to an IO Error. Please try again.", "Load IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DisplayList();
        }
        #endregion
    }
}
