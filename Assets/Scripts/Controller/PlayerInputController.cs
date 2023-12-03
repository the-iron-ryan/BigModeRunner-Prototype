using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameMode curMode = GameModeController.Instance.CurrentGameMode;
        switch(curMode)
        {
            case GameMode.Runner:
                break;
            case GameMode.Shooter:
                break;
            case GameMode.Puzzle:
                break;
        }
    }
}