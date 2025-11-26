public static class ImageExtractor
{
    public static List<Tiles> SliceMap(Raylib_cs.Texture2D mapTexture, int tileSize, int worldOffsetX, int worldOffsetY)
    {
        List<Tiles> tiles = new List<Tiles>();

        int tilesX = mapTexture.Width / tileSize;
        int tilesY = mapTexture.Height / tileSize;

        for (int y = 0; y < tilesY; y++)
        {
            for (int x = 0; x < tilesX; x++)
            {
                Tiles t = new Tiles();

                t.SourceRect = new Raylib_cs.Rectangle(
                    x * tileSize,
                    y * tileSize,
                    tileSize,
                    tileSize
                );

                t.DestinationRect = new Raylib_cs.Rectangle(
                    worldOffsetX + x * tileSize,
                    worldOffsetY + y * tileSize,
                    tileSize,
                    tileSize
                );

                tiles.Add(t);
            }
        }

        return tiles;
    }
}