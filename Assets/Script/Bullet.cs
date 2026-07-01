using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    public float m_Damage;

    public float m_Speed = 10;
    public Vector3 m_Dir;

    public float m_Duration;
    public float m_Timer;

    private void Update()
    {
        transform.Translate(m_Dir * m_Speed * Time.deltaTime, Space.World);

        m_Timer += Time.deltaTime;

        if(m_Duration <= m_Timer)
        {
            Destroy(gameObject);
        }

    }

    public void Damage(float damage)
    {
        m_Damage = damage;
    }

    internal void Init(Vector3 dir, float attackDamage, float speed = 10f)
    {
        m_Timer = 0;
        m_Dir = dir;
        m_Speed = speed;
        Damage(attackDamage);

        //�ӵ����ŷ�����ת
        transform.rotation = Quaternion.LookRotation(m_Dir);
        transform.Rotate(90, 0, 0, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.instance.Hurt(m_Damage);
            Destroy(gameObject);
        }
    }
}
