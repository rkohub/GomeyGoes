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

    public float gravityForce;

    public float switchCooldown;
    public float timeSinceLastSwitch;

    public AudioSource hitAudioSource;
    public AudioSource envSound;
    public AudioSource deathLoop;
    
    public AudioSource jumpSound;
    public AudioSource crashSound;
    public AudioSource moneySound;

    public GameObject goat;
    public Animator animations;


    public GameObject personAndMoneySpawner;
    public GameObject envSpawner1;
    public GameObject envSpawner2;

    public GameObject[] spawners;
    public SpawnEnv spawnE1;
    public SpawnEnv spawnE2;
    public PlayerSpawner spawnMP;

    void Start(){
        bodyPos = this.gameObject.transform;
        bodyPhy = this.gameObject.GetComponent<Rigidbody>();
        pos = 0;
        isChangingLanes = false;
        envSound.loop = true;
        hitAudioSource = this.gameObject.GetComponent<AudioSource>();

        animations = goat.GetComponent<Animator> ();
        animations.Play("Jump");
        spawners = new GameObject[3] {personAndMoneySpawner, envSpawner1, envSpawner2};
    }

    // Update is called once per frame
    void Update(){
        timeSincePress += Time.deltaTime;
        timeSinceLastSwitch += Time.deltaTime;

        if (Input.GetKeyDown("w")){
            // bodyPos.Translate(Vector3.forward * dist);
            // print("w");
        }else if (Input.GetKeyDown("a") && timeSinceLastSwitch > switchCooldown){
            if(pos > -0.99){
                pos--;
                moveTo = (int) pos-1;
                isChangingLanes = true;
                bodyPos.Translate(Vector3.left * dist);
                timeSinceLastSwitch = 0;
            }
            // print("a");
        }else if (Input.GetKeyDown("s")){
            // print("s");
        }else if (Input.GetKeyDown("d") && timeSinceLastSwitch > switchCooldown){
            if(pos < 0.99){
                pos++;
                moveTo = (int) pos+1;
                isChangingLanes = true;
                bodyPos.Translate(Vector3.right * dist);
                timeSinceLastSwitch = 0;
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

        if(!grounded){
            //ExtraGravity
            bodyPhy.AddForce(Vector3.down * gravityForce);
        }

        if(grounded && (timeSincePress < bufferJumpTime)){
            grounded = false;
            bodyPhy.AddForce(Vector3.up * jumpForce);
            // jumpSound.Play();
            print("JUMPPP");
            
            // animations.Play("Jump");
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
            animations.Play("Jump");
        }
        // bodyPhy.AddForce(Vector3.up * jumpForce);
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            grounded = false;
            print("Swim");
            animations.Play("Swim/Fly");
        }
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Student"){
            //Turn Off global Audio
            envSound.Stop();
            hitAudioSource.enabled = true;
            hitAudioSource.Play();
            crashSound.Play();
            Invoke("playDeathLoop",hitAudioSource.clip.length);
            // Time.timeScale = 0;

            // yield WaitForSeconds(2);
            print("DIE");
            //Stop Animations
            animations.Play("Death");
            //Stop Money Spin
            //Stop Spawning
            spawnE1.spawningEnable = false;
            spawnE2.spawningEnable = false;
            spawnMP.spawningEnable = false;
            //Stop Tree,Building,Player Move
            foreach (GameObject spawnObj in spawners){
                foreach (Transform child in spawnObj.transform){
                    GameObject ch = child.gameObject;
                    if(ch.tag == "Student" || ch.tag == "Moving" || ch.tag == "Money"){
                        StudentEnemyMove script = ch.GetComponent<StudentEnemyMove>();
                        script.dieEnable = false;
                        script.speed = 0;
                    }
                }
            }
            
            //Make things not Die
            //Open main Menu.
        }

        if(collision.gameObject.tag == "Money"){
            //KACHING
            print("KACHING");
            //Add Score
            //Sound Effect
            // collision.gameObject.GetComponent<AudioSource>().Play();
            moneySound.Play();
            Destroy(collision.gameObject);
        }
    }

    void playDeathLoop(){
        print("DED");
        deathLoop.enabled = true;
        deathLoop.loop = true;
        deathLoop.Play();
    }
}
