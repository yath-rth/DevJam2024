using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatToThin : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> meshes = new List<SkinnedMeshRenderer>();
    [SerializeField, Range(0f, 10f)] float increment;
    float weight;

    private void OnEnable() {
        EnemyStats.OnEnemyDeath += check;
    }

    private void OnDisable() {
        EnemyStats.OnEnemyDeath -= check;
    }

    void check(int a){
        weight += increment;
        weight = Math.Clamp(weight, 0, 150);

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.SetBlendShapeWeight(0, weight);
        }
    }
}
