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

    public (int x, int y) prevBoxPos;
    private (int dx, int dy) dir;
    public Box(Player player, (int x, int y) boxPos)
    {
        this.player = player;
        prevBoxPos = boxPos;
        //BoxSpawn(prevBoxPos);
    }

    public void BoxSpawn()
    {

        if (Map.mapInfos[prevBoxPos.y, prevBoxPos.x] == MapInfo.Blank)
        {
            Console.SetCursorPosition(prevBoxPos.x, prevBoxPos.y);
            Console.Write("□");
            Map.Instance.map[prevBoxPos.y, prevBoxPos.x] = '□';
            //Map.mapInfos[prevBoxPos.y, prevBoxPos.x] = Map.MapInfo.Goal;
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

        //var nextPos = prevBoxPos;
        //if(dir.dx != 0 || dir.dy != 0)
        //var nextPos = (prevBoxPos.x + dir.dx, prevBoxPos.y + dir.dy);

        if (isPushed)
        {
            var nextPos = (prevBoxPos.x + dir.dx, prevBoxPos.y + dir.dy);
            Move(nextPos);
            isPushed = false;
        }
    }
    private void Move((int x, int y) pos)
    {
        if (Map.mapInfos[pos.y, pos.x] == Map.MapInfo.Wall)
        {
            return;
        }

        prevBoxPos = pos;
        Map.Instance.map[prevBoxPos.y, prevBoxPos.x] = '□';
        Map.mapInfos[prevBoxPos.y, prevBoxPos.x] = Map.MapInfo.Box;
    }

    private bool CheckCollisionWithPlayer()
    {
        return Map.mapInfos[prevBoxPos.y, prevBoxPos.x] == Map.MapInfo.Player;
    }
    private bool CheckCollisionWithGoal()
    {
        var dir = player.dir;
        if (Map.mapInfos[prevBoxPos.y + dir.dy, prevBoxPos.x + dir.dx] == Map.MapInfo.Goal)
        {
            prevBoxPos = (prevBoxPos.x + dir.dx, prevBoxPos.y + dir.dy);
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
        else
        {
            Console.SetCursorPosition(prevBoxPos.x, prevBoxPos.y);
            Console.Write("$");
        }
    }
}