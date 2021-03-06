﻿using System;
using System.Drawing;

namespace MaxiMap
{
    /// <summary>
    /// Storage for the location of the SC2 Minimap for a given resolution
    /// </summary>
    public class MapLocation
    {
        /// <summary>
        /// Top left co-ordinate of map
        /// </summary>
        public Point TopLeft
        {
            get;
            private set;
        }

        /// <summary>
        /// Top right co-ordinate of map
        /// </summary>
        public Point TopRight
        {
            get;
            private set;
        }

        /// <summary>
        /// Bottom left co-ordinate of map
        /// </summary>
        public Point BottomLeft
        {
            get;
            private set;
        }

        /// <summary>
        /// Bottom right co-ordinate of map
        /// </summary>
        public Point BottomRight
        {
            get;
            private set;
        }

        /// <summary>
        /// Size of map
        /// </summary>
        /// <remarks>
        /// Width calculated between TopRight and TopLeft
        /// Height calculated between BottomLeft and TopLeft
        /// </remarks>
        public Size MapSize
        {
            get
            {
                return new Size(TopRight.X - TopLeft.X, BottomLeft.Y - TopLeft.Y);
            }
        }

        /// <summary>
        /// Create a Map Location with Points
        /// </summary>
        /// <param name="topLeft">Co-ordinate of top left map corner</param>
        /// <param name="topRight">Co-ordinate of top right map corner</param>
        /// <param name="bottomLeft">Co-ordinate of bottom left map corner</param>
        /// <param name="bottomRight">Co-ordinate of bottom right map corner</param>
        public MapLocation(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
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
        public MapLocation(int topLeftX, int topLeftY,
                           int topRightX, int topRightY,
                           int bottomLeftX, int bottomLeftY,
                           int bottomRightX, int bottomRightY
                          )
            : this(new Point(topLeftX, topLeftY), new Point(topRightX, topRightY), new Point(bottomLeftX, bottomLeftY), new Point(bottomRightX, bottomRightY))
        {   
        }
    }
}
