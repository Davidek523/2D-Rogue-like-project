using System.Numerics;

public enum RoomType
{
    RoomZero,
    RoomOne,
    RoomTwo,
    RoomThree
}
class RoomManager
{
    public RoomType CurrentRoom;
    public Weapon EquipedWeapon;
    public List<Enemy> Enemies;
    public List<Weapon> RewardWeapon;
    public List<Rectangle> Obstacles;
    public List<Doors> Door;
    private bool _clearRoom = false;
    private Map _map;

    public RoomManager(Map map)
    {
        CurrentRoom = RoomType.RoomZero;
        _map = map;

        Enemies = new List<Enemy>();
        RewardWeapon = new List<Weapon>();
        Obstacles = new List<Rectangle>();
        Door = new List<Doors>();

        LoadRoom(CurrentRoom);
    }

    public void LoadRoom(RoomType type)
    {
        CurrentRoom = type;
        Enemies.Clear();

        switch (type)
        {
            case RoomType.RoomZero:
                Door.Add(new Doors("Up"));
                break;
            case RoomType.RoomOne:
                for (int i = 0; i < 3; i++)
                {
                    Enemy mercenary = new Mercenary(50, 50, 300 + i * 100, 200 + i * 100);
                    EquipedWeapon = new Sword(25, 25, mercenary.X + 15, mercenary.Y + 5, EntityType.Player);
                    Enemies.Add(mercenary);
                    Door.Add(new Doors("Right"));
                }
                break;
            case RoomType.RoomTwo:
                for (int i = 0; i < 3; i++)
                {
                    Enemy skeleton = new Skeletons(50, 50, 300 + i * 100, 200 + i * 100);
                    EquipedWeapon = new Bow(25, 25, skeleton.X + 15, skeleton.Y + 5, EntityType.Player);
                    Enemies.Add(skeleton);
                    Door.Add(new Doors("Left"));
                }
                break;
            case RoomType.RoomThree:
                for (int i = 0; i < 3; i++)
                {
                    Enemy imp = new Imp(50, 50, 300 + i * 100, 200 + i * 100);
                    EquipedWeapon = new Fireball(25, 25, imp.X + 15, imp.Y + 5, EntityType.Player);
                    Enemies.Add(imp);
                }
                break;
        }
    }

    public void ClearRoom()
    {
        Enemies.Clear();
        RewardWeapon.Clear();
        Obstacles.Clear();
        _clearRoom = false;
    }

    public void Update(Player player, Enemy enemy, float deltaTime)
    {
        // Update every enemy in the room
        foreach (Enemy enemy in Enemies)
        {
            enemy.Update(player, deltaTime);
        }

        // Check if all enemies are defeated
        if (!_clearRoom && Enemies.TrueForAll(x => x.HP <= 0))
        {
            _clearRoom = true;
            UnlockDoors();
            SpawnReward();
        }

        // Update enemy wepaons
        EquipedWeapon.Update(player, enemy, deltaTime);

        // Update the doors
        foreach (Doors door in Door)
        {
            door.Update(player);

            if (door.IsDoorEntered)
            {
                GoToNextRoom();
                door.IsDoorEntered = false;
            }
        }
    }

    public void SpawnReward()
    {
        
    }

    public void UnlockDoors()
    {
        foreach (Doors door in Door)
        {
            door.IsDoorLocked = false;
        }
    }

    public void GoToNextRoom()
    {
        switch (CurrentRoom)
        {
            case RoomType.RoomZero:
                CurrentRoom = RoomType.RoomOne;
                break;
            case RoomType.RoomOne:
                CurrentRoom = RoomType.RoomTwo;
                break;
            case RoomType.RoomTwo:
                CurrentRoom = RoomType.RoomThree;
                break;
        }

        LoadRoom(CurrentRoom);
    }

    public void Draw()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.Draw();
        }

        foreach (Doors door in Door)
        {
            door.Draw();
        }

        EquipedWeapon.Draw();
    }
}