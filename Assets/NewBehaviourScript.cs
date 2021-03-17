using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 50;
    public float turnmultiplier = 4;
    Rigidbody2D rb;
    public GameObject prefab;
    public int jumpCount = 0;
    public int maxJumpCount = 6;
    public int maxSpeed = 5;
    public Vector2 jumpForce = new Vector2(0, 6);

    public Vector2 downForce = new Vector2(0, 6);

    public bool grounded = false;

    public float bigGunTimer = 3;
    public float timer = 0;

    private Vector3 linePos;
    public LineRenderer lr;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(speed * inputX, 0);
        movement += new Vector2(((rb.velocity.x < 0 && inputX > 0) ? 1 : 0) * turnmultiplier - ((rb.velocity.x > 0 && inputX < 0) ? 1 : 0) * turnmultiplier, 0);
        movement *= maxSpeed - 0.01f * Mathf.Pow(rb.velocity.x, 2);


        rb.AddForce(movement * Time.deltaTime, ForceMode2D.Impulse);

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (jumpCount < maxJumpCount)
            {
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!grounded)
            {
                rb.AddForce(transform.up * -downForce, ForceMode2D.Impulse);
            }
            transform.localScale = new Vector2(1f, 1.3f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            transform.localScale = new Vector2(1f, 2f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }

        if (true){
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, linePos);
        }

        if (timer > 0){
            timer -= Time.deltaTime;
        }else if (timer <= 0){
            lr.positionCount = 0;
        }

    }

    void shoot()
    {
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        if (timer <= 0){
            lr.positionCount = 2;
            drawLine(Input.mousePosition);
        }
    }

   void drawLine(Vector3 mousePos){
        linePos = Camera.main.ScreenToWorldPoint(mousePos);
        timer = bigGunTimer;
   }

    void OnCollisionEnter2D()
    {
        jumpCount = 0;
        grounded = true;
    }

    void OnCollisionExit2D()
    {
        grounded = false;
    }

}
