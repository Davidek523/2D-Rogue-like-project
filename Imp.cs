using System.Numerics;

class Imp : Enemy
{
    public Fireball FireballWeapon;
    public override int Attack { get; set; } = 15;
    public Imp(int width, int height, float x, float y) : base(width, height, x, y)
    {
        FireballWeapon = new Fireball(20, 20, x, y, EntityType.Enemy);
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        FireballWeapon.Update(player, this, deltaTime);
        FireballWeapon.Attack(this, player);
        MapCollision(X, Y);
    }

    public override void Draw()
    {
        base.Draw();
        FireballWeapon.Draw();
    }
}