using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentEnemyMove : MonoBehaviour{

    public float speed;
    public Transform bodyPos;
    public float timeAlive;
    public float liveTime;

    public bool dieEnable;

    // Start is called before the first frame update
    void Start(){
        bodyPos = this.gameObject.transform;
        timeAlive = 0;
        dieEnable = true;
    }

    // Update is called once per frame
    void Update(){
        timeAlive += Time.deltaTime;
        // bodyPos.Translate(new Vector3 (0,0,-1) * speed * Time.deltaTime);
        transform.position = transform.position + new Vector3 (0,0,-1) * speed * Time.deltaTime;
        if(timeAlive > liveTime && dieEnable){
            Destroy(this.gameObject);
        }
    }
}
