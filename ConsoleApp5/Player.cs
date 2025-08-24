using System;
using System.Diagnostics;
using static Map;
using static System.Formats.Asn1.AsnWriter;

public class Player
{
    private Goal goal;
    private LinkedList<(int x, int y)> bodyPos;

    private (int x, int y) prevHeadPos;
    private (int x, int y) oldHeadPos;

    public enum Direction { Up, Down, Left, Right }

    public (int dx, int dy) dir;

    private (int dx, int dy)[] directions =
{
    (0, -1),  // Up
    (0,  1),  // Down
    (-1, 0),  // Left
    (1,  0)   // Right
};
    public Player(Goal goal)
    {
        this.goal = this.goal;
        Random random = new Random();
        prevHeadPos = (random.Next(1, Map.Instance.col - 1), random.Next(1, Map.Instance.row - 1));
        bodyPos = new LinkedList<(int x, int y)>();
        dir = directions[random.Next(0, 4)];
    }

    public void Init()
    {
        oldHeadPos = prevHeadPos;
        Console.SetCursorPosition(prevHeadPos.x, prevHeadPos.y);
        Console.Write("P");
        Map.mapInfos[prevHeadPos.y, prevHeadPos.x] = Map.MapInfo.Player;
    }

    public void Update()
    {

        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        EventChangeDirection(keyInfo.Key);

        Map.mapInfos[prevHeadPos.y, prevHeadPos.x] = Map.MapInfo.Blank;

        var nextPos = (prevHeadPos.x + dir.dx, prevHeadPos.y + dir.dy);

        if (CheckCollisionWithGameOver(prevHeadPos))
        {
            Map.Instance.isGameOver = true;
            return;
        }

        if (CheckCollisionWithItem(nextPos))
        {

        }

        Move(nextPos);
        if(Map.mapInfos[prevHeadPos.y, prevHeadPos.x]!=Map.MapInfo.Dollar)
        {
            oldHeadPos = (prevHeadPos.x - dir.dx, prevHeadPos.y - dir.dy);
            Map.mapInfos[oldHeadPos.y, oldHeadPos.x] = Map.MapInfo.Blank;
            Console.SetCursorPosition(oldHeadPos.x, oldHeadPos.y);
            Console.Write(" ");
        }
    }
    public void EventChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                dir = directions[0];
                break;
            case ConsoleKey.DownArrow:
                dir = directions[1];
                break;
            case ConsoleKey.LeftArrow:
                dir = directions[2];
                break;
            case ConsoleKey.RightArrow:
                dir = directions[3];
                break;
            default:
                return;
        }
    }

    public void Move((int x, int y) nextPos)
    {
        prevHeadPos = nextPos;

        Map.Instance.map[prevHeadPos.y, prevHeadPos.x] = 'P';
        Map.mapInfos[prevHeadPos.y, prevHeadPos.x] = Map.MapInfo.Player;
    }

    public bool CheckCollisionWithGameOver((int x, int y) nextPos)
    {
        if (nextPos.x <= 0 || nextPos.x >= Map.Instance.col + 1 ||
           nextPos.y <= 0 || nextPos.y >= Map.Instance.row + 1)
        {
            return true;
        }

        if (Map.mapInfos[nextPos.y, nextPos.x] == Map.MapInfo.Wall ||
            Map.mapInfos[nextPos.y, nextPos.x] == Map.MapInfo.Player)
        {
            return true;
        }

        return false;
    }

    public bool CheckCollisionWithItem((int x, int y) nextPos)
    {
        if (Map.mapInfos[nextPos.y, nextPos.x] == Map.MapInfo.Goal)
        {
            return true;
        }
        return false;
    }

    public void Draw()
    {
        Console.SetCursorPosition(prevHeadPos.x, prevHeadPos.y);
        Console.Write("P");
    }
}
