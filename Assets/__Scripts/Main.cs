using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    [Header("Inscribed")]
    public GameObject[] prefabEnemies; //array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f; //num of Enemies/sec
    public float nextEnemy4SpawnTime = 15f;
    public float enemyDefaultPadding = 1.5f; //padding for position
    private BoundsCheck bndCheck;
    
    private float enemy4SpawnInterval = 30;
    void Awake()
    {
        S = this;

        //Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();

        //Invoke SpawnEnemy() once (in 2 secs, based on default values)
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        //pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);

        
        //there can only be a specific number of enemy 4s on screen
        Enemy_4 e4 = prefabEnemies[ndx].GetComponent<Enemy_4>();
        if (e4 != null)
        {
            if (Enemy_4.CurrentShips >= e4.maxShips || Time.time < nextEnemy4SpawnTime)
            {
                Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
                return;
            }

            // only set this when an Enemy_4 is actually allowed to spawn
            nextEnemy4SpawnTime = Time.time + enemy4SpawnInterval;
        }

        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //position Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;

        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //set the initial position for the spawned Enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //invoke SpawnEnemy() again
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("__Scene_0");
    }
}
