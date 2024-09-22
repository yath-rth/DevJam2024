using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenandCloseButton : MonoBehaviour
{
    public GameObject itemOpen;
    public GameObject itemClose;

    public void Open()
    {
        if(itemOpen != null)itemOpen.SetActive(true);
        if(itemClose != null)itemClose.SetActive(false);
        Time.timeScale = 0;
    }

    public void Close()
    {
        if(itemOpen != null) itemOpen.SetActive(true);
        if(itemClose != null)itemClose.SetActive(false);
        Time.timeScale = 1;
    }

    public void middle()
    {
        if(itemOpen != null)itemOpen.SetActive(true);
        if(itemClose != null)itemClose.SetActive(false);
        Time.timeScale = 0;
    }
}
