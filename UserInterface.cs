class UI
{
    public void DrawHealth(int hp)
    {
        Raylib_cs.Raylib.DrawText($"Health: {hp}", 10, 10, 20, Raylib_cs.Color.LightGray);
    }

    public void DrawArmor(int armor)
    {
        Raylib_cs.Raylib.DrawText($"Armor: {armor}", 10, 40, 20, Raylib_cs.Color.LightGray);
    }
}