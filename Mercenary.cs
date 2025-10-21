class Mercenary : Enemy
{
    public Sword SwordWeapon;
    public override int Attack { get; set; } = 5;
    public Mercenary(int width, int height, float x, float y) : base(width, height, x, y)
    {
        SwordWeapon = new Sword(20, 20, x, y, EntityType.Enemy);
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        // Enemy follows the player
        if (player.X < X)
        {
            X -= Speed;
        }

        if (player.X > X)
        {
            X += Speed;
        }

        if (player.Y < Y)
        {
            Y -= Speed;
        }

        if (player.Y > Y)
        {
            Y += Speed;
        }

        SwordWeapon.Update(player, this, deltaTime);
        SwordWeapon.Attack(this, player);
    }

    public override void Draw()
    {
        base.Draw();
    }
}