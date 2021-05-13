using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{

    bool isCollecting = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.tag == "Heart")
        {
            if (other.tag == "Squirrel")    //bird cant take them tho
            {
                if(isCollecting == false)   //prevents multiple ontriggerenter instances
                {
                    isCollecting = true;
                    other.GetComponent<PlayerMovement>().updateDamage(false);
                    Destroy(this.gameObject);
                    isCollecting = false;
                }
            }
        }
        else if (transform.tag == "Food")
        {
            if (other.tag == "Squirrel")
                if (isCollecting == false)
                {
                    isCollecting = true;
                    other.GetComponent<PlayerMovement>().foodCollected();
                    Destroy(this.gameObject);
                    isCollecting = false;
                }
            }   
        }
    }

