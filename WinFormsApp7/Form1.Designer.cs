namespace ServerApp
{
    partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.ListBox listBox1;

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
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.SuspendLayout();
        // 
        // listBox1
        // 
        this.listBox1.FormattingEnabled = true;
        this.listBox1.ItemHeight = 16;
        this.listBox1.Location = new System.Drawing.Point(12, 12);
        this.listBox1.Name = "listBox1";
        this.listBox1.Size = new System.Drawing.Size(776, 420);
        this.listBox1.TabIndex = 0;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.listBox1);
        this.Name = "Form1";
        this.Text = "Servidor";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.ResumeLayout(false);
    }
}
}