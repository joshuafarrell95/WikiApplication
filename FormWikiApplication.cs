﻿using System;
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

// Wiki Application
// Farrell, Joshua
// M153428
// 07/09/2022

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
            statusStrip.Items.Clear();
            for (int x = 0; x < row; x++)
            {
                InitialiseArray(x);
            }
            statusStrip.Items.Add("Wiki Array initialised and ready to use");
            DisplayList();
        }

        private void InitialiseArray(int x)
        {
            InitialiseArray(x, "");
        }

        private void InitialiseArray(int x, string s)
        {
            for (int y = 0; y < col; y++)
            {
                ArrayWiki[x, y] = s;
            }
        }
        #endregion

        // 9.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        #region 9.2
        private void ButtonAdd_MouseClick(object sender, MouseEventArgs e)
        {
            AddInformation();
        }

        private void AddInformation()
        {
            statusStrip.Items.Clear();
            bool flag = false;                  /* Flag to exit method if empty record found */

            for (int x = 0; x < row; x++)
            {
                if ((ArrayWiki[x, 0] == "") && !flag)       /* If empty record is found */
                {
                    TextBoxToArray(x);
                    flag = true;
                    break;
                }
            }
            if (!flag)                                      /* If no empty record is found */
            {
                MessageBox.Show("You must delete a record before adding a new record.",
                    "Too many records", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                statusStrip.Items.Add("Record added to Wiki Array");
            }
            BubbleSort();
            DisplayList();
        }

        private void TextBoxToArray(int indx)           /* Method to convert all textboxes to array */
        {
            ArrayWiki[indx, 0] = textBoxDataStructureName.Text;
            ArrayWiki[indx, 1] = textBoxCategory.Text;
            ArrayWiki[indx, 2] = textBoxStructure.Text;
            ArrayWiki[indx, 3] = textBoxDefinition.Text;
        }

        private void ArrayToTextBox(int indx)           /* Method to convert array to all textboxes */
        {
            textBoxDataStructureName.Text = ArrayWiki[indx, 0];
            textBoxCategory.Text = ArrayWiki[indx, 1];
            textBoxStructure.Text = ArrayWiki[indx, 2];
            textBoxDefinition.Text = ArrayWiki[indx, 3];
        }
        #endregion

        // 9.3	Create an EDIT button that will allow the user to modify any information from the 4 text boxes into the 2D array,
        #region 9.3
        private void ButtonEdit_MouseClick(object sender, MouseEventArgs e)
        {
            EditInformation();
        }

        private void EditInformation()
        {
            statusStrip.Items.Clear();
            try
            {
                int selectedIndex = GetSelectedIndex();
                if (selectedIndex >= 0)                             /* Edit record if there is a valid selected record */
                {
                    if (ArrayWiki[selectedIndex, 0] != "")          /* Edit record if it is not empty */
                    {
                        TextBoxToArray(selectedIndex);
                        statusStrip.Items.Add("Record " + ArrayWiki[selectedIndex, 0] + " successfully edited.");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Trace.TraceInformation(ex.ToString());
                statusStrip.Items.Add("Please select a valid record to edit");
            }
            BubbleSort();
            DisplayList();
        }

        private int GetSelectedIndex()                          /* Reuseable getter */
        {
            int selectedIndex;
            try
            {
                selectedIndex = listViewWiki.SelectedIndices[0];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Trace.TraceInformation(ex.ToString());
                return -1;
            }
            return selectedIndex;
        }
        #endregion

        // 9.4	Create a DELETE button that removes all the information from a single entry of the array;
        // the user must be prompted before the final deletion occurs,
        #region 9.4
        private void ButtonDelete_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
            int selectedIndex = GetSelectedIndex();

            if (selectedIndex >= 0)
            {
                /* Prompt the user before the final decision*/
                var userDecision = MessageBox.Show("Are you sure you want to delete the selected record " + ArrayWiki[selectedIndex, 0] + "?",
                        "Confirm record deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                string deletedRecordName = ArrayWiki[selectedIndex, 0];
                if (userDecision == DialogResult.Yes)       /* User confirms deletion request */
                {
                    DeleteInformation(selectedIndex);
                    //ClearTextBoxes();
                    statusStrip.Items.Add("Record " + deletedRecordName + " deleted from array");
                }
                else                                        /* User clicks on no or X button */
                {
                    statusStrip.Items.Add("Record not deleted");
                }
            }
            else
            {
                statusStrip.Items.Add("You must select a record to delete");
            }
        }

        private void ListViewWiki_DoubleClick(object sender, EventArgs e)
        {
            statusStrip.Items.Clear();
            var userDecision = MessageBox.Show("Are you sure you want to delete all records?", "Confirm all record deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

            if (userDecision == DialogResult.Yes)
            {
                DeleteInformation();
                statusStrip.Items.Clear();                          /* Required to delete existing messages from DeleteInformation() */
                statusStrip.Items.Add("All records deleted");
            }
            else
            {
                statusStrip.Items.Add("Deletion cancelled, no records affected");
            }
        }

        private void DeleteInformation()                        /* This method deletes all information from the array */
        {
            for (int x = 0; x < row; x++)
            {
                InitialiseArray();
            }
        }

        private void DeleteInformation(int selectedIndex)       /* This method deletes information from a selected index */
        {
            statusStrip.Items.Clear();
            try
            {
                if (ArrayWiki[selectedIndex, 0] != "")
                {
                    InitialiseArray(selectedIndex);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Trace.TraceInformation(ex.ToString());
                statusStrip.Items.Add("Please select a valid record to delete");
            }
            BubbleSort();
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
            BubbleSort(ArrayWiki);
        }

        private void BubbleSort(string[,] arrayTwoDim)
        {
            ReplaceString(arrayTwoDim, "", "~");    /* Temporarily change empty records to tilde to sink them to the bottom of the list */
            for (int xi = 0; xi < row; xi++)
            {
                for (int xj = 0; xj < row - 1; xj++)
                {
                    if (!(String.IsNullOrWhiteSpace(arrayTwoDim[xj, 0])))
                    {
                        if (String.CompareOrdinal(arrayTwoDim[xj, 0], arrayTwoDim[xj + 1, 0]) > 0)
                        {
                            Sort(arrayTwoDim, xj);
                        }
                    }
                }
            }
            ReplaceString(arrayTwoDim, "~", "");    /* Change tilde records back to empty for correct display in the ListView */
        }

        private void Sort(string[,] arrayTwoDim, int indx)
        {
            for (int indy = 0; indy < col; indy++)
            {
                string temp = arrayTwoDim[indx, indy];
                arrayTwoDim[indx, indy] = arrayTwoDim[indx + 1, indy];
                arrayTwoDim[indx + 1, indy] = temp;
            }
        }

        private void ReplaceString(string[,] arrayTwoDim, string sa, string sb)         /* Method used to replace empty record names with a tilde for correct sorting, and back again for correct display */
        {
            for (int indx = 0; indx < row; indx++)
            {
                if (arrayTwoDim[indx, 0] == sa)
                {
                    arrayTwoDim[indx, 0] = sb;
                }
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
                BubbleSort();
                DisplayList();
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
                    SelectDefinition(mid);
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
            if (!isItemFound)
            {
                MessageBox.Show("Record " + searchString + " not found.", "Search unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                statusStrip.Items.Clear();
                statusStrip.Items.Add("Record " + searchString + " found and highlighted.");
            }
            textBoxSearch.Clear();
        }

        private void HighlightRecord(int recordIndx)
        {
            for (int x = 0; x < row; x++)
            {
                listViewWiki.Items[x].Selected = false;         /* Unhighlights any record */
            }
            listViewWiki.Refresh();
            listViewWiki.Items[recordIndx].Selected = true;
            listViewWiki.Select();
        }
        #endregion

        // 9.8	Create a display method that will show the following information in a ListView: Name and Category,
        #region 9.8
        private void DisplayList()
        {
            listViewWiki.Items.Clear();
            for (int x = 0; x < row; x++)
            {
                ListViewItem lvi = new ListViewItem(ArrayWiki[x, 0]);   /* Name */
                lvi.SubItems.Add(ArrayWiki[x, 1].ToString());           /* Category */
                listViewWiki.Items.Add(lvi);
            }
        }
        #endregion

        // 9.9	Create a method so the user can select a definition (Name) from the ListView and all the information is displayed in the appropriate Textboxes,
        #region 9.9
        private void ListViewWiki_MouseClick(object sender, MouseEventArgs e)
        {
            SelectDefinition();
        }

        private void SelectDefinition()
        {
            int selectedIndex = GetSelectedIndex();
            if (selectedIndex >= 0)
            {
                SelectDefinition(selectedIndex);
            }            
        }

        private void SelectDefinition(int selectedIndex)
        {
            statusStrip.Items.Clear();
            string recordTitle;
            if (selectedIndex >= 0)
            {
                ArrayToTextBox(selectedIndex);
                recordTitle = ArrayWiki[selectedIndex, 0];
                if (recordTitle != "")
                {
                    statusStrip.Items.Add("Record " + recordTitle + " selected");
                }
            }
        }
        #endregion

        // 9.10	Create a SAVE button so the information from the 2D array can be written into a binary file called definitions.dat which is sorted by Name,
        // ensure the user has the option to select an alternative file. Use a file stream and BinaryWriter to create the file.
        #region 9.10
        const string DEFAULT_FILE_NAME = "definitions.dat";

        private void ButtonSave_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
            string savedFileName = "";

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "dat files (*.dat)|*.dat";
            saveFileDialog.Title = "Save a DAT file";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dat";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                if (saveFileDialog.FileName != "")
                {
                    savedFileName = SaveData(fileName);
                }
                else
                {
                    savedFileName = SaveData(Application.StartupPath + DEFAULT_FILE_NAME);
                }
            }

            if (savedFileName != "")
            {
                savedFileName = Path.GetFileName(savedFileName);

                statusStrip.Items.Add("File " + savedFileName + " saved successfully");
            }
        }

        private string SaveData(string saveFileName)
        {
            BubbleSort();
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
                Trace.TraceInformation(ex.ToString());
                MessageBox.Show("File " + saveFileName + " was unable to be saved due to an IO Error. Please try again.", "Save IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return Path.GetFileName(saveFileName);
        }
        #endregion

        // 9.11	Create a LOAD button that will read the information from a binary file called definitions.dat into the 2D array,
        // ensure the user has the option to select an alternative file.Use a file stream and BinaryReader to complete this task.
        #region 9.11
        private void ButtonLoad_MouseClick(object sender, MouseEventArgs e)
        {
            statusStrip.Items.Clear();
            string loadedFileName = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "dat files (*.dat)|*.dat";
            openFileDialog.Title = "Open a DAT file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                loadedFileName = LoadData(openFileDialog.FileName);

                if (loadedFileName != "")
                {
                    loadedFileName = Path.GetFileName(loadedFileName);

                    statusStrip.Items.Add("File " + loadedFileName + " loaded successfully");
                }
            }
        }

        private string LoadData(string loadFileName)
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
                Trace.TraceInformation(ex.ToString());
                MessageBox.Show("File " + loadFileName + " was unable to be loaded due to an IO Error. Please try again.", "Load IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            DisplayList();
            return Path.GetFileName(loadFileName);
        }
        #endregion

        // 9.12	All code is required to be adequately commented, and each interaction must have suitable error trapping and/or feedback.
        // All methods must utilise the appropriate Dialog Boxes, Message Boxes, etc to ensure fully user functionality.
        // Map the programming criteria and features to your code/methods by adding comments above the method signatures.
        // Ensure your code is compliant with the CITEMS coding standards (refer http://www.citems.com.au/).
        #region 9.12
        /* Commented out for functional testing */
        private void TextBoxDataStucture_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Enter the Data Structure Name here, or double click on this text box to clear all text boxes.", textBoxDataStructureName);
        }

        private void TextBoxCategory_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Enter the Category here.", textBoxCategory);
        }

        private void TextBoxStructure_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Enter the Structure here.", textBoxStructure);
        }

        private void TextBoxDefinition_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Enter the Definition here.", textBoxDefinition);
        }

        private void TextBoxSearch_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Enter a search term here, then click on the SEARCH button", textBoxSearch);
        }

        private void ButtonSearch_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Enter a search term in the Search textbox, then click on this button.", buttonSearch);
        }

        private void ListViewWiki_MouseHover(object sender, EventArgs e)
        {
            //DisplayToolTip("Click on a record to display its contents.\r\n" +
            //    "Double click to delete all records. ", listViewWiki);
        }

        private void DisplayToolTip(string message, TextBox textbox)
        {
            //toolTip.SetToolTip(textbox, message);
        }

        private void DisplayToolTip(string message, ListView listView)
        {
            //toolTip.SetToolTip(listView, message);
        }

        private void DisplayToolTip(string message, Button button)
        {
            //toolTip.SetToolTip(button, message);
        }
        #endregion
    }
}
