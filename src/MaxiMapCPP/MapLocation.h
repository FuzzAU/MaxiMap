#pragma once

namespace MaxiMap {

	using namespace System;
	using namespace System::Collections;
	using namespace System::Drawing;

	/// <summary>
	/// Storage for the location of the SC2 Minimap for a given resolution
	/// </summary>
	public ref class MapLocation : public System::Windows::Forms::Form
	{
	public:
        /// <summary>
        /// Top left co-ordinate of map
        /// </summary>
		property Point TopLeft
		{
		public: Point get()
				{
					return _TopLeft;
				}
		private: void set(Point value)
				 {
					 _TopLeft = value;
				 }
		}

        /// <summary>
        /// Bottom left co-ordinate of map
        /// </summary>
		property Point BottomLeft
		{
		public: Point get()
				{
					return _BottomLeft;
				}
		private: void set(Point value)
				 {
					 _BottomLeft = value;
				 }
		}

		/// <summary>
        /// Top right co-ordinate of map
        /// </summary>
		property Point TopRight
		{
		public: Point get()
				{
					return _TopRight;
				}
		private: void set(Point value)
				 {
					 _TopRight = value;
				 }
		}

		/// <summary>
        /// Bottom right co-ordinate of map
        /// </summary>
		property Point BottomRight
		{
		public: Point get()
				{
					return _BottomRight;
				}
		private: void set(Point value)
				 {
					 _BottomRight = value;
				 }
		}

		/// <summary>
        /// Size of map
        /// </summary>
        /// <remarks>
        /// Width calculated between TopRight and TopLeft
        /// Height calculated between BottomLeft and TopLeft
        /// </remarks>
		property System::Drawing::Size MapSize
		{
		public: System::Drawing::Size get()
				{
					return System::Drawing::Size(TopRight.X - TopLeft.X, BottomLeft.Y - TopLeft.Y);
				}
		}

        /// <summary>
        /// Create a Map Location with Points
        /// </summary>
        /// <param name="topLeft">Co-ordinate of top left map corner</param>
        /// <param name="topRight">Co-ordinate of top right map corner</param>
        /// <param name="bottomLeft">Co-ordinate of bottom left map corner</param>
        /// <param name="bottomRight">Co-ordinate of bottom right map corner</param>
		MapLocation(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }

		/// <summary>
        /// Create a Map Location
        /// </summary>
        /// <param name="topLeftX">Top left X position</param>
        /// <param name="topLeftY">Top left Y position</param>
        /// <param name="topRightX">Top right X position</param>
        /// <param name="topRightY">Top right Y position</param>
        /// <param name="bottomLeftX">Bottom left X position</param>
        /// <param name="bottomLeftY">Bottom left Y position</param>
        /// <param name="bottomRightX">Bottom right X position</param>
        /// <param name="bottomRightY">Bottom right Y position</param>
        MapLocation(int topLeftX, int topLeftY,
					int topRightX, int topRightY,
					int bottomLeftX, int bottomLeftY,
					int bottomRightX, int bottomRightY
					)
        {   
			_TopLeft = Point(topLeftX, topLeftY);
			_TopRight = Point(topRightX, topRightY);
			_BottomLeft = Point(bottomLeftX, bottomLeftY);
			_BottomRight = Point(bottomRightX, bottomRightY);
        }

	private:
		Point _TopLeft;
		Point _BottomLeft;
		Point _TopRight;
		Point _BottomRight;
	};

}