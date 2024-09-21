using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorButton : MonoBehaviour
{
    changeTshirt changeShirt;
    [SerializeField] int color_index, cost;

    private void Awake()
    {
        changeShirt = Player.instance.GetComponent<changeTshirt>();
    }

    public void change()
    {
        if (stepCounter.instance.steps >= cost)
        {
            changeShirt.change(color_index);
            stepCounter.instance.temp_steps += cost;
        }
    }
}
