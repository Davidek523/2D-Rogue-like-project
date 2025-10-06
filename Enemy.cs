using System.Numerics;

abstract class Enemy
{
    public int Width { get; set; }
    public int Height { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int Speed { get; set; } = 1;
    public int HP { get; set; } = 50;
    public virtual int Attack { get; set; } = 5;
    public bool IsPlayerHit { get; set; } = false;
    public float attackCooldown = 1.0f;

    public Enemy(int width, int height, float x, float y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public virtual void Update(Player player, float deltaTime)
    {
        // If enemy HP is less then 0, it doesn't let HP go below 0
        if (HP < 0)
        {
            HP = 0;
        }
        // TryAttack(player);
        EnemyDeath();
    }

    // public void TryAttack(Player player)
    // {
    //     AttackPlayer(player);
    // }

    // public abstract void AttackPlayer(Player player);

    public virtual void EnemyDeath()
    {
        // Temporary logic for enemy death
        if (HP <= 0)
        {
            X = -100;
            Y = -100;
        }
    }

    public virtual void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Maroon);
    }
}