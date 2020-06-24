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
using OfficeOpenXml;

namespace WindowsFormCore
{
    public partial class OpenFileDialogForm : Form
    {
        public OpenFileDialogForm()
        {
            InitializeComponent();
        }

        OpenFileDialog openFileDialog = new OpenFileDialog();
        //string filePath = string.Empty;
        string filePath = "C:\\Users\\Lisa\\OneDrive\\Desktop\\input.xlsx";
        string fileName = string.Empty;

        private void openFileButton_Click(object sender, EventArgs e)
        {
            
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Excel files (*.xlsx;*.xls;*.csv)|*.xlsx;*.xls;*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get path of selectd file
                filePath = openFileDialog.FileName;
                fileName = openFileDialog.SafeFileName;

                filePathBox.Text = filePath;
                fileNameBox.Text = fileName;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (filePath != string.Empty)
            {
                Form2 form2 = new Form2();
                form2.Show();
                ProcessSkyline.run(filePath);
            }
        }
    }
}
