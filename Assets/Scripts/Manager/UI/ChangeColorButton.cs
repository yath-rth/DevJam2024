using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorButton : MonoBehaviour
{
    changeTshirt changeShirt;
    [SerializeField] int color_index, cost;
    public bool upgraded;

    private void Awake()
    {
        changeShirt = Player.instance.GetComponent<changeTshirt>();
    }

    public void change()
    {
        if (stepCounter.instance.steps >= cost && upgraded == false)
        {
            changeShirt.change(color_index);
            stepCounter.instance.setSteps(cost);
            upgraded = true;
        }
    }
}
