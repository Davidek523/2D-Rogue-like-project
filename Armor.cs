enum ArmorType
{
    Leather,
    Iron,
    Diamond
}

class Armor
{
    public int ArmorStrength { get; set; }
    public bool IsArmorEquipped { get; set; }
    public ArmorType Type { get; set; }

    public Armor(ArmorType type)
    {
        Type = type;
        switch (Type)
        {
            case ArmorType.Leather:
                ArmorStrength = 50;
                break;
            case ArmorType.Iron:
                ArmorStrength = 100;
                break;
            case ArmorType.Diamond:
                ArmorStrength = 200;
                break;
        }
        IsArmorEquipped = false;
    }

    public void Update(Player player)
    {
        EquipArmor(player);
        BrokenArmor(player);
    }

    public void EquipArmor(Player player)
    {
        player.Armor = ArmorStrength;
        IsArmorEquipped = true;
    }

    public void BrokenArmor(Player player)
    {
        if (player.Armor <= 0)
        {
            IsArmorEquipped = false;
        }
    }
}