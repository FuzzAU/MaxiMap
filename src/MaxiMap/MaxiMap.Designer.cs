namespace MaxiMap
{
    partial class MaxiMap
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
            this.mapDisplay = new System.Windows.Forms.PictureBox();
            this.FPSLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // mapDisplay
            // 
            this.mapDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapDisplay.Location = new System.Drawing.Point(0, 0);
            this.mapDisplay.Name = "mapDisplay";
            this.mapDisplay.Size = new System.Drawing.Size(1357, 930);
            this.mapDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mapDisplay.TabIndex = 1;
            this.mapDisplay.TabStop = false;
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.BackColor = System.Drawing.Color.Transparent;
            this.FPSLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPSLabel.Location = new System.Drawing.Point(12, 9);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(120, 31);
            this.FPSLabel.TabIndex = 2;
            this.FPSLabel.Text = "FPS: 0.0";
            this.FPSLabel.Visible = false;
            // 
            // MaxiMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 930);
            this.Controls.Add(this.FPSLabel);
            this.Controls.Add(this.mapDisplay);
            this.Name = "MaxiMap";
            this.ShowIcon = false;
            this.Text = "MaxiMap";
            this.Load += new System.EventHandler(this.MaxiMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapDisplay;
        private System.Windows.Forms.Label FPSLabel;
    }
}