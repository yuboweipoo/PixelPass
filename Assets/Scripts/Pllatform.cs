using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D pe2d; // PlatformEffector2D组件引用
    private bool isRotated = false; // 防止重复触发

    public float rotationAngle = 180f; // 旋转角度
    public float resetTime = 0.5f; // 重置时间

    void Start()
    {
        pe2d = GetComponent<PlatformEffector2D>(); // 获取PlatformEffector2D组件
    }

    void Update()
    {
        HandleInput(); // 处理输入
    }

    // 处理输入逻辑
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S) && !isRotated)
        {
            RotatePlatform(); // 旋转平台
        }
    }

    // 旋转平台
    private void RotatePlatform()
    {
        pe2d.rotationalOffset = rotationAngle; // 设置旋转角度
        isRotated = true; // 标记为已旋转
        StartCoroutine(ResetRotationAfterDelay()); // 开始重置协程
    }

    // 在延迟后重置平台旋转
    private IEnumerator ResetRotationAfterDelay()
    {
        yield return new WaitForSeconds(resetTime); // 等待指定时间
        pe2d.rotationalOffset = 0f; // 重置旋转角度
        isRotated = false; // 标记为未旋转
    }
}
