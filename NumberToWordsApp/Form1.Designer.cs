namespace NumberToWordsApp
{
    partial class Form1
    {
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.InNumbertxtBox = new System.Windows.Forms.TextBox();
            this.Translatebtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.OpenDocxbtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Replacebtn = new System.Windows.Forms.Button();
            this.OutWordstxtBox = new System.Windows.Forms.TextBox();
            this.ReplaceAllDocbtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьДокументToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заменитьВсеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InNumbertxtBox
            // 
            this.InNumbertxtBox.Location = new System.Drawing.Point(495, 44);
            this.InNumbertxtBox.Name = "InNumbertxtBox";
            this.InNumbertxtBox.Size = new System.Drawing.Size(169, 22);
            this.InNumbertxtBox.TabIndex = 0;
            // 
            // Translatebtn
            // 
            this.Translatebtn.Location = new System.Drawing.Point(670, 42);
            this.Translatebtn.Name = "Translatebtn";
            this.Translatebtn.Size = new System.Drawing.Size(314, 34);
            this.Translatebtn.TabIndex = 1;
            this.Translatebtn.Text = "Перевести в словесную форму";
            this.Translatebtn.UseVisualStyleBackColor = true;
            this.Translatebtn.Click += new System.EventHandler(this.Translatebtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 110);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(972, 471);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDoubleClick);
            // 
            // OpenDocxbtn
            // 
            this.OpenDocxbtn.Location = new System.Drawing.Point(12, 44);
            this.OpenDocxbtn.Name = "OpenDocxbtn";
            this.OpenDocxbtn.Size = new System.Drawing.Size(115, 32);
            this.OpenDocxbtn.TabIndex = 3;
            this.OpenDocxbtn.Text = "Открыть файл";
            this.OpenDocxbtn.UseVisualStyleBackColor = true;
            this.OpenDocxbtn.Click += new System.EventHandler(this.OpenDocxbtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Replacebtn
            // 
            this.Replacebtn.Location = new System.Drawing.Point(377, 44);
            this.Replacebtn.Name = "Replacebtn";
            this.Replacebtn.Size = new System.Drawing.Size(111, 32);
            this.Replacebtn.TabIndex = 5;
            this.Replacebtn.Text = "Заменить";
            this.Replacebtn.UseVisualStyleBackColor = true;
            this.Replacebtn.Click += new System.EventHandler(this.Replacebtn_Click);
            // 
            // OutWordstxtBox
            // 
            this.OutWordstxtBox.Location = new System.Drawing.Point(12, 82);
            this.OutWordstxtBox.Name = "OutWordstxtBox";
            this.OutWordstxtBox.Size = new System.Drawing.Size(972, 22);
            this.OutWordstxtBox.TabIndex = 7;
            // 
            // ReplaceAllDocbtn
            // 
            this.ReplaceAllDocbtn.Location = new System.Drawing.Point(133, 44);
            this.ReplaceAllDocbtn.Name = "ReplaceAllDocbtn";
            this.ReplaceAllDocbtn.Size = new System.Drawing.Size(238, 32);
            this.ReplaceAllDocbtn.TabIndex = 8;
            this.ReplaceAllDocbtn.Text = "Заменить все числа в тексте";
            this.ReplaceAllDocbtn.UseVisualStyleBackColor = true;
            this.ReplaceAllDocbtn.Click += new System.EventHandler(this.ReplaceAllDocbtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.оПрограммеToolStripMenuItem,
            this.выходToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(997, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьДокументToolStripMenuItem,
            this.заменитьВсеToolStripMenuItem,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьДокументToolStripMenuItem
            // 
            this.открытьДокументToolStripMenuItem.Name = "открытьДокументToolStripMenuItem";
            this.открытьДокументToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.открытьДокументToolStripMenuItem.Text = "Открыть документ";
            this.открытьДокументToolStripMenuItem.Click += new System.EventHandler(this.открытьДокументToolStripMenuItem_Click);
            // 
            // заменитьВсеToolStripMenuItem
            // 
            this.заменитьВсеToolStripMenuItem.Name = "заменитьВсеToolStripMenuItem";
            this.заменитьВсеToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.заменитьВсеToolStripMenuItem.Text = "Заменить все";
            this.заменитьВсеToolStripMenuItem.Click += new System.EventHandler(this.заменитьВсеToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem1
            // 
            this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
            this.выходToolStripMenuItem1.Size = new System.Drawing.Size(67, 24);
            this.выходToolStripMenuItem1.Text = "Выход";
            this.выходToolStripMenuItem1.Click += new System.EventHandler(this.выходToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 593);
            this.Controls.Add(this.ReplaceAllDocbtn);
            this.Controls.Add(this.OutWordstxtBox);
            this.Controls.Add(this.Replacebtn);
            this.Controls.Add(this.OpenDocxbtn);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Translatebtn);
            this.Controls.Add(this.InNumbertxtBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перевод числа в словесную форму";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InNumbertxtBox;
        private System.Windows.Forms.Button Translatebtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button OpenDocxbtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Replacebtn;
        private System.Windows.Forms.TextBox OutWordstxtBox;
        private System.Windows.Forms.Button ReplaceAllDocbtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьДокументToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заменитьВсеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
    }
}

