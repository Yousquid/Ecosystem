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
    public GameManager gameManager;
    public Transform centerPoint; // The point to rotate around
    public float radius = 2f;
    public float duration = 2f;


    void Start()
    {
        DocircularMove();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        behavior_timer += Time.deltaTime;

        if (behavior_timer >= 5 && gameManager.fish_number_limit >= 5)
        {
            preparation = true;
            behavior_timer = 0;
        }

        if (preparation)
        {
            shark_sprite.sprite = calm;
            rigidbody2D.simulated = false;
            RandomPreparationPlace();
        }
        if (attack)
        {
            shark_sprite.sprite = eating;
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

    public void DocircularMove()
    {
        Vector3 startPosition = centerPoint.position + new Vector3(radius, 0, 0);
        transform.position = startPosition;

        transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart)
                 .OnUpdate(() =>
                 {
                     float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
                     transform.position = centerPoint.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                 });
    }


   

}
