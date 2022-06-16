using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    // �����ϴ� ����Ʈ
    public GameObject expEffect;

    // �������� ������ �ؽ�ó �迭
    public Texture[] textures;

    // �����ϴ� ��
    public float force = 1500.0f;

    // ���� �ݰ�
    public float radius = 10.0f;

    // �浹 Ƚ�� �˻�
    private int hitCount = 0;

    private Rigidbody barrelRigidbody = null;
    private Transform barrelTransform = null;

    private new MeshRenderer renderer;

    void Start()
    {
        barrelRigidbody = GetComponent<Rigidbody>();
        barrelTransform = GetComponent<Transform>();

        renderer = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[idx];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        // ���� ����Ʈ ����
        GameObject exp = Instantiate(expEffect, barrelTransform.position, Quaternion.identity);
        // 5�� �� ����
        Destroy(exp, 5.0f);

        // ������ 1���ؼ� ������ �����
        //barrelRigidbody.mass = 1.0f;
        //barrelRigidbody.AddForce(Vector3.up * force);

        IndirectDamage(barrelTransform.position);


        Destroy(this.gameObject, 3.0f);
    }

    Collider[] colls = new Collider[10];
    void IndirectDamage(Vector3 pos)
    {

        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);

        foreach ( var coll in colls)
        {
            if (coll == null)
                continue;

            Rigidbody rb = coll.GetComponent<Rigidbody>();

            // �巳�� ������ ���� 1
            rb.mass = 1.0f;

            // freezeRotation ���Ѱ� ����
            rb.constraints = RigidbodyConstraints.None;

            // ���߷� ����
            rb.AddExplosionForce(force, pos, radius, 1200.0f);
        }
    }
}
