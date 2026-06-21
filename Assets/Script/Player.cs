using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
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

    public bool m_IsMoving = false;

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
    }

    private void Update()
    {
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

        //��һ�����ӽǵ���Ϸ�У��������ķ�����Ҫ��ת45�ȣ�����Ӧ��Ϸ���ӽǡ�
        m_InputDirection = Quaternion.Euler(0f, -45f, 0f) * m_InputDirection;

        if(m_InputDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(m_InputDirection);
            Debug.Log("Input Direction: " + m_InputDirection);
        }
    }

    private void FixedUpdate()
    {
        m_RB.velocity = m_InputDirection * m_Speed;
       
    }
}
