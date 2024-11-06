using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FMODUnity;
public class Shark : MonoBehaviour
{
    public bool enter;
    public bool preparation = false;
    public bool attack = false;
    public bool isEating = false;
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
    public Transform centerPoint; 
    public float radius = 1.5f;
    public float duration = 2f;
    public bool playing = false;
    public float playing_timer = 0;
    public int play_randomer;
    public bool play_randomed = false;
    public float yOffset;
    public float wave_timer = 0;
    public bool yOffset_randomed = false;
    public FMODUnity.EventReference dash;
    public FMODUnity.EventReference play;

    void Start()
    {
        
        playing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      

        if (isEating)
        {
            behavior_timer += Time.deltaTime;
        }
       

        if (behavior_timer >= 15 && gameManager.fish_number_limit >= 8 && !playing && !attack)
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
            FMODUnity.RuntimeManager.PlayOneShot(dash);
            gameManager.Shark_Dash = true;
            shark_sprite.sprite = eating;
            rigidbody2D.simulated = true;
            Attacking();
        }

        if (playing)
        {
   
            playing_timer += Time.deltaTime;
        }
        if (playing_timer >= 15)
        {
            shark_sprite.sprite = calm;
            if (!play_randomed)
            {
                play_randomer = Random.Range(0, 2);

                if (play_randomer == 0)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(play);
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    this.transform.position = new Vector3(10f, Random.Range(-4f, 4f), this.transform.position.z);
                    centerPoint = FindNearestWithTag("Small_Fish").transform;
                    StartCoroutine(WaitforPlayingSeconds());
                    play_randomed = true;
                    
                }
                if (play_randomer == 1)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(play);
                    this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    this.transform.position = new Vector3(-12f, 0f, this.transform.position.z);
                    StartCoroutine(WaitForWaveLinePlay());
                    play_randomed = true;

                }
                play_randomed = true;
            }
        }

       
    }

    public void Facing()
    {
        if (this.transform.position.x < -9 && this.transform.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
        if (this.transform.position.x > 9 && this.transform.localScale.x < 0)
        { this.transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
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
        gameManager.Shark_Dash = true;
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
        gameManager.Shark_Dash = true;
        attackAnticipation = false;
        preparation = false;
        StopAllCoroutines();
    }

    public void Attacking()
    {
        gameManager.Shark_Dash = true;

        if (left_or_right == 0)
        {
            //StartCoroutine(AttackFinal());
            this.transform.DOMove(new Vector3(12f, y_position, 0), 2.5f).SetEase(Ease.InQuart);
            attack = false;
            behavior_timer = 0;
            
            playing = true;
        }
        if (left_or_right == 1)
        {
            //StartCoroutine(AttackFinal());
            this.transform.DOMove(new Vector3(-12f, y_position, 0), 2.5f).SetEase(Ease.InQuart);
            attack = false;
            behavior_timer = 0;
           
            playing = true;
        }

    }

    IEnumerator AttackFinal()
    {
        if (left_or_right == 0)
        { this.transform.DOMove(new Vector3(12f, y_position, 0), 2.5f).SetEase(Ease.InQuart); }
        else if (left_or_right == 1)
        { this.transform.DOMove(new Vector3(-12f, y_position, 0), 2.5f).SetEase(Ease.InQuart); }
        yield return new WaitForSeconds(2.5f);
       
        behavior_timer = 0;
        playing = true;
        attack = false;
        StopAllCoroutines();
    }
    IEnumerator WaitforPlayingSeconds()
    {
        this.transform.DOMove(new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f), 0), 2f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(2f);
        DocircularPlay();
        yield return new WaitForSeconds(2.5f);
        this.transform.DOMove(new Vector3(-12f, Random.Range(-4f, 4f), this.transform.position.y), 2f).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(2f);
        play_randomed = false;
        playing_timer = 0;
        playing = false;
        isEating = true;
        StopAllCoroutines();
    }

    public void DoWaveLinePlay()
    {
        transform.position += new Vector3(Random.Range(2f,5f),Random.Range(-4F,4F),0);
    }

    IEnumerator WaitForWaveLinePlay()
    {
        this.transform.DOMoveX(12f, 2.5f).SetEase(Ease.Linear);
        this.transform.DOMoveY(2f, 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        this.transform.DOMoveY(-2f, 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        this.transform.DOMoveY(2f, 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        this.transform.DOMoveY(-2f, 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        this.transform.DOMoveY(2f, 0.5f).SetEase(Ease.Linear);
        play_randomed = false;
        playing_timer = 0;
        playing = false;
        isEating = true;
        StopAllCoroutines();
    }
    public void DocircularPlay()
    {

        transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(2, LoopType.Restart)
                 .OnUpdate(() =>
                 {
                     float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
                     transform.position = centerPoint.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                 });
        
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


   
}
