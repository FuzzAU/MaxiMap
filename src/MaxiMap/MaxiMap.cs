using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MaxiMap
{
    public partial class MaxiMap : Form
    {
        /// <summary>
        /// Direct 3D interface device
        /// </summary>
        private Microsoft.DirectX.Direct3D.Device D3DDevice;
        
        /// <summary>
        /// Map bitmap to be copied from D3D stream
        /// </summary>
        private Bitmap MapBuffer;

        public MaxiMap()
        {
            InitializeComponent();
        }

        private void MaxiMap_Load(object sender, EventArgs e)
        {
            PresentParameters present_params = new PresentParameters();
            present_params.Windowed = true;
            present_params.SwapEffect = SwapEffect.Discard;
            D3DDevice = new Device(0, DeviceType.Hardware, this,
                    CreateFlags.SoftwareVertexProcessing, present_params);

            MapBuffer = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Surface s = D3DDevice.CreateOffscreenPlainSurface(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height, Format.A8R8G8B8, Pool.Scratch);
            D3DDevice.GetFrontBufferData(0, s);
            
            //var gs = SurfaceLoader.SaveToStream(ImageFileFormat.Bmp, s);
            SurfaceToBitmap(s, MapBuffer);

            mapDisplay.Image = MapBuffer;
            
        }

        // Copies from a surface to a bitmap assuming FormatA8R8G8B8
        private unsafe void SurfaceToBitmap(Surface s, Bitmap b)
        {
            // Lock the bitmap bits for copying in to
            BitmapData bmd = b.LockBits(new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                                        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            // Lock the surface for copying from
            GraphicsStream gs = s.LockRectangle(new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                                                LockFlags.ReadOnly);

            // Source location
            byte* srcLoc = (byte*)gs.InternalData;
            // Destination location
            byte* loc = (byte*)bmd.Scan0;

            byte* scan0 = (byte*)bmd.Scan0.ToPointer();
            byte srcBytesPerPixel = 4;
            byte destBytesPerPixel = 4;

            long len = gs.Length;
            for (int i = 0; i < bmd.Height; ++i)
            {
                for (int j = 0; j < bmd.Width; ++j)
                {
                    byte* srcBase = srcLoc + (i * bmd.Stride) + (j * 4);
                    byte* destBase = scan0 + (i * bmd.Stride) + (j * destBytesPerPixel);
                    destBase[0] = srcBase[0];
                    destBase[1] = srcBase[1];
                    destBase[2] = srcBase[2];
                    destBase[3] = srcBase[3];
                }
            }
            b.UnlockBits(bmd);
        }
    }
}
