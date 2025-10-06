// class Mercenary : Enemy
// {
//     public Mercenary(int width, int height, float x, float y) : base(width, height, x, y) { }

//     public override void Update(Player player, float deltaTime)
//     {
//         base.Update(player, deltaTime);

//         // Enemy follows the player
//         if (player.X < X)
//         {
//             X -= Speed;
//         }

//         if (player.X > X)
//         {
//             X += Speed;
//         }

//         if (player.Y < Y)
//         {
//             Y -= Speed;
//         }

//         if (player.Y > Y)
//         {
//             Y += Speed;
//         }

//         if (attackCooldown > 0)
//         {
//             attackCooldown -= deltaTime;
//         }
//     }

//     public override void AttackPlayer(Player player)
//     {
//         if (Raylib_cs.Raylib.CheckCollisionRecs(new Raylib_cs.Rectangle(X, Y, Width, Height),
//                                                 new Raylib_cs.Rectangle(player.X, player.Y, 25, 25)))
//         {
//             if (attackCooldown <= 0)
//             {
//                 IsPlayerHit = true;
//                 attackCooldown = 1.0f; // Reset cooldown
//             }
//         }
//     }

//     public override void Draw()
//     {
//         base.Draw();
//     }
// }