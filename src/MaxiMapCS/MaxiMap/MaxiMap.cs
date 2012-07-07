using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

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
        private Dictionary< SupportedResolution, MapLocation > MapCoords = new Dictionary<SupportedResolution,MapLocation>()
        { 
          { SupportedResolution.h1920v1080, new MapLocation( 28, 810,
                                                             289, 810,
                                                             28, 1068,
                                                             289, 1068 ) 
          }
        };

        /// <summary>
        /// Direct 3D interface device
        /// </summary>
        private Microsoft.DirectX.Direct3D.Device D3DDevice;
        
        /// <summary>
        /// Map bitmap to be copied from D3D stream
        /// </summary>
        private Bitmap MapBuffer;

        /// <summary>
        /// Direct X surface from capture
        /// </summary>
        private Surface DXSurface;

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

            // Initialise the D3D device
            PresentParameters present_params = new PresentParameters();
            present_params.Windowed = true;
            present_params.SwapEffect = SwapEffect.Discard;
            D3DDevice = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, present_params);

            // Make room in the bitmap to store the Minimap
            MapBuffer = new Bitmap(MapSize.Width, MapSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Time will go as fast as possible to get maximum frame rate
            UpdateTimer.Interval = 1;
            UpdateTimer.Tick += new EventHandler(UpdateTimer_Tick);
            UpdateTimer.Start();
        }

        /// <summary>
        /// Grab the frame from the D3DDevice and copy it to the form
        /// </summary>
        void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Only grab the width up to the end of the map. Unfortunately we need to grab the entire height
            DXSurface = D3DDevice.CreateOffscreenPlainSurface(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, Format.A8R8G8B8, Pool.Scratch);
            D3DDevice.GetFrontBufferData(0, DXSurface);
            
            // Move surface to a managed bitmap
            SurfaceToBitmap(DXSurface, MapBuffer);

            // TODO Determine is this release and dispose section is required, or can memory be reused
            DXSurface.ReleaseGraphics();
            DXSurface.Dispose();

            // Update the PictureBox
            mapDisplay.Image = MapBuffer;
        }

        /// <summary>
        /// Does an unsafe memory copy from the D3D surface to a bitmap assuming FormatA8R8G8B8 
        /// </summary>
        /// <param name="InputSurface"></param>
        /// <param name="OutputBitmap"></param>
        /// TODO Make bit format interchangable
        private unsafe void SurfaceToBitmap(Surface InputSurface, Bitmap OutputBitmap)
        {
            // Lock the bitmap bits for doing a raw copy
            BitmapData bmd = OutputBitmap.LockBits(new Rectangle(0, 0, MapSize.Width, MapSize.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            // Lock the surface for copying from
            GraphicsStream gs = InputSurface.LockRectangle(new Rectangle(MapStartLocation.X, MapStartLocation.Y, MapSize.Width, MapSize.Height), LockFlags.ReadOnly);

            // Source location
            byte* SourceAddr = (byte*)gs.InternalData;
            // Destination location
            byte* DestAddr = (byte*)bmd.Scan0.ToPointer();
            
            // Size (in bytes) of a source pixel (A, R, G, B)
            byte SrcBytesPerPixel = 4;
            // Size (in bytes) of a destination pixel (A, R, G, B)
            byte DestBytesPerPixel = 4;

            // Even though we only lock the section of the surface around the map, the stride is still the length
            // of the whole surface
            int SourceStride = Screen.PrimaryScreen.Bounds.Width * SrcBytesPerPixel;

            // Try and do a parallel copy (on systems that support it, this should be faster)
            Parallel.For(0, bmd.Height, i =>
            {
                for (int j = 0; j < bmd.Width; ++j)
                {
                    byte* srcBase = SourceAddr + (i * SourceStride) + (j * SrcBytesPerPixel);
                    byte* destBase = DestAddr + (i * bmd.Stride) + (j * DestBytesPerPixel);
                    destBase[0] = srcBase[0];
                    destBase[1] = srcBase[1];
                    destBase[2] = srcBase[2];
                    destBase[3] = srcBase[3];
                }
            });
            OutputBitmap.UnlockBits(bmd);
            InputSurface.UnlockRectangle();
        }
    }
}
