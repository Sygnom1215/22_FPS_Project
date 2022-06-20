using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    // Æø¹ßÇÏ´Â ÀÌÆåÆ®
    public GameObject expEffect;


    // Æø¹ßÇÏ´Â Èû
    public float force = 1500.0f;

    // Æø¹ß ¹Ý°æ
    public float radius = 10.0f;

    // Ãæµ¹ È½¼ö °Ë»ç
    private int hitCount = 0;

    private Rigidbody barrelRigidbody = null;
    private Transform barrelTransform = null;

    private new MeshRenderer renderer;

    void Start()
    {
        barrelRigidbody = GetComponent<Rigidbody>();
        barrelTransform = GetComponent<Transform>();

        renderer = GetComponentInChildren<MeshRenderer>();

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
        GameObject exp = Instantiate(expEffect, barrelTransform.position, Quaternion.identity);
        Destroy(exp, 5.0f);

        IndirectDamage(barrelTransform.position);

        Destroy(this.gameObject, 3.0f);
    }

    Collider[] colls = new Collider[10];
    void IndirectDamage(Vector3 pos)
    {
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);

        foreach ( var coll in colls)
        {
            if (coll == null)
                continue;

            Rigidbody rb = coll.GetComponent<Rigidbody>();

            rb.mass = 1.0f;

            rb.constraints = RigidbodyConstraints.None;

            rb.AddExplosionForce(force, pos, radius, 1200.0f);
        }
    }
}
