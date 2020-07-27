using MazeSolver.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MazeSolver.Dijkstra
{
    public class DijkstraPathFinder
    {
        private readonly Graph _graph;
        private readonly string _mazeImageFullPath;
        private readonly Base.Point _sourcePoint;

        public DijkstraPathFinder(string mazeImageFullPath, Base.Point sourcePoint)
        {
            _sourcePoint = sourcePoint;
            _graph = new GraphGenerator(mazeImageFullPath).GenerateGraph(sourcePoint);
            Console.WriteLine("Graph Generation Completed");
            _mazeImageFullPath = mazeImageFullPath;
        }

        private Node GetnearestUnvisitedNode()
        {
            Node nearestNode = null;
            long shortestDistance = long.MaxValue;
            foreach (Node node in _graph)
            {
                if (!node.IsVisited && node.DistanceFromSource <= shortestDistance)
                {
                    shortestDistance = node.DistanceFromSource;
                    nearestNode = node;
                }
            }
            return nearestNode;
        }

        private void RelaxNode(Node nearestUnvisitedNode, Node adjacent, Queue<Node> auxiliaryQueue)
        {
            if (adjacent != null && !adjacent.IsVisited)
            {
                if (nearestUnvisitedNode.DistanceFromSource + 1 < adjacent.DistanceFromSource)
                {
                    auxiliaryQueue.Enqueue(adjacent);
                    adjacent.DistanceFromSource = nearestUnvisitedNode.DistanceFromSource + 1;
                    adjacent.PreviousNode = nearestUnvisitedNode;
                }
            }
        }

        private void RelaxNeighbours(Node nearestUnvisitedNode, Queue<Node> auxiliaryQueue)
        {
            RelaxNode(nearestUnvisitedNode, nearestUnvisitedNode.Left, auxiliaryQueue);
            RelaxNode(nearestUnvisitedNode, nearestUnvisitedNode.Right, auxiliaryQueue);
            RelaxNode(nearestUnvisitedNode, nearestUnvisitedNode.Up, auxiliaryQueue);
            RelaxNode(nearestUnvisitedNode, nearestUnvisitedNode.Down, auxiliaryQueue);
        }

        public void PrepareGraph()
        {
            Console.WriteLine("Preparing graph...");
            Queue<Node> auxiliaryQueue = new Queue<Node>();
            auxiliaryQueue.Enqueue(_graph.SourceNode);
            while (auxiliaryQueue.Count > 0)
            {
                Node nearestUnvisitedNode = auxiliaryQueue.Dequeue();
                RelaxNeighbours(nearestUnvisitedNode, auxiliaryQueue);
                nearestUnvisitedNode.IsVisited = true;
            }
        }

        public void FindShortestPath(string destinationPath, Base.Point destinationPoint)
        {
            Node previousNode = null;
            try
            {
                previousNode = _graph[destinationPoint.Key];
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidDestinationPoint(destinationPoint);
            }
            Bitmap mazeImage = new Bitmap(_mazeImageFullPath);
            while (previousNode != null)
            {
                mazeImage.SetPixel(previousNode.Point.X, previousNode.Point.Y, Color.Red);
                previousNode = previousNode.PreviousNode;
            }
            mazeImage.Save(destinationPath);
            Console.WriteLine("Shortest path found!");

            Process process = new Process();
            process.StartInfo.FileName = @"C:\Windows\system32\mspaint.exe";
            process.StartInfo.Arguments = destinationPath;
            process.Start();
        }
    }
}
