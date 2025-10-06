class Player
{
    public int Width { get; set; }
    public int Height { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int Speed { get; set; } = 3;
    public int HP { get; set; } = 100;
    public int Armor { get; set; } = 0;
    public int Attack { get; set; } = 10;
    public bool IsEnemyHit { get; set; } = false;

    public Player(int width, int height, float x, float y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public void Update()
    {
        bool collisions = false;
        float oldX = (int)X;
        float oldY = (int)Y;

        // If player HP is less then 0, it doesn't let HP go below 0
        if (HP < 0)
        {
            HP = 0;
        }

        // Movement of the player
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.W))
        {
            Y -= Speed;
        }
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.S))
        {
            Y += Speed;
        }
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.A))
        {
            X -= Speed;
        }
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.D))
        {
            X += Speed;
        }

        // Looping through the grid and checking for collisions between player and wall/door
        int row = Grid.GetGrid().GetLength(0);
        int col = Grid.GetGrid().GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Grid.GetGrid()[i, j] == 1)
                {
                    if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height),
                                                            new Raylib_cs.Rectangle(j * 80, i * 80, 80, 80)))
                    {
                        collisions = true;
                        break;
                    }
                }

                if (Grid.GetGrid()[i, j] == 2)
                {
                    if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height),
                                                            new Raylib_cs.Rectangle(j * 80, i * 80, 80, 80)))
                    {
                        Console.WriteLine("Entered a door");
                        break;
                    }
                }
            }
            if (collisions)
            {
                break;
            }
        }

        // If there is a collision, it resets the player position to the old position
        if (collisions)
        {
            X = oldX;
            Y = oldY;
        }
    }

    public void PlayerAttack(float enemyX, float enemyY, Weapon weapon)
    {
        if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space))
        {
            if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(weapon.X, weapon.Y, weapon.Width, weapon.Height),
                                                    new Raylib_cs.Rectangle(enemyX, enemyY, 25, 25)))
            {
                IsEnemyHit = true;
                Console.WriteLine($"Enemy is hit for {Attack} damage");
            } 
        }
    }

    public void PlayerDeath()
    {
        // Temporary logic for player death
        if (HP <= 0)
        {
            X = -100;
            Y = -100;
        }
    }

    public void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Red);
    }
}