namespace SimpleMap.Controls
{
    partial class RegisterMapCtl
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
            if (disposing && (components != null))
            {
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.mapCtl_GeoRef = new SimpleMap.Controls.MapCtl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtWorldfile = new System.Windows.Forms.TextBox();
            this.btnCacheAllMap = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbCornersTL_BR = new System.Windows.Forms.RadioButton();
            this.rb4Pt_AffineRotation = new System.Windows.Forms.RadioButton();
            this.groupFullRectangle = new System.Windows.Forms.GroupBox();
            this.Lon_BR = new System.Windows.Forms.TextBox();
            this.Lon_BL = new System.Windows.Forms.TextBox();
            this.Lon_TR = new System.Windows.Forms.TextBox();
            this.Lon_TL = new System.Windows.Forms.TextBox();
            this.rbBR = new System.Windows.Forms.RadioButton();
            this.rbTR = new System.Windows.Forms.RadioButton();
            this.Lat_TR = new System.Windows.Forms.TextBox();
            this.Lat_BR = new System.Windows.Forms.TextBox();
            this.rbBL = new System.Windows.Forms.RadioButton();
            this.rbTL = new System.Windows.Forms.RadioButton();
            this.Lat_TL = new System.Windows.Forms.TextBox();
            this.Lat_BL = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLon = new System.Windows.Forms.TextBox();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.layout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupFullRectangle.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 451F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.mapCtl_GeoRef, 1, 0);
            this.layout.Controls.Add(this.panel1, 0, 0);
            this.layout.Controls.Add(this.panel2, 0, 1);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 2;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 482F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Size = new System.Drawing.Size(984, 552);
            this.layout.TabIndex = 12;
            // 
            // mapCtl_GeoRef
            // 
            this.mapCtl_GeoRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapCtl_GeoRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapCtl_GeoRef.Level = 12;
            this.mapCtl_GeoRef.Location = new System.Drawing.Point(454, 3);
            this.mapCtl_GeoRef.Name = "mapCtl_GeoRef";
            this.layout.SetRowSpan(this.mapCtl_GeoRef, 2);
            this.mapCtl_GeoRef.Size = new System.Drawing.Size(527, 546);
            this.mapCtl_GeoRef.TabIndex = 42;
            this.mapCtl_GeoRef.Load += new System.EventHandler(this.mapCtl_GeoRef_Load);
            this.mapCtl_GeoRef.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapCtl_GeoRef_MouseClick);
            this.mapCtl_GeoRef.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapCtl_GeoRef_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 478);
            this.panel1.TabIndex = 41;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupFullRectangle);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(447, 478);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Map Geo Registration Generation";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtWorldfile);
            this.groupBox5.Controls.Add(this.btnCacheAllMap);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.btnCalc);
            this.groupBox5.Location = new System.Drawing.Point(8, 315);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(435, 159);
            this.groupBox5.TabIndex = 47;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Worldfile Create - Georeference output";
            // 
            // txtWorldfile
            // 
            this.txtWorldfile.AcceptsReturn = true;
            this.txtWorldfile.Location = new System.Drawing.Point(152, 35);
            this.txtWorldfile.Multiline = true;
            this.txtWorldfile.Name = "txtWorldfile";
            this.txtWorldfile.Size = new System.Drawing.Size(114, 109);
            this.txtWorldfile.TabIndex = 33;
            this.txtWorldfile.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6";
            // 
            // btnCacheAllMap
            // 
            this.btnCacheAllMap.Image = global::SimpleMap.Properties.Resources.save_2_16x16;
            this.btnCacheAllMap.Location = new System.Drawing.Point(379, 84);
            this.btnCacheAllMap.Name = "btnCacheAllMap";
            this.btnCacheAllMap.Size = new System.Drawing.Size(33, 32);
            this.btnCacheAllMap.TabIndex = 35;
            this.btnCacheAllMap.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(77, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "World File";
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(318, 29);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(93, 29);
            this.btnCalc.TabIndex = 32;
            this.btnCalc.Text = "Calc";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbCornersTL_BR);
            this.groupBox3.Controls.Add(this.rb4Pt_AffineRotation);
            this.groupBox3.Location = new System.Drawing.Point(9, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(438, 61);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Number of Coordinates to Register";
            // 
            // rbCornersTL_BR
            // 
            this.rbCornersTL_BR.AutoSize = true;
            this.rbCornersTL_BR.Checked = true;
            this.rbCornersTL_BR.Location = new System.Drawing.Point(31, 31);
            this.rbCornersTL_BR.Name = "rbCornersTL_BR";
            this.rbCornersTL_BR.Size = new System.Drawing.Size(152, 17);
            this.rbCornersTL_BR.TabIndex = 0;
            this.rbCornersTL_BR.TabStop = true;
            this.rbCornersTL_BR.Text = "2 Point, Rectangle Corners";
            this.rbCornersTL_BR.UseVisualStyleBackColor = true;
            this.rbCornersTL_BR.CheckedChanged += new System.EventHandler(this.rbTL_BR_CheckedChanged);
            // 
            // rb4Pt_AffineRotation
            // 
            this.rb4Pt_AffineRotation.AutoSize = true;
            this.rb4Pt_AffineRotation.Location = new System.Drawing.Point(227, 31);
            this.rb4Pt_AffineRotation.Name = "rb4Pt_AffineRotation";
            this.rb4Pt_AffineRotation.Size = new System.Drawing.Size(122, 17);
            this.rb4Pt_AffineRotation.TabIndex = 1;
            this.rb4Pt_AffineRotation.Text = "4 = Affine && Rotation";
            this.rb4Pt_AffineRotation.UseVisualStyleBackColor = true;
            this.rb4Pt_AffineRotation.CheckedChanged += new System.EventHandler(this.rbFullRectangle_CheckedChanged);
            // 
            // groupFullRectangle
            // 
            this.groupFullRectangle.Controls.Add(this.Lon_BR);
            this.groupFullRectangle.Controls.Add(this.Lon_BL);
            this.groupFullRectangle.Controls.Add(this.Lon_TR);
            this.groupFullRectangle.Controls.Add(this.Lon_TL);
            this.groupFullRectangle.Controls.Add(this.rbBR);
            this.groupFullRectangle.Controls.Add(this.rbTR);
            this.groupFullRectangle.Controls.Add(this.Lat_TR);
            this.groupFullRectangle.Controls.Add(this.Lat_BR);
            this.groupFullRectangle.Controls.Add(this.rbBL);
            this.groupFullRectangle.Controls.Add(this.rbTL);
            this.groupFullRectangle.Controls.Add(this.Lat_TL);
            this.groupFullRectangle.Controls.Add(this.Lat_BL);
            this.groupFullRectangle.Location = new System.Drawing.Point(9, 96);
            this.groupFullRectangle.Name = "groupFullRectangle";
            this.groupFullRectangle.Size = new System.Drawing.Size(438, 214);
            this.groupFullRectangle.TabIndex = 44;
            this.groupFullRectangle.TabStop = false;
            this.groupFullRectangle.Text = "Coordinates - Click on Map";
            // 
            // Lon_BR
            // 
            this.Lon_BR.Location = new System.Drawing.Point(241, 172);
            this.Lon_BR.Name = "Lon_BR";
            this.Lon_BR.Size = new System.Drawing.Size(167, 20);
            this.Lon_BR.TabIndex = 52;
            // 
            // Lon_BL
            // 
            this.Lon_BL.Location = new System.Drawing.Point(36, 172);
            this.Lon_BL.Name = "Lon_BL";
            this.Lon_BL.Size = new System.Drawing.Size(167, 20);
            this.Lon_BL.TabIndex = 51;
            this.Lon_BL.Visible = false;
            // 
            // Lon_TR
            // 
            this.Lon_TR.Location = new System.Drawing.Point(241, 80);
            this.Lon_TR.Name = "Lon_TR";
            this.Lon_TR.Size = new System.Drawing.Size(167, 20);
            this.Lon_TR.TabIndex = 50;
            this.Lon_TR.Visible = false;
            // 
            // Lon_TL
            // 
            this.Lon_TL.Location = new System.Drawing.Point(36, 79);
            this.Lon_TL.Name = "Lon_TL";
            this.Lon_TL.Size = new System.Drawing.Size(167, 20);
            this.Lon_TL.TabIndex = 49;
            // 
            // rbBR
            // 
            this.rbBR.AutoSize = true;
            this.rbBR.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbBR.Location = new System.Drawing.Point(241, 119);
            this.rbBR.Name = "rbBR";
            this.rbBR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbBR.Size = new System.Drawing.Size(86, 17);
            this.rbBR.TabIndex = 48;
            this.rbBR.TabStop = true;
            this.rbBR.Text = "Bottom Right";
            this.rbBR.UseVisualStyleBackColor = true;
            // 
            // rbTR
            // 
            this.rbTR.AutoSize = true;
            this.rbTR.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbTR.Location = new System.Drawing.Point(241, 24);
            this.rbTR.Name = "rbTR";
            this.rbTR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbTR.Size = new System.Drawing.Size(72, 17);
            this.rbTR.TabIndex = 47;
            this.rbTR.TabStop = true;
            this.rbTR.Text = "Top Right";
            this.rbTR.UseVisualStyleBackColor = true;
            this.rbTR.Visible = false;
            // 
            // Lat_TR
            // 
            this.Lat_TR.Location = new System.Drawing.Point(241, 54);
            this.Lat_TR.Name = "Lat_TR";
            this.Lat_TR.Size = new System.Drawing.Size(167, 20);
            this.Lat_TR.TabIndex = 45;
            this.Lat_TR.Visible = false;
            // 
            // Lat_BR
            // 
            this.Lat_BR.Location = new System.Drawing.Point(241, 149);
            this.Lat_BR.Name = "Lat_BR";
            this.Lat_BR.Size = new System.Drawing.Size(167, 20);
            this.Lat_BR.TabIndex = 46;
            // 
            // rbBL
            // 
            this.rbBL.AutoSize = true;
            this.rbBL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbBL.Location = new System.Drawing.Point(36, 119);
            this.rbBL.Name = "rbBL";
            this.rbBL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbBL.Size = new System.Drawing.Size(79, 17);
            this.rbBL.TabIndex = 44;
            this.rbBL.TabStop = true;
            this.rbBL.Text = "Bottom Left";
            this.rbBL.UseVisualStyleBackColor = true;
            this.rbBL.Visible = false;
            // 
            // rbTL
            // 
            this.rbTL.AutoSize = true;
            this.rbTL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbTL.Checked = true;
            this.rbTL.Location = new System.Drawing.Point(36, 24);
            this.rbTL.Name = "rbTL";
            this.rbTL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbTL.Size = new System.Drawing.Size(65, 17);
            this.rbTL.TabIndex = 43;
            this.rbTL.TabStop = true;
            this.rbTL.Text = "Top Left";
            this.rbTL.UseVisualStyleBackColor = true;
            // 
            // Lat_TL
            // 
            this.Lat_TL.Location = new System.Drawing.Point(36, 54);
            this.Lat_TL.Name = "Lat_TL";
            this.Lat_TL.Size = new System.Drawing.Size(167, 20);
            this.Lat_TL.TabIndex = 34;
            // 
            // Lat_BL
            // 
            this.Lat_BL.Location = new System.Drawing.Point(36, 149);
            this.Lat_BL.Name = "Lat_BL";
            this.Lat_BL.Size = new System.Drawing.Size(167, 20);
            this.Lat_BL.TabIndex = 40;
            this.Lat_BL.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtLon);
            this.panel2.Controls.Add(this.txtLat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 485);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 64);
            this.panel2.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Longitude";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Latitude";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLon
            // 
            this.txtLon.Location = new System.Drawing.Point(139, 28);
            this.txtLon.Name = "txtLon";
            this.txtLon.Size = new System.Drawing.Size(167, 20);
            this.txtLon.TabIndex = 53;
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(139, 5);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(167, 20);
            this.txtLat.TabIndex = 52;
            // 
            // RegisterMapCtl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(325, 38);
            this.Name = "RegisterMapCtl";
            this.Size = new System.Drawing.Size(984, 552);
            this.layout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupFullRectangle.ResumeLayout(false);
            this.groupFullRectangle.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupFullRectangle;
        private System.Windows.Forms.RadioButton rbBR;
        private System.Windows.Forms.RadioButton rbTR;
        private System.Windows.Forms.TextBox Lat_TR;
        private System.Windows.Forms.TextBox Lat_BR;
        private System.Windows.Forms.RadioButton rbBL;
        private System.Windows.Forms.RadioButton rbTL;
        private System.Windows.Forms.TextBox Lat_TL;
        private System.Windows.Forms.TextBox Lat_BL;
        private System.Windows.Forms.TextBox Lon_BR;
        private System.Windows.Forms.TextBox Lon_BL;
        private System.Windows.Forms.TextBox Lon_TR;
        private System.Windows.Forms.TextBox Lon_TL;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rb4Pt_AffineRotation;
        private System.Windows.Forms.RadioButton rbCornersTL_BR;
        private MapCtl mapCtl_GeoRef;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLon;
        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtWorldfile;
        private System.Windows.Forms.Button btnCacheAllMap;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCalc;
    }
}
