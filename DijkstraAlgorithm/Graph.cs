namespace DijkstraAlgorithm
{
    class GraphEdge
    {
        public GraphEdge(GraphVertex conntected, int edgeWeight)
        {
            Conntected = conntected;
            EdgeWeight = edgeWeight;
        }

        public GraphVertex Conntected { get; }

        public int EdgeWeight { get; }
    }

    class GraphVertex
    {
        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = [];
        }

        public string Name { get; }

        public List<GraphEdge> Edges { get; }

        public void AddEdge(GraphEdge edge)
        {
            Edges.Add(edge);
        }

        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }
    }

    class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = [];
        }

        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }

        public GraphVertex FindVertex(string vertexName)
        {
            foreach (GraphVertex vertex in Vertices)
            {
                if (vertex.Name == vertexName) return vertex;
            }

            return null;
        }

        public void AddEdge(string startVertex, string targetVertex, int weight)
        {
            var firstVertex = FindVertex(startVertex);
            var secondVertex = FindVertex(targetVertex);

            if (firstVertex != null && secondVertex != null)
            {
                firstVertex.AddEdge(secondVertex, weight);
                secondVertex.AddEdge(firstVertex, weight);
            }
        }
    }

    class GraphVertexInfo
    {
        private bool _unvisited;
        private int _edgesWeightSum;
        private GraphVertex _previousVertex;

        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            _unvisited = true;
            _edgesWeightSum = int.MaxValue;
        }

        public GraphVertex Vertex { get; }

        public GraphVertex PreviousVertex
        {
            get => _previousVertex;
            set
            {
                if (value != Vertex)
                    _previousVertex = value;
                else
                    _previousVertex = null;
            }
        }

        public int EdgesWeightSum
        {
            get => _edgesWeightSum;
            set
            {
                if(value >= 0)
                    _edgesWeightSum = value;
            }
        }

        public bool Unvisited => _unvisited;

        public void Visit() => _unvisited = false;
    }
}
