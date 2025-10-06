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
    public string WeaponDir { get; set; } = "right";

    public Weapon(int width, int height, float x, float y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public virtual void Update(Player player, Enemy enemy, float deltaTime)
    {
        HandleInputs(player);
        TryAttack(enemy, player);
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

    public void HandleInputsEnemy(Enemy enemy)
    {
        if (enemy.X < X) WeaponDir = "left";
        if (enemy.X > X) WeaponDir = "right";
        if (enemy.Y < Y) WeaponDir = "up";
        if (enemy.Y > Y) WeaponDir = "down";

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

    public void TryAttack(Enemy enemy, Player player)
    {
        if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space))
        {
            Attack(enemy, player);
        }
    }

    public abstract void Attack(Enemy enemy, Player player);

    public virtual void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Gold);
    }
}