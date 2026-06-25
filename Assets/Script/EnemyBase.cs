using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //health
    public float m_MaxHealth = 100f;
    public float m_CurrentHealth = 100f; 
    public bool m_IsDead = false;
    public Animator m_Anima;

    //attack
    public float m_AttackTimer;
    public float m_AttackCooldown = 5f;
    public const float m_AttackDuration = 2.3f;//攻击持续时间,动画的长度
    public bool m_IsAttacking = false;
    public bool m_CanAttack = true;
    public float m_AttackDamage;

    public float m_RotationSpeed = 3f;
    public bool m_IsMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Hurt(float damage)
    {
        if (m_IsDead)
            return;

        m_MaxHealth -= damage;
        m_MaxHealth = Mathf.Max(0, m_MaxHealth);
        m_Anima.SetTrigger("Hurt");

        if(m_MaxHealth <=0)
        {
            Debug.Log("敌人死亡");
            Dead();
        }

    }


    public void Dead()
    {
        m_IsDead = true;
        m_Anima.SetTrigger("Dead");

        Spawner.instance.m_RemainEnemies--;
    }
}
