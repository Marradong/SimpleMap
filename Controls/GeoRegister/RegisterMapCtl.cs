using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace SimpleMap.Controls
{
    public partial class RegisterMapCtl : UserControl
    {

        public string Worldfile { get; set; }

        public string txtxsize { get; set; }
        public string txtysize { get; set; }
        //int xsize, ysize;
        //int.TryParse(txtxsize.Text, out xsize);
        //int.TryParse(txtysize.Text, out ysize);

        public int xsize { get; set; }
        public int ysize { get; set; }


        public RegisterMapCtl()
        {
            InitializeComponent();

            // mapCtl2.ControlClosing();
        }

        ~RegisterMapCtl()
        {
            ControlClosing();
        }

        public void ControlClosing()
        {
            //Stop background threads on deconstructor
            
            mapCtl_GeoRef.ControlClosing();

        }


            private void rbTL_BR_CheckedChanged(object sender, EventArgs e)
        {
            
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
        /*
         * 
         * 
            Suppose you know that pixel (50, 75) is at (8.3, 2.3) degrees and pixel (250, 900) is at (12.3, 6.3) degrees.

            Then your pixel width W is (12.3-8.3)/(250-50) and your pixel height H is (6.3 - 2.3)/(900-75)

            Then your top left corner (centre of pixel (0,0)) is at (8.3 - 50*W, 2.3 - 75*H) - i.e. its 50 pixel widths left of the first reference pixel, and 75 pixel heights above it).

            Your bottom right corner pixel centre is at top-left + (W * Number of columns), (H * Number of rows) - i.e. its that number of pixel widths across and down.

            Those calculations give you pixel centres. Divide the width and height of the image in ground units by the number of height and width columns to get a pixel width and height, and then add or subtract half of one of those from your corner pixel centres to get pixel edges. Now you have the bounding box.

            Now you can write a standard "World file" or other simple georeferencer.

            Note this depends on your image being properly rectified and in the correct coordinate system, 
            and north-up, and not subject to any distortions due to camera angle or field of view. 
            But with two reference points you can't do any better.


        */

        /*
            If image is not rotated and already rectified (scanned map etc.) then two points is enough. 
            Use for example gdal.org/gdal_translate.html with the option -a_ullr ulx uly lrx lry: 
            Assign/override the georeferenced bounds of the output file. 
            This assigns georeferenced bounds to the output file, ignoring what would have been derived from the source file. 
            So this does not cause reprojection to the specified SRS. 
            If you know the coordinates of the top-left pixel and pixel size you can write an ESRI worldfile

        rasterbounds.cs
        RasterBounds : IRasterBounds
                    Affine = new double[6];
                    Affine[1] = NextValue(sr); // Dx
                    Affine[2] = NextValue(sr); // Skew X
                    Affine[4] = NextValue(sr); // Skew Y
                    Affine[5] = NextValue(sr); // Dy
                    Affine[0] = NextValue(sr); // Top Left X
                    Affine[3] = NextValue(sr); // Top Left Y
        
        
        /// <summary>
        /// Gets or sets the double affine coefficients that control the world-file
        /// positioning of this image. X' and Y' are real world coords.
        /// X' = [0] + [1] * Column + [2] * Row
        /// Y' = [3] + [4] * Column + [5] * Row
        /// </summary>


        */
        private void btnCalc_Click(object sender, EventArgs e)
        {
            //Flattening the Earth: Two Thousand Years of Map Projections by John P.Snyder
            //Non-projected lat - lon formated images via worldfile, Using SRS defined as EPSG id 4326

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
            int.TryParse(txtxsize, out xsize);
            int.TryParse(txtysize, out ysize);

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
                    "0.00000\n0.00000\n" +
                    ppy.ToString() + "\n" +
                    lon2.ToString() + "\n" +
                    lat2.ToString();

            txtWorldfile.Text = ppx.ToString() + "\n" +
                    "0.00000\n0.00000\n" +
                    ppy.ToString() + "\n" +
                    lon2.ToString() + "\n" +
                    lat2.ToString();

            Worldfile = wf;

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

        private void mapCtl_GeoRef_Load(object sender, EventArgs e)
        {

        } 
        
        private void mapCtl_GeoRef_MouseClick(object sender, MouseEventArgs e)
        {
            Map.GeomCoordinate coord = mapCtl_GeoRef.CursorCoordinate;

            string Lat = txtLat.Text = coord.Latitude.ToString();
            string Lon = txtLon.Text = coord.Longitude.ToString();

            if(rbTL.Checked)
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
    }
}