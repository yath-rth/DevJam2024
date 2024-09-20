using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    int coins, count, prevselected, prevcount = -1;

    [SerializeField] TMP_Text coinCounter;
    [SerializeField] GameObject grid;

    [SerializeField] GameObject spawnedItem;
    [SerializeField] Transform parent1;
    [SerializeField] GameObject[] itemPrefabs;
    public Sprite[] itemIcons;

    public List<ShopItems> Items = new List<ShopItems>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins");

        if (coinCounter != null) coinCounter.text = coins.ToString("000");

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].unlocked == 1 && Items[i].assigned == 1)
            {
                SpawnItem((int)Items[i].obj);
                Items[i].assigned = 0;
            }
        }

        if (grid != null)
        {
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                grid.transform.GetChild(i).GetComponent<shopButton>().item = Items[i];
                grid.transform.GetChild(i).GetComponent<shopButton>().icon.sprite = itemIcons[i];
            }
        }
    }

    public void BuyItem(int index)
    {
        ShopItems item = Items[index];
        if (coinCounter != null) coinCounter.text = coins.ToString("000");

        if (item != null)
        {
            if (item.unlocked == 0)
            {
                if (item.cost <= coins)
                {
                    SpawnItem((int)item.obj);
                    coins -= item.cost;

                    item.unlocked = 1;
                    item.assigned = 1;

                    print("bought");
                }
                else
                {
                    cantBuy();
                }

                if (coinCounter != null) coinCounter.text = coins.ToString("000");
            }
            else
            {
                print("Already bought");
                SpawnItem((int)item.obj);
                Items[(int)item.obj].assigned = 1;
            }
        }
        else
        {
            print("No Item Assigned");
        }
    }

    void SetIndex(int index)
    {
        if (count > prevcount)
        {
            Items[prevselected].assigned = 0;
            prevselected = index;
            prevcount = count;
            count++;
        }
    }

    void SpawnItem(int index)
    {
        SetIndex(index);

        if (spawnedItem != null) Destroy(spawnedItem);
        spawnedItem = Instantiate(itemPrefabs[index], transform.position, Quaternion.identity, parent1);
    }

    void cantBuy()
    {
        Debug.Log("Cant buy");
    }

    public void AddCoins()
    {
        coins += 100;
        if (coinCounter != null) coinCounter.text = coins.ToString("000");
    }
}

[System.Serializable]
public class ShopItems
{
    public ItemObject obj;
    public int cost;
    public int unlocked;
    public int assigned;
}

[System.Serializable]
public enum ItemObject
{
    Bow,
    Crown,
    Specs
}