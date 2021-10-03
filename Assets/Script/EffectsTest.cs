using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using UnityEngine.Animations;

public class EffectsTest : MonoBehaviour
{

    public Renderer Orb;
    int colortype = 0;
    public Animation Idle;
    // Start is called before the first frame update
    void Start()
    {
        Orb = GetComponent<Renderer>();
        Idle = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Colortype
        if (colortype == 0)
        {
            Orb.material.color = Color.white;
            //Orb.material.color = new Color(255, 0, 0);
        }
        else if (colortype == 1)
        {
            Orb.material.color = Color.green;
        }
        else if (colortype == 2)
        {
            Orb.material.color = Color.magenta;
        }
        else if (colortype == 3)
        {
            Orb.material.color = Color.cyan;
        }
        else if (colortype == 4)
        {
            Orb.material.color = Color.red;
        }
        else if (colortype == 5)
        {
            Orb.material.color = Color.yellow;
        }
        else if (colortype == 6)
        {
            Orb.material.color = Color.blue;
        }
        #endregion

        #region Mouvement
       
        #endregion
    }

    public void SwapColor()
    {
        if (colortype!= 6)
        {
            colortype += 1;
        }
        else
        {
            colortype = 0;
        }
    }
}
