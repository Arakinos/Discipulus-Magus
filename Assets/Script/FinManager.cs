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
    public AudioSource menuSon;
    public bool click;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerGo = GameObject.Find("GameManager");
        gameManagerScript = gameManagerGo.GetComponent<GameManager>();

        click = false;

        float secondes = Mathf.FloorToInt(gameManagerScript.timer % 60);
        float minutes = Mathf.FloorToInt(gameManagerScript.timer / 60);
        string timerString = string.Format("{0:0} : {1:00}", minutes, secondes);
        //resultText.text = "You have channeled your spell \n for " + minutes.ToString() + " : " + secondes.ToString() + " !";
        resultText.text = "You have channeled your spell \n for " + timerString + " !";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickMainMenu()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickMainMenu());
        }
    }

    IEnumerator DelayClickMainMenu()
    {
        click = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}
