using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 80.0f;

    private Transform playerTransform;
    private Animation anim;

    // 초기 생명 값
    private readonly float iniHp = 100.0f;
    // 현재 생명 값
    public float currHp;

    // HPbar 연결할 변수
    private Image hpBar;

    IEnumerator Start()
    {
        // HP 초기화
        currHp = iniHp;

        playerTransform = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;

        hpBar = GameObject.FindGameObjectWithTag("HPBAR")?.GetComponent<Image>();
        DisplayHP();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();

        playerTransform.Translate(moveDir * moveSpeed * Time.deltaTime);

        playerTransform.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);

        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (h <= -0.1f)
            anim.CrossFade("RunL", 0.25f);
        else if (h >= 0.1f)
            anim.CrossFade("RunR", 0.25f);
        else if (v <= -0.1f)
            anim.CrossFade("RunB", 0.25f);
        else if (v >= 0.1f)
            anim.CrossFade("RunF", 0.25f);
        else
            anim.CrossFade("Idle", 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("PUNCH") && currHp > 0.0f )
        {
            currHp -= 10.0f;

            DisplayHP();

            Debug.Log($"Player HP : {currHp}");

            if( currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Die!");

        // MONSTER 태그를 가진 게임오브젝트 찾기
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        foreach(GameObject monster in monsters )
        {
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }

        GameManager.GetInstance().IsGameOver = true;
    }

    void DisplayHP()
    {
        hpBar.fillAmount = currHp / iniHp;
    }
}
