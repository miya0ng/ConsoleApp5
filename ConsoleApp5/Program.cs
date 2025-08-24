using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

bool open = true;
bool isPlaying = false;

while (open)
{
    Console.Clear();
    Console.WriteLine("Snake Game");
    Console.WriteLine();
    Console.WriteLine("SPACE 키를 눌러서 시작하세요!");

    ConsoleKeyInfo Key;
    Key = Console.ReadKey(intercept: true);

    if (Key.Key == ConsoleKey.Spacebar)
    {
        isPlaying = true;
    }

    if (Key.Key == ConsoleKey.Escape)
    {
        break;
    }

    Console.Clear();
    Console.CursorVisible = false;

    Map map = new Map(10, 7);
    Goal goal = new Goal();
    Player player = new Player((4,3));
    Box box = new Box(player, (3,2));
    Box box2 = new Box(player, (3,4));

    player.Init();
    goal.Init();

    while (isPlaying)
    {
        player.Update();
        box.Update();
        box2.Update();

        box.Draw();
        box2.Draw();
        player.Draw();

        if (Map.Instance.isGameOver)
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine("SPACE 키를 눌러서 다시 시작하세요!");

            ConsoleKeyInfo reStart = Console.ReadKey(intercept: true);

            if (reStart.Key == ConsoleKey.Spacebar)
            {
                Map.Instance.isGameOver = false;
                isPlaying = false;
            }
        }
    }
}