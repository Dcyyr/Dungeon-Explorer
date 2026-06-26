using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_RB;
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private float m_Speed = 5f;

    public float m_HorizontalInput;
    public float m_VerticalInput;

    public Vector3 m_InputDirection;

    //移动
    public bool m_IsMoving = false;

    //翻滚
    public bool m_CanRoll = true;//默认可以翻滚
    public bool m_IsRolling = false;
    public float m_RollingDuration = 0.4f;//翻滚持续时间
    public float m_RollingTimer;
    public float m_RollingCooldown = 0.8f;
    public float m_RollingCooldownTimer;

    //受伤
    public bool m_IsHurt = false;
    public float m_InvisibleDuration = 0.5f;//无敌持续时间
    public float m_InvisibleTimer;
    public float m_MaxHealth = 100f;
    public float m_CurrentHealth;

    //死亡
    public bool m_IsDead = false;


    //Grounded
    public bool m_IsGrounded = true;
    public float m_Gravity = -3.5f;

    public static Player instance;


    //attack 
    public float m_AttackDamage = 20f;
    public BoxCollider m_AttackBox;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        m_RB = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();

        m_RB.freezeRotation = true;//冻结旋转，防止物理碰撞导致角色旋转
        m_RB.useGravity = false;//不受重力影响

        m_CurrentHealth = m_MaxHealth;
    }

    private void Start()
    {
        m_AttackBox = transform.Find("Attackbox").GetComponent<BoxCollider>();
    }

    private void Update()
    {
        //死亡状态下不执行任何操作
        if (m_IsDead)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            m_Anim.SetTrigger("Attack");
        }

        m_HorizontalInput = Input.GetAxisRaw("Horizontal");
        m_VerticalInput = Input.GetAxisRaw("Vertical");

        m_InputDirection.Set(m_HorizontalInput, 0f, m_VerticalInput);
        m_InputDirection.Normalize();


        if (m_InputDirection.magnitude != 0)
        {
            m_IsMoving = true;

        }
        else
        {
            m_IsMoving = false;

        }

        m_Anim.SetBool("IsMove", m_IsMoving);

        //上帝视角的游戏移动
        m_InputDirection = Quaternion.Euler(0f, -45f, 0f) * m_InputDirection;

        //角色模型跟着转向
        if (m_InputDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(m_InputDirection);
            //Debug.Log("Input Direction: " + m_InputDirection);
        }



        //翻滚
        if (Input.GetKeyDown(KeyCode.LeftShift) && m_CanRoll)
        {
            m_CanRoll = false;
            m_IsRolling = true;
            m_Anim.SetTrigger("Roll");
        }

        //正在翻滚
        if (m_IsRolling)
        {
            m_RollingTimer += Time.deltaTime;
            if (m_RollingTimer > m_RollingDuration)//翻滚结束
            {
                m_IsRolling = false;
                m_RollingTimer = 0;
            }
        }

        //判断冷却时间
        if (!m_CanRoll)
        {
            m_RollingCooldownTimer += Time.deltaTime;
            if (m_RollingCooldownTimer > m_RollingCooldown)//冷却完成
            {
                m_CanRoll = true;//可以翻滚
                m_RollingCooldownTimer = 0;
            }
        }

        if (m_IsHurt)
        {
            m_InvisibleTimer += Time.deltaTime;
            if (m_InvisibleTimer >= m_InvisibleDuration)
            {
                m_IsHurt = false;
                m_InvisibleTimer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Hurt(20f);
        }

    }

    private void FixedUpdate()
    {

        if (!m_IsGrounded)
        {
            transform.Translate(0, m_Gravity * Time.fixedDeltaTime, 0);
        }


        if (m_IsRolling)
        {
            m_RB.velocity = transform.forward * m_Speed * 2f;

            return;//正在翻滚时不受输入影响
        }

        m_RB.velocity = m_InputDirection * m_Speed;


    }

    public void EnableAttackBox()
    {
        m_AttackBox.enabled = true;
    }

    public void DisableAttackBox()
    {
        m_AttackBox.enabled = false;
    }


    public void Hurt(float damage)
    {
        if (m_IsHurt)
            return;
        Debug.Log("玩家受伤: " + damage);
        m_CurrentHealth -= damage;
        m_CurrentHealth = Mathf.Max(0, m_CurrentHealth);
        m_IsHurt = true;
        m_Anim.SetTrigger("Hurt");

        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }

    }
    public void Die()
    {
        m_IsDead = true;
        m_Anim.SetTrigger("Dead");

        GameManager.instance.GameOver();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            m_IsGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            m_IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            m_IsGrounded = false;
        }
    }



}
