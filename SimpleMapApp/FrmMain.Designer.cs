
namespace SimpleMapApp
{
    partial class FrmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.registerImageMapClt1 = new SimpleMap.Controls.GeoRegister.RegisterImageMapClt();
            this.SuspendLayout();
            // 
            // registerImageMapClt1
            // 
            this.registerImageMapClt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registerImageMapClt1.Location = new System.Drawing.Point(0, 0);
            this.registerImageMapClt1.Name = "registerImageMapClt1";
            this.registerImageMapClt1.Size = new System.Drawing.Size(1131, 969);
            this.registerImageMapClt1.TabIndex = 0;
            this.registerImageMapClt1.Worldfile = null;
            this.registerImageMapClt1.xsize = 0;
            this.registerImageMapClt1.ysize = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 969);
            this.Controls.Add(this.registerImageMapClt1);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private SimpleMap.Controls.GeoRegister.RegisterImageMapClt registerImageMapClt1;
    }
}