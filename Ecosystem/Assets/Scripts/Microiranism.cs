using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    void Start()
    {
        TimeAlive = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeAlive += Time.deltaTime;
        if (this!= null)
        MoveMicroalgea();
        GenerateMicroalgea();
    }

    public void GenerateMicroalgea()
    {
        if (gameManager.Brightness <= 0.3f && TimeAlive > 10f && !Generated)
        {
            Instantiate(Microalgea, this.transform.position + new Vector3 (-0.1f,0,0), Quaternion.identity);
            Instantiate(Microalgea, this.transform.position + new Vector3(0.1f, 0, 0), Quaternion.identity);
           
            Destroy(gameObject);
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

            if (!Moved)
            {
                if (gameManager.Brightness <= 0.4f && this!= null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Destination, Random.Range(0.01f, 0.05f) * Random.Range(1.5f, 5f));
                    //this.transform.DOMove(Destination, Random.Range(1.5f, 5f)).SetEase(Ease.InSine);
                    
                }
                if (gameManager.Brightness > 0.4f && this != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Destination, Random.Range(0.01f, 0.03f) * Random.Range(1.5f, 5f));
                    //this.transform.DOMove(Destination, Random.Range(3f, 6f)).SetEase(Ease.InSine);
                   
                }
            }

            if (this.transform.position == Destination)
            {
                Moved = false;
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

}
