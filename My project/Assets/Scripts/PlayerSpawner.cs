using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

    public GameObject student;
    public float timeToSpawn;
    public float lastSpawned;
    public Vector3 spawnLocation;
    public GameObject largeStudent;
    public float skipProb;
    public GameObject money;
    public float moneyTimeInterval;
    public float timeSinceMoney;

    public float startParabolaTime;
    public Vector3 moneySpawn;
    public bool spawnStudents;

    public float A;
    public float B;
    public float C;


    public bool moneyEnabled;
    public bool[] moneyLanes;

    public float changeMoneyTime;
    public float timeSinceMoneyChange;

    public float timeToNextDisable;
    public float timeSinceMoneyDisable;

    public bool spawningEnable;

    // Start is called before the first frame update
    void Start(){
        startParabolaTime = -10.0f;
        A = -20.0f;
        B = 0.0f;
        C = 4.0f;
        moneyLanes = new bool[3] {false,false,false};
        timeSinceMoneyDisable = 0;
        timeSinceMoneyChange = 0;
        spawningEnable = true;
    }

    // Update is called once per frame
    void Update(){
        if(spawningEnable){
            lastSpawned += Time.deltaTime;
            timeSinceMoney += Time.deltaTime;
            if(lastSpawned > timeToSpawn){
                float val = Random.Range(0.0f,1.0f);
                if(val < skipProb){
                    print("SKIP");
                    lastSpawned = Random.Range(-1.0f,0.25f);
                }else{
                    // spawn();
                    spawnStudents = true;
                }
            }

            timeSinceMoneyChange += Time.deltaTime;
            timeSinceMoneyDisable += Time.deltaTime;

            if(timeSinceMoneyChange > changeMoneyTime){
                moneyLanes = new bool[3] {false,false,false};
                moneyLanes[Random.Range(0,3)] = true;
                timeSinceMoneyChange = 0;
            }

            if(!moneyEnabled && timeSinceMoneyDisable > 0){
                moneyEnabled = true;
                moneyLanes[Random.Range(0,3)] = true;
            }

            if(timeSinceMoneyDisable > timeToNextDisable){
                moneyEnabled = false;
                moneyLanes = new bool[3] {false,false,false};
                timeSinceMoneyDisable = Random.Range(-2.0f,-5.0f);
            }

            if(timeSinceMoney > moneyTimeInterval){
                float yPos = 0;
                float timePassed = Time.realtimeSinceStartup;
                if(timePassed > startParabolaTime){
                    // print("TP: " + (timePassed - startParabolaTime)); 
                    yPos = parabPos(A,B,C,timePassed - startParabolaTime);
                }
                if(yPos < 0){
                    yPos = 0;
                }
                // print(timePassed);
                // print("YPOS: " + yPos);


                if(spawnStudents){
                    // float leftStart = (-B + Mathf.Sqrt((B*B) - (4 * A * C)))/ (2 * A);
                    spawn(yPos, 7);
                    spawnStudents = false;
                    startParabolaTime = Time.realtimeSinceStartup;
                    // spawnMoney(new bool[3] {true,true,true}, new float[3] {yPos,yPos,yPos});
                    spawnMoney(moneyLanes,new float[3] {yPos,yPos,yPos});
                }else{
                    spawnMoney(moneyLanes,new float[3] {yPos,yPos,yPos});
                    // spawnMoney(new bool[3] {true,true,true}, new float[3] {yPos,yPos,yPos});
                }
                timeSinceMoney = 0;
            }
        }
    }

    float parabPos(float a, float b, float c, float x){
        // print("X: " + x);
        float leftStart = (-b + Mathf.Sqrt((b*b) - (4 * a * c)))/ (2 * a);
        // print("LS: " + leftStart);
        x += leftStart;
        // print("X: " + x);
        return (a * x * x) + (b * x) + c;
    }

    // void spawnMoney(bool spawnLeft, bool spawnCenter, bool spawnRight, float leftY, float centerY, float rightY){

    void spawnMoney(bool[] spawnSpots, float[] yPoss){
        bool enabled = false;
        if(enabled){
            if(spawnSpots[0]){
                Instantiate(money, moneySpawn + new Vector3(2*(-1),yPoss[0],0), Quaternion.identity,this.gameObject.transform);
            }
            if(spawnSpots[1]){
                Instantiate(money, moneySpawn + new Vector3(2*(0),yPoss[1],0), Quaternion.identity,this.gameObject.transform);
            }
            if(spawnSpots[2]){
                Instantiate(money, moneySpawn + new Vector3(2*(1),yPoss[2],0), Quaternion.identity,this.gameObject.transform);
            }
        }
    }

    void spawn(float yPos, float zBack){
        int pattern = Random.Range(0,5);
        print (pattern);
        if(pattern == 0){
            //1 Small
            int lane = Random.Range(0,3) - 1;

            bool[] money = new bool[3] {true,true,true};
            money[lane+1] = false;
            // spawnMoney(money, new float[3] {yPos,yPos,yPos});

            spawnPlayer(student, lane, new Vector3(0,0,zBack));
        }else if(pattern == 1){
            //1 Large
            int lane = Random.Range(0,3) - 1;

            bool[] money = new bool[3] {true,true,true};
            money[lane+1] = false;
            // spawnMoney(money, new float[3] {yPos,yPos,yPos});

            spawnPlayer(largeStudent, lane, new Vector3(0,0,zBack));
        }else if(pattern == 2){
            //2 Small
            int lane = Random.Range(0,3) - 1; //Lane Not To Spawn

            bool[] money = new bool[3] {true,true,true};

            if(lane != 0){
                spawnPlayer(student, 0, new Vector3(0,0,zBack));
                money[1] = false;
            }
            if(lane != 1){
                spawnPlayer(student, 1, new Vector3(0,0,zBack));
                money[2] = false;
            }
            if(lane != -1){
                spawnPlayer(student, -1, new Vector3(0,0,zBack));
                money[0] = false;
            }

            // spawnMoney(money, new float[3] {yPos,yPos,yPos});
        }else if(pattern == 3){
            //2 Big
            int lane = Random.Range(0,3) - 1; //Lane Not To Spawn

            bool[] money = new bool[3] {true,true,true};

            if(lane != 0){
                spawnPlayer(largeStudent, 0, new Vector3(0,0,zBack));
                money[1] = false;
            }
            if(lane != 1){
                spawnPlayer(largeStudent, 1, new Vector3(0,0,zBack));
                money[2] = false;
            }
            if(lane != -1){
                spawnPlayer(largeStudent, -1, new Vector3(0,0,zBack));
                money[0] = false;
            }

            // spawnMoney(money, new float[3] {yPos,yPos,yPos});
        }else if(pattern == 4){
            //random
            int lane = Random.Range(0,3) - 1; //Lane Not To Spawn

            bool[] money = new bool[3] {true,true,true};

            if(lane != 0){
                GameObject genStu = randomStudent();
                spawnPlayer(genStu, 0, new Vector3(0,0,zBack));
                money[1] = false;
            }
            if(lane != 1){
                GameObject genStu = randomStudent();
                spawnPlayer(genStu, 1, new Vector3(0,0,zBack));
                money[2] = false;
            }
            if(lane != -1){
                GameObject genStu = randomStudent();
                spawnPlayer(genStu, -1, new Vector3(0,0,zBack));
                money[0] = false;
            }

            // spawnMoney(money, new float[3] {yPos,yPos,yPos});
        }
        // spawnMoney(new bool[3] {true,true,true}, new float[3] {yPos,yPos,yPos});

        lastSpawned = Random.Range(-1.0f,0.25f);

        
    }

    void spawnPlayer(GameObject obj, int lane, Vector3 offset){
        //-1,0,1 = lane
        Instantiate(obj, spawnLocation + new Vector3(2*(lane),0,0) + offset, Quaternion.identity, this.gameObject.transform);
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
