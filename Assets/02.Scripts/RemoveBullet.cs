using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // ����ũ ��ƼŬ ������ ���� ����
    public GameObject sparkEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.collider.CompareTag("BULLET") )
        {   
            // ù��° �浹 ���� ����
            ContactPoint contactPoint = collision.contacts[0];
            // �浹ü�� �븻(��������) ������ �ݴ�� �������� ȸ��
            Quaternion rotSpark = Quaternion.LookRotation(-contactPoint.normal);
            
            GameObject spark = Instantiate(sparkEffect, contactPoint.point, rotSpark);
            spark.transform.SetParent(transform);

            Destroy(spark, 0.5f);

            // �浹 ������Ʈ ����
            Destroy(collision.gameObject);
        }
    }
}
