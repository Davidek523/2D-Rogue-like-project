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
    public List<Enemy> Enemies = new List<Enemy>();

    public void LoadRoom(RoomType type)
    {
        CurrentRoom = type;
        Enemies.Clear();

        switch (type)
        {
            case RoomType.RoomZero:
                break;
            case RoomType.RoomOne:
                for (int i = 0; i < 3; i++)
                {
                    Enemy mercenary = new Mercenary(50, 50, 300 + i * 100, 200 + i * 100);
                    EquipedWeapon = new Sword(25, 25, mercenary.X + 15, mercenary.Y + 5, EntityType.Player);
                    Enemies.Add(mercenary);
                }
                break;
            case RoomType.RoomTwo:
                for (int i = 0; i < 3; i++)
                {
                    Enemy skeleton = new Skeletons(50, 50, 300 + i * 100, 200 + i * 100);
                    EquipedWeapon = new Bow(25, 25, skeleton.X + 15, skeleton.Y + 5, EntityType.Player);
                    Enemies.Add(skeleton);
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
}