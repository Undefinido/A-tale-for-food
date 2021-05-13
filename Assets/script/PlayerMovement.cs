using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float runSpeed = 30f;
    bool jump = false;
    bool is_Squirrel;
    GameObject camera;
    int offset = -10;   //for camera
    int food = 0;
    public bool levelCleared;

    bool squirrelIsAbove = false;
    public int mass = 1;
    Vector2 movementVector;
    public Animator animator;
    GameObject heartsCanvas;
    int lives = 3;

    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    public bool m_Grounded;

    // Start is called before the first frame update
    void Start()
    {
        levelCleared = false;
        is_Squirrel = true;
        this.GetComponent<Rigidbody2D>().mass = mass;
        camera = GameObject.Find("Main Camera");
        heartsCanvas = GameObject.Find("Hearts");
    }

    // Update is called once per frame
    void Update()
    {

        //---------------TWEAK FOR EMPTY SCENES TEST -------------------
        if (GameObject.FindGameObjectsWithTag("Food").Length == 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().levelCleared();
        }
        //--------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Quit();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            is_Squirrel = !is_Squirrel;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (is_Squirrel && this.tag == "Squirrel")
        {   
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJumping", true);
            }
            camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, offset);  //polling
        }
        else if(!is_Squirrel && this.tag == "Bird")
        {
            verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
            camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, offset);  //polling
            /*if (squirrelIsAbove)
            {
                movementVector = new Vector2(horizontalMove, verticalMove);
                movementVector = movementVector.normalized * Time.deltaTime;
            }*/
        }

        if(transform.tag == "Squirrel" && is_Squirrel)
        {
            animator.SetBool("isWalking", horizontalMove != 0);
        }
        
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        if (is_Squirrel && this.tag == "Squirrel")
        {
            controller.Move(horizontalMove * Time.deltaTime, false, jump);
            jump = false;
        }else if(!is_Squirrel && this.tag == "Bird" && !squirrelIsAbove)
        {
            controller.MoveBird(horizontalMove * Time.deltaTime, verticalMove * Time.deltaTime);
        }

        /*
        if (squirrelIsAbove)
        {
            this.GetComponent<Rigidbody2D>().MovePosition(this.GetComponent<Rigidbody2D>().position + movementVector);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //SQUIRREL AND BIRD INTERACTION
        if(other.transform.tag == "Squirrel" && this.tag == "Bird")
        {
            BoxCollider2D collider = other.otherCollider as BoxCollider2D;  //only when touches "its head"
            if(collider != null)
            {
                /*other.collider.transform.SetParent(transform);
                squirrelIsAbove = true;
                other.transform.parent = this.transform;
                other.rigidbody.mass = 0;*/
            }
        }

        /*
        if(other.transform.tag == "Enemy")
        {
            //get dmg
            var positive = m_FacingRight? true : false;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(positive ? -1000 : 1000, positive ? -1000 : 1000));
        }*/


        if (other.transform.tag == "Enemy")
        {
            updateDamage(true);     //do dmg
            /*var rb = other.transform.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(rb.velocity.x > 0 ? -1000 : 1000, rb.velocity.y > 0 ? -1000 : 1000));*/
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(/*this.GetComponent<Rigidbody2D>().velocity.x > 0 ? -1000 : 100*/0, this.GetComponent<Rigidbody2D>().velocity.y > 0 ? -1300 : 1300));
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == "Squirrel" && this.tag == "Bird")
        {
            //other.rigidbody.velocity = this.transform.GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Squirrel" && this.tag == "Bird")
        {
            BoxCollider2D collider = other.otherCollider as BoxCollider2D;  //only when touches "its head"
            if (collider != null)
            {
                /*other.collider.transform.SetParent(null);
                squirrelIsAbove = false;
                other.rigidbody.mass = mass;*/
            }
        }
    }

    //if damage = true, takes 1 live, if false means heal 1 hp
    public void updateDamage(bool damage)
    {
        if(damage == false) //heals
        {
            if(lives != 3)
            {
                lives++;
            }
        }
        else
        {
            lives--;    
        }

        if (!levelCleared)
        {
            switch (lives)
            {
                case 0:
                    heartsCanvas.transform.GetChild(0).gameObject.SetActive(false);
                    GameOver();
                    break;
                case 1:
                    heartsCanvas.transform.GetChild(1).gameObject.SetActive(false);
                    break;
                case 2:
                    heartsCanvas.transform.GetChild(1).gameObject.SetActive(true);
                    heartsCanvas.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 3:
                    heartsCanvas.transform.GetChild(2).gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void foodCollected()
    {
        food++;
        GameObject.Find("Food").GetComponent<UnityEngine.UI.Text>().text = "Food taken: " + food;
        if (GameObject.FindGameObjectsWithTag("Food").Length -1 == 0) { //no le da tiempo a actualizar a tiempo el gameobject destruido
            GameObject.Find("GameManager").GetComponent<GameManager>().levelCleared();
        }
    }

    public void GameOver()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();
    }
}

