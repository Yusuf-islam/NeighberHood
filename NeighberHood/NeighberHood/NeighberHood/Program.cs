using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NeighberHood
{
    public class Program
    {
        #region const
        static int[,] ints = { { 1, 0 }, { 2, 3 }, { 4, 5 }, { 6, 7 }, { 8, 9 } };
        #endregion


        static void Main(string[] args)
        {
            PrintFirstLine();
            PrintNeighberHood();
            PrintElement();
            Console.ReadLine();
        }

        private static void PrintFirstLine()
        {
            #region First Line
            Console.Write("# ");
            foreach (var element in ints)
            {
                Console.Write(element + " ");
            }
            #endregion
        }
        static void PrintNeighberHood()
        {

            var neighberInfos = MainNeighber.GetMainNeighberInfos(ints);

            foreach (var item in ints)
            {
                Console.WriteLine();
                Console.Write(item + " ");
                var newList = neighberInfos.Where(x => x.Value == item);
                foreach (var element in ints)
                {
                    var query = from x in newList
                                where x.Neighbers.Contains(element)
                                select x;
                    var value = query.Count() == 0 ? "|" : "*";

                    Console.Write(value + " ");
                }
            }

        }
        static void PrintElement()
        {

            for (var i = 0; i < ints.GetLength(0); i++)
            {
                Console.WriteLine();
                for (var j = 0; j < ints.GetLength(1); j++)
                {
                    Console.Write(ints[i, j] + " ");
                }

            }
        }


    }
    public class MainNeighber
    {
        public string Index { get; set; }
        public int Value { get; set; }
        public List<int> Neighbers { get; set; }

        public static List<MainNeighber> GetMainNeighberInfos(int[,] lists)
        {
            List<MainNeighber> listEmployees = new List<MainNeighber>();
            foreach (int i in lists)
            {
                listEmployees.Add(new MainNeighber { Index = CoordinatesOf(lists, i).ToString(), Value = i, Neighbers = FindNeighbers(i, lists) });
            }
            return listEmployees;
        }
        private static List<int> FindNeighbers(int element, int[,] lists)
        {
            List<int> result = new List<int>();
            var index = CoordinatesOf(lists, element).ToString().Split(',');

            if (int.Parse(index[0]) == 0 && int.Parse(index[1]) == 0)
            {       //0,0
                index[0] = (int.Parse(index[0]) + 1).ToString();//1,0
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                index[0] = (int.Parse(index[0]) - 1).ToString();//0,0
                index[1] = (int.Parse(index[1]) + 1).ToString();//0,1
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
            }
            else if (int.Parse(index[0]) == 0 && int.Parse(index[1]) > 0)
            {               //0,1
                index[0] = (int.Parse(index[0]) + 1).ToString();//1,1
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                index[0] = (int.Parse(index[0]) - 1).ToString();//0,1
                index[1] = (int.Parse(index[1]) - 1).ToString();//0,0
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
            }
            else if (int.Parse(index[0]) > 0 && int.Parse(index[1]) == 0)
            {       //1,0
                index[0] = (int.Parse(index[0]) - 1).ToString();//0,0
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                index[0] = (int.Parse(index[0]) + 1).ToString();//1,0
                index[1] = (int.Parse(index[1]) + 1).ToString();//1,1
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                try
                {
                    index[0] = (int.Parse(index[0]) + 1).ToString();//2,1
                    index[1] = (int.Parse(index[1]) - 1).ToString();//2,0
                    result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                }
                catch
                {
                    result.Add(-1);
                }
            }
            else if (int.Parse(index[0]) > 0 && int.Parse(index[1]) > 0)
            {               //2,1
                index[0] = (int.Parse(index[0]) - 1).ToString();//1,1
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                index[0] = (int.Parse(index[0]) + 1).ToString();//2,1
                index[1] = (int.Parse(index[1]) - 1).ToString();//2,0
                result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                try
                {
                    index[0] = (int.Parse(index[0]) + 1).ToString();//3,0
                    index[1] = (int.Parse(index[1]) + 1).ToString();//3,1
                    result.Add(lists[int.Parse(index[0]), int.Parse(index[1])]);
                }
                catch
                {

                    result.Add(-1);
                }
            }
            return result;
        }
        //To find index to element of two dimensional array 
        public static string CoordinatesOf(int[,] matrix, int value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return $"{x},{y}";
                }
            }

            return "-1,-1";
        }

    }
}
