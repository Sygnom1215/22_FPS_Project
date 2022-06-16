using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMgr : MonoBehaviour
{
    // 점수 텍스트 연결 변수
    public TMP_Text scoreText;

    // 누적 점수를 기록하기 위한 변수
    private int totalScore = 0;

    // 몬스터 프리팹 연결 변수
    public GameObject monsterPrefab;

    // 몬스터 생성 간격
    public float createTime = 3.0f;

    // 몬스터 출연할 위치 저장 List
    public List<Transform> points = new List<Transform>();

    // 몬스터를 미리 생성해 저장할 List
    public List<GameObject> monsterPool = new List<GameObject>();

    // 오브젝트 풀에 생성할 몬스터의 최대 갯수
    public int maxMosnters = 50;

    // 게임 종료 여부 변수
    private bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
                CancelInvoke("CreateMonster");
        }
    }

    private static GameMgr instance;

    public static GameMgr GetInstance()
    {
        if( instance == null )
        {
            instance = FindObjectOfType<GameMgr>();
            if( instance == null )
            {
                GameObject container = new GameObject("GameMgr");
                instance = container.AddComponent<GameMgr>();
            }
        }

        return instance;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        DisplayScore(0);

        // 몬스터 오브젝트 풀 생성
        CreateMonsterPool();

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // List 타입 변수 사용
        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);

        foreach( Transform item in spawnPointGroup)
        {
            points.Add(item);
        }

        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        // 몬스터의 불규칙한 위치 생성
        int idx = Random.Range(0, points.Count);

        //Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);

        // 오브젝트 풀에서 몬스터 추출
        GameObject monster = GetMonsterInPool();

        // 몬스터의 위치와 회전 설정
        monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);

        monster?.SetActive(true);
    }

    void CreateMonsterPool()
    {
        for(int i = 0; i < maxMosnters; ++i)
        {
            var monster = Instantiate<GameObject>(monsterPrefab);
            // 몬스터 이름 지정 :형식 Monster_01
            monster.name = $"Monster_{i:00}";
            // 몬스터 비활성화
            monster.SetActive(false);
            // 오브젝트 풀에 추가
            monsterPool.Add(monster);
        }
    }

    public GameObject GetMonsterInPool()
    {
        // 오브젝트 풀 처음부터 끝가지 순회
        foreach(var monster in monsterPool)
        {
            if (monster.activeSelf == false)
                return monster;
        }
        return null;
    }

    public void DisplayScore(int score)
    {
        totalScore += score;
        scoreText.text = $"<color=#00ff00>SCORE:</color> <color=#ff0000>{totalScore:#,##0}</color>";
    }
}
