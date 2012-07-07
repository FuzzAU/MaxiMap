// MaxiMapD3D.h

#pragma once

#include <d3d9.h>

using namespace System;
using namespace System::Drawing;
using namespace System::Drawing::Imaging;

namespace MaxiMapD3D {

	public ref class MaxiMapD3D
	{
	public:
		MaxiMapD3D( IntPtr Handle )
		{
			LPDIRECT3D9 D3D9 = Direct3DCreate9( D3D_SDK_VERSION );
			// Set up presentation parameters for DX Device
			D3DPRESENT_PARAMETERS D3Dpp;
			ZeroMemory( &D3Dpp, sizeof(D3Dpp) );
			D3Dpp.Windowed = TRUE;
			D3Dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
			
			// Get HWND number
			HWND window = (HWND)Handle.ToInt32();
			
			ScreenBounds = gcnew Size();

			// Pin the pointer down so that it doesn't get moved when the GC moves stuff about
			//pin_ptr<LPDIRECT3DDEVICE9> device = &this->DXDevice;
			LPDIRECT3DDEVICE9 device;
			// TODO		Error handling here for failed creation. Use failed...
			if( FAILED(D3D9->CreateDevice( D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, window, D3DCREATE_SOFTWARE_VERTEXPROCESSING, &D3Dpp, &device )))
			{
				throw gcnew Exception("Fail");
			}
			this->DXDevice = device;
		}

		~MaxiMapD3D()
		{
			if(DXDevice)
			{
				DXDevice->Release();
				delete DXDevice;
			}
			
			if(ScreenBounds)
			{
				delete ScreenBounds;
			}
		}

		void SetSystemScreenSounds(int Width, int Height)
		{
			this->ScreenBounds->Width = Width;
			this->ScreenBounds->Height = Height;
		}

		void GetMap(Bitmap^ DestinationBitmap, Point^ MapStartLocation, Size^ MapSize)
		{
			IDirect3DSurface9* DXSurface;
			DXDevice->CreateOffscreenPlainSurface(ScreenBounds->Width, ScreenBounds->Height, D3DFMT_A8R8G8B8, D3DPOOL_SCRATCH, &DXSurface, NULL);
			DXDevice->GetFrontBufferData(0, DXSurface);

			SurfaceToBitmap(DXSurface, DestinationBitmap, MapStartLocation, MapSize);

			DXSurface->Release();
		}

	private:
		void SurfaceToBitmap(IDirect3DSurface9* InputSurface, Bitmap^ OutputBitmap, Point^ MapStartLocation, Size^ MapSize)
		{
			// Lock the bitmap bits for raw copying to
			System::Drawing::Rectangle OutLockRect(0, 0, MapSize->Width, MapSize->Height);
			BitmapData^ SrcBitmapData = OutputBitmap->LockBits(OutLockRect, ImageLockMode::ReadWrite, PixelFormat::Format32bppArgb);
			
			// Lock the surface for copying from
			// TODO Find a better way to initialise the rectangle
			RECT InLockRect;
			InLockRect.top = MapStartLocation->Y;
			InLockRect.bottom = MapStartLocation->Y + MapSize->Height;
			InLockRect.left = MapStartLocation->X;
			InLockRect.right = MapStartLocation->X + MapSize->Width;
			D3DLOCKED_RECT SrcLockedRect;
			InputSurface->LockRect( &SrcLockedRect, &InLockRect, D3DLOCK_READONLY );

			// Get the source location for the copy
			char* SourceAddr = (char*)SrcLockedRect.pBits;
			// Get the destination location for the copy
			char* DestAddr = (char*)(SrcBitmapData->Scan0.ToPointer());
			// Size (in bytes) of a source pixel (A, R, G, B)
			char SrcBytesPerPixel = 4;
			// Size (in bytes) of a destination pixel (A, R, G, B)
			char DestBytesPerPixel = 4;
			// Source stride
			int SourceStride = SrcLockedRect.Pitch;

			for(int i = 0; i < SrcBitmapData->Height; ++i)
            {
                for (int j = 0; j < SrcBitmapData->Width; ++j)
                {
                    char* srcBase = SourceAddr + (i * SourceStride) + (j * SrcBytesPerPixel);
                    char* destBase = DestAddr + (i * SrcBitmapData->Stride) + (j * DestBytesPerPixel);
                    destBase[0] = srcBase[0];
                    destBase[1] = srcBase[1];
                    destBase[2] = srcBase[2];
                    destBase[3] = srcBase[3];
                }
            }

			OutputBitmap->UnlockBits(SrcBitmapData);
			InputSurface->UnlockRect();
		}

		LPDIRECT3DDEVICE9 DXDevice;
		Size^ ScreenBounds;
	};
}
