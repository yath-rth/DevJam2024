using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(1)]
public class shopButton : MonoBehaviour
{
    ShopManager shopMan;
    public ShopItems item;

    public Image icon;
    public GameObject lockImage;
    public TMP_Text costText;

    private void Start()
    {
        shopMan = ShopManager.instance;

        if (lockImage != null && item != null)
        {
            if (item.unlocked == 0) lockImage.SetActive(true);
            else if (item.unlocked == 1) lockImage.SetActive(false);
        }

        if(item != null) costText.text = item.cost.ToString();
    }

    public void buy()
    {
        shopMan.BuyItem(((int)item.obj));
        item = shopMan.Items[(int)item.obj];
        
        if (lockImage != null && item != null)
        {
            if (item.unlocked == 0) lockImage.SetActive(true);
            else if (item.unlocked == 1) lockImage.SetActive(false);
        }
    }
}