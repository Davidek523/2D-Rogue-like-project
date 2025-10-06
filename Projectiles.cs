using System.Numerics;

enum ProjectileType
{
    Arrow,
    Fireball
}

class Projectiles
{
    public Vector2 Position { get; set; }
    public bool IsActive { get; set; }
    public float Speed { get; set; }
    public float Range { get; set; } = 450f;

    public Projectiles(float x, float y, float speed)
    {
        Position = new Vector2(x, y);
        IsActive = true;
        Speed = speed;
    }

    public void Update(Weapon weapon)
    {
        switch (weapon.WeaponDir)
        {
            case "up":
                Position = new Vector2(Position.X, Position.Y - Speed);
                break;
            case "down":
                Position = new Vector2(Position.X, Position.Y + Speed);
                break;
            case "left":
                Position = new Vector2(Position.X - Speed, Position.Y);
                break;
            case "right":
                Position = new Vector2(Position.X + Speed, Position.Y);
                break;
        }

        if (Position.X <= Range || Position.X >= 1360 - Range || Position.Y <= Range || Position.Y >= 720 - Range)
        {
            IsActive = false;
        }
    }

    public void Draw(ProjectileType type)
    {
        if (type == ProjectileType.Arrow)
        {
            Raylib_cs.Raylib.DrawCircle((int)Position.X, (int)Position.Y, 3, Raylib_cs.Color.Blue);
        }
        else if (type == ProjectileType.Fireball)
        {
            Raylib_cs.Raylib.DrawCircle((int)Position.X, (int)Position.Y, 15, Raylib_cs.Color.Orange);
        }
    }
}