using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timer;
    public int numInstability;  // 0 = Pas d'instabilité
    public GameObject jaugeGO;
    public Jauge jaugeScript;
    public int tempsApparitionInstability;
    public bool enPause;

    // Start is called before the first frame update
    void Start()
    {
        jaugeScript = jaugeGO.GetComponent<Jauge>();
        timer = 0;
        numInstability = 0;
        tempsApparitionInstability = 0;
        enPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (numInstability != 0)
        {
            if (enPause == false)
            {
                jaugeScript.stabilityGauge -= 1;
                jaugeScript.UpdateHealth();
            }
        }
        else
        {
            if (tempsApparitionInstability < 1000)
            {
                if(enPause == false)
                    tempsApparitionInstability += 1;
            }
            else
            {
                ApparitionInstability();
                tempsApparitionInstability = 0;
            }
        }

        if (jaugeScript.stabilityGauge == 0)
        {
            // Quand FIN du jeu 
        }
    }

    void ApparitionInstability()
    {
        numInstability = Random.Range(1, 7);
    }
}
