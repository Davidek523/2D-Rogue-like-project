using System.Numerics;

class Fireball : Weapon
{
    public List<Projectiles> Fireballs;
    public ProjectileType Type;
    public Fireball(int width, int height, float x, float y, EntityType entityType) : base(width, height, x, y, entityType)
    {
        Fireballs = new List<Projectiles>();
        Type = ProjectileType.Fireball;
    }

    public override void Update(Player player, Enemy enemy, float deltaTime)
    {
        base.Update(player, enemy, deltaTime);

        for (int i = Fireballs.Count - 1; i >= 0; i--)
        {
            var fireBall = Fireballs[i];
            fireBall.Update(this);
            if (!fireBall.IsActive)
            {
                Fireballs.RemoveAt(i);
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
        // Adding fire balls for player
        if (EntityType == EntityType.Player && Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space))
        {
            Vector2 playerPos = new Vector2(player.X, player.Y);
            Vector2 mousePos = Raylib_cs.Raylib.GetMousePosition();
            Vector2 direction = mousePos - playerPos;

            Fireballs.Add(new Projectiles(playerPos.X, playerPos.Y, direction, 10f, ProjectileType.Fireball));
        }

        // Adding fire balls for enemy
        if (EntityType == EntityType.Enemy)
        {
            Vector2 enemyPos = new Vector2(enemy.X, enemy.Y);
            Vector2 playerPos = new Vector2(player.X, player.Y);
            Vector2 direction = playerPos - enemyPos;

            Fireballs.Add(new Projectiles(enemyPos.X, enemyPos.Y, direction, 6f, ProjectileType.Fireball));
        }

        // Checking collisions for both player and enemy
        foreach (var fireBall in Fireballs)
        {
            if (EntityType == EntityType.Player && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(fireBall.Position.X, fireBall.Position.Y, fireBall.Width, fireBall.Height),
                                                                                      new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
            {
                player.IsEnemyHit = true;
                fireBall.IsActive = true;
            }

            if (EntityType == EntityType.Enemy && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(fireBall.Position.X, fireBall.Position.Y, fireBall.Width, fireBall.Height),
                                                                                     new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
            {
                enemy.IsPlayerHit = true;
                fireBall.IsActive = true;
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

        foreach (var fireBall in Fireballs)
        {
            fireBall.Draw(Type);
        }
    }
}