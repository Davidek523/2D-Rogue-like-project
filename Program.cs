using Raylib_cs;

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver,
    GameWon
}

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(1360, 720, "Rouge Like Game");
        Raylib.SetTargetFPS(60);

        Map map = new Map(80, 80);
        RoomManager roomManager = new RoomManager(map);
        GameState currentState = GameState.Menu;

        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            if (currentState == GameState.Menu)
            {
                Raylib.ClearBackground(Color.Black);
                Raylib.DrawText("Press ENTER to start", 540, 360, 20, Color.White);

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    currentState = GameState.Playing;
                }
            }
            else if (currentState == GameState.Playing)
            {
                roomManager.Update(deltaTime);
                roomManager.Draw();

                if (Raylib.IsKeyPressed(KeyboardKey.P))
                {
                    currentState = GameState.Paused;
                }

                if (roomManager.IsPlayerDead)
                {
                    currentState = GameState.GameOver;
                }
                    
                if (roomManager.CurrentRoom == RoomType.RoomThree && roomManager.IsEnemyDead)
                {
                    currentState = GameState.GameWon;
                } 
            }
            else if (currentState == GameState.Paused)
            {
                Raylib.ClearBackground(Color.Gray);
                Raylib.DrawText("Game Paused. Press P to resume.", 480, 360, 20, Color.White);

                if (Raylib.IsKeyPressed(KeyboardKey.P))
                {
                    currentState = GameState.Playing;
                }
            }
            else if (currentState == GameState.GameOver)
            {
                Raylib.ClearBackground(Color.Red);
                Raylib.DrawText("Game Over! Press ENTER to return to Menu.", 400, 360, 20, Color.White);

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    roomManager = new RoomManager(map); // Reset the game
                    currentState = GameState.Menu;
                }
            }
            else if (currentState == GameState.GameWon)
            {
                Raylib.ClearBackground(Color.Green);
                Raylib.DrawText("You Won! You succesfully looted the Dungeon! Press ENTER to return to Menu.", 300, 360, 20, Color.White);

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    roomManager = new RoomManager(map); // Reset the game
                    currentState = GameState.Menu;
                }
            }

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}