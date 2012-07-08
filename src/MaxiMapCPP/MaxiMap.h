#pragma once

#include "MapLocation.h"
#include "MaxiMapD3D.h"

namespace MaxiMap {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MaxiMap
	/// </summary>
	public ref class MaxiMapForm : public System::Windows::Forms::Form
	{
	public:
		MaxiMapForm(void) : 
			FrameCount( 0 ),
			LastFrameCount( 0 )
		{
			InitializeComponent();

			// Set FPS update rate
			FPSUpdateTime = 0.25;

			// Set up supported resolutions
			MapCoords = gcnew Dictionary<String^, MapLocation^>();
			MapCoords->Add("1920x1080", gcnew MapLocation( 28, 810,
                                                           289, 810,
                                                           28, 1068,
                                                           289, 1068 ) );

			// TODO Make selectable, and automatic check
			MapStartLocation = MapCoords["1920x1080"]->TopLeft;
		    MapSize = MapCoords["1920x1080"]->MapSize;

			// Make room in the buffer bitmap for the map
			MapBuffer = gcnew Bitmap(MapSize.Width, MapSize.Height, PixelFormat::Format32bppArgb);

			// Create the interface to DX
			MapD3D = gcnew MaxiMapD3D( this->Handle );			
			MapD3D->SetSystemScreenBounds(Screen::PrimaryScreen->Bounds.Width, Screen::PrimaryScreen->Bounds.Height);

			// Setup frame update timer
			UpdateTimer = gcnew Timer();
			UpdateTimer->Interval = 1;
			UpdateTimer->Tick += gcnew EventHandler(this, &MaxiMapForm::UpdateMap);
			UpdateTimer->Start();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~MaxiMapForm()
		{
			if (components)
			{
				delete components;
			}
		}

		void UpdateMap(Object^ sender, EventArgs^ e)
		{
			// Copy current map surface to map bitmap
            MapD3D->GetMap(MapBuffer, MapStartLocation, MapSize);
            // Update the PictureBox
            MapDisplay->Image = MapBuffer;
            // Update the frame counter for FPS calculation
            FrameCount++;
		}

	private:
		MaxiMapD3D^ MapD3D;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

		/// <summary>
		/// Map display picture box in form
		/// </summary>
		System::Windows::Forms::PictureBox^  MapDisplay;

		/// <summary>
		/// Dictionary to look up resolutions and map co-ordinates
		/// </summary>
		Dictionary<String^, MapLocation^>^ MapCoords;

		/// <summary>
        /// Map bitmap to be copied from D3D stream
        /// </summary>
        Bitmap^ MapBuffer;

        /// <summary>
        /// Capture and refresh timer
        /// </summary>
        System::Windows::Forms::Timer^ UpdateTimer;

        /// <summary>
        /// Frames per second timer
        /// </summary>
        System::Windows::Forms::Timer^ FPSTimer;
        
        /// <summary>
        /// Start location of the SC2 Minimap at the current resolution
        /// </summary>
        System::Drawing::Point MapStartLocation;

        /// <summary>
        /// Size of the current SC2 Minimap
        /// </summary>
        System::Drawing::Size MapSize;

		/// <summary>
		/// Frame count for FPS display
		/// </summary>
        int FrameCount;
		/// <summary>
		/// Last frame count
		/// </summary>
        int LastFrameCount;
        /// <summary>
        /// Time [in seconds] between FPS updates
        /// </summary>
        double FPSUpdateTime;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->MapDisplay = (gcnew System::Windows::Forms::PictureBox());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->MapDisplay))->BeginInit();
			this->SuspendLayout();
			// 
			// MapDisplay
			// 
			this->MapDisplay->Dock = System::Windows::Forms::DockStyle::Fill;
			this->MapDisplay->Location = System::Drawing::Point(0, 0);
			this->MapDisplay->Name = L"MapDisplay";
			this->MapDisplay->Size = System::Drawing::Size(975, 736);
			this->MapDisplay->SizeMode = System::Windows::Forms::PictureBoxSizeMode::StretchImage;
			this->MapDisplay->TabIndex = 0;
			this->MapDisplay->TabStop = false;
			// 
			// MaxiMapForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(975, 736);
			this->Controls->Add(this->MapDisplay);
			this->Name = L"MaxiMapForm";
			this->Text = L"MaxiMap";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->MapDisplay))->EndInit();
			this->ResumeLayout(false);

		}
#pragma endregion
	};
}
