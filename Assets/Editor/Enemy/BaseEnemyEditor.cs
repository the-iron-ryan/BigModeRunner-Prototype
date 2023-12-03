using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.TubeClient;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BaseEnemy), true)]
public class BaseEnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BaseEnemy enemy = (BaseEnemy)target;

        // If the enemy color has changed, update the enemy's mesh color
        if(!Application.isPlaying)
        {
            enemy.SetEnemyMeshColor();
        }
    }

}
