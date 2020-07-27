using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeSolver.Base
{
    public class GraphGenerator
    {
        private readonly Bitmap _mazeImage;
        private static readonly int _intensityThreshold = 128;

        public GraphGenerator(string mazeImageFullPath)
        {
            _mazeImage = new Bitmap(mazeImageFullPath);
        }

        private bool IsPathValid(Point point)
        {
            if (!point.IsWithinRange(_mazeImage.Width - 1, _mazeImage.Height - 1))
            {
                return false;
            }
            Color pixel = _mazeImage.GetPixel(point.X, point.Y);
            return
                pixel.R >= _intensityThreshold &&
                pixel.B >= _intensityThreshold &&
                pixel.G >= _intensityThreshold;
        }

        private Node UpdateAdjacentNode(Node currentNode, Point adjacentPoint, Graph graph, Queue<Point> auxiliaryQueue)
        {
            if (IsPathValid(adjacentPoint))
            {
                if (graph.Contains(adjacentPoint.Key))
                {
                    return graph[adjacentPoint.Key];
                }
                else
                {
                    auxiliaryQueue.Enqueue(adjacentPoint);
                    Node adjacentNode = new Node(null, null, null, null, adjacentPoint);
                    graph.Add(adjacentNode);
                    return adjacentNode;
                }
            }
            return null;
        }

        public Graph GenerateGraph(Point sourcePoint)
        {
            Console.WriteLine($"Generating Graph, source: {sourcePoint.Key}");
            Queue<Point> auxiliaryQueue = new Queue<Point>();
            Graph graph = null;
            auxiliaryQueue.Enqueue(sourcePoint);

            while (auxiliaryQueue.Count > 0)
            {
                Point currentPoint = auxiliaryQueue.Dequeue();
                Node currentNode;
                if (graph == null)
                {
                    currentNode = new Node(null, null, null, null, currentPoint)
                    {
                        DistanceFromSource = 0
                    };
                    graph = new Graph(currentNode)
                    {
                        currentNode
                    };
                }
                else
                {
                    currentNode = graph[currentPoint.Key];
                }
                Point left = currentPoint.GetPoint(Point.Direction.Left);
                Point right = currentPoint.GetPoint(Point.Direction.Right);
                Point up = currentPoint.GetPoint(Point.Direction.Up);
                Point down = currentPoint.GetPoint(Point.Direction.Down);

                currentNode.Left = UpdateAdjacentNode(currentNode, left, graph, auxiliaryQueue);
                currentNode.Right = UpdateAdjacentNode(currentNode, right, graph, auxiliaryQueue);
                currentNode.Up = UpdateAdjacentNode(currentNode, up, graph, auxiliaryQueue);
                currentNode.Down = UpdateAdjacentNode(currentNode, down, graph, auxiliaryQueue);
            }
            return graph;
        }
    }
}
