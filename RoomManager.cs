using System.Globalization;
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
    public List<Enemy> Enemies;
    public List<RewardWeapon> RewardWeaponPlayer;
    public List<Doors> Door;
    public List<Armor> RewardArmor;
    public List<Item> RewardItems;
    public bool IsPlayerDead = false;
    public bool IsEnemyDead = false;
    private Player _player;
    private UI _userInterace;
    private bool _clearRoom = false;
    private Map _map;
    private Obstacles _obstacles;

    public RoomManager(Map map)
    {
        CurrentRoom = RoomType.RoomZero;
        _map = map;
        _userInterace = new UI();

        Enemies = new List<Enemy>();
        RewardWeaponPlayer = new List<RewardWeapon>();
        RewardArmor = new List<Armor>();
        RewardItems = new List<Item>();
        Door = new List<Doors>();
        _player = new Player(25, 25, 600, 300);
        _obstacles = new Obstacles();

        LoadRoom(CurrentRoom);
    }

    public void LoadRoom(RoomType type)
    {
        Grid.ResetGrid();
        CurrentRoom = type;
        Enemies.Clear();
        Door.Clear();
        RewardWeaponPlayer.Clear();
        RewardArmor.Clear();
        RewardItems.Clear();
        _clearRoom = false;
        IsEnemyDead = false;

        switch (type)
        {
            case RoomType.RoomZero:
                Door.Add(new Doors("Up"));
                break;
            case RoomType.RoomOne:
                for (int i = 0; i < 3; i++)
                {
                    Enemy mercenary = new Mercenary(25, 25, 300 + i * 100, 200 + i * 100);
                    mercenary.EquipedWeapon = new Sword(25, 25, mercenary.X + 15, mercenary.Y + 5, EntityType.Enemy);
                    Enemies.Add(mercenary);
                    Door.Add(new Doors("Right"));
                }
                break;
            case RoomType.RoomTwo:
                for (int i = 0; i < 3; i++)
                {
                    Enemy skeleton = new Skeletons(25, 25, 300 + i * 100, 200 + i * 100);
                    skeleton.EquipedWeapon = new Bow(25, 25, skeleton.X + 15, skeleton.Y + 5, EntityType.Enemy);
                    Enemies.Add(skeleton);
                    Door.Add(new Doors("Left"));
                }
                break;
            case RoomType.RoomThree:
                for (int i = 0; i < 3; i++)
                {
                    Enemy imp = new Imp(25, 25, 300 + i * 100, 200 + i * 100);
                    imp.EquipedWeapon = new Fireball(25, 25, imp.X + 15, imp.Y + 5, EntityType.Enemy);
                    Enemies.Add(imp);
                }
                break;
        }
    }

    public void Update(float deltaTime)
    {
        Enemy updateEnemy = new Mercenary(25, 25, -100, -100);

        // Update every enemy in the room
        foreach (Enemy enemy in Enemies)
        {
            enemy.Update(_player, deltaTime);
            if (enemy.EquipedWeapon != null)
            {
                enemy.EquipedWeapon.Update(_player, enemy, deltaTime);
            }
        }

        // Check if all enemies are defeated
        if (!_clearRoom && Enemies.TrueForAll(x => x.HP <= 0))
        {
            _clearRoom = true;
            IsEnemyDead = true;
            UnlockDoors();
            SpawnReward();
        }

        // Update the doors
        foreach (Doors door in Door)
        {
            door.Update(_player);

            if (door.IsDoorEntered)
            {
                GoToNextRoom();
                _player.X = 600;
                _player.Y = 300;
                door.IsDoorEntered = false;
                break;
            }
        }

        // Update the player
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].HP > 0)
            {
                updateEnemy = Enemies[i];
                break;
            }
        }
        _player.Update(updateEnemy, deltaTime);
        _player.PlayerDeath();

        if (_player.HP <= 0)
        {
            IsPlayerDead = true;
        }

        // Update rewards (REMEMBER TO REMOVE THE OTHER REWARD ONCE ONE HAS BEEN PICKED UP)
        foreach (Armor armor in RewardArmor)
        {
            armor.Update(_player);
        }

        foreach (Item item in RewardItems)
        {
            item.Update(_player);
        }

        foreach (RewardWeapon weapon in RewardWeaponPlayer)
        {
            weapon.Update(_player);
        }

        RewardArmor.RemoveAll(x => x.IsPickedUp);
        RewardItems.RemoveAll(x => x.IsPickedUp);
        RewardWeaponPlayer.RemoveAll(x => x.IsPickedUp);
    }

    public void SpawnReward()
    {
        Random rand = new Random();

        // Random armor type
        Array armorValues = Enum.GetValues(typeof(ArmorType));
        ArmorType randomArmorType = (ArmorType)armorValues.GetValue(rand.Next(armorValues.Length));

        // Random item type
        Array itemValues = Enum.GetValues(typeof(ItemType));
        ItemType randomItemType = (ItemType)itemValues.GetValue(rand.Next(itemValues.Length));

        // Random weapon type
        Array weaponValues = Enum.GetValues(typeof(WeaponType));
        WeaponType randomWeaponType = (WeaponType)weaponValues.GetValue(rand.Next(weaponValues.Length));

        switch (CurrentRoom)
        {
            case RoomType.RoomOne:
                RewardArmor.Add(new Armor(randomArmorType, 680, 360));
                RewardItems.Add(new Item(randomItemType, 730, 360));
                RewardWeaponPlayer.Add(new RewardWeapon(randomWeaponType, 630, 360));
                break;
            case RoomType.RoomTwo:
                RewardArmor.Add(new Armor(randomArmorType, 680, 360));
                RewardItems.Add(new Item(randomItemType, 730, 360));
                RewardWeaponPlayer.Add(new RewardWeapon(randomWeaponType, 630, 360));
                break;
            case RoomType.RoomThree:
                RewardArmor.Add(new Armor(randomArmorType, 680, 360));
                RewardItems.Add(new Item(randomItemType, 730, 360));
                RewardWeaponPlayer.Add(new RewardWeapon(randomWeaponType, 630, 360));
                break;
        }
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
        _map.Draw();
        _player.Draw();

        foreach (Enemy enemy in Enemies)
        {
            enemy.Draw();
            if (enemy.EquipedWeapon != null)
            {
                enemy.EquipedWeapon.Draw();
            }
        }

        foreach (Armor armor in RewardArmor)
        {
            armor.Draw();
        }
        foreach (Item item in RewardItems)
        {
            item.Draw();
        }
        foreach (RewardWeapon weapon in RewardWeaponPlayer)
        {
            weapon.Draw();
        }

        foreach (Doors door in Door)
        {
            door.Draw();
        }

        _obstacles.Draw();

        _userInterace.DrawHealth(_player.HP);
        _userInterace.DrawArmor(_player.Armor);
    }
}