using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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
    public Sprite mature_sprite;
    public float hungry_timer = 8f;
    public float food_grow = 0f;
    public bool Mating = false;
    public Rigidbody2D rigidbody2D;
    public bool Death = false;
    public float Mate_Timer = 0;
    public GameObject heart;
    public bool generate_heart = false;
    public GameObject offspring;
    public bool offspring_generated = false;
    public Sprite original_sprite;
    public bool has_mate = false;
    public BoxCollider2D collider2D;
    public FMODUnity.EventReference eat;
    public FMODUnity.EventReference love;
    public FMODUnity.EventReference being_eaten;
    // Start is called before the first frame update
    void Start()
    {
        TimeAlive = 0f;
        this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        hungry_timer = Random.Range(6f, 12f);
        gameManager = FindObjectOfType<GameManager>();
        isMoving = false;
        Randomed = true;
        Moved = false;
        Generated = false;
        SpeedRandomed = false;
        Escaping = false;
        Foraging = false;
        food_grow = 0f;
        Mating = false;
        Death = false;
        Mate_Timer = 0;
        generate_heart = false;
        offspring_generated = false;
        has_mate = false;
        fish_spirte.sprite = original_sprite;
        this.tag = "Small_Fish";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.Shark_Dash)
        {
            rigidbody2D.simulated = true;
            collider2D.isTrigger = false;
        }
        

        TimeAlive += Time.deltaTime;
        MoveSmallFish();
        TurningFace();
        HungryTimer();
        GrowUpScale();
        MateAndDeath();
        Mating_Animation();
        Death_Land();
        if (Mate_Timer >= 2.3f)
        {
            Destroy(gameObject);
        }
        if (has_mate)
        {
            Mate_Timer += Time.deltaTime;
        }
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
        if (!Randomed && !Foraging && !Escaping && !Mate_Number_Detect())
        {
            Destination = Randoming_Position();
            Randomed = true;
            isMoving = true;
        }
        if (!Escaping && Foraging && !Mate_Number_Detect())
        {
            Destination = Foraging_Find();
            Randomed = true;
            isMoving = true;
        }
        if (Mate_Number_Detect() && !Death && Mating)
        {
            
            Destination = FindNearestMate("Mature_Fish").transform.position;
            
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

    public bool Mate_Number_Detect()
    {
        if (Mating)
        {
            if (FindNearestMate("Mature_Fish") != null)
            {
                return true;
            }
            else
            { return false; }
        }
        else return false;
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

    public Vector3 Mate_Find()
    {
        GameObject nearestmate = FindNearestMate("Mature_Fish");

        return (nearestmate.transform.position);

    }

   

    GameObject FindNearestMate(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestObject = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;

        foreach (GameObject obj in objectsWithTag)
        {
            if (obj == this.gameObject)
            {
                continue;
            }

            SmallFish fish = obj.GetComponent<SmallFish>();

            if (fish.Mating && !fish.has_mate)
            {
               
                float distanceToObj = Vector3.Distance(currentPosition, obj.transform.position);

                if (distanceToObj < shortestDistance)
                {
                    shortestDistance = distanceToObj;
                    nearestObject = obj;
                }

            }

        }

        return nearestObject;
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
        if (hungry_timer > 0f && !gameManager.Shark_Dash)
        {
            rigidbody2D.simulated = false;
            collider2D.isTrigger = true;
            hungry_timer -= Time.deltaTime;
          
        }
        if (hungry_timer <= 0f)
        {
            Foraging = true;
            collider2D.isTrigger = false;
            rigidbody2D.simulated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Microalgea"))
        {
            if (!Mating)
            {
                FMODUnity.RuntimeManager.PlayOneShot(eat);
                hungry_timer = Random.Range(6f, 12f);
                food_grow += 0.01f;
                Foraging = false;
            }
            
        }

        if (collision.gameObject.CompareTag("Shark") && this.transform.localScale.x >= 0.13)
        {
            FMODUnity.RuntimeManager.PlayOneShot(being_eaten);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shark") && this.transform.localScale.x >= 0.13)
        {
            FMODUnity.RuntimeManager.PlayOneShot(being_eaten);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Mating && collision.gameObject.CompareTag("Mature_Fish"))
        {
             has_mate = true;
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      /*  if (Mating && collision.gameObject.CompareTag("Mature_Fish"))
        {
            Mate_Timer = 0;
        } */
    }
    public void GrowUpScale()
    {
        this.transform.localScale = new Vector3(0.0008f * TimeAlive + 0.05f + food_grow, 0.0008f * TimeAlive + 0.05f + food_grow, 0.0008f * TimeAlive + 0.05f + food_grow);
    }

    public void MateAndDeath()
    {
        

        if (this.transform.localScale.x >= 0.16f)
        {
            
            fish_spirte.sprite = mature_sprite;

            this.gameObject.tag = "Mature_Fish";

            collider2D.isTrigger = false;

            rigidbody2D.simulated = true;

            Mating = true;

           
        }
    }

    IEnumerator GenerateHeartsAtIntervals()
    {
        yield return new WaitForSeconds(0.5f);
        FMODUnity.RuntimeManager.PlayOneShot(love);
        Instantiate(heart, this.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        FMODUnity.RuntimeManager.PlayOneShot(love);
        Instantiate(heart, this.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        FMODUnity.RuntimeManager.PlayOneShot(love);
        Instantiate(heart, this.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
    }
    public void Mating_Animation()
    {
        if (Mate_Timer > 0f && Mate_Timer < 2f && !generate_heart)
        {
            StartCoroutine(GenerateHeartEveryHalfSecond());
            generate_heart = true;
        }

        if (Mate_Timer >= 1.7f && !offspring_generated)
        {

            StopAllCoroutines();
            if (gameManager.fish_number_limit < 30)
            {
                GameObject newobject_one = Instantiate(offspring, this.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.5f, -0.3f), 0), Quaternion.identity);
                GameObject newobject_two = Instantiate(offspring, this.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.5f, -0.3f), 0), Quaternion.identity);
                newobject_one.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                newobject_two.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
           
           
            Death = true;
            offspring_generated = true;
        }

        
    }

    IEnumerator GenerateHeartEveryHalfSecond()
    {
        while (true) 
        {
            Instantiate(heart, this.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Death_Land()
    {
        if (Death)
        {
            this.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
            this.transform.position += new Vector3(0, -0.05f, 0f);
        }
    }

    public void OnDestroy()
    {
        gameManager.fish_number_list.Remove(this.gameObject);
    }


}
