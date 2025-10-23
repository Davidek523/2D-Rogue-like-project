using Raylib_cs;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(1360, 720, "Rouge Like Game");
        Raylib.SetTargetFPS(60);

        Map map = new Map(80, 80);

        RoomManager roomManager = new RoomManager(map);
        Player player = new Player(25, 25, 600, 300);

        Enemy enemy = new Mercenary(25, 25, 200, 200);
        // Enemy enemy = new Skeletons(25, 25, 400, 400);
        // Enemy enemy = new Imp(25, 25, 400, 400);
        Weapon weapon = new Sword(25, 25, player.X + 15, player.Y + 5, EntityType.Player);
        // Weapon weapon = new Bow(25, 25, player.X + 15, player.Y + 5, EntityType.Enemy);
        // Weapon weapon = new Fireball(25, 25, player.X + 15, player.Y + 5, EntityType.Enemy);
        UI userInterace = new UI();

        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();

            roomManager.Update(player, deltaTime);

            player.Update();
            // if (enemy.IsPlayerHit)
            // {
            //     player.HP -= enemy.Attack;
            //     enemy.IsPlayerHit = false;
            // }
            player.PlayerDeath();

            // enemy.Update(player, deltaTime);
            weapon.Update(player, enemy, deltaTime);
            if (player.IsEnemyHit)
            {
                enemy.HP -= player.Attack;
                player.IsEnemyHit = false;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            // map.Draw();
            roomManager.Draw();
            weapon.Draw();
            player.Draw();
            // enemy.Draw();

            userInterace.DrawHealth(player.HP);
            userInterace.DrawArmor(player.Armor);

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}