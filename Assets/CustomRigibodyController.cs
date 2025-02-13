using System.Collections;
using System.Globalization;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class CustomRigidbodyController : MonoBehaviour
{
    private Rigidbody rb;
    private NetworkObject networkObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 等待一帧，确保 NetworkRigidbody 初始化完成
        StartCoroutine(ResetKinematicAfterNetworkSync());
    }

    private IEnumerator ResetKinematicAfterNetworkSync()
    {
        yield return null; // 等待一帧
        if (networkObject != null && !networkObject.IsOwner)
        {
            rb.isKinematic = true; // 恢复你的自定义设置
        }
    }
}

