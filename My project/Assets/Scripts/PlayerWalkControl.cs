using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkControl : MonoBehaviour{

    public Animator animations;

    // Start is called before the first frame update
    void Start(){
        // animations.Play("Walk");
        animations.SetInteger("legs", 1);
    }

    // Update is called once per frame
    void Update(){
        animations.SetInteger("legs", 1);
    }
}
