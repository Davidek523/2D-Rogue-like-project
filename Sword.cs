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
        if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height), new Raylib_cs.Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height)))
        {
            player.IsEnemyHit = true;
            Console.WriteLine("Enemy hit by sword"); // DEBUG
        }
    }

    public override void Draw()
    {
        base.Draw();
    }
}