using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public abstract class Area : GraphNode
    {
        public Area()
        {
            this.EntityLabel = "Area";
            this.Labels = new List<string>() { "Location", "Area" };
        }
        public Coordinate BorderTopLeft {get; set; }
        public Coordinate BorderTopRight {get; set; }
        public Coordinate BorderBottomLeft { get; set; }
        public Coordinate BorderBottomRight { get; set; }
        public int Elevation { get; set; }

        public void SetCoordinates(int startingX, int startingY, int xLength, int yLength, int maxX, int maxY)
        {
            if (startingX + xLength > maxX)
                xLength = maxX - startingX;
            if (startingY + yLength > maxY)
                yLength = maxY - startingY;

            this.BorderBottomLeft = new Coordinate(startingX, startingY);
            this.BorderTopLeft = new Coordinate(startingX, startingY+yLength);
            this.BorderBottomRight = new Coordinate(startingX + xLength, startingY);
            this.BorderTopRight = new Coordinate(startingX + xLength, startingY + yLength);
        }

        public int xLength()
        {
            return (BorderBottomRight.X - BorderBottomLeft.X); 
        }
        public int yLength()
        {
            return (BorderTopRight.Y - BorderBottomRight.Y);
        }

        public int SquareArea()
        {
            return (BorderTopRight.Y - BorderBottomRight.Y) * (BorderBottomRight.X - BorderBottomRight.X);
        }

        public abstract string CalculateSize();
    }
}
