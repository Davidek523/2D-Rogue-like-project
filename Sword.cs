using System.Numerics;

class Sword : Weapon
{
    public bool IsSlashing { get; set; }
    public float Angle { get; set; }
    public float SlashSpeed = 360f;
    public float SlashDuration = 0.25f;
    private float slashTimer { get; set; }
    public Sword(int width, int height, float x, float y, EntityType entityType) : base(width, height, x, y, entityType)
    {
        IsSlashing = false;
        Angle = -45f;
    }

    public void StartSlash()
    {
        if (!IsSlashing)
        {
            IsSlashing = true;
            slashTimer = SlashDuration;
            Angle = -90f;
            Console.WriteLine("Slash started"); // DEBUG
        }
    }

    public override void Attack(Enemy enemy, Player player)
    {
        StartSlash();
        if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height), new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
        {
            player.IsEnemyHit = true;
            Console.WriteLine("Enemy hit by sword"); // DEBUG
        }
    }

    public override void Update(Player player, Enemy enemy, float deltaTime)
    {
        base.Update(player, enemy, deltaTime);

        if (IsSlashing)
        {
            slashTimer -= deltaTime;
            Angle += SlashSpeed * deltaTime;

            if (slashTimer <= 0)
            {
                IsSlashing = false;
            }
        }

        // Applying the rotation matrix
        Vector2 playerCenter = new Vector2(player.X + player.Width / 2, player.Y + player.Height / 2);

        float oldX = X - playerCenter.X;
        float oldY = Y - playerCenter.Y;

        float rX = oldX * MathF.Cos(Angle) - oldY * MathF.Sin(Angle);
        float rY = oldX * MathF.Sin(Angle) + oldY * MathF.Cos(Angle);

        this.X = rX + playerCenter.X;
        this.Y = rY + playerCenter.Y;
    }

    public override void Draw()
    {
        if (IsSlashing)
        {
            Vector2 origin = new Vector2(Width / 2, Height / 2);
            Raylib_cs.Raylib.DrawRectanglePro(new Raylib_cs.Rectangle(X, Y, Width, Height), origin, Angle, Raylib_cs.Color.Green);
        }
        else
        {
            base.Draw();
        }
    }
}