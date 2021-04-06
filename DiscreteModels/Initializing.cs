using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DiscreteModels.BaseModels;

namespace DiscreteModels
{
    public static class Initializing
    {
        public static Graph CreateGraph(string path)
        {
            Graph graph = new Graph();
            List<Edge> edges = new List<Edge>();
            List<Node> nodes = new List<Node>();

            string input = File.ReadAllText(path);

            if (input.Any(x => x.Equals('r')))
                input = input.Replace("\r", "");

            int arrDimension = Int32.Parse(input.Split("\n").ElementAt(0));
            graph.VerticesCount = arrDimension;

            input = input.Substring(2);
            int i = 0, j = 0;

            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    var value = int.Parse(col.Trim());
                    if (value != 0)
                    {
                        if (!edges.Exists(x => x.Source == j && x.Destination == i))
                        {
                            var edge = new Edge
                            {
                                Source = i,
                                Destination = j,
                                Weight = int.Parse(col.Trim())
                            };
                            edges.Add(edge);
                            if (nodes.Exists(x => x.Id == i))
                                nodes.First(x => x.Id == i).Rank++;
                            else
                                nodes.Add(new Node { Id = i, Rank = 1 });

                            if (nodes.Exists(x => x.Id == j))
                                nodes.First(x => x.Id == j).Rank++;
                            else
                                nodes.Add(new Node { Id = j, Rank = 1 });
                        }
                    }
                    j++;
                }
                i++;
            }

            graph.EdgesCount = edges.Count;
            graph.Edges = edges.ToArray();
            graph.Nodes = nodes.ToArray();

            return graph;
        }

        public static int[,] CreateMatrix(string path)
        {
            string input = File.ReadAllText(path);

            int arrDimension = Int32.Parse(input.Split("\n").ElementAt(0));

            int[,] matrixAdjacency = new int[arrDimension, arrDimension];

            input = input.Substring(2);
            int i = 0, j = 0;
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    var value = int.Parse(col.Trim());
                    if (value != 0)
                        matrixAdjacency[i, j] = value;
                    else
                    {
                        if (i == j)
                            matrixAdjacency[i, j] = -1;
                        else
                            matrixAdjacency[i, j] = 0;
                    }
                    j++;
                }
                i++;
            }
            return matrixAdjacency;
        }
    }
}