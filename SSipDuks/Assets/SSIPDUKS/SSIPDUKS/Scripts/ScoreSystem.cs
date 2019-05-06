using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{   
    public GameObject[] m_number;
    public Transform[] m_field;
    GameObject[] m_activeObj;
    public static int score = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        m_activeObj = new GameObject[m_field.Length];
        setValue(score);
    }


    void Clear()
    {
        for (int i = 0; i < m_field.Length; i++)
        {
            Destroy(m_activeObj[i]);
        }

    }

    public void inc()
    {
        
        
        Clear();
        if (score < 999999)
        { 
            score += 1 ;
        }

        setValue(score);
    }

    public void Dec()
    {
        Clear();
        if (score > 0)
        {
            score -= 100;
        }

        setValue(score);
    }

    void setValue(int score)
    {
        int convert = 1;
        for(int i = 0; i < m_field.Length; i++)
        {
            int scoreConvert = (score / convert) % 10;
            Print(i, scoreConvert, i);
            convert *= 10;
        }
    }

    void Print(int activeObj , int score, int field)
    {
        m_activeObj[activeObj] = Instantiate(m_number[score], m_field[field].position, m_field[field].rotation);
        m_activeObj[activeObj].name = m_field[field].name;
        m_activeObj[activeObj].transform.parent = m_field[field];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Clear();
        setValue(score);
    }
}