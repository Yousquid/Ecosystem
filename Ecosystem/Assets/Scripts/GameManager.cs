using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image Panel;
    public float Brightness = 0f;
    public Color Color_one;
    public bool SunRise;
    public bool SunSet;
    
    void Start()
    {
        Color_one = Panel.color;
        SunSet = true;
        SunRise = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SunRiseDown();
        print(Brightness);
    }
    public void SunRiseDown()
    {
        Panel.color = Color_one;

        Color_one.a = Brightness;

        if (Brightness < 0.8f && SunSet)
        {
            Brightness += 0.013f;
        }
        else if (Brightness >= 0.8f && SunSet)
        {
            SunRise = true;
            SunSet = false;
        }
        if (Brightness > 0f && SunRise)
        {
            Brightness -= 0.013f;
        }
        else if (Brightness <= 0f && SunRise)
        {
            SunRise = false;
            SunSet = true;
        }

        

    }
}
