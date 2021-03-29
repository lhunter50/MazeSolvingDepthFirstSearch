using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Assignment2;

namespace Assignment2
{
    class Maze
    {
        public Point StartingPoint { get; set; }
        public int RowLength { get; set; }
        public int ColumnLength { get; set; }
        public char[][] CharMaze { get; set; }

        private string fileName;
        Stack<Point> stack;

        public Maze(string fileName)
        {
            string[] fileLines = File.ReadAllLines(fileName);
            string[] startingPointText = fileLines[1].Split();
            StartingPoint = new Point(Convert.ToInt32(startingPointText[0]), Convert.ToInt32(startingPointText[1]));

            CharMaze = new char[fileLines.Length - 2][];
            for (int i = 2; i < fileLines.Length; i++)
            {
                CharMaze[i - 2] = fileLines[i].ToCharArray();
            }
        }

        public Maze(int startingRow, int startingColumn, char[][] existingMaze)
        {
            if (startingColumn == 0 || startingRow == 0 || existingMaze[startingRow][startingColumn] == 'E')
            {
                throw new ApplicationException();
            }
            else if (startingColumn <= -1 || startingRow <= -1 || startingColumn >= existingMaze[0].Length || startingRow >= existingMaze.Length)
            {
                throw new IndexOutOfRangeException();
            }
            CharMaze = existingMaze;
            StartingPoint = new Point(startingRow, startingColumn);
            RowLength = existingMaze.Length;
            ColumnLength = existingMaze[0].Length;
            //CurrentStack = new Stack<Point>();
        }

        public char[][] GetMaze()
        {
            return CharMaze;
        }

        public string PrintMaze()
        {
            string maze = "";
            for (int i = 0; i < RowLength; i++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    maze += CharMaze[i][x].ToString();
                }
                maze += "\n";
            }
            return maze.TrimEnd(Environment.NewLine.ToCharArray());
        }

        public string DepthFirstSearch()
        {
            stack = new Stack<Point>();
            stack.Push(StartingPoint);

            while(!stack.IsEmpty() && CharMaze[stack.Top().Row][stack.Top().Column] != 'E')
            {
                Point current = stack.Top();
                
                CharMaze[current.Row][current.Column] = 'V';

                if(CharMaze[current.Row + 1][current.Column] != 'W' && CharMaze[current.Row + 1][current.Column] != 'V')
                {
                    stack.Push(new Point(current.Row + 1, current.Column));
                }
                else if(CharMaze[current.Row][current.Column + 1] != 'W' && CharMaze[current.Row][current.Column + 1] != 'V' && CharMaze[current.Row][current.Column + 1] != '.')
                {
                    Point path = new Point(current.Row, current.Column + 1);
                    stack.Push(path);
                }
                else if(CharMaze[current.Row][current.Column - 1] != 'V' && CharMaze[current.Row][current.Column - 1] != 'W' && CharMaze[current.Row][current.Column - 1] != '.')
                {
                    Point path = new Point(current.Row, current.Column - 1);
                    stack.Push(path);
                }
                else if(CharMaze[current.Row - 1][current.Column] != 'V' && CharMaze[current.Row - 1][current.Column] != 'W' && CharMaze[current.Row - 1][current.Column] != '.')
                {
                    Point path = new Point(current.Row - 1, current.Column);
                    stack.Push(path);
                }
                else
                {
                    stack.Pop();
                }
            }

            string stringPath = "";
            if (!stack.IsEmpty())
            {
                Point EndingPoint = new Point(stack.Top().Row, stack.Top().Column);
                stringPath = PrintPath(StartingPoint, EndingPoint);
            }
            else
            {
                stringPath = "No exit found in maze!\n\n";
            }

            string mazePrint = PrintMaze();
            return stringPath + mazePrint;
        }

        public string PrintPath( Point StartingPoint, Point EndingPoint)
        {
            Stack<Point> ReverseStack = new Stack<Point>();
            string StringPath = "";
            while (!stack.IsEmpty())
            {
                Point p = stack.Pop();
                ReverseStack.Push(p);
                CharMaze[p.Row][p.Column] = CharMaze[p.Row][p.Column] != 'E' ? '.' : 'E'; 
                StringPath = p + "\n" + StringPath;
            }

            stack = ReverseStack;
            StringPath = string.Format("Path to follow from Start {0} to Exit {1} - {2} steps:\n{3}", StartingPoint, EndingPoint, stack.Size,StringPath);
           
            //while (!stack.IsEmpty())
            //{
            //    StringPath += stack.Top().ToString() + "\n";
            //    stack.Pop();
            //}
            return StringPath;
        }

        //public Stack<Point> OrderStack(Stack<Point> CurrentStack)
        //{
        //    Stack<Point> ReverseStack = new Stack<Point>();
        //    ReverseStack = CurrentStack;
        //    Stack<Point> OrderedStack = new Stack<Point>();
        //    while (!ReverseStack.IsEmpty())
        //    {
        //        OrderedStack.Push(ReverseStack.Top());
        //        ReverseStack.Pop();
        //    }

        //    return OrderedStack;
        //}

        public Stack<Point> GetPathToFollow()
        {
            if (stack == null)
            {
                throw new ApplicationException();
            }

            return stack.Clone();

            //Stack<Point> ClonedStack = OrderStack(stack);

            //return ClonedStack;
        }
    }
}
