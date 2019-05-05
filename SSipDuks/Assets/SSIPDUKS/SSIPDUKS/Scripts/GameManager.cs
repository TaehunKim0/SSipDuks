using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //default
    public Player player;
    public GameObject StartLogo;
    float elapsedTime = 0f;

    private bool check;

    //enum
    PROGRESS progress = PROGRESS.START;
    EnemyPattern pattern = EnemyPattern.Snake;

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

    //Function
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

            case PROGRESS.WAVE:  //위 : 9
                                 //아래 : -11
                                 //X : 24
                switch (pattern)
                {
                    case EnemyPattern.Diagolon:
                        
                        break;

                    case EnemyPattern.Snake:
                        //뱀 랜덤위치에 소환
                        if (elapsedTime >= 4)
                        {
                            if (check == false)
                            {
                                //랜덤 Y값 생성
                                float randomY = Random.Range(-11f, 8f);
                                float X = 24f;

                                //랜덤 Y값 저장
                                Vector3 pos = new Vector3(X, randomY, 0f);
                                check = true;

                                //뱀 오프셋
                                Vector3 offset = new Vector3(4f, 0f, 0f);

                                //뱀 생성
                                ObjectPool.Instance.PopFromPool("Snake").GetComponent<Snake>().Init(pos, offset, true);
                                pattern = EnemyPattern.NONE;
                                check = false;
                            }
                            
                        }
                        break;

                    case EnemyPattern.Smart:
                        //똑똑한 녀석 랜덤 위치에 소환
                        break;
                }
             
                break;

            case PROGRESS.BOSS:

                break;

        }
    }



}
