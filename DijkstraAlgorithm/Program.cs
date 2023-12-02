namespace DijkstraAlgorithm
{
    public class Program
    {
        static void Main()
        {
            Graph graph = new();

            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");
            graph.AddVertex("F");

            graph.AddEdge("A", "B", 18);
            graph.AddEdge("A", "C", 40);
            graph.AddEdge("A", "E", 26);
            graph.AddEdge("B", "E", 65);
            graph.AddEdge("B", "D", 20);
            graph.AddEdge("D", "F", 15);
            graph.AddEdge("E", "F", 5);

            DijkstraAlgorithm dijkstraAlgorithm = new(graph);
            string path = dijkstraAlgorithm.FindShortestPath("A", "F");
            Console.WriteLine(path);
        }
    }

    internal class DijkstraAlgorithm
    {
        private Graph _graph;

        private List<GraphVertexInfo> _info;

        public DijkstraAlgorithm(Graph graph)
        {
            _graph = graph;
        }

        private void Init()
        {
            _info = [];

            foreach (var vertex in _graph.Vertices)
            {
                _info.Add(new GraphVertexInfo(vertex));
            }
        }

        private GraphVertexInfo GetVertexInfo(GraphVertex vertex)
        {
            foreach (var info in _info)
            {
                if (info.Vertex == vertex)
                    return info;
            }

            return null;
        }

        public GraphVertexInfo FindVertex(bool unvisited = true, bool minSum = true)
        {
            int minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;

            foreach (var info in _info)
            {
                if (info.Unvisited && info.EdgesWeightSum < minValue)
                {
                    minVertexInfo = info;
                    minValue = info.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        public string FindShortestPath(string startName, string targetName)
        {
            return FindShortestPath(_graph.FindVertex(startName), _graph.FindVertex(targetName));
        }

        private string FindShortestPath(GraphVertex startVertex, GraphVertex targetVertex)
        {
            Init();
            var startInfo = GetVertexInfo(startVertex);
            startInfo.EdgesWeightSum = 0;

            GraphVertexInfo current;

            do
            {
                current = FindVertex();
                SetSumToNextVertex(current);
            }
            while (current != null);

            return GetPath(startVertex, targetVertex);
        }

        private void SetSumToNextVertex(GraphVertexInfo info)
        {
            if (info == null)
                return;

            info.Visit();

            foreach (var edge in info.Vertex.Edges)
            {
                GraphVertexInfo nextInfo = GetVertexInfo(edge.Conntected);
                int sum = info.EdgesWeightSum + edge.EdgeWeight;

                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        private string GetPath(GraphVertex startVertex, GraphVertex targetVertex)
        {
            string path = targetVertex.Name;

            while (startVertex != targetVertex)
            {
                targetVertex = GetVertexInfo(targetVertex).PreviousVertex;
                path = targetVertex.Name + "/" + path;
            }

            return path;
        }
    }
}