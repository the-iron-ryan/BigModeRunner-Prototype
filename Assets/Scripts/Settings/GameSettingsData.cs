using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsData", menuName = "Settings/GameSettingsData")]
public class GameSettingsData : ScriptableObject
{
    [Header("Player")]
    public Vector3 runnerDirection = Vector3.forward;

    [Header("Stage")]
    public float stageWidth = 10.0f;
    public float stageHeight = 10.0f;

}
