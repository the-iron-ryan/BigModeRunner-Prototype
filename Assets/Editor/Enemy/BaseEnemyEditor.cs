using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.TubeClient;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BaseEnemyCharacter), true)]
public class BaseEnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BaseEnemyCharacter enemy = (BaseEnemyCharacter)target;

        // If the enemy color has changed, update the enemy's mesh color
        if(!Application.isPlaying)
        {
            enemy.SetEnemyMeshColor();
        }
    }

}
