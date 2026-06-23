using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Enemy1 : EnemyBase
{

    public Transform m_Target;
    public NavMeshAgent m_Nav;


    public BoxCollider m_AttackBox;
        

    private void Awake()
    {
        m_Target = FindAnyObjectByType<Player>().transform;
        m_Nav = GetComponent<NavMeshAgent>();
        m_Anima = GetComponent<Animator>();
    }


    private void Start()
    {
        m_AttackDamage = 20f;

        m_AttackBox = transform.Find("Attackbox").GetComponent<BoxCollider>();  
       

    }

    private void Update()
    {

        if (m_Nav.speed > 0)
        {
            m_IsMoving = true;
        }
        else
        {
            m_IsMoving = false;
        }

        //attack timer
        if (m_IsAttacking)
        {
            m_AttackTimer -= Time.deltaTime;

            if (m_AttackTimer <= (m_AttackCooldown - m_AttackDuration))
            {
                m_IsAttacking = false;
            }
        }

        //在攻击冷却时间中
        if (!m_CanAttack && !m_IsAttacking)
        {
            m_AttackTimer -= Time.deltaTime;
            if (m_AttackTimer < m_AttackCooldown)
            {
                m_AttackTimer = 0;
                m_CanAttack = true;
            }
        }


        CaluclateEnemyMovement();
    }


    private void CaluclateEnemyMovement()
    {

        if (m_IsAttacking || m_IsDead)
            return;

        float distance = Vector3.Distance(m_Target.position, transform.position);

        //计算敌人到玩家的向量
        Vector3 dir = m_Target.position - transform.position;
        //让敌人看向向量
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        //超出视觉范围
        if (distance > 6)
        {
            m_Nav.speed = 0;
            m_Anima.SetFloat("Speed", m_Nav.speed);
            return;
        }

        //发现玩家
        if (distance >= 2)
        {
            m_Nav.speed = 4;
            m_Nav.SetDestination(m_Target.position);
            m_Anima.SetFloat("Speed", m_Nav.speed);

        }
        else
        {
            //attack
            m_Nav.speed = 0;
            m_Nav.SetDestination(m_Target.position);
            m_Anima.SetFloat("Speed", m_Nav.speed);

            if (m_CanAttack)
            {
                Attack();
            }

            //平滑转向玩家
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_RotationSpeed);

        }

    }


    private void Attack()
    {

        m_Anima.SetTrigger("Attack");
        m_CanAttack = false;
        m_IsAttacking = true;

        m_AttackTimer = m_AttackCooldown;


    }

    public void EnableAttackBox()
    {
        m_AttackBox.enabled = true;
        Debug.Log("打开AttackBox");

    }

    public void DisableAttackBox()
    {
        m_AttackBox.enabled = false;
        Debug.Log("关闭AttackBox");

    }

}
