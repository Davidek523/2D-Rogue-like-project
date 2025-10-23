class Doors
{
    public bool IsDoorEntered = false;
    public Doors() { }

    public void Update(Player player, string direction)
    {
        int row = Grid.GetGrid().GetLength(0);
        int col = Grid.GetGrid().GetLength(1);

        DoorCollision(player, row, col);
        FilterDoors(direction, row, col);

    }

    public void DoorCollision(Player player, int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Grid.GetGrid()[i, j] == 2)
                {
                    if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(player.X, player.Y, player.Width, player.Height),
                                                            new Raylib_cs.Rectangle(j * 80, i * 80, 80, 80)))
                    {
                        Console.WriteLine("Entered a door");
                        IsDoorEntered = true;
                        break;
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
                    Raylib_cs.Raylib.DrawRectangle(j * 80, i * 80, 80, 80, Raylib_cs.Color.Brown);
                }
            }
        }
    }
}