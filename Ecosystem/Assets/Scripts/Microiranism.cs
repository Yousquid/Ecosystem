using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FMODUnity;

public class Microiranism : MonoBehaviour
{
    public GameObject Microalgea;
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
    private GameObject[] objectsWithTag;
    public int count;
    public FMODUnity.EventReference buble;
   
    void Start()
    {
        TimeAlive = 0f;
        this.transform.localScale = new Vector3(0.016f, 0.016f, 0.016f);
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (gameManager.Brightness <= 0.4f)
        {
            TimeAlive += Time.deltaTime*2;
        }
        if (gameManager.Brightness > 0.4f)
        {
            TimeAlive += Time.deltaTime/2 ;
        }

       


        MoveMicroalgea();
        GenerateMicroalgea();
        GrowUpScale();
        


    }

    

    public void GenerateMicroalgea()
    {
        if ( TimeAlive > Life_duration && !Generated)
        {
            if (gameManager.count <= 80)
            {
                FMODUnity.RuntimeManager.PlayOneShot(buble);
                Instantiate(Microalgea, this.transform.position + new Vector3(-0.1f, 0, 0), Quaternion.identity);
                Instantiate(Microalgea, this.transform.position + new Vector3(0.1f, 0, 0), Quaternion.identity);
                Destroy(gameObject);
            }
            if (gameManager.count > 80)
            {
                FMODUnity.RuntimeManager.PlayOneShot(buble);
                Destroy(gameObject);
            }
           
           
            Generated = true;
        }
    }
    //x:-8.5 to 8.5; y_day:3- -1.8 , y_night: -1.8 - -4.4 
    public void MoveMicroalgea()
    {

        
            if (!isMoving)
            {
                Randomed = false;
            }
            if (!Randomed)
            {
                Destination = Randoming();
                Life_duration = Random.Range(8f, 11f);
                Randomed = true;
                isMoving = true;
            }
           
            if (isMoving)
            {
            //if (this.transform.position.x < Destination.x)
            //{
            //    this.transform.position += new Vector3( Random.Range(0.01f,0.05f), 0, 0);
            //}
            //if (this.transform.position.x > Destination.x)
            //{
            //    this.transform.position += new Vector3(Random.Range(-0.01f, -0.05f), 0, 0);
            //}
            //if (this.transform.position.y < Destination.y)
            //{
            //    this.transform.position += new Vector3(0, Random.Range(0.01f, 0.05f), 0);
            //}
            //if (this.transform.position.y > Destination.y)
            //{
            //    this.transform.position += new Vector3(0, Random.Range(-0.01f, -0.05f), 0);
            //}

                if (gameManager.Brightness <= 0.4f && gameObject!= null)
                {
                if (!SpeedRandomed)
                {
                    Speed = Random.Range(0.5f, 3f);
                    SpeedRandomed = true;
                }
                    transform.position = Vector3.MoveTowards(transform.position, Destination, Speed * Time.deltaTime);
                    //this.transform.DOMove(Destination, Random.Range(1.5f, 5f)).SetEase(Ease.InSine);
                    //Moved = true;

                }
                if (gameManager.Brightness > 0.4f && gameObject != null)
                {
                if (!SpeedRandomed)
                {
                    Speed = Random.Range(0.2f, 2f);
                    SpeedRandomed = true;
                }
                transform.position = Vector3.MoveTowards(transform.position, Destination, Speed * Time.deltaTime);
                    //this.transform.DOMove(Destination, Random.Range(3f, 6f)).SetEase(Ease.InSine);
                    //Moved = true;
                }
            

            if (this.transform.position == Destination)
            {
                Moved = false;
                SpeedRandomed = false;
                isMoving = false;
            }
            }
        

       

    }

    public Vector3 Randoming()
    {
        if (!Randomed)
        {
            if (gameManager.Brightness <= 0.4f)
            {
                X_randomer = Random.Range(-8.5f, 8.5f);

                Y_randomer = Random.Range(-1.8f, 3f);

                return new Vector3(X_randomer, Y_randomer, 0);

               
            }

            if (gameManager.Brightness > 0.4f)
            {
                X_randomer = Random.Range(-8.5f, 8.5f);

                Y_randomer = Random.Range(-4.4f, -1.8f);

                return new Vector3(X_randomer, Y_randomer, 0);
               
            }

           

        }

        return new Vector3(0, 0, 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Small_Fish"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Mature_Fish"))
        {
            Destroy(gameObject);
        }
    }

    public void GrowUpScale()
    {
        this.transform.localScale = new Vector3(0.001f * TimeAlive + 0.016f, 0.001f * TimeAlive + 0.016f, 0.001f * TimeAlive + 0.016f);
    }

}
