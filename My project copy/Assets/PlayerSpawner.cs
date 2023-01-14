using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

    public GameObject student;
    public float timeToSpawn;
    public float lastSpawned;
    public Vector3 spawnLocation;
    public GameObject largeStudent;

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
        int pattern = Random.Range(0,5);
        print (pattern);
        if(pattern == 0){
            //1 Small
            int lane = Random.Range(0,3) - 1;
            spawnPlayer(student, lane);
        }else if(pattern == 1){
            //1 Large
            int lane = Random.Range(0,3) - 1;
            spawnPlayer(largeStudent, lane);
        }else if(pattern == 2){
            //2 Small
            int lane = Random.Range(0,3) - 1; //Lane Not To Spawn
            if(lane != 0){
                spawnPlayer(student, 0);
            }
            if(lane != 1){
                spawnPlayer(student, 1);
            }
            if(lane != -1){
                spawnPlayer(student, -1);
            }
        }else if(pattern == 3){
            //2 Big
            int lane = Random.Range(0,3) - 1; //Lane Not To Spawn
            if(lane != 0){
                spawnPlayer(largeStudent, 0);
            }
            if(lane != 1){
                spawnPlayer(largeStudent, 1);
            }
            if(lane != -1){
                spawnPlayer(largeStudent, -1);
            }
        }else if(pattern == 4){
            //random
            int lane = Random.Range(0,3) - 1; //Lane Not To Spawn
            if(lane != 0){
                GameObject genStu = randomStudent();
                spawnPlayer(genStu, 0);
            }
            if(lane != 1){
                GameObject genStu = randomStudent();
                spawnPlayer(genStu, 1);
            }
            if(lane != -1){
                GameObject genStu = randomStudent();
                spawnPlayer(genStu, -1);
            }
        }

        lastSpawned = 0; //Random.Range(-1.0f,0.0f);

        
    }

    void spawnPlayer(GameObject obj, int lane){
        //-1,0,1 = lane
        Instantiate(obj, spawnLocation + new Vector3(2*(lane),0,0), Quaternion.identity);
    }

    public GameObject randomStudent(){
        int numStudents = 2;
        int randStu = Random.Range(0, numStudents);
        if(randStu == 0){
            return student;
        }else if(randStu == 1){
            return largeStudent;
        }
        return null;
    }
}
