// class Dungeon
// {
//     public Room[,] Rooms { get; set; }
//     public int CurrentRoomX { get; set; } = 1;
//     public int CurrentRoomY { get; set; } = 1;

//     public Dungeon(int width, int height)
//     {
//         Rooms = new Room[width, height];

//         for (int i = 0; i < width; i++)
//         {
//             for (int j = 0; j < height; j++)
//             {
//                 Rooms[i, j] = new Room(17, 9);
//             }
//         }
//     }

//     public Room GetCurrentRoom()
//     {
//         return Rooms[CurrentRoomX, CurrentRoomY];
//     }
// }