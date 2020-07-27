namespace MazeSolver.Base
{
    public class Node
    {
        private long _distanceFromSource;
        public long DistanceFromSource
        {
            get
            {
                return _distanceFromSource;
            }
            set
            {
                if(value < _distanceFromSource)
                {
                    _distanceFromSource = value;
                }
            }
        }

        public Point Point { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public Node PreviousNode { get; set; }
        public bool IsVisited { get; set; }

        public Node(Node left, Node right, Node up, Node down, Point point)
        {
            _distanceFromSource = long.MaxValue;
            Point = point;
            Left = left;
            Right = right;
            Up = up;
            Down = down;
            IsVisited = false;
        }

    }
}
