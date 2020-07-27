using MazeSolver.Base;
using System;

namespace MazeSolver.Dijkstra
{
    public class InvalidDestinationPoint : Exception
    {
        public Point DestinationPoint { get; }

        public InvalidDestinationPoint(Point destinationPoint) : base("Invalid destination point")
        {
            DestinationPoint = destinationPoint;
        }
    }
}
