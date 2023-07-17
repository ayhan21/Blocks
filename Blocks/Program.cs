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
        static int size_A, size_B, max;
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
                // size of 2d array
                size_A = int.Parse(Console.ReadLine());
                Console.Write("Enter size for B: ");
                size_B = int.Parse(Console.ReadLine());
                Console.Write("Enter multiple: ");
                // kratno
                multiple = double.Parse(Console.ReadLine());
                Console.Write("Enter max block size: ");
                // max size for block
                max = int.Parse(Console.ReadLine());
                initArrays(size_A, size_B, max);
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter a valid number!");
                input();
            }
        }

        public static void initArrays(int size_A, int size_B, int max)
        {
            Random rnd = new Random();
            blockList = new List<Block>();

            // generate size for each block
            foreach(char c in blockNames)
            {
                bool ItemSet = false;
                int tries = 0;
                // until a valid value is generated
                while (!ItemSet)
                {
                    int val = rnd.Next(1, max);
                    if (val % multiple == 0)
                    {
                        blockList.Add(new Block(val, c));
                        ItemSet = true;
                    } else
                    {
                        tries++;
                    }

                    // skip element after 5 invalid values
                    if (tries == 5)
                    {
                        break;
                    }
                }
            }


            blockArray = new Block[size_A, size_B];


            if (IsListEmpty(blockList))
            {
                Console.WriteLine("Could not generate blocks with the given input!");
                restart(false);
            } else
            {
                PrintList(blockList);
                fitBlocks();
            }
        }

        public static void fitBlocks()
        {
            int iter = 0;
            int fit = 0;
            // calculate total capacity of 2d array
            int cap = size_A * size_B;

            // start indexes - last element of array
            int start_i = blockArray.GetLength(0) - 1;
            int start_j = blockArray.GetLength(1) - 1;

            // iterate blocks
            foreach (Block block in blockList)
            {
                iter += 1;
                // calculate how many array elements the block will need
                double spots = block.GetSize() / multiple;
                // continue if capacity is bigger than block size
                if (cap >= spots)
                {
                    fit += 1;
                    // iterate 2d array row
                    for (int i = start_i; i >= 0; i--)
                    {
                        iter += 1;

                        // iterate 2d array column
                        if (spots != 0)
                        {
                            for (int j = start_j; j >= 0; j--)
                            {
                                iter += 1;

                                if (spots != 0)
                                {
                                    // if the current element is null, block can be added
                                    if (blockArray[i, j] == null)
                                    {
                                        blockArray[i, j] = block;
                                        // if added, spot -1 and total capacity -1
                                        spots -= 1;
                                        cap -= 1;
                                    }
                                }
                                else
                                {
                                    // remember the indexes of array when the block adding is finished; avoid iterating full array every time
                                    start_i = i;
                                    start_j = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // reset column index when block isn't finished adding, but the row is full
                            start_j = blockArray.GetLength(1) - 1;
                            break;
                        }

                    }
                }
            }

            Print2DArray(blockArray);
            //Console.WriteLine("\nIterations: " + iter.ToString());
            //Console.WriteLine("\nBlocks: " + fit.ToString());
            RestartPrompt();
        }
        public static void RestartPrompt()
        {
            Console.Write("\nType 0 to restart: ");
            if (Console.ReadLine() == "0")
            {
                restart(true);
            }
        }

        public static bool IsListEmpty(List<Block> list)
        {
            return list.FindIndex(x => x != null) == -1;
        }

        public static void restart(bool ClearConsole)
        {
            if (ClearConsole)
            {
                Console.Clear();
            }
            input();
        }

        public static void Print2DArray<T>(T[,] matrix)
        {
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

    // object for block
    class Block
    {
        int Size { get; set; }
        char Name { get; set; }

        public Block(int size, char name)
        {
            Size = size;
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public int GetSize() { return Size; }
        public string GetName() { return Name.ToString(); }

    }
}
