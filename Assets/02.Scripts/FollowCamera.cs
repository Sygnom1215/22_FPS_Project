using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetTransform;
    private Transform cameraTransform;

    [Range(2.0f, 20.0f)]
    public float distance = 10f;    // 거리

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;     // 높이

    public float moveDamping = 15f;     // 속도 계수
    public float rotateDamping = 10f;   // 회전 계수

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
        
        // 구면 보간 : 현재 -> 타겟의 위치
        cameraTransform.position = Vector3.Slerp(cameraTransform.position, pos, moveDamping* Time.deltaTime);

        // 구면 보간 : 현재 회전 -> 타겟의 회전 
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetTransform.rotation, rotateDamping* Time.deltaTime);

        cameraTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));
    }
}
