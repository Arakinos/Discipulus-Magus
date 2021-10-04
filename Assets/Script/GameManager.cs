using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timer, minutes, secondes;
    public Text timerText;
    public int numInstability;  // 0 = Pas d'instabilité
    public GameObject jaugeGO;
    public GameObject mainGaucheGO, mainDroiteGO;
    Animator mainGaucheAnimator, mainDroiteAnimator;
    public Jauge jaugeScript;
    public int tempsApparitionInstability;
    public bool enPause;
    public AudioSource sortSon, musique;
    public bool sortSonOn;

    public GameObject sortGO;
    public SpriteRenderer couleurSort;

    GameObject menuFin;
    public bool fin, debut;

    // Start is called before the first frame update
    void Start()
    {
        mainGaucheAnimator = mainGaucheGO.GetComponent<Animator>();
        mainDroiteAnimator = mainDroiteGO.GetComponent<Animator>();

        sortSonOn = true;
        jaugeScript = jaugeGO.GetComponent<Jauge>();
        timer = 0;
        numInstability = 0;
        tempsApparitionInstability = 0;
        enPause = false;
        couleurSort = sortGO.GetComponent<SpriteRenderer>();
        fin = false;
        debut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (debut == true)
        {



            if (enPause == true || fin == true)
            {
                sortSon.Stop();
                sortSonOn = false;
            }
            else
            {
                if (sortSonOn == false)
                {
                    sortSon.Play();
                    sortSonOn = true;
                }
            }

            #region Timer

            float secondes = Mathf.FloorToInt(timer % 60);
            float minutes = Mathf.FloorToInt(timer / 60);

            if (jaugeScript.stabilityGauge != 0 && !enPause)
                timer += Time.deltaTime;

            string timerString = string.Format("{0:0} : {1:00}", minutes, secondes);
            timerText.text = timerString;
            //timerText.text = minutes.ToString() + " : " + secondes.ToString();

            #endregion

            if (numInstability != 0)
            {
                if (enPause == false)
                {
                    if (minutes == 1)
                    {
                        jaugeScript.stabilityGauge -= 4;
                        jaugeScript.UpdateHealth();
                    }
                    else if (minutes >= 2)
                    {
                        jaugeScript.stabilityGauge -= 6;
                        jaugeScript.UpdateHealth();
                    }                    
                    else
                    {
                        jaugeScript.stabilityGauge -= 2;
                        jaugeScript.UpdateHealth();
                    }
                }
            }
            else
            {
                if (tempsApparitionInstability < 500)
                {
                    if (enPause == false)
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
            {
                //couleurSort.color = new Color(255, 255, 255, 255);
                sortSon.volume = 0.10f;
                musique.volume = 0.50f;
            }
            if (numInstability == 1)
            {
                // couleurSort.color = new Color(255, 0, 0, 255);
                sortSon.volume = 0.20f;
                musique.volume = 0.25f;
            }
            if (numInstability == 2)
            {
                // couleurSort.color = new Color(0, 0, 255, 255);
                sortSon.volume = 0.20f;
                musique.volume = 0.25f;
            }
            if (numInstability == 3)
            {
                // couleurSort.color = new Color(0, 255, 0, 255);
                sortSon.volume = 0.20f;
                musique.volume = 0.25f;
            }
            if (numInstability == 4)
            {
                //couleurSort.color = new Color(255, 255, 0, 255);
                sortSon.volume = 0.20f;
                musique.volume = 0.25f;
            }
            if (numInstability == 5)
            {
                //couleurSort.color = new Color(155, 0, 255, 255);
                sortSon.volume = 0.20f;
                musique.volume = 0.25f;
            }
            if (numInstability == 6)
            {
                // couleurSort.color = new Color(0, 0, 0, 255);
                sortSon.volume = 0.20f;
                musique.volume = 0.25f;
            }

            #endregion

            if (jaugeScript.stabilityGauge <= 6000)
            {
                mainGaucheAnimator.SetBool("Tremblement", true);
                mainDroiteAnimator.SetBool("Tremblement", true);
            }

            if (jaugeScript.stabilityGauge == 0)
            {
                // Quand FIN du jeu 
                if (fin == false)
                {

                    menuFin = Instantiate(Resources.Load("Prefab/Menu_Fin")) as GameObject;
                    menuFin.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    numInstability = 0;
                    fin = true;
                }
            }
        }
    }

    void ApparitionInstability()
    {
        numInstability = Random.Range(1, 7);
        if (numInstability != 0)
        {
            couleurSort.gameObject.GetComponent<EffectsTest>().SwapColor(numInstability);
        }
        GameObject.Find("GameManager").GetComponent<DataManager>().ChangeCurrentPatern(numInstability.ToString());
    }
}
