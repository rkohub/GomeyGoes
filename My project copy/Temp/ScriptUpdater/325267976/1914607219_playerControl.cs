using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public Transform bodyPos;
    public Rigidbody bodyPhy;
    public int spot;

    void Start(){
        bodyPos = this.gameObject.transform;
        bodyPhy = this.gameObject.GetComponent<Rigidbody>();
        spot = 0;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("w")){
            // bodyPos.Translate(Vector3.forward * speed);
            print("w");
        }else if (Input.GetKeyDown("a")){
            if(spot > -1){
                spot--;
                bodyPos.Translate(Vector3.left * speed);
            }
            print("a");
        }else if (Input.GetKeyDown("s")){
            print("s");
        }else if (Input.GetKeyDown("d")){
            if(spot < 1){
                spot++;
                bodyPos.Translate(Vector3.right * speed);
            }
            print("d");
        }else if (Input.GetKeyDown("space")){
            print("space");

            
        }
    }
}
