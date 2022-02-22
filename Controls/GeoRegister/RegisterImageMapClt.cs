using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleMap.Controls.GeoRegister
{
    public partial class RegisterImageMapClt : UserControl
    {
        public RegisterImageMapClt()
        {
            InitializeComponent();
            // Synchronize some private members with the form's values.
            _ZoomFactor = trbZoomFactor.Value;
            _BackColor = picImage.BackColor;

            // Set the sizemode of both pictureboxes. These modes are important
            // to the functionality and should not be changed.
            picImage.SizeMode = PictureBoxSizeMode.CenterImage;
            picZoom.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        ~RegisterImageMapClt()
        {
            ControlClosing();
        }

        #region SetVariables
        public string Worldfile { get; set; }
        public int xsize { get; set; }
        public int ysize { get; set; }

        public ImageRegistration imageRegistration = new ImageRegistration();
        /// <summary>
        /// Stores the zoomfactor of the picZoom picturebox
        /// </summary>
        private int _ZoomFactor;
        /// <summary>
        /// Stores the color used to fill any areas not covered by an image
        /// </summary>
        private Color _BackColor;
        /// <summary>
        /// Stores an instance of the originally loaded image
        /// </summary>
        private Image _OriginalImage;
        /// <summary>
        /// Stores an path of the originally loaded image
        /// </summary>
        private string _OriginalImageFileName;
        #endregion

        private void btnCalc_Click(object sender, EventArgs e)
        {
            double lat1, lat2;
            double lon1, lon2;

            double.TryParse(Lat_TL.Text, out lat1);
            //if (lat1neg.Text != "N")
            //    lat1 = 0 - lat1;

            double.TryParse(Lon_TL.Text, out lon1);
            //if (lon1neg.Text != "W")
            //    lon1 = 0 - lon1;

            double.TryParse(Lat_BR.Text, out lat2);
            //if (lat2neg.Text != "N")
            //    lat2 = 0 - lat2;

            double.TryParse(Lon_BR.Text, out lon2);
            //if (lon1neg.Text != "W")
            //    lon2 = 0 - lon2;

            int xsize, ysize;
            int.TryParse(txtxsize.Text, out xsize);
            int.TryParse(txtysize.Text, out ysize);

            if (lon1 < lon2)
            {
                var t = +lon1;
                lon1 = lon2;
                lon2 = t;
            }
            var ppx = (lon1 - lon2) / xsize;
            if (lat1 > lat2)
            {
                var t = +lat1;
                lat1 = lat2;
                lat2 = t;
            }
            var ppy = (lat1 - lat2) / ysize;
            lon2 += (ppx / 2); // x center of pixel
            lat2 += (ppy / 2); // y center of pixel
            var wf = ppx.ToString() + "\n" +
                    "0.00000" + "\n" + 
                    "0.00000" + "\n" +
                    ppy.ToString() + "\n" +
                    lon2.ToString() + "\n" +
                    lat2.ToString();

            txtWorldfile.Text = ppx.ToString() + "\n" +
                    "0.00000" + "\n" +
                    "0.00000" + "\n" +
                    ppy.ToString() + "\n" +
                    lon2.ToString() + "\n" +
                    lat2.ToString();

            Worldfile = wf;
        }

        private void btnCacheAllMap_Click(object sender, EventArgs e)
        {
            string str = Path.GetFileNameWithoutExtension(_OriginalImageFileName);
            
            string fileName = str + ".wld";
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using (StreamWriter fs = File.CreateText(fileName))
            {
                fs.Write(Worldfile);
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.AddExtension = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Supported Image File|*.jpg;*.jpeg;*.bmp;*.png;*.dib;*.gif";
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Multiselect = false;
            openFileDialog.ReadOnlyChecked = false;
            openFileDialog.ShowHelp = true;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.SupportMultiDottedExtensions = true;
            openFileDialog.Title = "Select an image..";
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _OriginalImage = Image.FromFile(openFileDialog.FileName);
                    _OriginalImageFileName = openFileDialog.FileName;
                    ResizeAndDisplayImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured loading the image " +
                                    openFileDialog.FileName + "\r\n" +
                                    ex.Message +
                                    "Please ensure you select a supported image type.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

            openFileDialog.Dispose();
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.AllowFullOpen = true;
            colorDialog.AnyColor = true;
            colorDialog.Color = _BackColor;
            colorDialog.FullOpen = true;
            colorDialog.ShowHelp = true;
            colorDialog.SolidColorOnly = false;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _BackColor = colorDialog.Color;
                ResizeAndDisplayImage();
            }

            colorDialog.Dispose();
        }

        private void trbZoomFactor_ValueChanged(object sender, EventArgs e)
        {
            _ZoomFactor = trbZoomFactor.Value;
            lblZoomFactor.Text = string.Format("x{0}", _ZoomFactor);
        }

        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            // If no picture is loaded, return
            if (picImage.Image == null)
                return;

            UpdateZoomedImage(e);
        }

        #region functions
        public void ControlClosing()
        {
            //Stop background threads on deconstructor
            mapCtl_GeoRef.ControlClosing();
        }
        private void rbFullRectangle_CheckedChanged(object sender, EventArgs e)
        {
            showcontrols();
        }
        void showcontrols()
        {
            bool vis = rb4Pt_AffineRotation.Checked;
            rbBL.Visible = vis; ;
            Lat_BL.Visible = vis;
            Lon_BL.Visible = vis;
            rbTR.Visible = vis;
            Lat_TR.Visible = vis;
            Lon_TR.Visible = vis;
        }

        /// <summary>
        /// Resizes the image stored in _OriginalImage to fit in picImage,
        /// maintaining the aspect ratios and displays it.
        /// </summary>
        private void ResizeAndDisplayImage()
        {
            // Set the backcolor of the pictureboxes
            picImage.BackColor = _BackColor;
            picZoom.BackColor = _BackColor;

            // If _OriginalImage is null, then return. This situation can occur
            // when a new backcolor is selected without an image loaded.
            if (_OriginalImage == null)
                return;

            picImage.Image = _OriginalImage;

            // sourceWidth and sourceHeight store the original image's width and height
            // targetWidth and targetHeight are calculated to fit into the picImage picturebox.
            int sourceWidth = _OriginalImage.Width;
            int sourceHeight = _OriginalImage.Height;
            int targetWidth;
            int targetHeight;
            double ratio;

            // Calculate targetWidth and targetHeight, so that the image will fit into
            // the picImage picturebox without changing the proportions of the image.
            if (sourceWidth > sourceHeight)
            {
                // Set the new width
                targetWidth = picImage.Width;
                // Calculate the ratio of the new width against the original width
                ratio = (double)targetWidth / sourceWidth;
                // Calculate a new height that is in proportion with the original image
                targetHeight = (int)(ratio * sourceHeight);
            }
            else if (sourceWidth < sourceHeight)
            {
                // Set the new height
                targetHeight = picImage.Height;
                // Calculate the ratio of the new height against the original height
                ratio = (double)targetHeight / sourceHeight;
                // Calculate a new width that is in proportion with the original image
                targetWidth = (int)(ratio * sourceWidth);
            }
            else
            {
                // In this case, the image is square and resizing is easy
                targetHeight = picImage.Height;
                targetWidth = picImage.Width;
            }

            // Calculate the targetTop and targetLeft values, to center the image
            // horizontally or vertically if needed
            int targetTop = (picImage.Height - targetHeight) / 2;
            int targetLeft = (picImage.Width - targetWidth) / 2;

            // Create a new temporary bitmap to resize the original image
            // The size of this bitmap is the size of the picImage picturebox.
            Bitmap tempBitmap = new Bitmap(picImage.Width, picImage.Height, PixelFormat.Format24bppRgb);

            // Set the resolution of the bitmap to match the original resolution.
            tempBitmap.SetResolution(_OriginalImage.HorizontalResolution, _OriginalImage.VerticalResolution);

            // Create a Graphics object to further edit the temporary bitmap
            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // First clear the image with the current backcolor
            bmGraphics.Clear(_BackColor);

            // Set the interpolationmode since we are resizing an image here
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the original image on the temporary bitmap, resizing it using
            // the calculated values of targetWidth and targetHeight.
            bmGraphics.DrawImage(_OriginalImage,
                                 new Rectangle(targetLeft, targetTop, targetWidth, targetHeight),
                                 new Rectangle(0, 0, sourceWidth, sourceHeight),
                                 GraphicsUnit.Pixel);

            // Dispose of the bmGraphics object
            bmGraphics.Dispose();

            // Set the image of the picImage picturebox to the temporary bitmap
            picImage.Image = tempBitmap;
        }

        /// <summary>
        /// Updates the picZoom image to show the portion of the main image
        /// the mouse is currently over.
        /// </summary>
        private void UpdateZoomedImage(MouseEventArgs e)
        {
            // Calculate the width and height of the portion of the image we want
            // to show in the picZoom picturebox. This value changes when the zoom
            // factor is changed.
            int zoomWidth = picZoom.Width / _ZoomFactor;
            int zoomHeight = picZoom.Height / _ZoomFactor;

            // Calculate the horizontal and vertical midpoints for the crosshair
            // cursor and correct centering of the new image
            int halfWidth = zoomWidth / 2;
            int halfHeight = zoomHeight / 2;

            // Create a new temporary bitmap to fit inside the picZoom picturebox
            Bitmap tempBitmap = new Bitmap(zoomWidth, zoomHeight, PixelFormat.Format24bppRgb);

            // Create a temporary Graphics object to work on the bitmap
            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // Clear the bitmap with the selected backcolor
            bmGraphics.Clear(_BackColor);

            // Set the interpolation mode
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the portion of the main image onto the bitmap
            // The target rectangle is already known now.
            // Here the mouse position of the cursor on the main image is used to
            // cut out a portion of the main image.
            bmGraphics.DrawImage(picImage.Image,
                                 new Rectangle(0, 0, zoomWidth, zoomHeight),
                                 new Rectangle(e.X - halfWidth, e.Y - halfHeight, zoomWidth, zoomHeight),
                                 GraphicsUnit.Pixel);

            // Draw the bitmap on the picZoom picturebox
            picZoom.Image = tempBitmap;

            // Draw a crosshair on the bitmap to simulate the cursor position
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight - 4, halfWidth + 1, halfHeight - 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight + 6, halfWidth + 1, halfHeight + 3);
            bmGraphics.DrawLine(Pens.Black, halfWidth - 4, halfHeight + 1, halfWidth - 1, halfHeight + 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 6, halfHeight + 1, halfWidth + 3, halfHeight + 1);

            // Dispose of the Graphics object
            bmGraphics.Dispose();

            // Refresh the picZoom picturebox to reflect the changes
            picZoom.Refresh();
        }
        #endregion

        private void picImage_Click(object sender, EventArgs e)
        {
            if (picImage.Image == null) return;

            MouseEventArgs mouse = (MouseEventArgs)e;
            Point coordinates = mouse.Location;

            using (Graphics g = Graphics.FromImage(picImage.Image))
            {
                Pen p = new Pen(Color.Red, 2.0f);
                Size dotsize = new Size(new Point(2, 2));

                g.DrawEllipse(p, new Rectangle(coordinates, dotsize));

            }

            if (rbTL.Checked)
            {
                imageRegistration.TopLeft = coordinates;

                txtTL_X.Text = imageRegistration.TopLeft.X.ToString();
                txtTL_Y.Text = imageRegistration.TopLeft.Y.ToString();

                rbBL.Checked = true;
            }

            if (rbBL.Checked)
            {
                imageRegistration.BottomLeft = coordinates;

                txtBL_X.Text = imageRegistration.BottomLeft.X.ToString();
                txtBL_Y.Text = imageRegistration.BottomLeft.Y.ToString();
                rbTR.Checked = true;
            }

            if (rbTR.Checked)
            {
                imageRegistration.TopRight = coordinates;
                txtTR_X.Text = imageRegistration.TopRight.X.ToString();
                txtTR_Y.Text = imageRegistration.TopRight.Y.ToString();
                rbBR.Checked = true;
            }

            if (rbBR.Checked)
            {
                imageRegistration.BottomRight = coordinates;
                txtBR_X.Text = imageRegistration.BottomRight.X.ToString();
                txtBR_Y.Text = imageRegistration.BottomRight.Y.ToString();
                rbTL.Checked = true;
            }
        }

        private void picImage_Paint(object sender, PaintEventArgs e)
        {
            //if (IsDrawRect) // Flag Variable to check if need to draw rect
            //{

            //    Rectangle RectMark = new Rectangle(startX, StartY, Hieght, Widht); // your location to draw
            //    e.Graphics.DrawRectangle(new Pen(Color.Red, 1), RectMark);
            //}

            //using (Graphics g = picImage.CreateGraphics())
            //{
            //    g.Clear(Color.Transparent);
            //    Pen p = new Pen(Color.Red, 2.0f);

            //    for (int n = 1; n <= 50; n++)
            //    {
            //        g.DrawLine(p, n * (Cursor.Position.X), Cursor.Position.Y - 30.0f, n * (Cursor.Position.X), Cursor.Position.Y + 30.0f);

            //    }
            //}
        }
        public class ImageRegistration
        {
            public Point TopLeft { get; set; }
            public Point BottomLeft { get; set; }
            public Point TopRight { get; set; }
            public Point BottomRight { get; set; }

        }

        private void mapCtl_GeoRef_Load(object sender, EventArgs e)
        {

        }

        private void mapCtl_GeoRef_MouseClick(object sender, MouseEventArgs e)
        {
            Map.GeomCoordinate coord = mapCtl_GeoRef.CursorCoordinate;

            string Lat = txtLat.Text = coord.Latitude.ToString();
            string Lon = txtLon.Text = coord.Longitude.ToString();

            if (rbTL.Checked)
            {
                Lat_TL.Text = Lat;
                Lon_TL.Text = Lon;
                if (rb4Pt_AffineRotation.Checked)
                    rbBL.Checked = true;
                else
                    rbBR.Checked = true;
                return;
            }

            if (rbBL.Checked)
            {
                Lat_BL.Text = Lat;
                Lon_BL.Text = Lon;
                if (rb4Pt_AffineRotation.Checked)
                    rbTR.Checked = true;
                else
                    rbBR.Checked = true;
                return;
            }

            if (rbTR.Checked)
            {
                Lat_TR.Text = Lat;
                Lon_TR.Text = Lon;
                rbBR.Checked = true;
                return;
            }

            if (rbBR.Checked)
            {
                Lat_BR.Text = Lat;
                Lon_BR.Text = Lon;
                rbBR.Checked = true;
                return;
            }
        }

        private void mapCtl_GeoRef_MouseMove(object sender, MouseEventArgs e)
        {
            Map.GeomCoordinate coord = mapCtl_GeoRef.CursorCoordinate;
            try
            {
                txtLat.Text = coord.Latitude.ToString();
                txtLon.Text = coord.Longitude.ToString();
            }
            catch
            {
                //Shutting down
            }
        }
    }
}
