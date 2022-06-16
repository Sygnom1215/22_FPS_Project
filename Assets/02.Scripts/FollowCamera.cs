using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetTransform;
    private Transform cameraTransform;

    [Range(2.0f, 20.0f)]
    public float distance = 10f;    // �Ÿ�

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;     // ����

    public float moveDamping = 15f;     // �ӵ� ���
    public float rotateDamping = 10f;   // ȸ�� ���

    public float targetOffset = 2f;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = targetTransform.position
                      + (-targetTransform.forward * distance)
                      + (targetTransform.up * height);
        
        // ���� ���� : ���� -> Ÿ���� ��ġ
        cameraTransform.position = Vector3.Slerp(cameraTransform.position, pos, moveDamping* Time.deltaTime);

        // ���� ���� : ���� ȸ�� -> Ÿ���� ȸ�� 
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetTransform.rotation, rotateDamping* Time.deltaTime);

        cameraTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));
    }
}
