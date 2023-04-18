using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [NonSerialized] public float handler2;
    public ShockWaveManager[] shockWaves;


    private void Awake()
    {
        Instance = this;
    }

    public void SpawnShockWaves(Vector3 playerPosition, int index)
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
    }








}
