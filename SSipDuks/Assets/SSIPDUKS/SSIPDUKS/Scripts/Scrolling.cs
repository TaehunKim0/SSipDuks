using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);

    public bool isLinkedToCamera = false;
    public bool isLooping = false;

    private List<Transform> backGroundPart;

    public bool hasGap = false;
    public float minGap = 7.0f;
    public float maxGap = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        if(isLooping)
        {
            backGroundPart = new List<Transform>();
            for(int i =0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if(child.GetComponent<Renderer>() != null)
                {
                    backGroundPart.Add(child);
                }
            }
            backGroundPart = backGroundPart.OrderBy(t => t.position.x).ToList();
        }
    }

    void Scroll(Vector3 movement)
    {
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping)
        {
            Transform firstChild = backGroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                if (firstChild.position.x < Camera.main.transform.position.x)
                {
                    Renderer firstRender = firstChild.GetComponent<Renderer>();
                    if (firstRender.IsVisibleFrom(Camera.main) == false)
                    {
                        Transform lastChild = backGroundPart.LastOrDefault();
                        Vector3 lastPosition = lastChild.transform.position;
                        Renderer lastRender = lastChild.GetComponent<Renderer>();
                        Vector3 lastSize = (lastRender.bounds.max - lastRender.bounds.min);

                        if (hasGap)
                        {  //갭이 있다면
                            var dist = (transform.position - Camera.main.transform.position).z;
                            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

                            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

                            firstChild.position = new Vector3(lastPosition.x + lastSize.x + Random.Range(minGap, maxGap),
                                Random.Range(bottomBorder, topBorder), firstChild.position.z);
                        }
                        else
                        {  //갭이 없다면 바로 뒤에 붙여주고
                            firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
                        }

                        backGroundPart.Remove(firstChild);
                        backGroundPart.Add(firstChild);
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
        {
            Vector3 movement = new Vector3(
                speed.x * direction.x,
                speed.y * direction.y, 
                0
                );

            movement *= Time.deltaTime;
            transform.Translate(movement);

            Scroll(movement);


        }
    }

