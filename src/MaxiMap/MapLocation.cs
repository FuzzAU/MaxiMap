using System;
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
        public Point Size
        {
            get
            {
                return new Point(TopRight.X - TopLeft.X, TopLeft.Y - BottomLeft.Y);
            }
        }

        public MapLocation(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }

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
