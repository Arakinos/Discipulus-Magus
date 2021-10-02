using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject gameManagerGo;
    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerGo = GameObject.Find("GameManager");
        gameManagerScript = gameManagerGo.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickContinue()
    {
        gameManagerScript.enPause = false;
        Destroy(this.gameObject);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
