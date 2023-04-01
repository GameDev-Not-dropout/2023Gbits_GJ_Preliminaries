using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeController : MonoBehaviour
{
    public struct RecordeData
    {
        public Vector2 pos;
        public Vector2 vel;
        public float animationTime;
    }

    RecordeData[,] recordedData;

    public int recordSeconds = 3;
    int recordMax;             // 数据的最大存储量
    int frameDataIndex;        // 帧索引，用来辅助存储每帧产生的数据
    int recordIndex;           // 记录索引，用来读取已经存储好的每帧的数据
    bool wasSteppingBack = false;

    TimeControlled[] timeObjects;
    PlayerController playerController;
    public Volume postProcessing;
    ColorAdjustments colorAdjustments;

    private void Awake()
    {
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();
        playerController = FindObjectOfType<PlayerController>();

        recordMax = recordSeconds * 600000 * 3;
        recordedData = new RecordeData[timeObjects.Length, recordMax];

        postProcessing.profile.TryGet<ColorAdjustments>(out colorAdjustments);

    }


    private void Update()
    {
        bool pause = Input.GetKey(KeyCode.UpArrow);
        bool stepBack = Input.GetKey(KeyCode.LeftArrow);
        bool stepForward = Input.GetKey(KeyCode.RightArrow);

        if (stepBack)   // 时间回溯状态
        {
            wasSteppingBack = true;
            playerController.SetUseGravity(false);
            colorAdjustments.saturation.Override(-50f);

            if (recordIndex > 0)
            {
                recordIndex--;
                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)  // 倒带数据
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordeData data = recordedData[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.velocity = data.vel;
                    timeObject.animationTime = data.animationTime;

                    timeObject.UpdateAnimation();

                }
            }
        }
        else if (pause && stepForward)  // 时间快进状态
        {
            wasSteppingBack = true;
            playerController.SetUseGravity(false);

            if (recordIndex < frameDataIndex - 1)
            {
                recordIndex++;
                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)  // 快进数据
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordeData data = recordedData[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.velocity = data.vel;
                    timeObject.animationTime = data.animationTime;

                    timeObject.UpdateAnimation();

                }
            }
        }
        else if (!pause && !stepBack)   // 时间正常流动状态
        {
            if (wasSteppingBack)
            {
                frameDataIndex = recordIndex;  // 倒带结束后重新开始记录数据
                wasSteppingBack = false;
                playerController.SetUseGravity(true);
                colorAdjustments.saturation.Override(0f);

            }

            for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)  // 记录数据
            {
                TimeControlled timeObject = timeObjects[objectIndex];
                RecordeData data = new RecordeData();
                data.pos = timeObject.transform.position;
                data.vel = timeObject.velocity;
                data.animationTime = timeObject.animationTime;
                recordedData[objectIndex, frameDataIndex] = data;
            }
            frameDataIndex++;
            recordIndex = frameDataIndex;

            foreach (TimeControlled timeObject in timeObjects)  // 更新单位
            {
                timeObject.TimeUpdate();
                timeObject.UpdateAnimation();
            }
        }
    }
    
    private void OnGUI()    // 可视化debug
    {
        Rect rect = new Rect(200, 200, 500 , 500);
        string message = "时间是否正在回溯:" + wasSteppingBack;
        GUIStyle style = new GUIStyle();
        style.fontSize = 40;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(rect, message, style);
    }


}
