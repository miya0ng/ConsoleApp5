using static Map;
public class Goal
{
    Random random = new Random();
    public Goal()
    {

    }

    public void Init()
    {
        GoalSpawn();
        GoalSpawn();
    }

    public void GoalSpawn()
    {
        int GoalX = random.Next(1, Map.Instance.col - 1);
        int GoalY = random.Next(1, Map.Instance.row - 1);
        if (Map.mapInfos[GoalY, GoalX] == MapInfo.Blank)
        {
            Map.Instance.map[GoalY, GoalX] = '*';
            Map.mapInfos[GoalY, GoalX] = Map.MapInfo.Goal;
            Console.SetCursorPosition(GoalX, GoalY);
            Console.Write("*");
        }
    }
}