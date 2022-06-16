using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    // Bullet ������
    public GameObject bulletPrefab;
    // Bullet Fire Pos
    public Transform firePos;

    // �ѼҸ��� ����� ����
    public AudioClip fireSFX;

    // ����� �ҽ� ������Ʈ ������ ����
    private new AudioSource audio;

    // �ѱ� ȭ��
    private MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    void Update()
    {
        if( Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet ���纻 ����
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);

        audio.PlayOneShot(fireSFX, 1.0f);

        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;

        // ������ ������
        muzzleFlash.material.mainTextureOffset = offset;

        // ������ ȸ��
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);

        // ������ ũ��
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;
    }
}
