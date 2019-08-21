using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float speedRun;
    public int direction;
    public bool run;
    public bool aim;
    public bool fire;
    public bool walk;
    public Animator animator;


    private bool moveDisable = false;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    private int weapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    // FixedUpdate is called many times per frame
    void FixedUpdate() {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        


        weaponSelection();
        runBoolean(moveInput);
        gunMechanic(moveInput);


        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        animator.SetFloat("Speed", moveVelocity.x);
        animator.SetInteger("Direction", direction);
        animator.SetBool("Run", run);
        animator.SetBool("Aim", aim);
        animator.SetBool("Fire", fire);
        animator.SetInteger("Weapon", weapon);
        animator.SetBool("Walk", walk);
    }

    void runBoolean(Vector2 moveInput) {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
            walk = false;
            moveVelocity = moveInput.normalized * speedRun;
            if (moveVelocity.x < 0)
            {
                direction = -1;
            }
            if (moveVelocity.x > 0)
            {
                direction = 1;
            }
            if (moveVelocity.x == 0) {
                run = false;
            }
        }

        else
        {
            run = false;
            
            moveVelocity = moveInput.normalized * speed;
            if (moveVelocity.x < 0)
            {
                direction = -1;
                walk = true;
            }
            if (moveVelocity.x > 0)
            {
                direction = 1;
                walk = true;
            }
            if (moveVelocity.x == 0) {
                walk = false;
            }
        }
    }

    void gunMechanic(Vector2 moveInput){
        if (Input.GetKey(KeyCode.X))
        {
            moveVelocity.x = 0;
            aim = true;
            if (Input.GetKey(KeyCode.C))
            {
                StartCoroutine(AnimationCoroutine(moveInput));   
            }
        }
        else {
            aim = false;
            moveDisable = false;
        }
    }

    void weaponSelection() {
        if (Input.GetKey(KeyCode.Alpha0)) {
            weapon = 0;
            Debug.Log("Nothing Selected");
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            weapon = 1;
            Debug.Log("HandGun Selected");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon = 2;
            Debug.Log("Shotgun Selected");
        }
    }


    IEnumerator AnimationCoroutine(Vector2 moveInput) {
        if (weapon == 1)
        {
            switch (direction)
            {
                case 1: animator.Play("PlayerShootingHandgunRight");
                    break;
                case -1: animator.Play("PlayerShootingHandgunLeft");
                    break;
            }
        }
        if (weapon == 2)
        {
            switch (direction)
            {
                case 1:
                    animator.Play("PlayerShootingShotgunRight");
                    break;
                case -1:
                    animator.Play("PlayerShootingShotgunLeft");
                    break;
            }
        }
        
        yield return 0;
    }
}
