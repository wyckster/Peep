namespace Peep
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureView1 = new Peep.PictureView();
            this.SuspendLayout();
            // 
            // pictureView1
            // 
            this.pictureView1.BackColor = System.Drawing.Color.Black;
            this.pictureView1.Bitmap = null;
            this.pictureView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureView1.Location = new System.Drawing.Point(0, 0);
            this.pictureView1.Name = "pictureView1";
            this.pictureView1.Size = new System.Drawing.Size(800, 450);
            this.pictureView1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureView1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Peep";
            this.ResumeLayout(false);

        }

        #endregion

        private PictureView pictureView1;
    }
}

