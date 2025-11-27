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

    public void DrawInstructions()
    {
        Raylib_cs.Raylib.DrawText("Use WASD to move", 10, 70, 20, Raylib_cs.Color.LightGray);
        Raylib_cs.Raylib.DrawText("Press SPACE to attack", 10, 100, 20, Raylib_cs.Color.LightGray);
        Raylib_cs.Raylib.DrawText("Use ARROWS for weapon direction", 10, 130, 20, Raylib_cs.Color.LightGray);
        Raylib_cs.Raylib.DrawText("Press P to pause", 10, 160, 20, Raylib_cs.Color.LightGray);
        Raylib_cs.Raylib.DrawText("Press ESC to quit", 10, 190, 20, Raylib_cs.Color.LightGray);
    }
}