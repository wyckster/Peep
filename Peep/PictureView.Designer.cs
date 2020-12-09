﻿namespace Peep
{
    partial class PictureView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PictureView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.DoubleBuffered = true;
            this.Name = "PictureView";
            this.Size = new System.Drawing.Size(602, 539);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PictureView_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PictureView_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureView_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureView_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureView_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureView_MouseUp);
            this.Resize += new System.EventHandler(this.PictureView_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
