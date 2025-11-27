using System.Numerics;

class Doors
{
    public bool IsDoorEntered = false;
    public bool IsDoorLocked = true;
    public string Direction;
    private Raylib_cs.Texture2D _doorsTexture;
    public List<Raylib_cs.Rectangle> Frames;
    public Raylib_cs.Rectangle SourceRect;
    public Doors(string direction)
    {
        Direction = direction;
        _doorsTexture = Raylib_cs.Raylib.LoadTexture("assets/doors.png");
        Frames = ImageExtractor.SliceCharacters(_doorsTexture, 80);
        int tileIndex = 0;
        switch (Direction)
        {
            case "Up":
                tileIndex = 0;
                break;
            case "Down":
                tileIndex = 1;
                break;
            case "Left":
                tileIndex = 3;
                break;
            case "Right":
                tileIndex = 2;
                break;
        }
        SourceRect = Frames[tileIndex];
    }

    public void Update(Player player)
    {
        int row = Grid.GetGrid().GetLength(0);
        int col = Grid.GetGrid().GetLength(1);

        DoorCollision(player, row, col);
        FilterDoors(Direction, row, col);
    }

    // Add a check for if the door is locked
    public void DoorCollision(Player player, int row, int col)
    {
        IsDoorEntered = false;
        bool foundDoor = false;

        for (int i = 0; i < row && !foundDoor; i++)
        {
            for (int j = 0; j < col && !foundDoor; j++)
            {
                if (Grid.GetGrid()[i, j] == 2)
                {
                    if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height),
                                                            new Raylib_cs.Rectangle(j * 80, i * 80, 80, 80)))
                    {
                        foundDoor = true;

                        if (IsDoorLocked)
                        {
                            player.X = player.PreviousX;
                            player.Y = player.PreviousY;
                        }
                        else
                        {
                            Console.WriteLine("Entered a door");
                            IsDoorEntered = true;
                        }
                        return;
                    }
                }
            }
        }
    }

    public void FilterDoors(string direction, int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Grid.GetGrid()[i, j] == 2)
                {
                    string doorDirection = "";

                    if (i == 0)
                    {
                        doorDirection = "Up";
                    }
                    else if (i == row - 1)
                    {
                        doorDirection = "Down";
                    }
                    else if (j == 0)
                    {
                        doorDirection = "Left";
                    }
                    else if (j == col - 1)
                    {
                        doorDirection = "Right";
                    }

                    if (doorDirection != direction)
                    {
                        Grid.GetGrid()[i, j] = 1;
                    }

                }
            }
        }
    }

    public void Draw()
    {
        int row = Grid.GetGrid().GetLength(0);
        int col = Grid.GetGrid().GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Grid.GetGrid()[i, j] == 2)
                {
                    Raylib_cs.Raylib.DrawTextureRec(_doorsTexture, SourceRect, new Vector2(j * 80, i * 80), Raylib_cs.Color.White);
                }
            }
        }
    }
}