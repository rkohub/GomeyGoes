using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update

    public float dist;
    public Transform bodyPos;
    public Rigidbody bodyPhy;
    public float pos;
    public int moveTo;
    public int jumpForce;
    public bool grounded;
    public float bufferJumpTime;
    public float timeSincePress;
    public float changeLaneTime;
    public bool isChangingLanes;
    public bool isMovingRight;

    void Start(){
        bodyPos = this.gameObject.transform;
        bodyPhy = this.gameObject.GetComponent<Rigidbody>();
        pos = 0;
        isChangingLanes = false;
    }

    // Update is called once per frame
    void Update(){
        timeSincePress += Time.deltaTime;

        if (Input.GetKeyDown("w")){
            // bodyPos.Translate(Vector3.forward * dist);
            // print("w");
        }else if (Input.GetKeyDown("a")){
            if(pos > -0.99){
                pos--;
                moveTo = (int) pos-1;
                isChangingLanes = true;
                bodyPos.Translate(Vector3.left * dist);
            }
            // print("a");
        }else if (Input.GetKeyDown("s")){
            // print("s");
        }else if (Input.GetKeyDown("d")){
            if(pos < 0.99){
                pos++;
                moveTo = (int) pos+1;
                isChangingLanes = true;
                bodyPos.Translate(Vector3.right * dist);
            }
            // print("d");
        }else if (Input.GetKeyDown("space")){
            // print("space");
            timeSincePress = 0;
            // if(grounded){
            //     grounded = false;
            //     bodyPhy.AddForce(Vector3.up * jumpForce);
            // }
            
        }
        if(grounded && (timeSincePress < bufferJumpTime)){
            grounded = false;
            bodyPhy.AddForce(Vector3.up * jumpForce);
        }
        // if(isChangingLanes && isMovingRight && pos < moveTo ){
        //     bodyPos.Translate(Vector3.right * Time.deltaTime * );
        // }
        // if(isChangingLanes && !isMovingRight && pos > moveTo ){
        //     bodyPos.Translate(Vector3.left * Time.deltaTime * );
        // }

    }

    void OnCollisionEnter(Collision collision){
        // print("COLL");
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
        }
        // bodyPhy.AddForce(Vector3.up * jumpForce);
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Student"){
            print("DIE");
        }
    }
}
