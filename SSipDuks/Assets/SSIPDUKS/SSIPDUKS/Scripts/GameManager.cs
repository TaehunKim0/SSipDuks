using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //default
    public Player player;
    public GameObject StartLogo;

    public GameObject MiddleBossPrefab;
    public GameObject LastBossPrefab;

    public Transform VicotryPanel;

    float elapsedTime = 0f;
    float TotalTime = 0f;
    private bool check = false;
    //snake
    int Snakecount = 0;
    int DiagolonCount = 0;

    //enum
    PROGRESS progress = PROGRESS.START;
    EnemyPattern WavePattern = EnemyPattern.Snake;

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
        Middle,
        Last
    }

    //Function
    private void Start()
    {
       
    }

    IEnumerator SpawnSnake(Vector3 spos, Vector3 offset)
    {
        while (Snakecount <= 4)
        {
            yield return new WaitForSeconds(0.4f);

            ObjectPool.Instance.PopFromPool("Snake").GetComponent<Snake>().Init(spos, offset * Snakecount);

            Snakecount++;
        }
    }

    IEnumerator SpawnDiagolon(Vector3 spos, Vector3 offset)
    {
        while (DiagolonCount <= 4)
        {
            yield return new WaitForSeconds(0.4f);

            ObjectPool.Instance.PopFromPool("Diagolon").gameObject.GetComponent<Diagolon>().Init(spos);

            DiagolonCount++;
        }
    }

    void SpawnPattern(EnemyPattern pattern)
    {
        switch (pattern)
        {
            case EnemyPattern.Diagolon:
                {  //6.4
                   //-11
                    int random = Random.Range(0, 2);
                    float randomY;
                    float X = 24f;

                    if (random == 1)
                        randomY = 6.4f;
                    else
                        randomY = -11f;

                    //랜덤 Y값 저장
                    Vector3 pos = new Vector3(X, randomY, 0f);
                    Vector3 offset = new Vector3(4f, 0f, 0f);

                    StartCoroutine(SpawnDiagolon(pos, offset * DiagolonCount));

                    if (DiagolonCount >= 5)
                        DiagolonCount = 0;

                    break;
                }
            case EnemyPattern.Snake:
                {//뱀 랜덤위치에 소환
                 //랜덤 Y값 생성
                    float randomY = Random.Range(-8f, 8f);
                    float X = 24f;

                    //랜덤 Y값 저장
                    Vector3 pos = new Vector3(X, randomY, 0f);
                   

                    //뱀 오프셋
                    Vector3 offset = new Vector3(4f, 0f, 0f);

                    //뱀 생성
                    if (Snakecount >= 5)
                        Snakecount = 0;

                    StartCoroutine(SpawnSnake(/*transform.position*/pos, offset * Snakecount));

                    break;
                }

            case EnemyPattern.Smart:
                //똑똑한 녀석 랜덤 위치에 소환
                {
                    float randomYY = Random.Range(-10f, 8f);
                    float XX = 24f;

                    //랜덤 Y값 저장
                    Vector3 spos = new Vector3(XX, randomYY, 0f);

                    ObjectPool.Instance.PopFromPool("Smart").gameObject.GetComponent<Smart>().Init(spos);
                    break;
                }
        }
    }

    //위 : 9
    //아래 : -11
    //X : 24
    bool Checking = false;

    bool MiddleBossSpawn = false;
    GameObject MiddleBos;

    bool LastBossSpawn = false;
    GameObject LastBoss;

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
                TotalTime += Time.deltaTime;

              
                /*
                    뱀 -> 5번
                    사선 -> 4번 
                    중간보스(총알 5방향)
                    똑똑이 -> 4마리 + 사선 2마리 2번
                    최종보스 -> 여러방향
                 */

                switch (WavePattern)
                {
                    case EnemyPattern.Snake:
                        if (elapsedTime >= 6f)
                        {
                            SpawnPattern(EnemyPattern.Snake);
                            elapsedTime = 0f;
                        }

                        if (TotalTime >= 30f)
                        {
                            WavePattern = EnemyPattern.Diagolon;
                            elapsedTime = 0f;
                        }

                        break;

                    case EnemyPattern.Diagolon:
                        if (elapsedTime >= 2.5f)
                        {
                            SpawnPattern(EnemyPattern.Diagolon);
                            elapsedTime = 0f;
                        }

                        if(TotalTime >= 50f)
                        {
                            WavePattern = EnemyPattern.Smart;
                            elapsedTime = 0f;
                        }
                        break;

                    case EnemyPattern.Smart:
                        if (elapsedTime >= 2.5f)
                        {
                            if(Checking == false)
                                SpawnPattern(EnemyPattern.Smart);

                            Checking = true;
                        }

                        if(elapsedTime >= 3f)
                        {
                            SpawnPattern(EnemyPattern.Diagolon);
                            Checking = false;
                            elapsedTime = 0f;
                        }
                        if (TotalTime >= 65f)
                        {
                            WavePattern = EnemyPattern.Middle;
                        }

                        break;

                    case EnemyPattern.Middle:

                            if (MiddleBossSpawn == false)
                            {
                                MiddleBos = Instantiate(MiddleBossPrefab);
                                MiddleBos.GetComponent<Naco>().Init(new Vector3(24f, -5f, 0f));
                                MiddleBossSpawn = true;

                            }

                            if (MiddleBos == null)
                            {
                                WavePattern = EnemyPattern.Last;
                                elapsedTime = 0f;
                            }
                        
                        break;

                    case EnemyPattern.Last:
                        if (LastBossSpawn == false)
                        {
                            LastBoss = Instantiate(LastBossPrefab);
                            LastBoss.GetComponent<Hitomi>().Init(new Vector3(16f, -27f, 0f));
                            LastBossSpawn = true;
                        }

                        if (LastBoss == null)
                        {
                            if (VicotryPanel != null)
                            {
                                VicotryPanel.GetComponent<CanvasGroup>().alpha = 1;
                                VicotryPanel.GetComponent<CanvasGroup>().interactable = true;
                                GameObject g = GameObject.Find("UI");
                                g.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                                player.gameObject.SetActive(false);
                            }
                        }

                        break;
                }
                
                
                break;

            case PROGRESS.BOSS:

                break;

        }
    }



}
