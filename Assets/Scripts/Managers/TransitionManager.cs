using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [NonSerialized] public float handler2, handler3;


    private void Awake()
    {
        Instance = this;
    }










}
