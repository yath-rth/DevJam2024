using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class stepCounter : MonoBehaviour
{
    public static stepCounter instance;

    [SerializeField] TMP_Text text, text1, text2;
    public int steps;
    public int temp_steps;

    private void Awake()
    {
        if (instance != null) { Destroy(this.gameObject); }
        instance = this;

        AndroidRuntimePermissions.RequestPermission("android.permission.ACTIVITY_RECOGNITION");
    }

    void Start()
    {
        InputSystem.EnableDevice(AndroidStepCounter.current);
        AndroidStepCounter.current.MakeCurrent();

        temp_steps = PlayerPrefs.GetInt("Steps");
        steps = AndroidStepCounter.current.stepCounter.ReadValue() - temp_steps;
        if(steps < 0) steps *= -1;
    }


    void Update()
    {
        if (AndroidStepCounter.current != null)
        {
            text.text = AndroidStepCounter.current.stepCounter.ReadValue().ToString();
            text1.text = PlayerPrefs.GetInt("Steps").ToString();
            text2.text = steps.ToString();

            steps = AndroidStepCounter.current.stepCounter.ReadValue() - temp_steps;
            if(steps < 0) steps *= -1;
        }
    }

    void OnApplicationQuit()
    {
        InputSystem.DisableDevice(AndroidStepCounter.current);
    }
}
