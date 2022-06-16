using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    // Bullet 프리팹
    public GameObject bulletPrefab;
    // Bullet Fire Pos
    public Transform firePos;

    // 총소리에 사용할 음원
    public AudioClip fireSFX;

    // 오디오 소스 컴포넌트 저장할 변수
    private new AudioSource audio;

    // 총구 화염
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
        // Bullet 복사본 생성
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);

        audio.PlayOneShot(fireSFX, 1.0f);

        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;

        // 랜덤한 오프셋
        muzzleFlash.material.mainTextureOffset = offset;

        // 랜덤한 회전
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);

        // 랜덤한 크기
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;
    }
}
