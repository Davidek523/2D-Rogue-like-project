public enum ItemType
{
    HealthBoost,
    SpeedBoost,
    StrengthBoost
}

class Item
{
    public ItemType Type { get; set; }
    public int Effect { get; set; }

    public Item(ItemType type)
    {
        Type = type;
        switch (Type)
        {
            case ItemType.HealthBoost:
                Effect = 150;
                break;
            case ItemType.SpeedBoost:
                Effect = 6;
                break;
            case ItemType.StrengthBoost:
                Effect = 20;
                break;
        }
    }

    public void Update(Player player)
    {
        ApplyEffect(player);
    }

    public void ApplyEffect(Player player)
    {
        switch (Type)
        {
            case ItemType.HealthBoost:
                player.HP = Effect;
                break;
            case ItemType.SpeedBoost:
                player.Speed = Effect;
                break;
            case ItemType.StrengthBoost:
                player.Attack = Effect;
                break;
        }
    }
}