using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeTshirt : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer shirtRender;
    [SerializeField] List<Material> mats = new List<Material>();

    private void Awake()
    {
        shirtRender.material = mats[PlayerPrefs.GetInt("colorIndex", 3)];
    }

    public void change(int index)
    {
        shirtRender.material = mats[index];
    }
}
