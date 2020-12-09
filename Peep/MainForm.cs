using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peep
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.pictureView1.FilenameChanged += PictureView1_FilenameChanged;
        }

        private void PictureView1_FilenameChanged(object sender, PictureView.FilenameChangedEventArgs e)
        {
            Filename = e.Filename;
        }

        string filename;
        string path;
        Bitmap bitmap;
        public string Filename {
            get => filename;
            set {
                Unwatch();
                Unload();
                filename = value;
                path = Path.GetFullPath(filename);
                WatchBitmap();
                LoadBitmap();
                this.pictureView1.ResetTransform();
            }
        }

        void Unwatch()
        {
            if (this.watcher != null) {
                this.watcher.EnableRaisingEvents = false;
                this.watcher = null;
            }
        }
        void Unload()
        {
            if (this.bitmap != null) {
                this.bitmap = null;
            }
        }

        private void LoadBitmap()
        {
            Bitmap theNewBitmap = null;
            try {
                // Set the title to the path
                this.Text = path;
                var bytes = File.ReadAllBytes(path);
                var ms = new MemoryStream(bytes);
                theNewBitmap = new Bitmap(Image.FromStream(ms));
            } catch (Exception) {
                return;
            }
            if (theNewBitmap != null) {
                Unload();
                this.bitmap = theNewBitmap;
                this.pictureView1.Bitmap = theNewBitmap;
            }
        }

        FileSystemWatcher watcher;
        private Rectangle savedBounds;

        private void WatchBitmap()
        {
            Unwatch();
            string directory = Path.GetDirectoryName(path);
            watcher = new FileSystemWatcher();
            watcher.Path = directory;
            watcher.Filter = Path.GetFileName(path);
            watcher.Changed += watcher_Changed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Renamed += Watcher_Renamed;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            LoadBitmap();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            LoadBitmap();
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            //LoadBitmap();
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e) 
        {
            // reload the file
            LoadBitmap();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D0) {
                this.pictureView1.ResetTransform();
            } else if (keyData == Keys.D1) {
                this.pictureView1.OneToOne();
            } else if (keyData == Keys.F10) {
                this.pictureView1.IsFilteringEnabled = !this.pictureView1.IsFilteringEnabled;
                return true;
            } else if (keyData == Keys.F11) {
                ToggleFullScreen();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ToggleFullScreen()
        {
            if (IsFullScreen()) {
                // Go back to normal
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.Bounds = savedBounds;
            } else {
                // Go Fullscreen!
                // save the current bounds
                savedBounds = this.Bounds;
                this.FormBorderStyle = FormBorderStyle.None;
                // Figure out which screen we're on
                int maxArea = 0;
                Screen bestScreen = null;
                foreach (var screen in Screen.AllScreens) {
                    Rectangle intersection = this.Bounds;
                    intersection.Intersect(screen.Bounds);
                    int area = intersection.Width * intersection.Height;
                    if (area > maxArea) {
                        bestScreen = screen;
                        maxArea = area;
                    }
                }
                if (bestScreen != null) {
                    // fill this screen
                    this.Bounds = bestScreen.Bounds;
                }
            }
            

        }

        private bool IsFullScreen()
        {
            return (this.FormBorderStyle == FormBorderStyle.None);
        }

        //protected override void On(PreviewKeyDownEventArgs e)
        //{
        //    if (e.KeyCode == Keys.D0) {
        //        this.pictureView1.ResetTransform();
        //    }
        //    base.OnPreviewKeyDown(e);
        //}

    }
}
