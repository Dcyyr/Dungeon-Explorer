using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    public VisualEffect m_FootStep;
    public VisualEffect m_Heal;

    public ParticleSystem m_Attack1;
    public ParticleSystem m_Attack2;
    public ParticleSystem m_Attack3;

    public bool m_IsFootstepPlaying = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        //异或 ，两者相同为false，不同为true，为了让脚步特效和玩家移动状态保持一致，
        if (m_IsFootstepPlaying ^ Player.instance.m_IsMoving)
        {
            UpdateFootStep(Player.instance.m_IsMoving);
            m_IsFootstepPlaying = !m_IsFootstepPlaying;
        }
    }


    public void UpdateFootStep(bool state)
    {
        if(state)
        {
            m_FootStep.Play();

        }
        else
        {
            m_FootStep.Stop();
        }

    }

    public void Attack1()
    {
        m_Attack1.Play();   
    }

    public void Attack2()
    {
        m_Attack2.Play();
    }

    public void Attack3()
    {
        m_Attack3.Play();
    }

}
