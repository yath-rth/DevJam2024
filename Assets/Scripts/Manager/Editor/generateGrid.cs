using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (mapSpawner))]
public class generateGrid : Editor
{
   public override void OnInspectorGUI ()
	{

		mapSpawner map = target as mapSpawner;

		if (DrawDefaultInspector ()) {
			map.GenerateGrid ();
		}

		if (GUILayout.Button("Generate Grid")) {
			map.GenerateGrid ();
		}


	}
}
