using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

    public GameObject student;
    public float timeToSpawn;
    public float lastSpawned;
    public Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        lastSpawned += Time.deltaTime;
        if(lastSpawned > timeToSpawn){
            spawn();
        }
    }

    void spawn(){
        int lane = Random.Range(0,3) - 1;
        Instantiate(student, spawnLocation + new Vector3(2*lane,0,0), Quaternion.identity);
        lastSpawned = 0;
    }
}
