using System;
using System.Collections.Generic;

namespace DiscreteModels.Menu
{
    public static class Menu
    {
        public static void StartingMenu()
        {
            while (true)
            {
                MyPrinter.HeaderPrint("Which laboratorna need to show?");
                GetActions();
                SelectMenuAction(Console.ReadLine());
            }
        }

        private static readonly Dictionary<string, Action> Menuactions = new Dictionary<string, Action>
        {
            { "Press [== 1 ==] for first laba Boruvka's slgorithm", Actions.GetBoruvka },
            { "Press [== 2 ==] for second laba Route inspection problem", Actions.GetPostman },
            { "Press [== 3 ==] for third laba Travelling salesman problem", Actions.GetSalesman },
            { "Press [== 4 ==] for fourth laba Maximum flow problem", Actions.GetMaxFlowByFF },
        };

        private static void GetActions()
        {
            foreach (var item in Menuactions.Keys)
            {
                System.Console.WriteLine(item);
            }
        }

        private static void SelectMenuAction(string selector)
        {
            foreach (var item in Menuactions)
            {
                if (item.Key.Contains($"[== {selector} ==]"))
                {
                    Menuactions[item.Key]();
                    break;
                }
            }
        }
    }
}