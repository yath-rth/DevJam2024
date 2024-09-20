using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dumbellGun : MonoBehaviour
{
    [SerializeField] FieldOfView fov;
    [SerializeField] Transform spawn_point;
    GameObject dumbell_obj;
    [Range(0f, 5f)] public float timeBtwShot;
    [Range(0f, 100f)] public float speed;
    float timeRn;
    Vector3 pos;

    private void Update()
    {
        if (timeRn < Time.time)
        {
            if (fov.closestTarget != null)
            {
                pos = fov.closestTarget.position;
                pos.y = spawn_point.position.y;
            }
            else
            {
                pos = spawn_point.parent.transform.forward;
            }

            spawn_point.LookAt(pos);

            dumbell_obj = ObjectPooler.pool.GetObject(0);
            dumbell_obj.transform.position = spawn_point.position;
            dumbell_obj.transform.rotation = spawn_point.parent.rotation;

            dumbell_obj.GetComponent<Rigidbody>().AddForce(spawn_point.forward * speed, ForceMode.Impulse);
            StartCoroutine(dumbell_obj.GetComponent<poolObject>().despawn());

            timeRn = Time.time + timeBtwShot;
        }
    }
}
