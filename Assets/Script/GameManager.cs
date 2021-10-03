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

    public GameObject sortGO;
    public SpriteRenderer couleurSort;

    // Start is called before the first frame update
    void Start()
    {
        jaugeScript = jaugeGO.GetComponent<Jauge>();
        timer = 0;
        numInstability = 0;
        tempsApparitionInstability = 0;
        enPause = false;
        couleurSort = sortGO.GetComponent<SpriteRenderer>();
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

        #region Instabilitées du sort

        if (numInstability == 0)
            couleurSort.color = new Color(255, 255, 255, 255);
        if (numInstability == 1)
            couleurSort.color = new Color(255, 0, 0, 255);
        if (numInstability == 2)
            couleurSort.color = new Color(0, 0, 255, 255);
        if (numInstability == 3)
            couleurSort.color = new Color(0, 255, 0, 255);
        if (numInstability == 4)
            couleurSort.color = new Color(255, 255, 0, 255);
        if (numInstability == 5)
            couleurSort.color = new Color(155, 0, 255, 255);
        if (numInstability == 6)
            couleurSort.color = new Color(0, 0, 0, 255);

        #endregion

        if (jaugeScript.stabilityGauge == 0)
        {
            // Quand FIN du jeu 
        }
    }

    void ApparitionInstability()
    {
        numInstability = Random.Range(1, 4);
        GameObject.Find("GameManager").GetComponent<DataManager>().ChangeCurrentPatern(numInstability.ToString());
    }
}
