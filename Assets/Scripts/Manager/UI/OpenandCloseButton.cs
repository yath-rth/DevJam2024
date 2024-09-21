using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenandCloseButton : MonoBehaviour
{
    public GameObject item;

    public void Open()
    {
        item.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        item.SetActive(false);
        Time.timeScale = 1;
    }
}
