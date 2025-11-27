public enum WeaponType
{
    Sword,
    Bow,
    Fireball
}

class RewardWeapon
{
    public WeaponType Type { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsPickedUp { get; set; }

    public RewardWeapon(WeaponType type, float x, float y)
    {
        X = x;
        Y = y;
        Width = 25;
        Height = 25;
        IsPickedUp = false;
        Type = type;
    }

    public void Update(Player player)
    {
        if (!IsPickedUp && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height),
                                                               new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
        {
            switch (Type)
            {
                case WeaponType.Sword:
                    player.EquippedWeapon = new Sword(25, 25, player.X + 15, player.Y + 5, EntityType.Player);
                    break;
                case WeaponType.Bow:
                    player.EquippedWeapon = new Bow(25, 25, player.X + 15, player.Y + 5, EntityType.Player);
                    break;
                case WeaponType.Fireball:
                    player.EquippedWeapon = new Fireball(25, 25, player.X + 15, player.Y + 5, EntityType.Player);
                    break;
            }
            IsPickedUp = true;
        }
    }

    public void Draw()
    {
        if (!IsPickedUp)
        {
            Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Red);
        }
    }
}