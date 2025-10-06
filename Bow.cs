using System.Numerics;

class Bow : Weapon
{
    public List<Projectiles> Arrows;
    public ProjectileType Type;
    public EntityType entityType;
    public Bow(int width, int height, float x, float y) : base(width, height, x, y)
    {
        Arrows = new List<Projectiles>();
        Type = ProjectileType.Arrow;
    }

    public override void Update(Player player, Enemy enemy, float deltaTime)
    {
        base.Update(player, enemy, deltaTime);

        foreach (var arrow in Arrows)
        {
            arrow.Update(this);
        }
    }

    public override void Attack(Enemy enemy, Player player)
    {
        AttackPlayer(player, enemy);
        AttackEnemy(player, enemy);
    }

    public void AttackPlayer(Player player, Enemy enemy)
    {
        entityType = EntityType.Player;

        // Problem: Because Attack is called only when Space is pressed, the last bullet is removed after the second time Space is pressed
        if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space))
        {
            Arrows.Add(new Projectiles(X + Width / 2, Y + Height / 2, 10f));
            foreach (var arrow in Arrows)
            {
                if (!arrow.IsActive || Raylib_cs.Raylib.CheckCollisionPointRec(new Vector2(arrow.Position.X, arrow.Position.Y), new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
                {
                    Arrows.Remove(arrow);
                    player.IsEnemyHit = true;
                    Console.WriteLine("Enemy hit by arrow"); // DEBUG
                    break;
                }
            }
        }
    }

    public void AttackEnemy(Player player, Enemy enemy)
    {
        entityType = EntityType.Enemy;

        // Problem: Because Attack is called only when Space is pressed, the last bullet is removed after the second time Space is pressed
        Arrows.Add(new Projectiles(X + Width / 2, Y + Height / 2, 10f));
        foreach (var arrow in Arrows)
        {
            if (!arrow.IsActive || Raylib_cs.Raylib.CheckCollisionPointRec(new Vector2(arrow.Position.X, arrow.Position.Y), new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
            {
                Arrows.Remove(arrow);
                enemy.IsPlayerHit = true;
                Console.WriteLine("Enemy hit by arrow"); // DEBUG
                break;
            }
        }
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var arrow in Arrows)
        {
            arrow.Draw(Type);
        }
    }
}