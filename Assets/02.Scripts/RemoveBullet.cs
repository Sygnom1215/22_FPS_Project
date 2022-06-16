using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 스파크 파티클 프리팹 연결 변수
    public GameObject sparkEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.collider.CompareTag("BULLET") )
        {   
            // 첫번째 충돌 지점 정보
            ContactPoint contactPoint = collision.contacts[0];
            // 충돌체의 노말(법선벡터) 방향의 반대로 방향으로 회전
            Quaternion rotSpark = Quaternion.LookRotation(-contactPoint.normal);
            
            GameObject spark = Instantiate(sparkEffect, contactPoint.point, rotSpark);
            spark.transform.SetParent(transform);

            Destroy(spark, 0.5f);

            // 충돌 오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
