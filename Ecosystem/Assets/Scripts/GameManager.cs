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
    public int count;
    public List<GameObject> maturedFish;
    public int mature_fish_count;

    void Start()
    {
        
        Brightness = 0f;
        Color_one = Panel.color;
        SunSet = true;
        SunRise = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SunRiseDown();

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Microalgea");
        int count = objectsWithTag.Length;
        mature_fish_count = maturedFish.Count;
        Mature_Fish_Count();
    }
    public void SunRiseDown()
    {
        Panel.color = Color_one;

        Color_one.a = Brightness;

        if (Brightness < 0.8f && SunSet)
        {
            Brightness += 0.00043f;
        }
        else if (Brightness >= 0.8f && SunSet)
        {
            SunRise = true;
            SunSet = false;
        }
        if (Brightness > 0f && SunRise)
        {
            Brightness -= 0.00043f;
        }
        else if (Brightness <= 0f && SunRise)
        {
            SunRise = false;
            SunSet = true;
        }

        

    }

    public void Mature_Fish_Count()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Small_Fish");


        foreach (GameObject obj in objectsWithTag)
        {
            SmallFish fish = obj.GetComponent<SmallFish>();

            if (fish.Mating)
            {
                maturedFish.Add(obj);
            }

        }
    }

}
