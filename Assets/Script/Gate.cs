using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static Gate instance;

    public float m_Pos = -2.5f;//向下
    public float m_Duration = 2;//开门时间


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void Open()
    {
        StartCoroutine(OpenGate());
    }

    private IEnumerator OpenGate()
    {
        float currentDuration = 0;

        Vector3 currnetPos = transform.position;

        Vector3 openPos = currnetPos + Vector3.up * m_Pos;

        while(currentDuration < m_Duration)
        {
            currentDuration += Time.deltaTime;

            transform.position = Vector3.Lerp(currnetPos, openPos,currentDuration/m_Duration);
            yield return null;
        }

 
    }
}
