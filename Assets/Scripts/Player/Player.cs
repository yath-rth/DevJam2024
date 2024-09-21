using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    PlayerStats stats;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance != null){ Destroy(this.gameObject);}
        instance = this;

        stats = GetComponent<PlayerStats>();
    }

    public PlayerStats GetPlayerStats(){
        return stats;
    }
}
