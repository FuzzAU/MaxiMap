﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;

using MaxiMapD3D;

namespace MaxiMap
{
    public partial class MaxiMap : Form
    {
        /// <summary>
        /// List of resolutions supported for Starcraft 2 Minimaps
        /// </summary>
        private enum SupportedResolution { h1920v1080 };

        /// <summary>
        /// Mapping between resolution and map location
        /// </summary>
        //private Dictionary< SupportedResolution, MapLocation > MapCoords = new Dictionary<SupportedResolution,MapLocation>()
        //{ 
        //  { SupportedResolution.h1920v1080, new MapLocation( 28, 810,
        //                                                     289, 810,
        //                                                     28, 1068,
        //                                                     289, 1068 ) 
        //  }
        //};
        private Dictionary<SupportedResolution, MapLocation> MapCoords = new Dictionary<SupportedResolution, MapLocation>()
        { 
          { SupportedResolution.h1920v1080, new MapLocation( 28, 810,
                                                             289, 810,
                                                             28, 1000,
                                                             289, 1000 ) 
          }
        };

        /// <summary>
        /// Map bitmap to be copied from D3D stream
        /// </summary>
        private Bitmap MapBuffer;

        /// <summary>
        /// Capture and refresh timer
        /// </summary>
        private System.Windows.Forms.Timer UpdateTimer = new System.Windows.Forms.Timer();
        
        /// <summary>
        /// Start location of the SC2 Minimap at the current resolution
        /// </summary>
        private Point MapStartLocation = new Point(0, 0);

        /// <summary>
        /// Size of the current SC2 Minimap
        /// </summary>
        private Size MapSize = new Size(0, 0);

        private MaxiMapD3D.MaxiMapD3D MapD3D;
        public MaxiMap()
        {
            InitializeComponent();
        }

        private void MaxiMap_Load(object sender, EventArgs e)
        {
            // By default, lets just use the default resolution for 1920x1080
            var coords = MapCoords[SupportedResolution.h1920v1080];
            MapStartLocation = coords.TopLeft;
            MapSize = coords.MapSize;

            // Make room in the bitmap to store the Minimap
            MapBuffer = new Bitmap(MapSize.Width, MapSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            MapD3D = new MaxiMapD3D.MaxiMapD3D( this.Handle );
            MapD3D.SetSystemScreenSounds(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            
            // Time will go as fast as possible to get maximum frame rate
            UpdateTimer.Interval = 30;
            UpdateTimer.Tick += new EventHandler(UpdateTimer_Tick);
            UpdateTimer.Start();
        }

        /// <summary>
        /// Grab the frame from the D3DDevice and copy it to the form
        /// </summary>
        void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Copy current map surface to map bitmap
            MapD3D.GetMap(MapBuffer, MapStartLocation, MapSize);

            // Update the PictureBox
            mapDisplay.Image = MapBuffer;
        }

    }
}
