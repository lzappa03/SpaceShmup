using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Inscribed")]

    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("Dynamic")]

    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        //pick any point on the left side of the screen
        p0 = Vector3.zero;

        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        //pick any point on the right side of the screen
        p1 = Vector3.zero;
        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        //possible swap sides
        if(Random.value > 0.5f)
        {
            //setting the .x of each point to its negative will move it to the other side of the screen
            p0.x *= -1;
            p1.x *= -1;
        }

        //set the birthTime to the current time
        birthTime = Time.time;        
    }

    public override void Move()
    {
        //Bezier curves work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / lifeTime;

        //if u>1, then it has been longer than lifeTime since birthTime
        if(u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        //adjust u by adding a U Curve based on a Sine wave
        u = u + sinEccentricity*(Mathf.Sin(u * Mathf.PI * 2));

        //interpolate the two linear interpolation points
        pos = (1 - u) * p0 + u * p1;
    }
}
