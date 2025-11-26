using System.Numerics;

class Skeletons : Enemy
{
    public Bow BowWeapon;
    private Random _rand = new Random();
    public override int Attack { get; set; } = 10;
    private Raylib_cs.Texture2D _enemyTexture;
    public List<Raylib_cs.Rectangle> Frames;
    public Raylib_cs.Rectangle SourceRect;
    public Skeletons(int width, int height, float x, float y) : base(width, height, x, y)
    {
        BowWeapon = new Bow(15, 15, x, y, EntityType.Enemy);
        this.X = _rand.Next(0, Raylib_cs.Raylib.GetScreenWidth() - 90 - Width);
        this.Y = _rand.Next(0, Raylib_cs.Raylib.GetScreenHeight() - 90 - Height);
        _enemyTexture = Raylib_cs.Raylib.LoadTexture("assets/characters.png");
        Frames = ImageExtractor.SliceCharacters(_enemyTexture, 32);
        int tileIndex = 2;
        SourceRect = Frames[tileIndex];
    }

    public override void Update(Player player, float deltaTime)
    {
        base.Update(player, deltaTime);

        float oldX = this.X;
        float oldY = this.Y;

        MapCollision(oldX, oldY);
        BowWeapon.Update(player, this, deltaTime);
        BowWeapon.Attack(this, player);
    }

    public override void Draw()
    {
        Raylib_cs.Raylib.DrawTextureRec(_enemyTexture, SourceRect, new Vector2(X, Y), Raylib_cs.Color.White);
        BowWeapon.Draw();
    }
}