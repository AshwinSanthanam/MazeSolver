namespace MazeSolver.Base
{
    public class Point
    {
        public enum Direction
        {
            Left, Right, Up, Down
        }

        public int X { get; }
        public int Y { get; }
        public string Key => $"{X},{Y}";

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point GetPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Point(X, Y + 1);
                case Direction.Left:
                    return new Point(X - 1, Y);
                case Direction.Right:
                    return new Point(X + 1, Y);
                case Direction.Up:
                    return new Point(X, Y - 1);
            }
            return null;
        }

        public bool IsWithinRange(int x, int y)
        {
            return X >= 0 && Y >= 0 && X <= x && Y <= y;
        }
    }
}
