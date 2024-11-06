using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float existing_timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        existing_timer += Time.deltaTime;

        this.transform.position += new Vector3(0, 0.03f, 0);

        if (existing_timer >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
