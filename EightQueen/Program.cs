using System;
using System.Collections.Generic;
using System.Linq;

namespace EightQueen
{
    class Program
    {
        static void Main(string[] args)
        {
            int queenCount = 8;

            List<List<int>> locationsGroups = new List<List<int>>();

            for (int q = 0; q < queenCount; q++)
            {
                locationsGroups.Add(new List<int> { q });
            }

            int y = 1;

            while (y < queenCount)
            {
                foreach (var locations in locationsGroups.ToList())
                {
                    //下一階層所有可能性
                    List<int> nexts = FindNext(queenCount, locations);

                    //拓展可能性
                    foreach (var next in nexts)
                    {
                        var tempLocations = new List<int>(locations);
                        tempLocations.Add(next);

                        locationsGroups.Add(tempLocations);
                    }
                }

                //過濾沒有下一層的
                locationsGroups = locationsGroups.Where(l => l.Count == y + 1).ToList();

                y = y + 1;
            }

            //列印結果
            Print(locationsGroups);
            Console.Read();
        }

        private static List<int> FindNext(int queenCount, List<int> locations)
        {

            List<int> nexts = new List<int>();
            for (int next = 0; next < queenCount; next++)
            {
                if (CheckNext(locations, next))
                {
                    nexts.Add(next);
                }
            }

            return nexts;
        }

        private static bool CheckNext(List<int> locations, int next)
        {
            //檢查直線
            if (locations.Contains(next))
            {
                return false;
            }

            //檢查斜線
            for (int index = 0; index < locations.Count; index++)
            {
                if (Math.Abs(locations[index] - next) == Math.Abs(index - locations.Count))
                {
                    return false;
                }
            }

            return true;
        }

        private static void Print(List<List<int>> locationsGroups)
        {
            foreach (var locationsGroup in locationsGroups)
            {
                for (int y = 0; y < locationsGroup.Count; y++)
                {
                    for (int x = 0; x < locationsGroup.Count; x++)
                    {
                        if (locationsGroup[y] == x)
                        {
                            Console.Write("Q ");
                        }
                        else
                        {
                            Console.Write(". ");
                        }
                    }
                    Console.Write("\r\n");
                }
                Console.Write("-------------------------- \r\n");
            }

            Console.Write($"組合數量 : {locationsGroups.Count}");

        }
    }
}
