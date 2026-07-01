using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : EnemyBase
{

    public Transform m_Target;
    public NavMeshAgent m_Nav;



    public GameObject m_Bullet;

    public Transform m_BulletPos;

    public float m_BulletSpeed = 10f;

    private void Awake()
    {
        m_Target = FindAnyObjectByType<Player>().transform;

        m_Nav = GetComponent<NavMeshAgent>();
        m_Anima = GetComponent<Animator>();

        m_Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        m_BulletPos = transform.Find("BulletPos");
    }


    private void Start()
    {
        m_AttackDamage = 20f;
    }

    private void Update()
    {

        if (m_IsDead)
            return;

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

        //朝向玩家，找到方向
        Vector3 dir = m_Target.position - transform.position;
        //得到看向旋转
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        //超出视觉范围
        if (distance > 8)
        {
            m_Nav.speed = 0;
            m_Anima.SetFloat("Speed", m_Nav.speed);
            return;
        }

        //平滑转向玩家
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_RotationSpeed);

        //追逐范围
        if (distance >= 6)
        {
            m_Nav.speed = 4;
            m_Nav.SetDestination(m_Target.position);
            m_Anima.SetFloat("Speed", m_Nav.speed);
        }
        else
        {
            m_Nav.speed = 0;
            m_Nav.SetDestination(m_Target.position);
            m_Anima.SetFloat("Speed", m_Nav.speed);

            if (m_CanAttack)
            {
                Attack();
            }
        }

    }


    private void Attack()
    {

        m_Anima.SetTrigger("Attack");
        m_CanAttack = false;
        m_IsAttacking = true;

        m_AttackTimer = m_AttackCooldown;


    }


    public void Shoot()
    {
       Vector3 dir = m_Target.position - transform.position;
       dir = dir.normalized;

       //开火时瞬间面朝玩家
       transform.rotation = Quaternion.LookRotation(dir);

       GameObject go = Instantiate(m_Bullet, m_BulletPos.position, Quaternion.identity);

       go.GetComponent<Bullet>().Init(dir, m_AttackDamage, m_BulletSpeed);
    }

}
