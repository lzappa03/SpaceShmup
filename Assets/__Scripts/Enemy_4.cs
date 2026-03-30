using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    [Header("Inscribed")]

    public GameObject laserPrefab;

    public float lifeTime = 5;

    public float radius = 10f;

    public int maxShips = 1; //max amount of enemies on screen at a time

    [Header("Dynamic")]

    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;
    public Vector3 laserLocalOffset = new Vector3(0f, -26.27f, 0f);

    private GameObject laserInstance;
    private static int currentShips = 0; //current amount of enemies on screen
    public static int CurrentShips { get { return currentShips; } }

    // Start is called before the first frame update
    void Start()
    {
        //pick any point on the left side of the screen
        p0 = Vector3.zero;

        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = bndCheck.camHeight - radius;

        //pick any point on the right side of the screen
        p1 = Vector3.zero;

        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = bndCheck.camHeight - radius;

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

        pos = (1 - u) * p0 + u * p1;
    }

    void OnEnable()
    {
        currentShips++;
        if (laserPrefab != null && laserInstance == null)
        {
            laserInstance = Instantiate(laserPrefab, transform);
            laserInstance.transform.localPosition = laserLocalOffset;
            laserInstance.transform.localRotation = Quaternion.identity;
        }
    }

    void OnDisable()
    {
        currentShips = Mathf.Max(0, currentShips - 1);
        if (laserInstance != null)
        {
            Destroy(laserInstance);
            laserInstance = null;
        }
    }

    
}
