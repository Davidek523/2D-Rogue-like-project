class Player
{
    public int Width { get; set; }
    public int Height { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float PreviousX { get; set; }
    public float PreviousY { get; set; }
    public int Speed { get; set; } = 3;
    public int HP { get; set; } = 100;
    public int Armor { get; set; } = 0;
    public int Attack { get; set; } = 10;
    public bool IsEnemyHit { get; set; } = false;
    public bool IsFlashing { get; set; }
    public float FlashTimer { get; set; }
    public float FlashDuration { get; set; } = 1f;
    public bool IsArmorEquipped { get; set; }
    public Weapon EquippedWeapon { get; set; }

    public Player(int width, int height, float x, float y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
        EquippedWeapon = new Sword(25, 25, X + 15, Y + 5, EntityType.Player);
    }

    public void Update(Enemy enemy, float deltaTime)
    {
        // If player HP is less then 0, it doesn't let HP go below 0
        if (HP < 0)
        {
            HP = 0;
        }

        if (IsFlashing)
        {
            FlashTimer -= deltaTime;
            if (FlashTimer <= 0)
            {
                FlashTimer = 0;
                IsFlashing = false;
            }
        }

        HandleMovement();
        HandleCollision();
        BrokenArmor();

        if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space))
        {
            EquippedWeapon.Attack(enemy, this);
            Console.WriteLine("Attack executed"); // DEBUG
        }
        EquippedWeapon.Update(this, enemy, deltaTime);
    }

    public void HandleMovement()
    {
        PreviousX = X;
        PreviousY = Y;

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
    }

    public void HandleCollision()
    {
        bool collisions = false;
        float oldX = X;
        float oldY = Y;

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

    public void PlayerDeath()
    {
        // Temporary logic for player death
        if (HP <= 0)
        {
            X = -100;
            Y = -100;
        }
    }

    public void BrokenArmor()
    {
        if (Armor > 0)
        {
            IsArmorEquipped = true;
        }

        if (Armor <= 0)
        {
            IsArmorEquipped = false;
        }
    }

    public void Draw()
    {
        Raylib_cs.Color playerColor = Raylib_cs.Color.Red;

        if (IsFlashing)
        {
            if ((int)(FlashTimer * 5) % 2 == 0)
            {
                playerColor = Raylib_cs.Color.Maroon;
            }
            else
            {
                playerColor = Raylib_cs.Color.Red;
            }
        }
        Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, playerColor);
        EquippedWeapon.Draw();
    }
}