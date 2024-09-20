using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolObject : MonoBehaviour
{
    [SerializeField] int obj_index;
    [SerializeField] float TimeToDespawn = 1f;

    public IEnumerator despawn()
    {
        yield return new WaitForSeconds(TimeToDespawn);
        ObjectPooler.pool.ReturnObject(this.gameObject, obj_index);
    }
}
