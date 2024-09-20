using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class CameraController : MonoBehaviour
{
    Vector3 pos;
    Player player;

    public float offset;
    public bool x = false;

    void Awake()
    {
        player = Player.instance;
    }

    void Update()
    {
        if (x == true) pos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + offset);
        else pos = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offset);
        
        transform.position = Vector3.Lerp(transform.position, pos, 1f);
    }
}
