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

        // �ȴ�һ֡��ȷ�� NetworkRigidbody ��ʼ�����
        StartCoroutine(ResetKinematicAfterNetworkSync());
    }

    private IEnumerator ResetKinematicAfterNetworkSync()
    {
        yield return null; // �ȴ�һ֡
        if (networkObject != null && !networkObject.IsOwner)
        {
            rb.isKinematic = true; // �ָ�����Զ�������
        }
    }
}

