using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject LevelClearedCanvas;
    GameObject GameOverCanvas;

    private void Start()
    {
        GameOverCanvas = GetCanvasGameObject("GameOverCanvas");
        LevelClearedCanvas = GetCanvasGameObject("LevelClearedCanvas");

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameObject.Find("SoundManager").GetComponent<SoundManagerScript>().PlayMenuAudio2();
            //SoundManagerScript.PlayMenuAudio();
        }
        else
        {
            GameObject.Find("SoundManager").GetComponent<SoundManagerScript>().PlaySoundtrack2();

        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        GameOverCanvas.SetActive(true);
        GameObject.Find("Squirrel").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Bird").GetComponent<PlayerMovement>().enabled = false;
    }

    public void levelCleared()
    {
        GameObject.Find("Squirrel").GetComponent<PlayerMovement>().levelCleared = true;
        LevelClearedCanvas.SetActive(true);
        GameObject.Find("Squirrel").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Bird").GetComponent<PlayerMovement>().enabled = false;
    }

    /*
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }*/

    GameObject GetCanvasGameObject(string name)
    {
        Canvas[] items = Resources.FindObjectsOfTypeAll(typeof(Canvas)) as Canvas[];

        GameObject itemObj = null;

        foreach (var i in items)
        {
            if (i.name == name)
            {
                itemObj = i.gameObject;
                break;
            }
        }

        return itemObj;
    }
}
