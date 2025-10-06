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

    // public void AttackPlayer(Player player, Enemy enemy)
    // {
    //     entityType = EntityType.Player;

    //     // Problem: Because Attack is called only when Space is pressed, the last bullet is removed after the second time Space is pressed
    //     Arrows.Add(new Projectiles(X + Width / 2, Y + Height / 2, 10f));
    //     foreach (var arrow in Arrows)
    //     {
    //         if (!arrow.IsActive || Raylib_cs.Raylib.CheckCollisionPointRec(new Vector2(arrow.Position.X, arrow.Position.Y), new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
    //         {
    //             Arrows.Remove(arrow);
    //             enemy.IsPlayerHit = true;
    //             Console.WriteLine("Enemy hit by arrow"); // DEBUG
    //             break;
    //         }
    //     }
    // }

    // public void AttackEnemy(Player player, Enemy enemy)
    // {
    //     entityType = EntityType.Enemy;

    //     // Problem: Because Attack is called only when Space is pressed, the last bullet is removed after the second time Space is pressed
    //     Bones.Add(new Projectiles(X + Width / 2, Y + Height / 2, 10f));
    //     foreach (var bone in Bones)
    //     {
    //         if (!bone.IsActive || Raylib_cs.Raylib.CheckCollisionPointRec(new Vector2(bone.Position.X, bone.Position.Y), new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
    //         {
    //             Bones.Remove(bone);
    //             player.IsEnemyHit = true;
    //             Console.WriteLine("Player hit by a bone!"); // DEBUG
    //             break;
    //         }
    //     }
    // }

    public override void Draw()
    {
        base.Draw();

        foreach (var projectile in ProjectileList)
        {
            projectile.Draw(TypeProjectile);
        }
    }
}