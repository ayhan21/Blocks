using System;
using System.Collections.Generic;
using System.Linq;

namespace Blocks
{
    class Program
    {
        static Block[,] blockArray;
        static List<Block> blockList;
        static char[] blockNames = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        //static char[] blockNames = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K' };
        static int size_A, size_B;
        static double multiple;

        public static void Main(string[] args)
        {
            input();
        }

        public static void input()
        {
            try
            {
                Console.Write("Enter size for A: ");
                size_A = int.Parse(Console.ReadLine());
                Console.Write("Enter size for B: ");
                size_B = int.Parse(Console.ReadLine());
                Console.Write("Enter multiple: ");
                multiple = double.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter a valid number!");
                input();
            }

            initArrays(size_A, size_B);
        }

        public static void initArrays(int size_A, int size_B)
        {
            Random rnd = new Random();
            blockList = new List<Block>();

            foreach(char c in blockNames)
            {
                int val = rnd.Next(1, 10);
                if(val % multiple == 0)
                {
                    blockList.Add(new Block(val, c));
                }
            }


            blockArray = new Block[size_A, size_B];

            PrintList(blockList);
            fitBlocks();
        }

        public static void fitBlocks()
        {
            int iter = 0;
            int fit = 0;
            int cap = size_A * size_B;
            int start_i = blockArray.GetLength(0) - 1;
            int start_j = blockArray.GetLength(1) - 1;

            foreach (Block block in blockList)
            {
                iter += 1;
                double spots = block.GetSize() / multiple;
                if (cap >= spots)
                {
                    fit += 1;
                    for (int i = start_i; i >= 0; i--)
                    {
                        iter += 1;

                        if (spots != 0)
                        {
                            for (int j = start_j; j >= 0; j--)
                            {
                                iter += 1;

                                if (spots != 0)
                                {
                                    if (blockArray[i, j] == null)
                                    {
                                        blockArray[i, j] = block;
                                        spots -= 1;
                                        cap -= 1;
                                    }
                                }
                                else
                                {
                                    start_i = i;
                                    start_j = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            start_j = blockArray.GetLength(1) - 1;
                            break;
                        }

                    }
                }
            }

            Print2DArray(blockArray);
            Console.WriteLine("\nIterations: " + iter.ToString());
            Console.WriteLine("\nBlocks: " + fit.ToString());
        }

        public static void RollBack(Block[,] blockArray, Block block)
        {
            for (int i = 0; i < blockArray.GetLength(0); i++)
            {
                for (int j = 0; j < blockArray.GetLength(1); j++)
                {
                    if (blockArray[i, j].GetName() == block.GetName())
                    {
                        blockArray[i, j] = null;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            
        }

                public static void restart()
        {
            Console.Clear();
            input();
        }

        public static void Print2DArray<T>(T[,] matrix)
        {
            Console.WriteLine();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i, j] != null)
                    {
                        Console.Write(matrix[i, j].ToString() + "\t");
                    }
                    else
                    {
                        Console.Write("." + "\t");
                    }                   
                }
                Console.WriteLine();
            }
        }

        public static void PrintList(List<Block> list)
        {
            foreach (Block b in list)
            {
                Console.Write("[" + b.GetName() + " - " + b.GetSize() + "] ");
            }
            Console.WriteLine();
        }
    }

    class Block
    {
        public Block(int size, char name)
        {
            Size = size;
            Name = name;
        }

        int Size { get; set; }
        char Name { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public int GetSize() { return Size; }
        public string GetName() { return Name.ToString(); }

    }
}
