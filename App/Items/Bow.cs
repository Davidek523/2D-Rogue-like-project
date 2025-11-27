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

        for (int i = ProjectileList.Count - 1; i >= 0; i--)
        {
            var projectile = ProjectileList[i];
            projectile.Update(this);

            if (!projectile.IsActive)
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
            Vector2 playerPos = new Vector2(player.X, player.Y);
            Vector2 mousePos = Raylib_cs.Raylib.GetMousePosition();
            Vector2 direction = mousePos - playerPos;

            ProjectileList.Add(new Projectiles(playerPos.X, playerPos.Y, direction, 10f, ProjectileType.Arrow));
        }

        // Adding projectiles for enemy
        if (EntityType == EntityType.Enemy)
        {
            Vector2 enemyPos = new Vector2(enemy.X, enemy.Y);
            Vector2 playerPos = new Vector2(player.X, player.Y);
            Vector2 direction = playerPos - enemyPos;

            ProjectileList.Add(new Projectiles(enemyPos.X, enemyPos.Y, direction, 6f, ProjectileType.Arrow));
        }

        // Checking collisions for both player and enemy
        foreach (var projectile in ProjectileList)
        {
            if (EntityType == EntityType.Player && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(projectile.Position.X, projectile.Position.Y, projectile.Width, projectile.Height),
                                                                                       new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
            {
                player.IsEnemyHit = true;
                projectile.IsActive = true;
            }

            if (EntityType == EntityType.Enemy && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(projectile.Position.X, projectile.Position.Y, projectile.Width, projectile.Height),
                                                                                      new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
            {
                enemy.IsPlayerHit = true;
                projectile.IsActive = true;
            }
        }

        // Check for dealing damage
        if (enemy.IsPlayerHit)
        {
            player.HP -= enemy.Attack;
            enemy.IsPlayerHit = false;
        }

        if (player.IsEnemyHit)
        {
            enemy.HP -= player.Attack;
            player.IsEnemyHit = false;
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