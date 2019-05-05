using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //default
    public Player player;
    public GameObject StartLogo;
    float elapsedTime = 0f;
    int DiagolonCount = 0;

    //Diagolon
    public bool check;
    Vector3 pos;


    //enum
    PROGRESS progress = PROGRESS.START;
    EnemyPattern pattern = EnemyPattern.Diagolon;

    enum PROGRESS
    {
        START,
        WAVE,
        BOSS
    }

    enum EnemyPattern
    {
        NONE,
        Diagolon,
        Snake,
        Smart,
    }

    

    private void Start()
    {
        check = false;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        switch (progress)
        {
            case PROGRESS.START:
                if(elapsedTime >= 2)
                {
                    Instantiate(StartLogo);
                    progress = PROGRESS.WAVE;
                    elapsedTime = 0f;
                }

                break;

            case PROGRESS.WAVE:
                switch (pattern)
                {
                    //위 : 9
                    //아래 : -11
                    //X : 24

                    case EnemyPattern.Diagolon:
                        
                        DiagolonCount++;

                        if(check == false)
                        {
                            Debug.Log("적 소환");
                            float randomY = Random.Range(-11f, 8f);
                            float X = 24f;

                            pos = new Vector3(X, randomY, 0f); 
                            check = true;

                            Vector3 offset = new Vector3(4f, 0f, 0f);
                            Debug.Log(pos);

                            StartCoroutine(SpawnDiagolon(pos, offset * DiagolonCount));
                        }

                        if (DiagolonCount >= 4)
                        {
                            pattern = EnemyPattern.NONE;
                            DiagolonCount = 0;
                            check = false;
                        }

                        break;

                    case EnemyPattern.Snake:
                        //뱀 랜덤위치에 소환
                        break;

                    case EnemyPattern.Smart:
                        //똑똑한 녀석 랜덤 위치에 소환
                        break;
                }
               // elapsedTime = 0f;
                break;

            case PROGRESS.BOSS:

                break;
        }
    }

    IEnumerator SpawnDiagolon(Vector3 spos, Vector3 offset)
    {
        while (DiagolonCount <= 4)
        {
            yield return new WaitForSeconds(0.4f);

            GameObject temp = ObjectPool.Instance.PopFromPool("Diagolon");
            temp.transform.position = spos + offset;
            temp.SetActive(true);

            DiagolonCount++;
        }
    }


}
