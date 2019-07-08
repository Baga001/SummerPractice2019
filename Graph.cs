using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Graph
    {
        int n; // Число вершин
        int depth;
        int countOfBridges;

        int[] depthIn; // глубина захода в вершину v
        int[] funcUp;  // можем ли мы вернуться в вершину v из остальных вершин
        bool[] used;
        int[,] graph;

        static List<string> Cycles;

        public Graph(string filename)
        {
            string[] t = File.ReadAllLines(filename);
            n = t.Length;
            int[] temp = new int[n];
            depthIn = new int[n];
            funcUp = new int[n];
            graph = new int[n, n];
            used = new bool[n];
            countOfBridges = 0;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    graph[i, j] = Int32.Parse(t[i].Split()[j]);
        }

        public void SearchCycles()
        {
            Cycles = new List<string>();
            int[] color = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                    color[k] = 0;
                List<int> cycle = new List<int> { i + 1 };
                DFScycle(i, i, color, -1, cycle);
            }

            string minCycle = null;

            Console.WriteLine("Все циклы: ");
            for (int i = 0; i < Cycles.Count; i++)
            {
                if (i == 0)
                    minCycle = Cycles[0];
                if(minCycle.Length > Cycles[i].Length)
                    minCycle = Cycles[i];
                Console.WriteLine(Cycles[i]);
            }
            Console.WriteLine("Минимальный цикл: ");
            if (minCycle != null)
                Console.WriteLine(minCycle);
            else
                Console.WriteLine("Не найден");

        }

        private void DFScycle(int u, int endV, int[] used, int unavailableEdge, List<int> cycle)
        {
            //если u == endV, то эту вершину перекрашивать не нужно, иначе мы в нее не вернемся, а вернуться необходимо
            if (u != endV)
                used[u] = 1;
            else if (cycle.Count >= 2)
            {
                cycle.Reverse();
                string s = cycle[0].ToString();
                for (int i = 1; i < cycle.Count; i++)
                    s += "-" + cycle[i].ToString();
                bool flag = false; //есть ли палиндром для этого цикла графа в List<string> Cycles?
                for (int i = 0; i < Cycles.Count; i++)
                    if (Cycles[i].ToString() == s)
                    {
                        flag = true;
                        break;
                    }
                if (!flag)
                {
                    cycle.Reverse();
                    s = cycle[0].ToString();
                    for (int i = 1; i < cycle.Count; i++)
                        s += "-" + cycle[i].ToString();
                    Cycles.Add(s);
                }
                return;
            }
            for (int i = 0; i < n; i++)
            {
                if (i == unavailableEdge)
                    continue;
                if (graph[u, i] == 1 && used[i] == 0)
                {
                    List<int> cycleNEW = new List<int>(cycle) { i + 1 };
                    DFScycle(i, endV, used, u, cycleNEW);
                    used[i] = 0;
                }
            }
        }

        private void DFSbridges(int v, int p = -1)
        {
            used[v] = true;
            depthIn[v] = funcUp[v] = depth++;
            for (int i = 0; i < n; i++)
            {
                if (graph[v, i] == 1)
                {
                    int to = i;
                    if (to == p)
                        continue;
                    if (used[to])
                        funcUp[v] = Math.Min(funcUp[v], depthIn[to]);
                    else
                    {
                        DFSbridges(to, v);
                        funcUp[v] = Math.Min(funcUp[v], funcUp[to]);
                        if (funcUp[to] > depthIn[v])
                        {
                            Console.WriteLine("bridge: (" + (v + 1) + " , " + (to + 1) + ")");
                            countOfBridges++;
                        }
                    }
                }
            }
        }

        public void FindBridges()
        {
            depth = 0;
            for (int i = 0; i < n; ++i)
                used[i] = false;
            for (int i = 0; i < n; ++i)
                if (!used[i])
                    DFSbridges(i);
            Console.WriteLine("Найдено мостов: " + countOfBridges);
        }
    }
}
