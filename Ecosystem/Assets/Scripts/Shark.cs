using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shark : MonoBehaviour
{
    public bool enter;
    public bool preparation = false;
    public bool attack = false;
    public bool randomed = false;
    public float behavior_timer = 0;
    public Rigidbody2D rigidbody2D;
    public bool attackAnticipation = false;
    public Vector3 Destination;
    public int left_or_right;
    public float y_position;
    public SpriteRenderer shark_sprite;
    public Sprite calm;
    public Sprite eating;



    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        behavior_timer += Time.deltaTime;

        if (behavior_timer >= 5)
        {
            preparation = true;
            behavior_timer = 0;
        }

        if (preparation)
        {
            rigidbody2D.simulated = false;
            RandomPreparationPlace();
        }
        if (attack)
        {
            rigidbody2D.simulated = true;
            Attacking();
        }
        
    }
   

    public void RandomPreparationPlace()
    {
         left_or_right = Random.Range (0,2);
         y_position = Random.Range(-4f, 3.36f);
        if (left_or_right == 0)
        {
            StartCoroutine(AttackPreparationLeft());
            preparation = false;
        }
        if (left_or_right == 1)
        {
            StartCoroutine(AttackPreparationRight());
            preparation = false;
        }

    }

    IEnumerator AttackPreparationLeft()
    {
       
        this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        this.transform.position = new Vector3(-9.57f, y_position, 0);
        Destination = new Vector3(-7.74f, y_position, 0f);
        this.transform.DOMove(new Vector3(-7.74f, y_position, 0f), 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        this.transform.DOMove(new Vector3(-8.5f, y_position, 0f), 0.5f).SetEase(Ease.InSine);
        attack = true;
        attackAnticipation = false;
        preparation = false;
        StopAllCoroutines();
    }

    IEnumerator AttackPreparationRight()
    {
        
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        this.transform.position = new Vector3(9.57f, y_position, 0);
        Destination = new Vector3(-7.74f, y_position, 0f);
        this.transform.DOMove(new Vector3(7.59f, y_position, 0f), 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        this.transform.DOMove(new Vector3(8.5f, y_position, 0f), 0.5f).SetEase(Ease.InSine);
        attack = true;
        attackAnticipation = false;
        preparation = false;
        StopAllCoroutines();
    }

    public void Attacking()
    {
        if (left_or_right == 0)
        {
            this.transform.DOMove(new Vector3(12f, y_position, 0), 2.5f).SetEase(Ease.InQuart);
            attack = false;
        }
        if (left_or_right == 1)
        {
            this.transform.DOMove(new Vector3(-12f, y_position, 0), 2.5f).SetEase(Ease.InQuart);
            attack = false;
        }

    }

    IEnumerator WaitforAttackingSeconds()
    {
        yield return new WaitForSeconds(1f);

    }

   

}
