using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour{

    public float timeForFloor;
    public float timeSinceLastFloor;
    
    public Vector3 floorPos;
    public GameObject floorObj;


    // Start is called before the first frame update
    void Start(){
        timeSinceLastFloor = 10000;
    }

    // Update is called once per frame
    void Update(){
        timeSinceLastFloor += Time.deltaTime;
        if(timeSinceLastFloor > timeForFloor){
            spawnFloor();
            timeSinceLastFloor = 0;
        }
    }

    void spawnFloor(){
        Instantiate(floorObj, floorPos, Quaternion.identity, this.gameObject.transform);
    }
}
