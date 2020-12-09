using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Peep
{
    public partial class PictureView : UserControl
    {
        public class FilenameChangedEventArgs : EventArgs
        {
            public string Filename;
        }
        public delegate void FilenameChangedDelegate(object sender, FilenameChangedEventArgs e);
        public event FilenameChangedDelegate FilenameChanged;

        public PictureView()
        {
            InitializeComponent();
            this.MouseWheel += PictureView_MouseWheel;
        }

        Bitmap bitmap;
        public Bitmap Bitmap { 
            get => bitmap;
            set {
                this.bitmap = value;
                Invalidate();
            }
        }


        Matrix m = new Matrix();
        private bool isDragging = false;
        private Point lastLocation;
        private bool isFilteringEnabled = true;
        public bool IsFilteringEnabled {
            get {
                return isFilteringEnabled;
            }
            set {
                isFilteringEnabled = value;
                Invalidate();
            }
        }

        private void PictureView_Paint(object sender, PaintEventArgs e)
        {
            if (this.DesignMode) return;
            if (this.bitmap == null) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.ResetTransform();
            e.Graphics.InterpolationMode = isFilteringEnabled ? InterpolationMode.HighQualityBilinear : InterpolationMode.NearestNeighbor;
            e.Graphics.Transform = m;
            RectangleF srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            RectangleF dstRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            e.Graphics.DrawImage(this.bitmap, dstRect, srcRect, GraphicsUnit.Pixel);
        }

        bool hasUserAdjustedTheView = false;
        private void PictureView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control) {
                double factor = 1.0;
                if (e.Delta > 0) {
                    factor = 1.0 + (e.Delta / 1000.0);
                } else if (e.Delta < 0) {
                    factor = 1.0 / (1.0 + (-e.Delta / 1000.0));
                }
                // First translate this matrix so that the location is at the origin.
                m.Translate(-e.X, -e.Y, MatrixOrder.Append);
                m.Scale((float)factor, (float)factor, MatrixOrder.Append);
                m.Translate(e.X, e.Y, MatrixOrder.Append);
                hasUserAdjustedTheView = true;
                Invalidate();
            }
        }

        internal void ResetTransform()
        {
            if (bitmap == null) return;
            // center the image
            float scale = 1.0f;
            float dx = 0.0f;
            float dy = 0.0f;
            if (bitmap.Width * Height > Width * bitmap.Height ) {
                // content is wider than container, fit to width
                scale = Width / (float)bitmap.Width;
                dx = 0.0f;
                // and center vertically
                dy = (Height - bitmap.Height * scale) / 2.0f;
            } else {
                // content is taller than container, fit to height
                scale = Height / (float)bitmap.Height;
                dy = 0.0f;
                // and center horizontally
                dx = (Width - bitmap.Width * scale) / 2.0f;
            }
            m.Reset();
            m.Scale(scale, scale, MatrixOrder.Append);
            m.Translate(dx, dy, MatrixOrder.Append);
            hasUserAdjustedTheView = false;

            Invalidate();
        }

        internal void OneToOne()
        {
            if (bitmap == null) return;
            // center the image
            float scale = 1.0f;
            float dx = (float)Math.Floor((Width - bitmap.Width * scale) / 2.0f);
            float dy = (float)Math.Floor((Height - bitmap.Height * scale) / 2.0f);
            m.Reset();
            m.Scale(scale, scale, MatrixOrder.Append);
            m.Translate(dx, dy, MatrixOrder.Append);
            hasUserAdjustedTheView = true;

            Invalidate();
        }

        private void PictureView_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void PictureView_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;

        }

        private void PictureView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging) {
                m.Translate(e.X - lastLocation.X, e.Y - lastLocation.Y, MatrixOrder.Append);
                lastLocation = e.Location;
                hasUserAdjustedTheView = true;
                Invalidate();
            }
        }

        private void PictureView_Resize(object sender, EventArgs e)
        {
            Invalidate();
            if (!hasUserAdjustedTheView) {
                ResetTransform();
            }
        }

        private void PictureView_DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("PictureView_DragEnter");
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.All;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PictureView_DragDrop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("PictureView_DragDrop");
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s.Length > 0) {
                if (this.FilenameChanged != null) {
                    this.FilenameChanged(this, new FilenameChangedEventArgs() { Filename = s[0] });
                }
            }
        }
    }
}
