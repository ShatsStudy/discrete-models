using System;
using System.Collections.Generic;
using System.Linq;
using DiscreteModels.BaseModels;
using DiscreteModels.Fourth;

namespace DiscreteModels.Menu
{
    public static class MyPrinter
    {
        public static void HeaderPrint(string content)
        {
            System.Console.WriteLine($"{"".PadRight(100, '_')}");
            System.Console.WriteLine($"{"".PadRight(50 - content.Length / 2, ' ')}{content}");
            // System.Console.WriteLine($"{"".PadRight(50, '_')}");
        }

        public static void PrintMatrix(int[,] matrix, string content = "Graph matrix")
        {
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    System.Console.Write($"\t{matrix[i, j]}");
                }
                System.Console.WriteLine("\t");
            }
            System.Console.WriteLine();
        }

        public static void PrintGraph(Graph graph, string content = "Edges")
        {
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");
            System.Console.WriteLine($"\t\tEdges Count: {graph.EdgesCount}");
            System.Console.WriteLine($"\t\tNodes Count: {graph.VerticesCount}");
            System.Console.WriteLine($"\n\t\tSource\tDestiny\tWeight");
            for (int i = 0; i < graph.EdgesCount; i++)
            {
                System.Console.WriteLine($"\t\t{graph.Edges[i].Source}\t{graph.Edges[i].Destination}\t{graph.Edges[i].Weight}");
            }
            System.Console.WriteLine("\n");
        }

        public static void ShowAdditionalEdges(Graph oldGraph, Graph newGraph)
        {
            string content = "Additional adges:";
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");

            var differencesCount = newGraph.EdgesCount - oldGraph.EdgesCount;
            var differences = newGraph.Edges.TakeLast(differencesCount);

            System.Console.WriteLine($"\t\tNew graph edges count: {newGraph.EdgesCount}");
            System.Console.WriteLine($"\n\t\tSource\tDestination\tWeight");
            foreach (var item in differences)
            {
                System.Console.WriteLine($"\t\t{item.Source}\t\t{item.Destination}\t{item.Weight}");
            }
        }

        public static void PrintNodes(Node[] oldNodes, Node[] newNodes)
        {
            string content = "Nodes";
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");
            System.Console.WriteLine($"\tNode id  \tOld Power  \tNew Power");
            for (int i = 0; i < oldNodes.Length; i++)
            {
                System.Console.WriteLine($"\t  {oldNodes[i].Id}\t\t{oldNodes[i].Rank}\t\t{newNodes.First(x => x.Id == oldNodes[i].Id).Rank}");
            }
            System.Console.WriteLine();
        }

        public static void PrintPath(Node[] nodes)
        {
            string content = "Path";
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");
            System.Console.Write("");
            for (int i = 0; i < nodes.Length; i++)
            {
                System.Console.Write($"{nodes[i].Id} ==> ");
            }
            System.Console.Write($"\n");
        }

        public static void PrintPath(Edge[] edges)
        {
            string content = "Path";
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");
            System.Console.Write("");
            for (int i = 0; i < edges.Length; i++)
            {
                System.Console.Write($"{edges[i].Source} ==> {edges[i].Destination} ==> ");
            }
            System.Console.WriteLine("\n");
        }

        public static void PrintFlow(Edge[] edges, FlowModel[] flows)
        {
            string content = "Path";
            System.Console.WriteLine($"{"".PadRight(25 - content.Length / 2, ' ')}{content}");
            System.Console.WriteLine();

            var maxNodeId = edges.Max(x => x.Destination);
            for (int i = 0; i < edges.Length; i++)
            {
                // if (edges[i].Destination == maxNodeId)
                    System.Console.Write($"\t\t{edges[i].Source} =(Filled: {flows.First(x => x.Edge.Source == edges[i].Source && x.Edge.Destination == edges[i].Destination).Flow}\t/ {edges[i].Weight})=>\t{edges[i].Destination}\n");
            }
        }
    }
}