using System.Collections.ObjectModel;

namespace MazeSolver.Base
{
    public class Graph : KeyedCollection<string, Node>
    {
        public Node SourceNode { get; }

        public Graph(Node sourceNode)
        {
            SourceNode = sourceNode;
        }

        protected override string GetKeyForItem(Node node)
        {
            return node.Point.Key;
        }
    }
}
