using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMgr : MonoBehaviour
{
    // ���� �ؽ�Ʈ ���� ����
    public TMP_Text scoreText;

    // ���� ������ ����ϱ� ���� ����
    private int totalScore = 0;

    // ���� ������ ���� ����
    public GameObject monsterPrefab;

    // ���� ���� ����
    public float createTime = 3.0f;

    // ���� �⿬�� ��ġ ���� List
    public List<Transform> points = new List<Transform>();

    // ���͸� �̸� ������ ������ List
    public List<GameObject> monsterPool = new List<GameObject>();

    // ������Ʈ Ǯ�� ������ ������ �ִ� ����
    public int maxMosnters = 50;

    // ���� ���� ���� ����
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

        // ���� ������Ʈ Ǯ ����
        CreateMonsterPool();

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // List Ÿ�� ���� ���
        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);

        foreach( Transform item in spawnPointGroup)
        {
            points.Add(item);
        }

        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        // ������ �ұ�Ģ�� ��ġ ����
        int idx = Random.Range(0, points.Count);

        //Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);

        // ������Ʈ Ǯ���� ���� ����
        GameObject monster = GetMonsterInPool();

        // ������ ��ġ�� ȸ�� ����
        monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);

        monster?.SetActive(true);
    }

    void CreateMonsterPool()
    {
        for(int i = 0; i < maxMosnters; ++i)
        {
            var monster = Instantiate<GameObject>(monsterPrefab);
            // ���� �̸� ���� :���� Monster_01
            monster.name = $"Monster_{i:00}";
            // ���� ��Ȱ��ȭ
            monster.SetActive(false);
            // ������Ʈ Ǯ�� �߰�
            monsterPool.Add(monster);
        }
    }

    public GameObject GetMonsterInPool()
    {
        // ������Ʈ Ǯ ó������ ������ ��ȸ
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
