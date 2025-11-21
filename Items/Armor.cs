enum ArmorType
{
    Leather,
    Iron,
    Diamond
}

class Armor
{
    public int ArmorStrength { get; set; }
    public ArmorType Type { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsPickedUp { get; set; }
    public Armor(ArmorType type, float x, float y)
    {
        X = x;
        Y = y;
        Width = 25;
        Height = 25;
        IsPickedUp = false;

        Type = type;
        switch (Type)
        {
            case ArmorType.Leather:
                ArmorStrength = 50;
                break;
            case ArmorType.Iron:
                ArmorStrength = 100;
                break;
            case ArmorType.Diamond:
                ArmorStrength = 200;
                break;
        }
    }

    public void Update(Player player)
    {
        if (!IsPickedUp && Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height),
                                                               new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height)))
        {
            EquipArmor(player);
            IsPickedUp = true;
        }
    }

    public void EquipArmor(Player player)
    {
        player.Armor = ArmorStrength;
    }

    public void Draw()
    {
        if (!IsPickedUp)
        {
            Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, Width, Height, Raylib_cs.Color.Brown);
        }
    }
}