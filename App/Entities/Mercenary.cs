using System.Numerics;

class Mercenary : Enemy
{
    public Sword SwordWeapon;
    public override int Attack { get; set; } = 5;
    private Raylib_cs.Texture2D _enemyTexture;
    public List<Raylib_cs.Rectangle> Frames;
    public Raylib_cs.Rectangle SourceRect;
    public Mercenary(int width, int height, float x, float y) : base(width, height, x, y)
    {
        SwordWeapon = new Sword(20, 20, x, y, EntityType.Enemy);
        _enemyTexture = Raylib_cs.Raylib.LoadTexture("assets/characters.png");
        Frames = ImageExtractor.SliceCharacters(_enemyTexture, 32);
        int tileIndex = 3;
        SourceRect = Frames[tileIndex];
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        float oldX = X;
        float oldY = Y;

        // Enemy follows the player
        if (player.X < X)
        {
            X -= Speed;
        }

        if (player.X > X)
        {
            X += Speed;
        }

        if (player.Y < Y)
        {
            Y -= Speed;
        }

        if (player.Y > Y)
        {
            Y += Speed;
        }

        MapCollision(oldX, oldY);
        SwordWeapon.Update(player, this, deltaTime);
        SwordWeapon.Attack(this, player);
    }

    public override void Draw()
    {
        Raylib_cs.Raylib.DrawTextureRec(_enemyTexture, SourceRect, new Vector2(X, Y), Raylib_cs.Color.White);
    }
}