using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector3 dir;

    // Start is called before the first frame update
    public void Setup(Vector3 parentPos)
    {
        rb = GetComponent<Rigidbody2D>();

        dir = Input.mousePosition - parentPos;
        dir = dir.normalized;

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;


        transform.eulerAngles = new Vector3(0, 0, n);

    }
    // Update is called once per frame
    void Update()
    {
        transform.position += dir * 50f * Time.deltaTime;
        Debug.Log(dir);
    }
}
