class Map
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Update()
    {
        return;
    }

    public void Draw()
    {
        int row = Grid.GetGrid().GetLength(0);
        int col = Grid.GetGrid().GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Grid.GetGrid()[i, j] == 0)
                {
                    Raylib_cs.Raylib.DrawRectangle(j * Width, i * Height, Width, Height, Raylib_cs.Color.LightGray);
                }
                else if (Grid.GetGrid()[i, j] == 1)
                {
                    Raylib_cs.Raylib.DrawRectangle(j * Width, i * Height, Width, Height, Raylib_cs.Color.DarkGray);
                }
                else if (Grid.GetGrid()[i, j] == 2)
                {
                    Raylib_cs.Raylib.DrawRectangle(j * Width, i * Height, Width, Height, Raylib_cs.Color.Brown);
                }
            }
        }
    }
}