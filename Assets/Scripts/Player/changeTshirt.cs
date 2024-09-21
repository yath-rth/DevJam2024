using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeTshirt : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer shirtRender;
    [SerializeField]List<Material> mats = new List<Material>();

    public void change(int index){
        shirtRender.material = mats[index];
    }
}
