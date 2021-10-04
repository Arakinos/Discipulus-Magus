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
    public SpriteRenderer Heart;
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
    public ParticleSystem Poison;
    public ParticleSystem Smoke;
    public ParticleSystem.MainModule MainWaves;
    public ParticleSystem.MainModule MainPoints;

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

    }

    public void SwapColor(int Value)
    {
        MainWaves = Waves.main;
        MainPoints = point.main;
        Stable.gameObject.SetActive(false);
        Magic.gameObject.SetActive(true);

        if (Value == 1)
        {
            Orb.material.color = Color.green;
            MainWaves.startColor = new Color(0, 1, 0, 0.5f);
            Heart.color = hexColor(20, 3, 19, 248);
            Poison.gameObject.SetActive(true);
        }
        else if (Value == 2)
        {
            Orb.material.color = Color.magenta;
            MainWaves.startColor = new Color(1, 0, 1, 0.2f);
            Heart.color = hexColor(20, 3, 19, 248);
            Disruptive.gameObject.SetActive(true);
        }
        else if (Value == 3)
        {
            Orb.material.color = Color.cyan;
            MainWaves.startColor = new Color(0, 1, 1, 0.5f);
            Heart.color = hexColor(20, 3, 19, 248);
            Elec.gameObject.SetActive(true);
        }
        else if (Value == 4)
        {
            Orb.material.color = Color.red;
            MainWaves.startColor = new Color(1, 0, 0, 0.5f);
            Heart.color = hexColor(20, 3, 19, 248);
            Chocs.gameObject.SetActive(true);
        }
        else if (Value == 5)
        {
            Orb.material.color = Color.yellow;
            MainWaves.startColor = new Color(0.8f, 0.8f, 0, 0.5f);
            Heart.color = hexColor(20, 3, 19, 248);
            Stars.gameObject.SetActive(true);
        }
        else if (Value == 6)
        {
            Orb.material.color = Color.blue;
            MainWaves.startColor = new Color(0, 0, 1, 0.5f);
            Heart.color = hexColor(20, 3, 19, 248);
            Smoke.gameObject.SetActive(true);
        }

    }

    public void SetNormal()
    {
        colortype = 0;
        Orb.material.color = Color.white;
        Heart.color = hexColor(255, 255, 255, 248);
        Stable.gameObject.SetActive(true); 
        Elec.gameObject.SetActive(false);
        Magic.gameObject.SetActive(false);
        Disruptive.gameObject.SetActive(false);
        Stars.gameObject.SetActive(false);
        Chocs.gameObject.SetActive(false);
        Poison.gameObject.SetActive(false);
        Smoke.gameObject.SetActive(false);
    }
    public static Vector4 hexColor(float r, float g, float b, float a)
    {
        Vector4 color = new Vector4(r / 255, g / 255, b / 255, a / 255);
        return color;
    }
}
