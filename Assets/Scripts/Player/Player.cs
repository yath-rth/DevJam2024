using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Player : MonoBehaviour
{
    public static Player instance;
    PlayerStats stats;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Time.timeScale = 1;

        if (instance != null){ Destroy(this.gameObject);}
        instance = this;

        stats = GetComponent<PlayerStats>();
    }

    public PlayerStats GetPlayerStats(){
        return stats;
    }
}
