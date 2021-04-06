using System.Collections.Generic;
using System.Linq;
using DiscreteModels.Second;
using DiscreteModels.Fourth;
using DiscreteModels.Third;
using DiscreteModels.BaseModels;
using DiscreteModels.First;

namespace DiscreteModels.Menu
{
    public static class Actions
    {
        
        public static void GetBoruvka()
        {
            var graph = Initializing.CreateGraph(@"./boruvkas.txt");
            var matrix = Initializing.CreateMatrix(@"./boruvkas.txt");
            var graphToReturn = Boruvkas.BoruvkasSolve(graph);

            System.Console.WriteLine("\n");
            MyPrinter.HeaderPrint("Minimum spanning tree by Boruvka's method");
            MyPrinter.PrintMatrix(matrix, "Started instance:");
            System.Console.WriteLine($"\t Edges count in started instance: {graph.EdgesCount}");
            System.Console.WriteLine($"\t Nodes count in started instance: {graph.VerticesCount}\n");
            // MyPrinter.PrintGraph(graph, "Started graph:");
            MyPrinter.PrintGraph(graphToReturn, "New minimized graph:");
        }
        public static void GetSalesman()
        {
            var graph = Initializing.CreateGraph(@"./salesman.txt");
            var matrix = Initializing.CreateMatrix(@"./salesman.txt");

            System.Console.WriteLine("\n");
            MyPrinter.HeaderPrint("Salesman problem\n");
            MyPrinter.PrintMatrix(matrix, "Matrix instance\n");

            BranchAndBoundSolver brunchAndBound = new BranchAndBoundSolver();

            var edges = brunchAndBound.BranchAndBound(matrix);

            // MyPrinter.PrintGraph(graph, "Graph instance");
            MyPrinter.PrintPath(edges);
        }
        public static void GetPostman()
        {
            var graph = Initializing.CreateGraph(@"./postman.txt");

            var matrix = Initializing.CreateMatrix(@"./postman.txt");

            Graph newGraph = new Graph();
            if (!Postman.IsEvenDegree(graph.Nodes))
            {
                var oddNodes = OddFinder.FindOddNodes(graph.Nodes);
                newGraph = Postman.PairingOddVertices(graph, oddNodes);
            }
            var eulerianPath = Postman.FindEulerianPath(newGraph);

            System.Console.WriteLine("\n");
            MyPrinter.HeaderPrint("Chinese postman problem");
            MyPrinter.PrintMatrix(matrix, "Matrix instance\n");
            // MyPrinter.PrintGraph(graph, "Graph instance: Edges");
            MyPrinter.ShowAdditionalEdges(graph, newGraph);
            MyPrinter.PrintNodes(graph.Nodes, newGraph.Nodes);
            MyPrinter.PrintPath(eulerianPath.ToArray());
        }

        public static void GetMaxFlowByFF()
        {
            var graph = Initializing.CreateGraph(@"./flow.txt");
            var matrix = Initializing.CreateMatrix(@"./flow.txt");

            FlowFinder FlowFinder = new FlowFinder(graph);
            var flow = FlowFinder.FindMaximumFlow();

            var maxFlow = flow.Where(e => e.Edge.Destination == graph.Nodes.Last().Id).Sum(x => x.Flow);

            System.Console.WriteLine("\n");
            MyPrinter.HeaderPrint("Max flow");
            MyPrinter.PrintMatrix(matrix, "Matrix instance");
            // MyPrinter.PrintGraph(graph, "Edges instance");
            MyPrinter.PrintFlow(graph.Edges, flow.ToArray());
            System.Console.WriteLine($"\n\t\tmax flow = {maxFlow}\n\n");
        }
    }
}