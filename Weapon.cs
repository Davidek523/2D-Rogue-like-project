public enum EntityType
{
    Player,
    Enemy
}

abstract class Weapon
{
    public int Width { get; set; }
    public int Height { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float AttackCooldown = 0f;
    public float MaxCooldown = 1.5f;
    public string WeaponDir { get; set; } = "up";
    public EntityType EntityType { get; set; }

    public Weapon(int width, int height, float x, float y, EntityType entityType)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
        EntityType = entityType;
    }

    public virtual void Update(Player player, Enemy enemy, float deltaTime)
    {
        if (AttackCooldown > 0)
        {
            AttackCooldown -= deltaTime;
        }

        if (EntityType == EntityType.Player)
        {
            HandleInputs(player);
        }
        else if (EntityType == EntityType.Enemy)
        {
            HandleInputsEnemy(enemy, player);
        }
    }

    public void HandleInputs(Player player)
    {
        // Weapon changes direction with arrow keys
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Up)) WeaponDir = "up";
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Down)) WeaponDir = "down";
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Left)) WeaponDir = "left";
        if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Right)) WeaponDir = "right";

        // Weapon dynamically changes position based on player's movement
        switch (WeaponDir)
        {
            case "up":
                X = player.X + (player.Width / 2) - (Width / 2);
                Y = player.Y - Height;
                break;
            case "down":
                X = player.X + (player.Width / 2) - (Width / 2);
                Y = player.Y + player.Height;
                break;
            case "right":
                X = player.X + player.Width;
                Y = player.Y + (player.Height / 2) - (Height / 2);
                break;
            case "left":
                X = player.X - Width;
                Y = player.Y + (player.Height / 2) - (Height / 2);
                break;
        }
    }

    public void HandleInputsEnemy(Enemy enemy, Player player)
    {
        float dx = player.X - enemy.X;
        float dy = player.Y - enemy.Y;

        // if (enemy.X < X) WeaponDir = "left";
        // if (enemy.X > X) WeaponDir = "right";
        // if (enemy.Y < Y) WeaponDir = "up";
        // if (enemy.Y > Y) WeaponDir = "down";

        if (Math.Abs(dx) > Math.Abs(dy))
        {
            if (dx > 0)
            {
                WeaponDir = "right";
            }
            else
            {
                WeaponDir = "left";
            }

            if (dy > 0)
            {
                WeaponDir = "down";
            }
            else
            {
                WeaponDir = "up";
            }
        }

        switch (WeaponDir)
        {
            case "up":
                X = enemy.X + (enemy.Width / 2) - (Width / 2);
                Y = enemy.Y - Height;
                break;
            case "down":
                X = enemy.X + (enemy.Width / 2) - (Width / 2);
                Y = enemy.Y + enemy.Height;
                break;
            case "rigt":
                X = enemy.X + enemy.Width;
                Y = enemy.Y + (enemy.Height / 2) - (Height / 2);
                break;
            case "left":
                X = enemy.X - enemy.Width;
                Y = enemy.Y + (enemy.Height / 2) - (Height / 2);
                break;
        }
    }

    public abstract void Attack(Enemy enemy, Player player);

    public virtual void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Gold);
    }
}