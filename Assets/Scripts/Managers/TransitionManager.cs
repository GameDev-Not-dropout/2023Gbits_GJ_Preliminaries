using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public float defaultHandler2;
    public float defaultHandler3;
    [NonSerialized] public float handler2, handler3;


    private void Awake()
    {
        Instance = this;
        Instance.handler2 = defaultHandler2;
        Instance.handler3 = defaultHandler3;

    }










}
