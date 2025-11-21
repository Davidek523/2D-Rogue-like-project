using System.Numerics;

enum ProjectileType
{
    Arrow,
    Fireball
}

class Projectiles
{
    public Vector2 Position { get; set; }
    private Vector2 _startPos { get; set; }
    public Vector2 Direction { get; set; }
    public bool IsActive { get; set; } = true;
    public float Speed { get; set; }
    public float Range { get; set; } = 450f;
    public float Width { get; set; }
    public float Height { get; set; }
    public ProjectileType Type { get; set; }

    public Projectiles(float x, float y, Vector2 direction, float speed, ProjectileType type)
    {
        Position = new Vector2(x, y);
        _startPos = Position;
        Speed = speed;
        Direction = Vector2.Normalize(direction);
        Type = type;

        switch (type)
        {
            case ProjectileType.Arrow:
                Width = 6;
                Height = 6;
                break;
            case ProjectileType.Fireball:
                Width = 30;
                Height = 30;
                break;
        }
    }

    public void Update(Weapon weapon)
    {
        if (!IsActive) return;

        Position += Direction * Speed;

        if (Vector2.Distance(Position, _startPos) >= Range)
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