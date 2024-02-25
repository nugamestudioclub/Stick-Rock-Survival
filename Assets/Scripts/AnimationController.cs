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
    public void SetSpeed(float speed)
    {
        anim.SetFloat("Speed", speed);
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
    public void SetHoldingRock(bool holding)
    {
        anim.SetBool("HasRock", holding);
    }

    public void RotateTowardsMovement(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        rotationObject.transform.rotation = Quaternion.Lerp(rotationObject.transform.rotation, rotation, Time.time * rotationSpeed);
    }

    public void SetGrounded(bool grounded)
    {
        anim.SetBool("IsGrounded",grounded);
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
           
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SetHoldingRock(!anim.GetBool("HasRock"));
            }
            
        }
    }
    private void FixedUpdate()
    {
        print(cc.velocity.y);
        SetGrounded(Mathf.Abs(cc.velocity.y) <= 0.5f);
    }
}
