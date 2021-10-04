using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public AudioSource menuSon, spellSon;
    public bool click;
    public GameObject gameManagerGO;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        click = false;
        gameManager = gameManagerGO.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBegin()
    {
        if (click == false)
        {
            menuSon.Play();
            StartCoroutine(DelayClickBegin());
        }
    }

    IEnumerator DelayClickBegin()
    {
        click = true;
        yield return new WaitForSeconds(0.5f);
        spellSon.Play();
        yield return new WaitForSeconds(0.5f);
        gameManager.debut = true;
        Destroy(this.gameObject);
    }
}
