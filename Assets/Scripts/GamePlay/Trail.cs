using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    public float distance = 10f;
    public Vector3 offset = Vector3.zero;

    private void Update()
    {
        //自身坐标转换成屏幕坐标
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //让鼠标的屏幕坐标与对象坐标一致
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
        //transform.position = mousePos + offset;
        //Vector3 mousePos = ray.GetPoint(distance);
        //转换成世界坐标
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = mousePos + offset;
    }




}
