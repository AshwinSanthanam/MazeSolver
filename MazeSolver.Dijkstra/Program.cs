using System;
using System.IO;

namespace MazeSolver.Dijkstra
{
    internal class Program
    {
        private static void SolveMaze(Base.Point source, Base.Point destination, string sourceFile, string destinationFile)
        {
            DijkstraPathFinder dijkstraPathFinder = new DijkstraPathFinder(sourceFile, source);
            dijkstraPathFinder.PrepareGraph();
            dijkstraPathFinder.FindShortestPath(destinationFile, destination);
        }

        private static void Main(string[] args)
        {
            var path = Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
            try
            {
                SolveMaze(new Base.Point(3, 3), new Base.Point(1800, 1799), $@"{path}\Input\1k.png", $@"{path}\Output\Output.png");
            }
            catch (Exception excption)
            {
                Console.WriteLine(excption.Message);
            }
        }
    }
}
