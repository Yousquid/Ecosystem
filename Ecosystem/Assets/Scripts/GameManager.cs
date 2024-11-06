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
    public int fish_number_limit;
    public List<GameObject> fish_number_list;
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
        Maturefish();
        mature_fish_count = maturedFish.Count;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Microalgea");
        count = objectsWithTag.Length;
        CountFish();
        fish_number_limit = fish_number_list.Count;
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

    public void Maturefish()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Mature_Fish");

        foreach (GameObject obj in objectsWithTag)
        {
            SmallFish fish = obj.GetComponent<SmallFish>();

            if (fish.Mating)
            {
                if (!maturedFish.Contains(obj))
                { maturedFish.Add(obj); }

            }

            if (obj == null)
            {
                maturedFish.Remove(obj);
            }
        }
    }

    public void CountFish()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Small_Fish");

        foreach (GameObject obj in objectsWithTag)
        {
            if (!fish_number_list.Contains(obj))
            {
                fish_number_list.Add(obj);
            }
        }
    }
}
