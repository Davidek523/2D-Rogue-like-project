public enum ItemType
{
    HealthBoost,
    SpeedBoost,
    StrengthBoost
}

class Item
{
    public ItemType Type { get; set; }
    public int Effect { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsPickedUp { get; set; }

    public Item(ItemType type, float x, float y)
    {
        X = x;
        Y = y;
        Width = 25;
        Height = 25;
        IsPickedUp = false;

        Type = type;
        switch (Type)
        {
            case ItemType.HealthBoost:
                Effect = 150;
                break;
            case ItemType.SpeedBoost:
                Effect = 6;
                break;
            case ItemType.StrengthBoost:
                Effect = 20;
                break;
        }
    }

    public void Update(Player player)
    {
        if (!IsPickedUp && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height),
                                                               new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
        {
            ApplyEffect(player);
            IsPickedUp = true;
        }
    }

    public void ApplyEffect(Player player)
    {
        switch (Type)
        {
            case ItemType.HealthBoost:
                player.HP = Effect;
                break;
            case ItemType.SpeedBoost:
                player.Speed = Effect;
                break;
            case ItemType.StrengthBoost:
                player.Attack = Effect;
                break;
        }
    }

    public void Draw()
    {
        if (!IsPickedUp)
        {
            Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Gold);
        }
    }
}