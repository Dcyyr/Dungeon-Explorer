using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public SphereCollider m_Coll;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player.instance.AddHealth(20);
            Destroy(gameObject);
        }
    }

   
}
