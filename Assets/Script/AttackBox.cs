using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public enum AttackType
{
    Enemy,
    Player
}
public class AttackBox : MonoBehaviour
{
    public BoxCollider m_Col;

    public EnemyBase m_Enemy;

  
    public AttackType m_AttackType;

    private void Awake()
    {
        m_Col = GetComponent<BoxCollider>();
        m_Enemy = GetComponentInParent<EnemyBase>();
        m_Col.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {

        if(m_AttackType == AttackType.Player)
        {
            if(other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyBase>().Hurt(Player.instance.m_AttackDamage);
            }
        }
        else if(m_AttackType == AttackType.Enemy)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"[AttackBox] 命中Player, 伤害: {m_Enemy.m_AttackDamage}");
                Player.instance.Hurt(m_Enemy.m_AttackDamage);
            }
        }

       
    }


}
