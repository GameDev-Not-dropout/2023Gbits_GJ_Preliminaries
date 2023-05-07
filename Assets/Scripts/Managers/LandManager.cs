using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public static LandManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Vector3 lastJumpPoint = Vector3.zero;











}
