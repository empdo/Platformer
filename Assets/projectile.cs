using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public float projectileSpeed = 2f;
    Vector3 dir;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        Quaternion q = Quaternion.AngleAxis(n, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime);

    }
    // Update is called once per frame
    void Update()
    {
        transform.position += dir * projectileSpeed * Time.deltaTime;
    }
}
