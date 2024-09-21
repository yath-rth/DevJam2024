using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenandCloseButton : MonoBehaviour
{
    public GameObject itemOpen;
    public GameObject itemClose;

    public void Open()
    {
        itemOpen.SetActive(true);
        itemClose.SetActive(false);
        Time.timeScale = 0;
    }

    public void Close()
    {
        itemOpen.SetActive(true);
        itemClose.SetActive(false);
        Time.timeScale = 1;
    }
}
