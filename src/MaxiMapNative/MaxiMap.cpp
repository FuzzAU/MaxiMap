// MaxiMap.cpp : main project file.

#include "stdafx.h"
#include "MaxiMap.h"

using namespace MaxiMap;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	// Enabling Windows XP visual effects before any controls are created
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 

	// Create the main window and run it
	Application::Run(gcnew MaxiMap());
	return 0;
}
