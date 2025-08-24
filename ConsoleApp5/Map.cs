using System.Drawing;
using System.Runtime.CompilerServices;

public class Map
{
    public static Map? Instance { get; private set; }

    public int row;
    public int col;

    public char[,] map;
    public enum MapInfo { Player, Goal, Wall, Blank , Box, Dollar}

    public static MapInfo[,]? mapInfos;

    public bool isGameOver = false;
    public Map(int c, int r)
    {
        Instance = this;
        map = new char[r, c];

        this.col = map.GetLength(1);
        this.row = map.GetLength(0);
        mapInfos = new MapInfo[row, col];
        DrawMap();
    }
    public void DrawMap()
    {
        for (int y = 0; y < row; y++)
        {
            if (y == 0 || y == row - 1)
            {
                for (int x = 0; x < col; x++)
                {
                    if (x == 0 || x == col - 1)
                    {
                        map[y, x] = '#';
                        mapInfos[y, x] = MapInfo.Wall;

                        Console.Write("#");
                    }
                    else
                    {
                        map[y, x] = '#';

                        Console.Write("#");
                        mapInfos[y, x] = MapInfo.Wall;
                    }
                }
            }
            else
            {
                for (int x = 0; x < col; x++)
                {
                    if (x == 0 || x == col - 1)
                    {
                        map[y, x] = '#';

                        Console.Write("#");
                        mapInfos[y, x] = MapInfo.Wall;
                    }
                    else
                    {
                        map[y, x] = ' ';

                        Console.Write(" ");
                        mapInfos[y, x] = MapInfo.Blank;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
