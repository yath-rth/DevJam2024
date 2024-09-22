using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dumbellGun : MonoBehaviour
{
    [SerializeField] FieldOfView fov;
    [SerializeField] Transform spawn_point;
    GameObject dumbell_obj;
    [Range(0f, 5f)] public float TimeBtwShot;
    [HideInInspector] public float timeBtwShot;
    [SerializeField, Range(0f, 1f)] float TimeToDespawn = .5f;
    [SerializeField, Range(0f, 100f)] float speed;
    float timeRn;
    Vector3 pos;

    private void Awake()
    {
        timeBtwShot = PlayerPrefs.GetFloat("firerate", TimeBtwShot);
    }

    private void Update()
    {
        if (!Player.instance.GetPlayerStats().IsDead)
        {
            if (timeRn < Time.time && fov.closestTarget != null)
            {
                pos = fov.closestTarget.position;
                pos.y = spawn_point.position.y;

                spawn_point.LookAt(pos);

                dumbell_obj = ObjectPooler.pool.GetObject(0);
                dumbell_obj.transform.position = spawn_point.position;
                dumbell_obj.transform.rotation = spawn_point.parent.rotation;

                dumbell_obj.GetComponent<Rigidbody>().AddForce(spawn_point.forward * speed, ForceMode.Impulse);
                StartCoroutine(dumbell_obj.GetComponent<poolObject>().despawn(TimeToDespawn));

                timeRn = Time.time + timeBtwShot;
            }
        }
    }
}
