using System;
using System.Collections.Generic;
using System.Linq;
using DiscreteModels.BaseModels;

namespace DiscreteModels.Second
{
    public class Postman
    {
        public static Graph graph = new Graph();
        public static bool IsEvenDegree(Node[] nodes)
        {
            foreach (var node in nodes)
            {
                if (node.Rank % 2 != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private static List<(Node, Node)> GetOddNodesCombinations(List<Node> oddNodes)
        {
            Node firstNode = oddNodes.First();
            int minWeight = int.MaxValue;
            List<(Node, Node)> bestPairs = new List<(Node, Node)>();
            (Node, Node) currentPair = (null, null);
            if (oddNodes.Count == 2)
            {
                return new List<(Node, Node)>() { (firstNode, oddNodes.Last()) };
            }
            else
            {
                foreach (var node in oddNodes.Skip(1))
                {
                    currentPair = (firstNode, node);
                    List<(Node, Node)> newPair = GetOddNodesCombinations(oddNodes.Except(new List<Node>() { node, firstNode }).ToList());
                    int weight =
                        CalculateWeightOfPairs(newPair);
                    if (weight < minWeight)
                    {
                        minWeight = weight;
                        bestPairs = new List<(Node, Node)>() { currentPair };
                        bestPairs.AddRange(newPair);
                    }
                }
            }

            return bestPairs;
        }
private static int CalculateWeightOfPairs(List<(Node firstNode, Node secondNode)> pairs)
        {
            int sum = 0;
            foreach (var pair in pairs)
            {
                var temporary = graph.Edges.FirstOrDefault(x => x.Source == pair.firstNode.Id
                && x.Destination == pair.secondNode.Id
                || x.Destination == pair.firstNode.Id
                && x.Source == pair.secondNode.Id);
                sum += temporary is null ? int.MaxValue : temporary.Weight;
            }
            return sum;
        }

        private static List<Edge> CreateEdgeBetweenOddVertices(List<(Node firstNode, Node secondNode)> pairsOfOddNodes)
        {
            var edges = graph.Edges.ToList();
            foreach (var pair in pairsOfOddNodes)
            {
                if (pair.firstNode != null && pair.secondNode != null)
                {
                    var temporary = Postman.graph.Edges.FirstOrDefault(x => x.Source == pair.firstNode.Id
                                                                            && x.Destination == pair.secondNode.Id
                                                                            || x.Destination == pair.firstNode.Id
                                                                            && x.Source == pair.secondNode.Id);
                    if (temporary != null)
                    {
                        edges.Add(new Edge()
                        {
                            Source = pair.firstNode.Id,
                            Destination = pair.secondNode.Id,
                            Weight = temporary.Weight
                        });
                        graph.Nodes.First(x => x.Id == pair.firstNode.Id).Rank++;
                        graph.Nodes.First(x => x.Id == pair.secondNode.Id).Rank++;
                    }
                }
            }
            return edges;
        }
        public static Graph PairingOddVertices(Graph graph, Node[] oddNode)
        {
            Postman.graph = (Graph)graph.Clone();
            var pairs = GetOddNodesCombinations(oddNode.ToList());
            var newEdges = CreateEdgeBetweenOddVertices(pairs);
            Postman.graph.Edges = newEdges.ToArray();
            Postman.graph.EdgesCount = Postman.graph.Edges.Length;
            return Postman.graph;
        }

        public static List<Node> FindEulerianPath(Graph graph)
        {
            Stack<Node> nodesStack = new Stack<Node>();
            List<Node> result = new List<Node>();
            nodesStack.Push(graph.Nodes.First());
            var edgesToCompile = graph.Edges.ToList();
            var nodesToCompile = graph.Nodes.ToList();
            while (nodesStack.Count != 0)
            {
                var vNode = nodesStack.Peek();
                if (edgesToCompile.Any(x => x.Source == vNode.Id || x.Destination == vNode.Id))
                {
                    Edge edgeToRemove = new Edge();
                    Node vNodeConnected = new Node();
                    if (edgesToCompile.Any(x => x.Source == vNode.Id))
                    {
                        vNodeConnected = nodesToCompile.First(x => x.Id == edgesToCompile.First(x => x.Source == vNode.Id).Destination);
                        nodesStack.Push(vNodeConnected);
                        edgeToRemove = edgesToCompile.First(x => x.Destination == vNodeConnected.Id && x.Source == vNode.Id);
                    }
                    else if (edgesToCompile.Any(x => x.Destination == vNode.Id))
                    {
                        vNodeConnected = nodesToCompile.First(x => x.Id == edgesToCompile.First(x => x.Destination == vNode.Id).Source);
                        nodesStack.Push(vNodeConnected);
                        edgeToRemove = edgesToCompile.First(x => x.Source == vNodeConnected.Id && x.Destination == vNode.Id);

                    }
                    if (edgeToRemove != null) edgesToCompile.Remove(edgeToRemove);
                }
                else
                {
                    nodesStack.Pop();
                    result.Add(vNode);
                }

            }
            return result;
        }
    }

}