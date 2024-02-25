using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private CharacterController cc;
    [SerializeField]
    private bool debug;
    [SerializeField]
    private Transform rotationObject;
    [SerializeField]
    private float rotationSpeed = 50f;
    private float speed = 0;
    
    public void SetIdle(bool hasRock, bool hasStick, bool hasSomething)
    {
        Debug.Log("I am bored");
        Debug.Log(hasRock);
        Debug.Log(hasStick);
        Debug.Log(hasSomething);
        if (hasStick || hasRock || hasSomething) //set anim for item that is being held (if there is one)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleWithRock", hasRock);
            anim.SetBool("IdleWithStick", hasStick);
            anim.SetBool("IdleWithSomething", hasSomething);
        }
        else //normal stuff
        {
            anim.SetBool("IdleWithRock", hasRock);
            anim.SetBool("IdleWithStick", hasStick);
            anim.SetBool("IdleWithSomething", hasSomething);
            anim.SetBool("Idle", true);
        }
    }

    public void SetWalking(bool hasRock, bool hasStick, bool hasSomething)
    {
        Debug.Log("I am walking");
        Debug.Log(hasRock);
        Debug.Log(hasStick);
        Debug.Log(hasSomething);
        if (hasStick || hasRock || hasSomething) //set anim for item that is being held (if there is one)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkWithRock", hasRock);
            anim.SetBool("WalkWithStick", hasStick);
            anim.SetBool("WalkWithSomething", hasSomething);
        }
        else //normal stuff
        {
            anim.SetBool("WalkWithRock", hasRock);
            anim.SetBool("WalkWithStick", hasStick);
            anim.SetBool("WalkWithSomething", hasSomething);
            anim.SetBool("Walk", true);
        }
    }

    public void SetRunning(bool hasRock, bool hasStick, bool hasSomething)
    {
        Debug.Log("I am running");
        Debug.Log(hasRock);
        Debug.Log(hasStick);
        Debug.Log(hasSomething);

        if (hasStick || hasRock || hasSomething) //set anim for item that is being held (if there is one)
        {
            anim.SetBool("Run", false);
            anim.SetBool("RunWithRock", hasRock);
            anim.SetBool("RunWithStick", hasStick);
            anim.SetBool("RunWithSomething", hasSomething);
        }
        else //normal stuff
        {
            anim.SetBool("RunWithRock", hasRock);
            anim.SetBool("RunWithStick", hasStick);
            anim.SetBool("RunWithSomething", hasSomething);
            anim.SetBool("Run", true);
        }
    }

    public void SetDamage(bool hasRock, bool hasStick, bool hasSomething)
    {
        Debug.Log("Ouch");
        Debug.Log(hasRock);
        Debug.Log(hasStick);
        Debug.Log(hasSomething);
        if (hasStick || hasRock || hasSomething) //set anim for item that is being held (if there is one)
        {
            anim.SetBool("Hurt", false);
            anim.SetBool("HurtWithRock", hasRock);
            anim.SetBool("HurtWithStick", hasStick);
            anim.SetBool("HurtWithSomething", hasSomething);
        }
        else //normal stuff
        {
            anim.SetBool("HurtWithRock", hasRock);
            anim.SetBool("HurtWithStick", hasStick);
            anim.SetBool("HurtWithSomething", hasSomething);
            anim.SetBool("Hurt", true);
        }
    }
    public void Attack(bool hasStick, bool hasRock) //must have stick or rock
    {
        Debug.Log("I am attacking");
        if (hasStick) //use stick attack anim
        {
            anim.SetTrigger("StickAttack");
        } else if (hasRock) //use rock attack anim
        {
            anim.SetTrigger("RockAttack");
        }
        else
        {
            //not sure what to do if attack is called and they dont contain the attack stuff
        }
    } //must disable afterwards

    public void SetGrounded(bool grounded) //make chnages if holding item
    {
        anim.SetBool("IsGrounded", grounded);
    }

    /**
     * GAMEPLAY FUNCTIONALITY
     */
    public void RotateTowardsMovement(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        rotationObject.transform.rotation = Quaternion.Lerp(rotationObject.transform.rotation, rotation, Time.time * rotationSpeed);
    }

    

    public void SetSpeed(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            Vector3 modifiedVelocityVector = new Vector3(cc.velocity.x, 0, cc.velocity.z);
            SetSpeed(modifiedVelocityVector.magnitude);
            if(modifiedVelocityVector.magnitude > 0)
            {
                RotateTowardsMovement(modifiedVelocityVector);
            }
            if (Input.GetMouseButtonDown(0) && (PlayerMovement.hasStick || PlayerMovement.hasRock))
            {
                Attack(PlayerMovement.hasStick, PlayerMovement.hasRock);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                SetRunning(PlayerMovement.hasRock, PlayerMovement.hasStick, PlayerMovement.hasSomething);
            }
            if (Input.GetKeyDown(KeyCode.Space) && PlayerMovement.isJumping)
            {
                //do jump animation until it hits ground
            }
            else if (Mathf.Round(cc.velocity.x) > 0 || Mathf.Round(cc.velocity.z) > 0) //is moving
            {
                SetWalking(PlayerMovement.hasRock, PlayerMovement.hasStick, PlayerMovement.hasSomething);
            }
            else
            {
                SetIdle(PlayerMovement.hasRock, PlayerMovement.hasStick, PlayerMovement.hasSomething);
            }
        }
    }
    private void FixedUpdate()
    {
        print(cc.velocity.y);
        SetGrounded(Mathf.Abs(cc.velocity.y) <= 0.5f);
    }
}
