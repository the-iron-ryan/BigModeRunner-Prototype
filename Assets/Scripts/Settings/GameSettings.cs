using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic singleton class that holds a reference to the game settings data
/// </summary>
public class GameSettings : Singleton<GameSettings>
{
    [SerializeField] private GameSettingsData gameSettingsData;

    public GameSettingsData GameSettingsData { get => gameSettingsData; }
}
