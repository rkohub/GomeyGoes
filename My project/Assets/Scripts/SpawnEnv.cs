using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnv : MonoBehaviour{

    public GameObject tree;
    public GameObject building;

    public Vector3 treeSpot;
    public Vector3 buildingSpot;

    public float timeSinceLastTree;
    public float timeSinceLastBuilding;

    public float timeForTree;
    public float timeForBuilding;

    public bool spawningTrees;
    public int treeCount;
    public int treesSpawned;

    public int buildingCount;
    public int buildingsSpawned;

    public bool spawningEnable;

    // Start is called before the first frame update
    void Start(){
        spawningTrees = true;
        treeCount = Random.Range(5,10);
        treesSpawned = 0;
        spawningEnable = true;
    }

    // Update is called once per frame
    void Update(){

        if(spawningEnable){
            if(spawningTrees){
                timeSinceLastTree += Time.deltaTime;
                if(timeSinceLastTree > timeForTree){
                    spawnTree();
                    if(treesSpawned >= treeCount){
                        spawningTrees = false;
                        buildingCount = Random.Range(3,5);
                        buildingsSpawned = 0;
                    }
                }
            }else{
                timeSinceLastBuilding += Time.deltaTime;
                if(timeSinceLastBuilding > timeForBuilding){
                    spawnBuilding();
                    if(buildingsSpawned >= buildingCount){
                        spawningTrees = true;
                        treeCount = Random.Range(5,10);
                        treesSpawned = 0;
                    }
                }
            }
        }
    
        
    }

    void spawnTree(){
        treesSpawned++;
        timeSinceLastTree = 0;
        Instantiate(tree, treeSpot, Quaternion.identity, this.gameObject.transform);
    }

    void spawnBuilding(){
        buildingsSpawned++;
        timeSinceLastBuilding = 0;
        Instantiate(building, buildingSpot, Quaternion.identity, this.gameObject.transform);
    }

}
