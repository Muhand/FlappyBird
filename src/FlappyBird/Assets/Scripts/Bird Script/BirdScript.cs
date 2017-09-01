using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public static BirdScript instance;
    [SerializeField]
    private Rigidbody2D myRigidBoy;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float forwardSpeed = 3f;
    [SerializeField]
    private float bounceSpeed = 4f;
    private GameObject[] flapButtons;
    private bool didFlap;
    public bool isAlive;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flapClip, pointClip, diedClip;
    public int score;

	void Awake () {
        if(instance == null)
            instance = this;

        flapButtons = GameObject.FindGameObjectsWithTag("FlapButton");
        for (int i = 0; i < flapButtons.Length; i++)
        {
            Button temp = flapButtons[i].GetComponent<Button>();
            temp.onClick.AddListener(() => FlapTheBird());
        }

        //TODO: REMOVE THIS, THIS IS FOR DEBUGGING ONLY
        isAlive = true;
        score = 0;

        SetCameraOffsetX();
	}

    void FixedUpdate()
    {
        if (isAlive)
        {
            //Get current position of the bird
            Vector3 temp = transform.position;
            //Move the bird forward based on it's speed
            temp.x += forwardSpeed * Time.fixedDeltaTime;
            //Reassign our position
            transform.position = temp;

            if(didFlap)
            {
                //Flap only once so disable didFlap
                didFlap = false;
                myRigidBoy.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("Flap");
            }

            if(myRigidBoy.velocity.y >= 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBoy.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    void SetCameraOffsetX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x);
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        didFlap = true;                     //Flap the bird once
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Ground")
        {
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(diedClip);
                GameplayController.instance.PlayerDiedShowScore(score);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PipeHolder")
        {
            score++;
            GameplayController.instance.SetScore(score);
            audioSource.PlayOneShot(pointClip);
        }
    }
}
