using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}, {1}: ", i+1, j+1);
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
                Console.WriteLine();
            }

            int n = FindMinWay(matrix, out matrix);
            int[] values = new int[2];
            Degree(matrix, out values);
            int[,] newMatr = DeleteCollNRow(matrix, values);
            for (int i = 0; i < newMatr.GetLength(0); i++)
            {
                for (int j = 0; j < newMatr.GetLength(0); j++)
                {
                    Console.Write(newMatr[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }

        static int FindMinWay(int[,] matrix, out int[,] matrix_)
        {
            int sum = 0;
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                int min = int.MaxValue;
                for (int k = 0; k < n; k++)
                {
                    if (matrix[i,k] < min)
                    {
                        min = matrix[i, k];
                    }
                }
                sum += min;
                for (int k = 0; k < n; k++)
                {
                    matrix[i, k] -= min;
                }
            }

            for (int i = 0; i < n; i++)
            {
                int min = int.MaxValue;
                for (int k = 0; k < n; k++)
                {
                    if (matrix[k, i] < min)
                    {
                        min = matrix[i, k];
                    }
                }
                sum += min;
                for (int k = 0; k < n; k++)
                {
                    matrix[k, i] -= min;
                }
            }

            matrix_ = matrix;
            return sum;
        }

        static void Degree(int[,] matrix, out int[] indfordel)
        {
            Dictionary<int[], int> dix = new Dictionary<int[], int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    if (matrix[i,k] == 0)
                    {
                        int min = FindMin(matrix, k, i);
                        dix.Add(new int[] {i, k}, min);
                    }
                }
            }

            dix.OrderBy(e => e.Value);

            indfordel = dix.Keys.ElementAt(0);
        }

        static int[,] DeleteCollNRow(int[,] matrix, int[] ind)
        {
            int n = matrix.GetLength(0) - 1;
            int row = ind[0];
            int coll = ind[1];
            int[,] res = new int[n, n];

            for (int i = 0, j = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row)
                {
                    continue;
                }

                for (int k = 0, u = 0; k < matrix.GetLength(0); k++)
                {
                    if (k == coll) continue;

                    res[j, u] = matrix[i, k];
                    u++;
                }
                j++;
            }

            res[coll, row] = int.MaxValue;

            return res;
        }

        static int FindMin(int[,] matrix, int a, int b)
        {
            int min1 = int.MaxValue;
            int min2 = int.MaxValue;
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[a, i] < min1)
                {
                    min1 = matrix[a, i];
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[a, i] < min1)
                {
                    min1 = matrix[a, i];
                }
            }

            return min1 + min2;
        }    
    }
}
