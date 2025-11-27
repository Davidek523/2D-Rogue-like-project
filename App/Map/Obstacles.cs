using System.Numerics;

class Obstacles
{
    public int Width { get; set; }
    public int Height { get; set; }
    private Raylib_cs.Texture2D _obstacleTexture;

    public Obstacles()
    {
        Height = 80;
        Width = 80;
        _obstacleTexture = Raylib_cs.Raylib.LoadTexture("assets/Column.png");
    }

    public void Draw()
    {
        int col = Grid.GetGrid().GetLength(0);
        int row = Grid.GetGrid().GetLength(1);

        int tileSize = 80;

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if (Grid.GetGrid()[i, j] == 3)
                {
                    Raylib_cs.Raylib.DrawTexture(_obstacleTexture, j * tileSize, i * tileSize, Raylib_cs.Color.White);
                }
            }
        }
    }
}