using System.Numerics;

class Imp : Enemy
{
    public Fireball FireballWeapon;
    private Random rand = new Random();
    public float TeleportationCooldown = 0f;
    public float MaxTeleportationCooldown = 6f;
    public override int Attack { get; set; } = 15;
    public Imp(int width, int height, float x, float y) : base(width, height, x, y)
    {
        FireballWeapon = new Fireball(20, 20, x, y, EntityType.Enemy);
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        Teleport(deltaTime);

        FireballWeapon.Update(player, this, deltaTime);
        FireballWeapon.Attack(this, player);
        MapCollision(X, Y);
    }

    public void Teleport(float deltaTime)
    {
        if (TeleportationCooldown <= 0)
        {
            X = rand.Next(0, Raylib_cs.Raylib.GetScreenWidth() - Width);
            Y = rand.Next(0, Raylib_cs.Raylib.GetScreenHeight() - Height);
            TeleportationCooldown = MaxTeleportationCooldown;
        }
        else
        {
            TeleportationCooldown -= 1f * deltaTime;
        }
    }

    public override void Draw()
    {
        base.Draw();
        FireballWeapon.Draw();
    }
}