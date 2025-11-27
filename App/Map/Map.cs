using System.Numerics;

class Map
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int TileSize { get; set; }
    public List<Tiles> TileList { get; private set; }

    private Raylib_cs.Texture2D _mapTexture;

    public Map(int width, int height, int tileSize)
    {
        Width = width;
        Height = height;

        TileSize = tileSize;
        TileList = new List<Tiles>();
    }

    public void Update()
    {
        return;
    }

    public void LoadMap(string imagePath, int offsetX = 0, int offsetY = 0)
    {
        _mapTexture = Raylib_cs.Raylib.LoadTexture(imagePath);
        TileList = ImageExtractor.SliceMap(_mapTexture, TileSize, offsetX, offsetY);
    }

    public void Draw()
    {
        if (TileList == null || TileList.Count == 0)
        {
            return;
        }

        foreach (var tile in TileList)
        {
            Raylib_cs.Raylib.DrawTexturePro(_mapTexture, tile.SourceRect, tile.DestinationRect, Vector2.Zero, 0f, Raylib_cs.Color.White);
        }

        // int row = Grid.GetGrid().GetLength(0);
        // int col = Grid.GetGrid().GetLength(1);

        // for (int i = 0; i < row; i++)
        // {
        //     for (int j = 0; j < col; j++)
        //     {
        //         if (Grid.GetGrid()[i, j] == 0)
        //         {
        //             Raylib_cs.Raylib.DrawRectangle(j * Width, i * Height, Width, Height, Raylib_cs.Color.LightGray);
        //         }
        //         else if (Grid.GetGrid()[i, j] == 1)
        //         {
        //             Raylib_cs.Raylib.DrawRectangle(j * Width, i * Height, Width, Height, Raylib_cs.Color.DarkGray);
        //         }
        //         else if (Grid.GetGrid()[i, j] == 2)
        //         {
        //             Raylib_cs.Raylib.DrawRectangle(j * Width, i * Height, Width, Height, Raylib_cs.Color.Brown);
        //         }
        //     }
        // }
    }
}