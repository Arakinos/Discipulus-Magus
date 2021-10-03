using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinManager : MonoBehaviour
{
    public GameObject gameManagerGo;
    public GameManager gameManagerScript;
    public float minutes, secondes;
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerGo = GameObject.Find("GameManager");
        gameManagerScript = gameManagerGo.GetComponent<GameManager>();

        float secondes = Mathf.FloorToInt(gameManagerScript.timer % 60);
        float minutes = Mathf.FloorToInt(gameManagerScript.timer / 60);
        resultText.text = "You have channeled your spell \n for " + minutes.ToString() + " : " + secondes.ToString() + " long !";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
