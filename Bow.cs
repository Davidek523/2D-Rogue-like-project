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

        if (base.AttackCooldown > 0)
        {
            base.AttackCooldown -= deltaTime;
        }

        for (int i = ProjectileList.Count - 1; i >= 0; i--)
        {
            var projectile = ProjectileList[i];
            projectile.Update(this);

            if (projectile.IsActive)
            {
                ProjectileList.RemoveAt(i);
            }
        }
    }

    public override void Attack(Enemy enemy, Player player)
    {
        if (base.AttackCooldown <= 0)
        {
            TryAttack(enemy, player);
            base.AttackCooldown = base.MaxCooldown;
        }
    }

    public void TryAttack(Enemy enemy, Player player)
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
                projectile.IsActive = true;
            }

            if (EntityType == EntityType.Enemy && Raylib_cs.Raylib.CheckCollisionPointRec(projectile.Position, new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
            {
                enemy.IsPlayerHit = true;
                projectile.IsActive = true;
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