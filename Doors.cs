class Doors
{
    public bool IsDoorEntered = false;
    public bool IsDoorLocked = true;
    public string Direction;
    public Doors(string direction)
    {
        Direction = direction;
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
                    Raylib_cs.Raylib.DrawRectangle(j * 80, i * 80, 80, 80, Raylib_cs.Color.Brown);
                }
            }
        }
    }
}