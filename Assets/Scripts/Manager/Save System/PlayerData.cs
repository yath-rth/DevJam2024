using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public List<ShopItems> unlockedItems;
    public int totalCoins;
    public int Highscore;

    public PlayerData(List<ShopItems> items)
    {
        unlockedItems = items;
    }

    public PlayerData(List<ShopItems> items, int score, int coins)
    {
        unlockedItems = items;
        Highscore = score;
        totalCoins = coins;
    }
}
