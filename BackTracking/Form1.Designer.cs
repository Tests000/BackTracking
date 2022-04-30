
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BackTracking
{
    partial class Form1
    {
        private int[,] matrix;
        private Button[,] bMatrix;
        private List<Button> chess;
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        int lShift = 130, tShift = 30;
        int buttonSize, fieldSize;

        private void DeleteField()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    this.Controls.Remove(bMatrix[i, j]);
                }
            }
        }

        private void InitField(int size)
        {
            if (bMatrix != null)
            {
                DeleteField();
            }
            fieldSize = size;
            matrix = new int[size, size];
            InitButtons();
        }

        private void InitButtons(bool active = true)
        {

            chess = new List<Button>();
            if (fieldSize < 0)
                return;

            bMatrix = new Button[fieldSize, fieldSize];
            buttonSize = 400 / fieldSize;
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(j * buttonSize + lShift, i * buttonSize + tShift);
                    this.Controls.Add(button);
                    bMatrix[i, j] = button;
                    if ((i + j) % 2 == 0)
                    {
                        button.BackColor = Color.White;
                    }
                    else
                    {
                        button.BackColor = Color.Gray;
                    }
                    if (active)
                        button.MouseDown += new MouseEventHandler(OnCellPress);
                }
            }
        }

        public void OnCellPress(object sender, MouseEventArgs args)
        {
            Button pressedButton = sender as Button;
            if (args.Button == MouseButtons.Left)
            {
                matrix[(pressedButton.Location.Y - tShift) / buttonSize, (pressedButton.Location.X - lShift) / buttonSize] = 1;
                if (!chess.Contains(pressedButton)) chess.Add(pressedButton);
            }
            else
            {
                matrix[(pressedButton.Location.Y - tShift) / buttonSize, (pressedButton.Location.X - lShift) / buttonSize] = 0;
                chess.Remove(pressedButton);
            }
            DrawCell();
        }

        private void DrawCell()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    Button button = bMatrix[i, j];
                    if (matrix[i, j] > 0)
                    {
                        Image part = new Bitmap(buttonSize, buttonSize);
                        Graphics graphics = Graphics.FromImage(part);
                        graphics.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, buttonSize, buttonSize), 5 * 132, 132, 150, 150, GraphicsUnit.Pixel);
                        button.BackgroundImage = part;
                    }
                    else if (matrix[i, j] < 0)
                    {
                        Image part = new Bitmap(buttonSize, buttonSize);
                        Graphics graphics = Graphics.FromImage(part);
                        graphics.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, buttonSize, buttonSize), 3 * 132, 0, 150, 150, GraphicsUnit.Pixel);
                        button.BackgroundImage = part;
                    }
                    else
                        button.BackgroundImage = null;
                }
            }
            if (chess.Count > 0)
            {
                startButton.Enabled = true;
            }
            else
                startButton.Enabled = false;
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputFieldSize = new System.Windows.Forms.TextBox();
            this.CreateField = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputFieldSize
            // 
            this.inputFieldSize.Location = new System.Drawing.Point(127, 42);
            this.inputFieldSize.Name = "inputFieldSize";
            this.inputFieldSize.Size = new System.Drawing.Size(34, 22);
            this.inputFieldSize.TabIndex = 0;
            // 
            // CreateField
            // 
            this.CreateField.Location = new System.Drawing.Point(22, 70);
            this.CreateField.Name = "CreateField";
            this.CreateField.Size = new System.Drawing.Size(100, 47);
            this.CreateField.TabIndex = 1;
            this.CreateField.Text = "Создать поле";
            this.CreateField.UseVisualStyleBackColor = true;
            this.CreateField.Click += new System.EventHandler(this.CreateField_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Размер поля";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(770, 499);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 42);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Запустить";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreateField);
            this.Controls.Add(this.inputFieldSize);
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox inputFieldSize;
        private Button CreateField;
        private Label label1;
        private Button startButton;
    }
}

