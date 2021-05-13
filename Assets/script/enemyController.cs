using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    public Transform groundDetection;
    int direction;

    // Start is called before the first frame update
    void Start()
    {
        groundDetection = transform.Find("GroundDetection").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f);
        Debug.DrawRay(groundDetection.position, Vector2.down, Color.red, 0.5f);
        if (groundInfo.collider == false) {
            if(movingRight == true)
            {
                //transform.eulerAngles = new Vector3(0, -180, 0);
                //movingRight = false;
                Flip();
            }
            else
            {
                //transform.eulerAngles = new Vector3(0, 0, 0);
                //movingRight = true;
                Flip();
            }
        }
        if (movingRight)
        {
            this.direction = 1;
        }
        else
        {
            this.direction = -1;
        }
        this.transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Bird" || other.transform.tag == "Squirrel")
        {
            //TakeDamage();       //do dmg
            /*var rb = other.transform.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(rb.velocity.x > 0 ? -1000 : 1000, rb.velocity.y > 0 ? -1000 : 1000));*/
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);  //prevent from getting pushed
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        this.movingRight = !this.movingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }
}
