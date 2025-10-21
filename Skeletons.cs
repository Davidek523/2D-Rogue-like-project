using System.Numerics;

class Skeletons : Enemy
{
    public Bow BowWeapon;
    public override int Attack { get; set; } = 10;
    public Skeletons(int width, int height, float x, float y) : base(width, height, x, y)
    {
        BowWeapon = new Bow(15, 15, x, y, EntityType.Enemy);
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        float oldX = X;
        float oldY = Y;

        if (player.X < X)
        {
            X += Speed;
        }
        if (player.X > X)
        {
            X -= Speed;
        }
        if (player.Y < Y)
        {
            Y += Speed;
        }
        if (player.Y > Y)
        {
            Y -= Speed;
        }

        MapCollision(oldX, oldY);
        BowWeapon.Update(player, this, deltaTime);
        BowWeapon.Attack(this, player);
    }

    public override void Draw()
    {
        base.Draw();
        BowWeapon.Draw();
    }
}