using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D pe2d; // PlatformEffector2D�������
    private bool isRotated = false; // ��ֹ�ظ�����

    public float rotationAngle = 180f; // ��ת�Ƕ�
    public float resetTime = 0.5f; // ����ʱ��

    void Start()
    {
        pe2d = GetComponent<PlatformEffector2D>(); // ��ȡPlatformEffector2D���
    }

    void Update()
    {
        HandleInput(); // ��������
    }

    // ���������߼�
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S) && !isRotated)
        {
            RotatePlatform(); // ��תƽ̨
        }
    }

    // ��תƽ̨
    private void RotatePlatform()
    {
        pe2d.rotationalOffset = rotationAngle; // ������ת�Ƕ�
        isRotated = true; // ���Ϊ����ת
        StartCoroutine(ResetRotationAfterDelay()); // ��ʼ����Э��
    }

    // ���ӳٺ�����ƽ̨��ת
    private IEnumerator ResetRotationAfterDelay()
    {
        yield return new WaitForSeconds(resetTime); // �ȴ�ָ��ʱ��
        pe2d.rotationalOffset = 0f; // ������ת�Ƕ�
        isRotated = false; // ���Ϊδ��ת
    }
}
