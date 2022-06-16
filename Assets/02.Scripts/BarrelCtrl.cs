using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    // 폭발하는 이펙트
    public GameObject expEffect;

    // 무작위로 적용할 텍스처 배열
    public Texture[] textures;

    // 폭발하는 힘
    public float force = 1500.0f;

    // 폭발 반경
    public float radius = 10.0f;

    // 충돌 횟수 검사
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
        // 폭발 이펙트 생성
        GameObject exp = Instantiate(expEffect, barrelTransform.position, Quaternion.identity);
        // 5초 후 제거
        Destroy(exp, 5.0f);

        // 질량을 1로해서 가볍게 만들기
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

            // 드럼통 가볍게 질량 1
            rb.mass = 1.0f;

            // freezeRotation 제한값 해제
            rb.constraints = RigidbodyConstraints.None;

            // 폭발력 전달
            rb.AddExplosionForce(force, pos, radius, 1200.0f);
        }
    }
}
