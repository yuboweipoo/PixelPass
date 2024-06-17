using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPointGizimos : MonoBehaviour
{
    // 画出他的范围
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3(0.3f,0.3f,0f));
    }
}
