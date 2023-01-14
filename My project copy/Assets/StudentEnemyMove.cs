using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentEnemyMove : MonoBehaviour{

    public float speed;
    public Transform bodyPos;
    public float timeAlive;
    public float liveTime;

    // Start is called before the first frame update
    void Start(){
        bodyPos = this.gameObject.transform;
        timeAlive = 0;
    }

    // Update is called once per frame
    void Update(){
        timeAlive += Time.deltaTime;
        bodyPos.Translate(Vector3.back * speed * Time.deltaTime);
        if(timeAlive > liveTime){
            Destroy(this.gameObject);
        }
    }
}
