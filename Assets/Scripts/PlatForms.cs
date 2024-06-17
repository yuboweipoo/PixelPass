using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForms : MonoBehaviour
{
    [SerializeField] private GameObject[] points;//移动平平台的目标
    [SerializeField] private float speed = 2f;// 平台的移动速度

    private bool isJoin;// 是否加入成为子物体
    private int pointIndex = 1;// 当前目标点索引位置

    private float WaitTime = 0.5f; //等待时间
    // Update is called once per frame
    private void FixedUpdate()
    {
        // 平台向目标点移动
        transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position, speed * Time.deltaTime);
        // 检查平台是否接近目标点
        if (Vector2.Distance(transform.position, points[pointIndex].transform.position) < 0.1f)
        {
            if (WaitTime < 0)
            {
                pointIndex = 1 - pointIndex;
                WaitTime = 0.5f;
                Rolling();
            }
            else
            {
                WaitTime -= Time.deltaTime;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);// 设置成了子物体
            isJoin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);// 结束了子物体
            isJoin = false;
        }
    }

    private void Rolling()
    {
        Transform _chid = null;
        if (isJoin)
        {
            _chid = transform.GetChild(0);
            transform.GetChild(0).SetParent(null);
        }
        // 获取当前物体缩放信息
        Vector3 playerScale = transform.localScale;
        // 把x改为-x
        playerScale.x *= -1;
        // 必须得在赋值回去，否则不生效
        transform.localScale = playerScale;
        if(_chid){
            _chid.SetParent(transform);
        }
    }


}
