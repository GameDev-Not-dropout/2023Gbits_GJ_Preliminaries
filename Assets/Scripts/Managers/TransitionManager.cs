using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public ShockWaveManager[] shockWaves;
    public GameObject transitionTriggerA;
    public GameObject transitionTriggerB;
    bool isC3;

    float triggerAPos;
    float triggerBPos;

    public float TriggerAPos 
    { 
        get => triggerAPos;
        set
        {
            triggerAPos = value;
            transitionTriggerA.transform.position = new Vector3(triggerAPos, 0, 0);
        }
    }
    public float TriggerBPos
    {
        get => triggerBPos;
        set
        {
            triggerBPos = value;
            transitionTriggerB.transform.position = new Vector3(triggerBPos, 0, 0);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            isC3 = true;
        }
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
