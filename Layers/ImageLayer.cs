using SimpleMap.Framework;
using SimpleMap.Framework.WorkerThread;
using SimpleMap.Framework.WorkerThread.Types;
using SimpleMap.Map;
using SimpleMap.Map.Tile;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace SimpleMap.Layers
{
    public class ImageLayer : GraphicLayer
    {

        private Image _image;
        /// <summary>
        /// Gets or sets a value indicating the image to display
        /// </summary>
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                //_envelope = new Envelope(0, _image.Width, 0, _image.Height);
            }
        }

        private string _layerName;
        private string _imagefilename;
        private string _worldfilename;
       


        /// <summary>
        /// Gets or sets the name of the layer
        /// </summary>
        public string LayerName
        {
            get { return _layerName; }
            set { _layerName = value; }
        }

        [NonSerialized]
        private Image _tmpImage;

        [NonSerialized]
        public WorldFile worldFile = new WorldFile();

        private float _transparency;

        /// <summary>
        /// Gets or sets the <see cref="T:System.Drawing.Drawing2D.InterpolationMode"/> to use
        /// </summary>
        public  InterpolationMode InterpolationMode { get; set; }

        private Rectangle _imageView;
        private readonly Bitmap _emptyBlock;

        

        public ImageLayer(int width, int height, GeomCoordinate centerCoordinate, int level) : base(width, height, centerCoordinate, level)
        {
            
            // _emptyBlock = CreateCompatibleBitmap(null, TileBlock.BlockSize, TileBlock.BlockSize, PiFormat);
        }

        public void LoadGeoRefImage(string layerName, string ImagefileName)
        {
           
            if (File.Exists(ImagefileName))
            {
                InterpolationMode = InterpolationMode.HighQualityBicubic;
                _image = (Bitmap)Image.FromFile(ImagefileName);
                
                _imagefilename = ImagefileName;
                _layerName = layerName;

                worldFile.Load(ImagefileName);

               // base.CenterCoordinate
                base.Width = _image.Width;
                //_emptyBlock = CreateCompatibleBitmap(null, TileBlock.BlockSize, TileBlock.BlockSize, PiFormat);
                //int width, int height, GeomCoordinate centerCoordinate, int level) : base(width, height, centerCoordinate, level)
            }




        }

        /// <summary>
        /// Gets or sets a value indicating the transparency level
        /// </summary>
        public float Transparency
        {
            get { return _transparency; }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("value", "Must be in the range [0, 1]");

                _transparency = value;
            }
        }

        #region WorldFile

        public class WorldFile
        {
            public readonly Matrix2D matrix = new Matrix2D();
            public Matrix2D inverse;


            /// <summary>
            /// Create different world file extensions to try and open
            /// </summary>
            private static string CreateWorldFileExtension(string ext, int style = 0)
            {
                if (!ext.StartsWith(".")) ext = "." + ext;
                if (ext.Length != 4) return null;

                if (style == 1)
                {
                    //eg. .jpgw
                    ext += 'w';
                }

                if (style == 2)
                {
                    //e.g jgw
                    var caExt = ext.ToCharArray();
                    caExt[2] = caExt[3];
                    caExt[3] = 'w';
                    ext = caExt.ToString();
                }
                if (style == 3)
                {
                    //e.g jpw
                    var caExt = ext.ToCharArray();
                    caExt[3] = 'w';
                    ext = caExt.ToString();
                }
                if (style == 4)
                {
                    ext = ".wld";
                }

                return ext;

            }

            /// <summary>
            /// Creates an instance of this class
            /// </summary>
            /// <param name="a11">x-component of the pixel width</param>
            /// <param name="a21">y-component of the pixel width</param>
            /// <param name="a12">x-component of the pixel height</param>
            /// <param name="a22">y-component of the pixel height</param>
            /// <param name="b1">x-ordinate of the center of the top left pixel</param>
            /// <param name="b2">y-ordinate of the center of the top left pixel</param>
            public WorldFile(double a11 = 1d, double a21 = 0d, double a12 = 0d, double a22 = -1, double b1 = 0d,
                double b2 = 0d)
            {
                matrix.A11 = a11;
                matrix.A21 = a21;
                matrix.A12 = a12;
                matrix.A22 = a22;
                inverse = matrix.Inverse();

                B1 = b1;
                B2 = b2;
            }

            /// <summary>
            /// Loads a world file
            /// </summary>
            /// <param name="file">The filename</param>
            /// <exception cref="ArgumentNullException"/>
            /// <exception cref="ArgumentException"/>
            public void Load(string fileName)
            {
                string worldFile = "";

                for (int wldFileExtTypes = 1; wldFileExtTypes <= 4; wldFileExtTypes++)
                {
                    worldFile = Path.ChangeExtension(fileName, CreateWorldFileExtension(Path.GetExtension(fileName), wldFileExtTypes));
                    if (File.Exists(worldFile))
                    {
                        //Found a Worldfile
                        break;
                    }
                }

                if (string.IsNullOrEmpty(worldFile))
                {
                    //When worldfile is present the "image to world" transformation is performed

                    //Debug report on "World File" Missing 
                    string ext = Path.GetExtension(fileName);
                    string errormsg = string.Format("Image Worldfile (i.e {0} or {1} or {2} or {3}) not found so cannot Georeference image", CreateWorldFileExtension(ext, 1), CreateWorldFileExtension(ext, 2), CreateWorldFileExtension(ext, 3), CreateWorldFileExtension(ext, 4));
                    Debug.WriteLine(errormsg, "Raster Image - Georeference Worldfile Missing");

                    /*
                     * https://www.codeproject.com/articles/12963/geo-referencing-map-calibration
                     * 
                    The image-to-world transformation is a six-parameter affine transformation in the form of
	                
                    x1 = Ax + By + C
	                y1 = Dx + Ey + F


                    where
	                    x1 = calculated x-coordinate of the pixel on the map
	                    y1 =  calculated y-coordinate of the pixel on the map
	                    x = column number of a pixel in the image
	                    y = row number of a pixel in the image
	                    A = x-scale; dimension of a pixel in map units in x direction
	                    B, D = rotation terms, most cad programs cannot do rotations, so rotate the image in a program like Gimp to align it to north, and use the world file without the rotation parameters
	                    C, F = translation terms; x,y map coordinates of the center of the upper left pixel
	                    E = negative of y-scale; dimension of a pixel in map units in y direction 
                      
                     
                     *  Simpler form 
                     * http://wiki.gis.com/wiki/index.php/World_file
                        Line 1: A, pixel size in the x-direction in map units/ pixel
                        Line 2: D: rotation about y - axis
                        Line 3: B: rotation about x - axis
                        Line 4: E: pixel size in the y-direction in map units, almost always negative[3]
                        Line 5: C: x - coordinate of the center of the upper left pixel
                        Line 6: F: y - coordinate of the center of the upper left pixel

                        A UTM Example where 1 pixel = 32 meters
                        32.0
                        0.0
                        0.0
                        -32.0
                        691200.0
                        4576000.0

                    */
                    float PixelSizeX = 32;
                    float RotationX = 0;
                    float RotationY = 0;
                    float PixelSizeY = -32; //Mirrors image if not neg
                    float UtmX = 691200.0f;
                    float UtmY = 4576000.0f;
                    //UTM(grid) zone is not given so the coordinates are ambiguous — they can represent a position in any of the approx. 1200 UTM grid zones

                    matrix.A11 = PixelSizeX;
                    matrix.A21 = RotationX;
                    matrix.A12 = RotationY;
                    matrix.A22 = PixelSizeY;
                    B1 = UtmX;
                    B2 = UtmY;
                }
                else
                {

                    using (var sr = new StreamReader(worldFile))
                    {
                        matrix.A11 = double.Parse(sr.ReadLine(), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
                        matrix.A21 = double.Parse(sr.ReadLine(), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
                        matrix.A12 = double.Parse(sr.ReadLine(), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
                        matrix.A22 = double.Parse(sr.ReadLine(), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
                        B1 = double.Parse(sr.ReadLine(), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
                        B2 = double.Parse(sr.ReadLine(), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
                    }
                }
                inverse = matrix.Inverse();
            }

            /// <summary>
            /// Saves a world file
            /// </summary>
            /// <param name="file">The filename</param>
            /// <exception cref="ArgumentNullException"/>
            public void Save(string file)
            {
                if (string.IsNullOrEmpty(file))
                    throw new ArgumentNullException("file");

                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(A11.ToString("R", NumberFormatInfo.InvariantInfo));
                    sw.WriteLine(A21.ToString("R", NumberFormatInfo.InvariantInfo));
                    sw.WriteLine(A12.ToString("R", NumberFormatInfo.InvariantInfo));
                    sw.WriteLine(A22.ToString("R", NumberFormatInfo.InvariantInfo));
                    sw.WriteLine(B1.ToString("R", NumberFormatInfo.InvariantInfo));
                    sw.WriteLine(B2.ToString("R", NumberFormatInfo.InvariantInfo));
                }
            }

            /// <summary>
            /// x-component of the pixel width
            /// </summary>
            public double A11
            {
                get { return matrix.A11; }
            }

            /// <summary>
            /// y-component of the pixel width
            /// </summary>
            public double A21
            {
                get { return matrix.A21; }
            }

            /// <summary>
            /// x-component of the pixel height
            /// </summary>
            public double A12
            {
                get { return matrix.A12; }
            }

            /// <summary>
            /// y-component of the pixel height (negative most of the time)
            /// </summary>
            public double A22
            {
                get { return matrix.A22; }
            }

            /// <summary>
            /// x-ordinate of the center of the top left pixel
            /// </summary>
            public double B1 { get; private set; }

            /// <summary>
            /// y-ordinate of the center of the top left pixel
            /// </summary>
            public double B2 { get; private set; }

            /// <summary>
            /// Gets a value indicating the point (<see cref="B1"/>, <see cref="B2"/>).
            /// </summary>
            public GeomCoordinate Location
            {
                get { return new GeomCoordinate(B1, B2); }
            }

            /// <summary>
            /// Function to compute the ground coordinate for a given <paramref name="x"/>, <paramref name="y"/> pair.
            /// </summary>
            /// <param name="x">The x pixel</param>
            /// <param name="y">The y pixel</param>
            /// <returns>The ground coordinate</returns>
            public GeomCoordinate ToGround(int x, int y)
            {
                var resX = B1 + (A11 * x + A21 * y);
                var resY = B2 + (A12 * x + A22 * y);

                return new GeomCoordinate(resX, resY);
            }

            /// <summary>
            /// Function to compute the ground x-ordinate for a given <paramref name="x"/>, <paramref name="y"/> pair.
            /// </summary>
            /// <param name="x">The x pixel</param>
            /// <param name="y">The y pixel</param>
            /// <returns>The ground coordinate</returns>
            public double ToGroundX(int x, int y)
            {
                return B1 + (A11 * x + A21 * y);
            }

            /// <summary>
            /// Function to compute the ground y-ordinate for a given <paramref name="x"/>, <paramref name="y"/> pair.
            /// </summary>
            /// <param name="x">The x pixel</param>
            /// <param name="y">The y pixel</param>
            /// <returns>The ground coordinate</returns>
            public double ToGroundY(int x, int y)
            {
                return B2 + (A12 * x + A22 * y);
            }




            public Point ToRaster(ScreenCoordinate point)
            {
                point.X -= (long)B1;
                point.Y -= (long)B2;

                var x = (int)Math.Round(inverse.A11 * point.X + inverse.A21 * point.Y,
                    MidpointRounding.AwayFromZero);
                var y = (int)Math.Round(inverse.A12 * point.X + inverse.A22 * point.Y,
                    MidpointRounding.AwayFromZero);

                return new Point(x, y);
            }

            /*
            // get 4 corners of image
            public Coordinate[] GetFourCorners()
            {
                var points = new Coordinate[4];

                if (_gdalDataset != null)
                {
                    var geoTrans = new double[6];
                    _gdalDataset.GetGeoTransform(geoTrans);

                    // no rotation..use default transform
                    if (!_useRotation && !HaveSpot || (DoublesAreEqual(geoTrans[0], 0) && DoublesAreEqual(geoTrans[3], 0)))
                        geoTrans = new[] { 999.5, 1, 0, 1000.5, 0, -1 };

                    points[0] = new Coordinate(geoTrans[0], geoTrans[3]);
                    points[1] = new Coordinate(geoTrans[0] + (geoTrans[1] * _imageSize.Width),
                                             geoTrans[3] + (geoTrans[4] * _imageSize.Width));
                    points[2] = new Coordinate(geoTrans[0] + (geoTrans[1] * _imageSize.Width) + (geoTrans[2] * _imageSize.Height),
                                             geoTrans[3] + (geoTrans[4] * _imageSize.Width) + (geoTrans[5] * _imageSize.Height));
                    points[3] = new Coordinate(geoTrans[0] + (geoTrans[2] * _imageSize.Height),
                                             geoTrans[3] + (geoTrans[5] * _imageSize.Height));

                    // transform to map's projection
                    if (CoordinateTransformation != null)
                    {
                        for (var i = 0; i < 4; i++)
                        {
                            double[] dblPoint = CoordinateTransformation.MathTransform.Transform(new[] { points[i].X, points[i].Y });
                            points[i] = new Coordinate(dblPoint[0], dblPoint[1]);
                        }
                    }
                }

                return points;
            }
            */

            public int ToRasterX(ScreenCoordinate point)
            {
                point.X -= (long)B1;
                point.Y -= (long)B2;

                return (int)Math.Round(inverse.A11 * point.X + inverse.A21 * point.Y,
                    MidpointRounding.AwayFromZero);
            }

            public int ToRasterY(ScreenCoordinate point)
            {
                point.X -= (long)B1;
                point.Y -= (long)B2;

                return (int)Math.Round(inverse.A12 * point.X + inverse.A22 * point.Y, MidpointRounding.AwayFromZero);
            }


            public class Matrix2D
            {
                /// <summary>
                /// x-component of the pixel width
                /// </summary>
                public double A11 { get; set; }

                /// <summary>
                /// y-component of the pixel width
                /// </summary>
                public double A21 { get; set; }

                /// <summary>
                /// x-component of the pixel height
                /// </summary>
                public double A12 { get; set; }

                /// <summary>
                /// y-component of the pixel height (negative most of the time)
                /// </summary>
                public double A22 { get; set; }

                /// <summary>
                /// Gets a value indicating the determinant of this matrix
                /// </summary>
                private double Determinant
                {
                    get { return A22 * A11 - A21 * A12; }
                }

                /// <summary>
                /// Gets a value indicating that <see cref="Inverse()"/> can be computed.
                /// </summary>
                /// <remarks>
                /// Shortcut for <c><see cref="Determinant"/> != 0d</c>
                /// </remarks>
                private bool IsInvertible
                {
                    get { return Determinant != 0d; }
                }

                /// <summary>
                /// Method to compute the inverse Matrix of this matrix
                /// </summary>
                /// <returns>The inverse matrix</returns>
                /// <exception cref="Exception"/>
                public Matrix2D Inverse()
                {
                    if (!IsInvertible)
                        throw new Exception("Matrix not invertible");

                    var det = Determinant;

                    return new Matrix2D
                    {
                        A11 = A22 / det,
                        A21 = -A21 / det,
                        A12 = -A12 / det,
                        A22 = A11 / det
                    };
                }
            }





        }
        #endregion

        /*
        /// <summary>
        /// Renders the layer
        /// </summary>
        /// <param name="g">Graphics object reference</param>
        /// <param name="map">Map which is rendered</param>
        public override void Render(Graphics g) //, MapViewport map)
        {
            if (map.Center == null)
                throw (new ApplicationException("Cannot render map. View center not specified"));

            if (_image == null)
                throw new Exception("Image not set");


            // View to render
            var mapView = map.Envelope;

            // Layer view
            var lyrView = _envelope;

            // Get the view intersection
            var vi = mapView.Intersection(lyrView);
            if (!vi.IsNull)
            {
                // Image part
                // ReSharper disable InconsistentNaming
                var imgLT = Clip(worldFile.ToRaster(new GeomCoordinate(vi.MinX, vi.MaxY)));
                var imgRB = Clip(worldFile.ToRaster(new GeomCoordinate(vi.MaxX, vi.MinY)));
                var imgRect = new Rectangle(imgLT, PointDiff(imgLT, imgRB, 1));

                // Map Part
                var mapLT = Point.Truncate(map.WorldToImage(new GeomCoordinate(vi.MinX, vi.MaxY)));
                var mapRB = Point.Ceiling(map.WorldToImage(new GeomCoordinate(vi.MaxX, vi.MinY)));
                var mapRect = new Rectangle(mapLT, PointDiff(mapLT, mapRB, 1));
                // ReSharper restore InconsistentNaming

                // Set the interpolation mode
                var tmpInterpolationMode = g.InterpolationMode;
                g.InterpolationMode = InterpolationMode;

                // Render the image
                using (var ia = new ImageAttributes())
                {
                    ia.SetColorMatrix(new ColorMatrix { Matrix44 = 1 - Transparency }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    g.DrawImage(_image, mapRect, imgRect.X, imgRect.Y, imgRect.Width, imgRect.Height,
                        GraphicsUnit.Pixel, ia);
                }

                // reset the interpolation mode
                g.InterpolationMode = tmpInterpolationMode;

            }

            // Obsolete (and will cause infinite loop) 
            //base.Render(g, map);
        }

        */
        /// <summary>
        /// Clip to the extent of the image
        /// </summary>
        /// <param name="pt">The point</param>
        /// <returns>The clipped point</returns>
        private Point Clip(Point pt)
        {
            var x = pt.X;
            if (x < 0) x = 0;
            if (x > _image.Width) x = Image.Width;

            var y = pt.Y;
            if (y < 0) y = 0;
            if (y > _image.Height) y = Image.Height;

            return new Point(x, y);
        }
        public Point ToRaster(ScreenCoordinate point)
        {
            point.X -= (long)worldFile.B1;
            point.Y -= (long)worldFile.B2;


            var x = (int)Math.Round(worldFile.inverse.A11 * point.X + worldFile.inverse.A21 * point.Y, MidpointRounding.AwayFromZero);
            var y = (int)Math.Round(worldFile.inverse.A12 * point.X + worldFile.inverse.A22 * point.Y, MidpointRounding.AwayFromZero);

            return new Point(x, y);
        }


        private static Size PointDiff(Point p1, Point p2, int invertY = -1)
        {

            return new Size(p2.X - p1.X, invertY * (p2.Y - p1.Y));
        }



        override protected void TranslateCoords()
        {
            base.TranslateCoords();

            _imageView = ScreenView.BlockView;
        }

        override protected void DrawLayer(Rectangle clipRectangle)
        {
            try
            {
                SwapDrawBuffer();
              
                DrawGeoRefImagetoScreenRect((Bitmap)Image, clipRectangle);
                
            }
            finally
            {
                SwapDrawBuffer();
            }

            FireInvalidateLayer(clipRectangle);
        }

       

        public static Bitmap RotateImage(Image image, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            const double pi2 = Math.PI / 2.0;


            double oldWidth = (double)image.Width;
            double oldHeight = (double)image.Height;

            // Convert degrees to radians
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;

            // Ensure theta is now [0, 2pi)
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight; // The newWidth/newHeight expressed as ints



            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;


            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);

            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {

                Point[] points;

                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[] {
                                             new Point( (int) oppositeBottom, 0 ),
                                             new Point( nWidth, (int) oppositeTop ),
                                             new Point( 0, (int) adjacentBottom )
                                         };

                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[] {
                                             new Point( nWidth, (int) oppositeTop ),
                                             new Point( (int) adjacentTop, nHeight ),
                                             new Point( (int) oppositeBottom, 0 )
                                         };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[] {
                                             new Point( (int) adjacentTop, nHeight ),
                                             new Point( 0, (int) adjacentBottom ),
                                             new Point( nWidth, (int) oppositeTop )
                                         };
                }
                else
                {
                    points = new Point[] {
                                             new Point( 0, (int) adjacentBottom ),
                                             new Point( (int) oppositeBottom, 0 ),
                                             new Point( (int) adjacentTop, nHeight )
                                         };
                }

                g.DrawImage(image, points);
            }

            return rotatedBmp;
        }
    }
}
