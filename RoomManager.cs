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
                Enemies.Add(new Mercenary(50, 50, 300, 300));
                Enemies.Add(new Mercenary(50, 50, 800, 300));
                Enemies.Add(new Mercenary(50, 50, 400, 500));
                break;
            case RoomType.RoomTwo:
                Enemies.Add(new Skeletons(50, 50, 400, 200));
                Enemies.Add(new Skeletons(50, 50, 600, 400));
                Enemies.Add(new Skeletons(50, 50, 500, 500));
                break;
            case RoomType.RoomThree:
                Enemies.Add(new Imp(50, 50, 350, 250));
                Enemies.Add(new Imp(50, 50, 550, 350));
                Enemies.Add(new Imp(50, 50, 450, 450));
                break;
        }
    }
}