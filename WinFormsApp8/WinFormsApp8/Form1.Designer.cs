namespace ClientApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox emojiComboBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            listBox1 = new ListBox();
            emojiComboBox = new ComboBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.InfoText;
            textBox1.Location = new Point(8, 387);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(580, 23);
            textBox1.TabIndex = 0;
            textBox1.KeyPress += textBox1_KeyPress;
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // button1
            // 
            button1.ForeColor = Color.Blue;
            button1.Location = new Point(622, 387);
            button1.Name = "button1";
            button1.Size = new Size(66, 23);
            button1.TabIndex = 1;
            button1.Text = "Enviar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Blue;
            button2.Location = new Point(8, 359);
            button2.Name = "button2";
            button2.Size = new Size(73, 22);
            button2.TabIndex = 3;
            button2.Text = "Papu Señal";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            // 
            // button3
            // 
            button3.ForeColor = Color.Red;
            button3.Location = new Point(87, 359);
            button3.Name = "button3";
            button3.Size = new Size(95, 22);
            button3.TabIndex = 5;
            button3.Text = "Desconectar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            // 
            // button4
            // 
            button4.ForeColor = Color.Green;
            button4.Location = new Point(188, 359);
            button4.Name = "button4";
            button4.Size = new Size(95, 22);
            button4.TabIndex = 6;
            button4.Text = "Reconectar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(128, 128, 255);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 4);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(676, 349);
            listBox1.TabIndex = 2;
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // emojiComboBox
            // 
            emojiComboBox.FormattingEnabled = true;
            emojiComboBox.Location = new Point(594, 387);
            emojiComboBox.Name = "emojiComboBox";
            emojiComboBox.Size = new Size(22, 23);
            emojiComboBox.TabIndex = 4;
            emojiComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            emojiComboBox.SelectedIndexChanged += emojiComboBox_SelectedIndexChanged;
            emojiComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 192, 255);
            ClientSize = new Size(700, 422);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(emojiComboBox);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "ChaTic's";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
