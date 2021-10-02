using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject menuPause;
    public GameObject gameManagerGo;
    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Jeu")
            gameManagerScript = gameManagerGo.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Jeu");
    }

    public void OnClickQuit()
    {
       Application.Quit();
    }

    public void OnClickOption()
    {
        gameManagerScript.enPause = true;
        menuPause = Instantiate(Resources.Load("Prefab/Menu_Pause")) as GameObject;
        menuPause.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }
}
