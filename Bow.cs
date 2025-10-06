using System.Numerics;

class Bow : Weapon
{
    public List<Projectiles> ProjectileList;
    public ProjectileType TypeProjectile;
    public Bow(int width, int height, float x, float y, EntityType entityType) : base(width, height, x, y, entityType)
    {
        ProjectileList = new List<Projectiles>();
        TypeProjectile = ProjectileType.Arrow;
    }

    public override void Update(Player player, Enemy enemy, float deltaTime)
    {
        base.Update(player, enemy, deltaTime);

        foreach (var projectile in ProjectileList)
        {
            projectile.Update(this);
        }
        ProjectileList.RemoveAll(x => !x.IsActive);
    }

    public override void Attack(Enemy enemy, Player player)
    {
        // Adding projectiles for player
        if (EntityType == EntityType.Player && Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space))
        {
            ProjectileList.Add(new Projectiles(X, Y, 10f));
        }

        // Adding projectiles for enemy
        if (EntityType == EntityType.Enemy)
        {
            ProjectileList.Add(new Projectiles(X, Y, 6f));
        }

        // Checking collisions for both player and enemy
        foreach (var projectile in ProjectileList)
        {
            if (EntityType == EntityType.Player && Raylib_cs.Raylib.CheckCollisionPointRec(projectile.Position, new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
            {
                player.IsEnemyHit = true;
                projectile.IsActive = false;
            }

            if (EntityType == EntityType.Enemy && Raylib_cs.Raylib.CheckCollisionPointRec(projectile.Position, new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
            {
                enemy.IsPlayerHit = true;
                projectile.IsActive = false;
            }
        }
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var projectile in ProjectileList)
        {
            projectile.Draw(TypeProjectile);
        }
    }
}