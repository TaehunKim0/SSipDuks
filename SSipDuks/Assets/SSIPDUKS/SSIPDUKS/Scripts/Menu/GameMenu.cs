using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
