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


    public override void Update(Player player, Enemy enemy, float deltaTime)
    {
        base.Update(player, enemy, deltaTime);

        if (base.AttackCooldown > 0)
        {
            base.AttackCooldown -= deltaTime;
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
        if (EntityType == EntityType.Player)
        {
            if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(this.X, this.Y, this.Width, this.Height),
                                                    new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
            {
                player.IsEnemyHit = true;
                Console.WriteLine("Enemy Hit!"); // DEBUG
            }
        }

        if (EntityType == EntityType.Enemy)
        {
            if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(this.X, this.Y, this.Width, this.Height),
                                                    new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
            {
                enemy.IsPlayerHit = true;
                Console.WriteLine("Player Hit!"); // DEBUG
            }
        }

        if (player.IsEnemyHit)
        {
            enemy.HP -= player.Attack;
            player.IsEnemyHit = false;
        }
        if (enemy.IsPlayerHit)
        {
            player.HP -= enemy.Attack;
            enemy.IsPlayerHit = false;
        }
    }

    public override void Draw()
    {
        base.Draw();
    }
}