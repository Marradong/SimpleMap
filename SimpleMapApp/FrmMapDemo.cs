using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using SimpleMap;
using SimpleMap.Controls;

namespace SimpleMapApp
{
    public partial class FrmMapDemo : Form
    {
        public FrmMapDemo()
        {
            InitializeComponent();

            Icon = Miscellaneous.DefaultIcon();
        }
        
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var handleParam = base.CreateParams;
        //        handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
        //        return handleParam;
        //    }
        //}

        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }

        private void FrmOpticMap_Load(object sender, EventArgs e)
        {
            Text = Miscellaneous.GetAssemblyTitle();
        }

        private void FrmOpticMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            mapCtl1.ControlClosing();
        }

        public void RefreshForm()
        {
            mapCtl1.RefreshControl();
        }

        public void SetCenterMapObject(decimal longitude, decimal latitude)
        {
            //var coordinate = Coordinate.GetCoordinateFromScreen(_netLayer.ScreenView, location);

            //CenterCoordinate = Settings.CenterMapBound;

           /*
            var longitude1 = Convert.ToDecimal(Settings.Default.LeftMapBound + (double)rnd.Next(0, rangeX) / 100000);
            var latitude1 = Convert.ToDecimal(Settings.Default.BottomMapBound + (double)rnd.Next(0, rangeY) / 100000);

            mapCtl1.MoveCenterMapObject(longitude, latitude); //

            decimal lat = -31.95194;
            decimal lon = 115.88267;
            mapCtl1.MoveCenterMapObject(115.88267, -31.95194);
           */
        }

        private void buttonPanelCtl1_RefreshMapClicked(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void buttonPanelCtl1_LevelValueChanged(object sender, ButtonPanelCtl.LevelValueArgs e)
        {
            mapCtl1.Level = e.Level;
        }

        private void buttonPanelCtl1_CenterMapClicked(object sender, EventArgs e)
        {
            mapCtl1.SetCenterMap();
        }

        public int Level
        {
            set { buttonPanelCtl1.Level = value; }
        }

        private void mapCtl1_LevelValueChanged(object sender, ButtonPanelCtl.LevelValueArgs e)
        {
            buttonPanelCtl1.Level = e.Level;
        }


        private void buttonPanelCtl1_PrintMapClicked(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        void Document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            try
            {
                var imageMap = mapCtl1.GetMapImageForPrint();
                var printSize = e.PageBounds.Size;
                var k1 = (double)imageMap.Width / printSize.Width;
                var k2 = (double)imageMap.Height / printSize.Height;
                var k = (k1 > k2) ? k1 : k2;
                var newSize = new Size((int)(imageMap.Size.Width / k), (int)(imageMap.Size.Height / k));

                var screnCenter = new Point(printSize.Width / 2, printSize.Height / 2);
                var mapCenter = new Point(newSize.Width / 2, newSize.Height / 2);
                var shift = new Size(screnCenter.X - mapCenter.X, screnCenter.Y - mapCenter.Y);
                var p = new Point(0, 0) + shift;

                var rectangle = new Rectangle(p, newSize);
                e.Graphics.DrawImage(imageMap, rectangle);
            }
            catch (Exception ex)
            {
                //do nothing
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void buttonPanelCtl1_SaveAllMapClicked(object sender, EventArgs e)
        {
            FrmMapDownloader.DownloadMap();
        }

        private void buttonPanelCtl1_SaveMapAsImageClicked(object sender, EventArgs e)
        {
            FrmMapDownloader.SaveMapAsImage(mapCtl1.PiFormat, mapCtl1.Level);
        }

        private void buttonPanelCtl1_Load(object sender, EventArgs e)
        {

        }

        private void buttonPanelCtl1_LoadGeoRefImageClicked(object sender, EventArgs e)
        {
            

            // SetEnvelope(fileName);
            //Image.FromFile(fileName)
            string statupPath = System.Windows.Forms.Application.StartupPath;
            string pathToMaps = Path.Combine(statupPath, "Maps");


            //Load and georeferenced Images then upload to mapservder so tile come back with georeferenced layers from server?.
            
            
            //Test  loading a georeferenced image 
            
            string GeorefImage = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Grid.jpg");
            //mapCtl1.GraphicLayer
            mapCtl1.LoadGeoRefImage(GeorefImage, "TestGeoRefLayer");

        }

        private void mapCtl1_Load(object sender, EventArgs e)
        {
            mapCtl1.SetCenterMap();
        }
    }
}
