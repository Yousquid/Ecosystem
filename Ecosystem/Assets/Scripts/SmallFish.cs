using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    public GameObject smallFish;
    public GameManager gameManager;
    public float X_randomer;
    public float Y_randomer;
    public float Move_time;
    public bool isMoving = false;
    public bool Randomed = true;
    public Vector3 Destination;
    public bool Moved = false;
    public Vector3 CurrentPosition;
    public float TimeAlive;
    public bool Generated = false;
    public bool SpeedRandomed = false;
    public float Speed;
    public float Life_duration;
    public bool Escaping = false;
    public bool Foraging = false;
    public SpriteRenderer fish_spirte;
    public float hungry_timer = 8f;



    // Start is called before the first frame update
    void Start()
    {
        TimeAlive = 0f;
        this.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeAlive += Time.deltaTime;
        MoveSmallFish();
        TurningFace();
        HungryTimer();
        print(hungry_timer);
        print(Foraging);
    }
    public Vector3 Randoming_Position ()
    {
        if (!Randomed && !Escaping && !Foraging)
        {
            if (this.transform.position.x <= 0f)
            {
                X_randomer = Random.Range(0f, 9f);

                if (this.transform.position.y >= 0f)
                {
                    Y_randomer = Random.Range(0f, -4f);
                }
                else if (this.transform.position.y < 0f)
                {
                    Y_randomer = Random.Range(0f, 4f);
                }

                return new Vector3(X_randomer, Y_randomer, 0);
            }

            if (this.transform.position.x > 0f)
            {
                X_randomer = Random.Range(-8.5f, 0f);

                if (this.transform.position.y >= 0f)
                {
                    Y_randomer = Random.Range(0f, -4f);
                }
                else if (this.transform.position.y < 0f)
                {
                    Y_randomer = Random.Range(0f, 4f);
                }

                return new Vector3(X_randomer, Y_randomer, 0);
            }
        }

        return new Vector3(0, 0, 0);

    }

    public void MoveSmallFish()
    {
       

        if (!isMoving)
        {
            Randomed = false;
        }
        if (!Randomed && !Foraging && !Escaping)
        {
            Destination = Randoming_Position();
            Randomed = true;
            isMoving = true;
        }
        if (!Escaping && Foraging)
        {
            Destination = Foraging_Find();
            Randomed = true;
            isMoving = true;
        }
        if (isMoving)
        {
            

            if (!Foraging && !Escaping)
            {
                if (!SpeedRandomed)
                {
                    Speed = Random.Range(1f, 3f);
                    SpeedRandomed = true;
                    
                }
                transform.position = Vector3.MoveTowards(transform.position, Destination, Speed * Time.deltaTime);
                //this.transform.DOMove(Destination, Random.Range(1.5f, 5f)).SetEase(Ease.InSine);
                //Moved = true;
               
            }

            if (Foraging && !Escaping)
            {
                Speed = 4f;
                transform.position = Vector3.MoveTowards(transform.position, Destination, Speed * Time.deltaTime);
            }


            if (this.transform.position == Destination)
            {
                SpeedRandomed = false;
                isMoving = false;
               
            }
        }




    }

    public void TurningFace()
    {
        if (Destination.x > this.transform.position.x)
        {
            this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
        if (Destination.x < this.transform.position.x)
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public Vector3 Foraging_Find()
    {
        GameObject nearestmicroalgea = FindNearestWithTag("Microalgea");

        return (nearestmicroalgea.transform.position);
    
    }

    GameObject FindNearestWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestObject = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;

        foreach (GameObject obj in objectsWithTag)
        {
            float distanceToObj = Vector3.Distance(currentPosition, obj.transform.position);

            if (distanceToObj < shortestDistance)
            {
                shortestDistance = distanceToObj;
                nearestObject = obj;
            }
        }

        return nearestObject;
    }

    public void HungryTimer()
    {     
        if (hungry_timer > 0f)
        {
            hungry_timer -= Time.deltaTime;
          
        }
        if (hungry_timer <= 0f)
        {
            Foraging = true;
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Microalgea"))
        {
            Destroy(collision);
            hungry_timer = 8f;
            Foraging = false;
        }
    }


}
