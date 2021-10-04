using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using UnityEngine.Animations;
using UnityEngine.ParticleSystemJobs;

public class EffectsTest : MonoBehaviour
{

    public Renderer Orb;
    int colortype = 0;
    public Animation Idle;

    public ParticleSystem Elec;
    public ParticleSystem Magic;
    public ParticleSystem Waves;
    public ParticleSystem point;
    public ParticleSystem Stable;
    public ParticleSystem Disruptive;
    public ParticleSystem Stars;
    public ParticleSystem Chocs;

    // Start is called before the first frame update
    void Start()
    {
        Orb = GetComponent<Renderer>();
        Idle = gameObject.GetComponent<Animation>();

        foreach(ChildrenName name in gameObject.GetComponentsInChildren<ChildrenName>())
        {
            switch (name.name)
            {
                case "Waves":
                    {
                        Waves = name.gameObject.GetComponent<ParticleSystem>();
                        break;
                    }

                case "Points":
                    {
                        point = name.gameObject.GetComponent<ParticleSystem>();
                        break;
                    }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        var MainWawes = Waves.main;
        var MainPoints = point.main;
        #region Colortype
        if (colortype == 0)
        {
            Orb.material.color = Color.white;
            //Orb.material.color = new Color(255, 0, 0);
        }
        else if (colortype == 1)
        {
            Orb.material.color = Color.green;
            MainWawes.startColor = new Color(0, 1, 0,0.5f);
            
        }
        else if (colortype == 2)
        {
            Orb.material.color = Color.magenta;
            MainWawes.startColor = new Color(1, 0, 1, 0.2f);
        }
        else if (colortype == 3)
        {
            Orb.material.color = Color.cyan;
            MainWawes.startColor = new Color(0, 1, 1, 0.5f);
        }
        else if (colortype == 4)
        {
            Orb.material.color = Color.red;
            MainWawes.startColor = new Color(1, 0, 0, 0.5f);
        }
        else if (colortype == 5)
        {
            Orb.material.color = Color.yellow;
            MainWawes.startColor = new Color(0.8f, 0.8f, 0, 0.5f) ;
        }
        else if (colortype == 6)
        {
            Orb.material.color = Color.blue;
            MainWawes.startColor = new Color(0, 0, 1, 0.5f);
        }
        #endregion

    }

    public void SwapColor()
    {

        colortype += Random.Range(1, 7);
        Stable.gameObject.SetActive(false);
        Magic.gameObject.SetActive(true);
        if (colortype == 1)
        {


        }
        else if (colortype == 2)
        {
            Disruptive.gameObject.SetActive(true);
        }
        else if (colortype == 3)
        {
            Elec.gameObject.SetActive(true);
        }
        else if (colortype == 4)
        {
            Chocs.gameObject.SetActive(true);
        }
        else if (colortype == 5)
        {
            Stars.gameObject.SetActive(true);
        }
        else if (colortype == 6)
        {

        }

    }

    public void SetNormal()
    {
        colortype = 0;
        Stable.gameObject.SetActive(true); 
        Elec.gameObject.SetActive(false);
        Magic.gameObject.SetActive(false);
        Disruptive.gameObject.SetActive(false);
        Stars.gameObject.SetActive(false);
        Chocs.gameObject.SetActive(false);
    }
}
