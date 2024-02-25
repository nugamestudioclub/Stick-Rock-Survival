using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Basic Player Movement
 */
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;
    public float jumpHeight = 10;
    public float gravity = 9.81f;
    public float airControl = 10;
    public float sensitivity = 180f;
    public static bool isJumping = false;
    CharacterController controller;
    Vector3 input, moveDirection;
    [SerializeField]
    private Transform relDirObj;

    /**
     * For determining which animatiom to play (either noraml cylce, stick cycle or stone cycle (or other))
     */
    [Header("ItemChecker")]
    public static bool hasStick = false;
    public static bool hasRock = false;
    public static bool hasSomething = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //moves left and right
        float moveVertical = Input.GetAxis("Vertical"); //moves forward and backwards

        Vector3 right = new Vector3(relDirObj.transform.right.x, 0, relDirObj.transform.right.z);
        Vector3 forward = new Vector3(relDirObj.transform.forward.x, 0, relDirObj.transform.forward.z);

        input = (right * moveHorizontal + forward * moveVertical).normalized;

        input *= moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = input;

            if (Input.GetButton("Jump")) //jump
            {
                isJumping = true;
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                isJumping = false;
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime; //gravity

        controller.Move(moveDirection * Time.deltaTime); //applies movement to character controller
    }

    public static void collectStick()
    {
        hasStick = true;
    }

    public static void collectRock()
    {
        hasRock = true;
        //increment count if already has stick
    }

    public static void collectSoemthing()
    {
        hasSomething = true;
    }

    public static void dropStick()
    {
        //instatiate the stick back into the world
        hasStick = false;
    }

    public static void dropRock()
    {
        //instatiate the rock back into the world
        hasRock = false;
    }

    public static void dropSomething()
    {
        //instatiate the item back into the world
        hasSomething = false;
    }
}
