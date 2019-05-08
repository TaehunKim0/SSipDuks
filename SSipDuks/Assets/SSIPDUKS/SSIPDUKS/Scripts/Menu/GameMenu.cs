using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        Screen.SetResolution(1920, 1080 , true);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("SGame");

    }
    public void GotoTitle()
    {
        SceneManager.LoadScene("SMenu");
        ScoreSystem.score = 0;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void DefaultPlayButton()
    {
        GameObject temp = GameObject.Find("Button");
        temp.gameObject.GetComponent<Image>().sprite = Resources.Load("play_0", typeof(Sprite)) as Sprite;
    }

    public void ChangeHoverPlayButton()
    {
        GameObject temp = GameObject.Find("Button");
        temp.gameObject.GetComponent<Image>().sprite = Resources.Load("play_1", typeof(Sprite)) as Sprite;
    }

    public void DefaultEndButton()
    {
        GameObject temp = GameObject.Find("Button2");
        temp.gameObject.GetComponent<Image>().sprite = Resources.Load("exit_0", typeof(Sprite)) as Sprite;
    }

    public void ChangeHoverEndButton()
    {
        GameObject temp = GameObject.Find("Button2");
        temp.gameObject.GetComponent<Image>().sprite = Resources.Load("exit_1", typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
