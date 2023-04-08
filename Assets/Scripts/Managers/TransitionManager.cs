using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [NonSerialized] public float handler2, handler3;
    public ShockWaveManager[] shockWaves;


    private void Awake()
    {
        Instance = this;
    }

    public void SpawnShockWaves(Vector3 playerPosition, int index, float offset = 0)
    {
        if (index == 1)
        {
            shockWaves[0].transform.position = playerPosition;
            shockWaves[0].CallshockWave();
        }
        else if (index == 2)
        {
            shockWaves[1].transform.position = playerPosition;
            shockWaves[1].CallshockWave();
        }
        else if (index == 3)
        {
            shockWaves[2].transform.position = new Vector3(playerPosition.x + offset, playerPosition.y);
            shockWaves[2].CallshockWave();
        }
    }








}
