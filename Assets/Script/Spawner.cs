using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Spawner instance;

    public GameObject m_Prefabs;

    public List<Transform> m_SpawnPos1 = new List<Transform>();
    public List<Transform> m_SpawnPos2 = new List<Transform>();

    public int m_RemainEnemies = 8;

    public bool m_IsSpawning = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        m_Prefabs = Resources.Load<GameObject>("Prefabs/Enemy1");
    }

    private void Start()
    {
        SpawnEnemies(m_SpawnPos1);
    }

    private void Update()
    {
        if(m_RemainEnemies == 2 && m_IsSpawning)
        {
            //开门
            Gate.instance.Open();
            //生成第二波敌人
            SpawnEnemies(m_SpawnPos2);
            //
            m_IsSpawning = false;
            //

        }

        if(m_RemainEnemies == 0 && GameManager.instance.m_IsWin)
        {
            GameManager.instance.Win();
        }
    }

    public void SpawnEnemies(List<Transform> spawnList)
    {
        foreach (Transform spawn in spawnList)
        {
            Instantiate(m_Prefabs, spawn.position, Quaternion.identity);

        }
    }
}
