using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class CloseGame : MonoBehaviour
{
    public void close()
    {
        PlayerPrefs.SetInt("Steps", AndroidStepCounter.current.stepCounter.ReadValue() + stepCounter.instance.steps);
        Application.Quit();
    }
}
