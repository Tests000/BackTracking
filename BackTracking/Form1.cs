using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackTracking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            startButton.Visible = false;
            startButton.Enabled = false;
            inputFieldSize.KeyDown += (object sender, KeyEventArgs e) => { if (e.KeyCode == Keys.Enter) CreateField_Click(sender, e); };
        }

        private void CreateField_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(this.inputFieldSize.Text, out int size) && size >= 3 && size <= 15)
            {
                InitField(size);
                startButton.Visible = true;
            }
            else
                MessageBox.Show("Размер поля должен быть числом от 3 до 15");
            inputFieldSize.Text = "";
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            var res = (new BackTrack(matrix));
            if (res.OnError)
            {
                MessageBox.Show(res.IER);
                return;
            }
            matrix = res.Result;
            DeleteField();
            InitButtons(false);
            DrawCell();
        }
    }
}
