using System.Numerics;

class Fireball : Weapon
{
    public List<Projectiles> Fireballs;
    public ProjectileType Type;
    public Fireball(int width, int height, float x, float y) : base(width, height, x, y)
    {
        Fireballs = new List<Projectiles>();
        Type = ProjectileType.Fireball;
    }

    public override void Update(Player player, Enemy enemy, float deltaTime)
    {
        base.Update(player, enemy, deltaTime);

        foreach (var fireBall in Fireballs)
        {
            fireBall.Update(this);
        }
    }

    public override void Attack(Enemy enemy, Player player)
    {
        Fireballs.Add(new Projectiles(X + Width / 2, Y + Height / 2, 10f));
        foreach (var fireBall in Fireballs)
        {
            if (!fireBall.IsActive || Raylib_cs.Raylib.CheckCollisionPointRec(new Vector2(fireBall.Position.X, fireBall.Position.Y), new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
            {
                Fireballs.Remove(fireBall);
                player.IsEnemyHit = true;
                Console.WriteLine("Enemy hit by fireball"); // DEBUG
                break;
            }
        }
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var fireBall in Fireballs)
        {
            fireBall.Draw(Type);
        }
    }
}