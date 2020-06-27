﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormCore
{
    public partial class OpenFileForm : Form
    {
        public OpenFileForm()
        {
            InitializeComponent();
        }

        OpenFileDialog openFileDialog = new OpenFileDialog();

        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\input.xlsx"; // for testing
        string fileName = string.Empty;

        private void openFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get path of selected file
                filePath = openFileDialog.FileName;
                fileName = openFileDialog.SafeFileName;

                filePathBox.Text = filePath;
                fileNameBox.Text = fileName;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (filePath == string.Empty) return;

            var progressWindow = new ProgressWindow();
            progressWindow.Show();

            Dictionary<string, List<KeyValuePair<string, string>>> dataMap = null;

            if (skylineRadioButton.Checked)
            {
                dataMap = ProcessSkyline.Run(filePath, 1, 3, 10);
            }
            else if (radioButton1.Checked)
            {
                // placeholder
            }
            else if (radioButton2.Checked)
            {
                // placeholder
            }

            if (dataMap == null)
            {
                progressWindow.progressTextBox.AppendLine("Error reading file.");
                return;
            }

            progressWindow.progressTextBox.AppendLine("Finished reading data.\r\nWriting data...");
            
            bool removeNA = removeMissingCheckBox.Checked;
            var missingValPercent = missingValueBox.Text ?? missingValueBox.PlaceholderText;
            var missingValReplace = replaceMissingValueTextBox.Text ?? replaceMissingValueTextBox.PlaceholderText;

            WriteOutputFile.Main(removeNA, missingValPercent, missingValReplace, progressWindow, dataMap);

        }

    }

}
