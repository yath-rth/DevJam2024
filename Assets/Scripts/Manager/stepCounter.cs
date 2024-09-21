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
    int temp_steps;

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
    }


    void Update()
    {
        if (AndroidStepCounter.current != null)
        {
            text.text = AndroidStepCounter.current.stepCounter.ReadValue().ToString();
            text1.text = PlayerPrefs.GetInt("Steps").ToString();
            text2.text = steps.ToString();

            if (temp_steps != AndroidStepCounter.current.stepCounter.ReadValue())
            {
                steps = steps + AndroidStepCounter.current.stepCounter.ReadValue() - temp_steps;
                temp_steps = AndroidStepCounter.current.stepCounter.ReadValue();
            }

            //steps -= AndroidStepCounter.current.stepCounter.ReadValue();
            //steps = -steps;
        }
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus == false)
        {
            if (AndroidStepCounter.current != null)
            {
                PlayerPrefs.SetFloat("Steps", AndroidStepCounter.current.stepCounter.ReadValue());
            }
            InputSystem.DisableDevice(AndroidStepCounter.current);
        }
    }
}
