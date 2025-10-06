using System.Numerics;

class Skeletons : Enemy
{
    public List<Projectiles> Bones;
    public ProjectileType Type;
    public override int Attack { get; set; } = 10;
    public Skeletons(int width, int height, float x, float y) : base(width, height, x, y)
    {
        Bones = new List<Projectiles>();
        Type = ProjectileType.Arrow;
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        if (player.X < X)
        {
            X += Speed;
        }
        if (player.X > X)
        {
            X -= Speed;
        }
        if (player.Y < Y)
        {
            Y += Speed;
        }
        if (player.Y > Y)
        {
            Y -= Speed;
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= deltaTime;
        }
    }

    // public override void AttackPlayer(Player player)
    // {

    // }

    public override void Draw()
    {
        base.Draw();
    }
}