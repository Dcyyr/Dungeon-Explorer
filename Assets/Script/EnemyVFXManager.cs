using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXManager : MonoBehaviour
{
    public VisualEffect m_FootstepVFX;
    public VisualEffect m_AttackVFX;


    public EnemyBase m_EnemyBase;

    private void Awake()
    {
        m_EnemyBase = GetComponent<EnemyBase>();
    }


    public void FootStepVFX()
    {
        m_FootstepVFX.Play();
    }


    public void AttackVFX()
    {
        m_AttackVFX.Play();
    }

}
