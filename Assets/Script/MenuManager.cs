using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject menuPause, menuCredits;
    public GameObject gameManagerGo;
    public GameManager gameManagerScript;
    public AudioSource menuSon;
    public bool click;

    // Start is called before the first frame update
    void Start()
    {
        click = false;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
       
        if (sceneName == "Jeu")
        {
            gameManagerScript = gameManagerGo.GetComponent<GameManager>();
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickPlay());
        }       
    }

    public void OnClickCredits()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickPlay());
        }
    }

    public void OnClickQuit()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickQuit());
        }
    }

    public void OnClickOption()
    {
        menuSon.Play();
        gameManagerScript.enPause = true;
        menuPause = Instantiate(Resources.Load("Prefab/Menu_Pause")) as GameObject;
        menuPause.transform.SetParent(GameObject.Find("Canvas").transform, false);       
    }

    IEnumerator DelayClickPlay()
    {
        click = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Jeu");
    }
    IEnumerator DelayClickCredits()
    {
        click = true;
        yield return new WaitForSeconds(1);
        menuCredits = Instantiate(Resources.Load("Prefab/Menu_Pause")) as GameObject;
        menuCredits.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }
    IEnumerator DelayClickQuit()
    {
        click = true;
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
