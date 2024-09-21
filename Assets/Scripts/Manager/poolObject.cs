using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolObject : MonoBehaviour
{
    [SerializeField] int obj_index;

    public IEnumerator despawn(float TimeToDespawn)
    {
        yield return new WaitForSeconds(TimeToDespawn);
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        ObjectPooler.pool.ReturnObject(this.gameObject, obj_index);
    }
}
