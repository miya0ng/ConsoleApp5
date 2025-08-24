using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Map;


public class Box
{

    private Player player;

    private bool isGoal = false;
    private bool isPushed = false;

    private (int x, int y) prevBoxPos;
    private (int dx, int dy) dir;
    public Box(Player player, (int x, int y) boxPos)
    {
        this.player = player;
        prevBoxPos = boxPos;
        BoxSpawn(prevBoxPos);
    }

    public void BoxSpawn((int x, int y) boxPos)
    {
        int BoxX = boxPos.x;
        int BoxY = boxPos.y;

        if (Map.mapInfos[BoxY, BoxX] == MapInfo.Blank)
        {
            Map.Instance.map[BoxY, BoxX] = '□';
            Map.mapInfos[BoxY, BoxX] = Map.MapInfo.Goal;
        }
    }

    public void Update()
    {
        dir = player.dir;

        if (CheckCollisionWithGoal())
        {
            isGoal = true;
        }

        if (CheckCollisionWithPlayer())
        {
            isPushed = true;
        }

        var nextPos = (prevBoxPos.x + dir.dx, prevBoxPos.y + dir.dy);

        if (isPushed)
        {
            Move(nextPos);
            isPushed = false;
        }
    }
    private void Move((int x, int y) pos)
    {
        prevBoxPos = pos;

        Map.Instance.map[prevBoxPos.y, prevBoxPos.x] = '□';
        Map.mapInfos[prevBoxPos.y, prevBoxPos.x] = Map.MapInfo.Box;
    }

    private bool CheckCollisionWithPlayer()
    {
        return true;
    }
    private bool CheckCollisionWithGoal()
    {
        if (Map.mapInfos[prevBoxPos.y, prevBoxPos.x] == Map.MapInfo.Goal)
        {
            Map.mapInfos[prevBoxPos.y, prevBoxPos.x] = MapInfo.Dollar;
            return true;
        }
        else return false;
    }
    public void Draw()
    {
        if (!isGoal)
        {
            Console.SetCursorPosition(prevBoxPos.x, prevBoxPos.y);
            Console.Write("□");
        }
        //else
        //{
        //    Console.SetCursorPosition(prevBoxPos.x, prevBoxPos.y);
        //    Console.Write("$");
        //}
    }
}