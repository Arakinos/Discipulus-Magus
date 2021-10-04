using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject gameManagerGo;
    public GameManager gameManagerScript;
    public AudioSource menuSon;
    public bool click;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerGo = GameObject.Find("GameManager");
        gameManagerScript = gameManagerGo.GetComponent<GameManager>();
        click = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickContinue()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickContinue());
        }     
    }

    public void OnClickMainMenu()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickMainMenu());
        }
    }

    IEnumerator DelayClickContinue()
    {
        click = true;
        yield return new WaitForSeconds(1);
        gameManagerScript.enPause = false;
        Destroy(this.gameObject);
    }
    IEnumerator DelayClickMainMenu()
    {
        click = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}
