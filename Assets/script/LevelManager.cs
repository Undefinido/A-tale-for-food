using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.sceneCount - 1 == SceneManager.GetActiveScene().buildIndex)
        {
            transform.Find("LevelClearedText").GetComponent<UnityEngine.UI.Text>().text = "You beated the game!";
            transform.Find("NextLevelButton").GetComponent<UnityEngine.UI.Button>().gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void nextLevel()
    {
        if(SceneManager.sceneCount -1 != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
