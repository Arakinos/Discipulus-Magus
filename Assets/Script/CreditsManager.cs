using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public AudioSource menuSon;
    public bool click;

    // Start is called before the first frame update
    void Start()
    {
        click = false;
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
        Destroy(this.gameObject);
    }
}
