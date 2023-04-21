using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    private Vector2 UserMouseInput;
    private Vector3 UserMovementInput;
    private Vector3 Velocity;
    private float xRotation;
    public bool rotateObject = false;
    public bool objectRotated = false;
    public bool ghostMode = true;


    [SerializeField] private Transform UserCam;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float sentitivity;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        UserMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        UserMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (ghostMode)
        {
            MoveUserCamera();
        }
        userInputMeasurementView();
    }
    /*
    private void MoveUser()
    {
        Vector3 MoveVector = transform.TransformDirection(UserMovementInput);

        if (Input.GetKey(KeyCode.Space))
        {
            Velocity.y = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Velocity.y = -1f;
        }

        controller.Move(MoveVector * speed * Time.deltaTime);
        controller.Move(Velocity * speed * Time.deltaTime);

        Velocity.y = 0f;
    }
    */
    private void userInputMeasurementView()
    {
        if (Input.GetKey(KeyCode.E) && rotateObject == false)
        {
            rotateObject = true;
            Debug.Log("rotated e");
        }
        else if (Input.GetKey(KeyCode.Q) && rotateObject == true)
        {
            rotateObject = false;
            Debug.Log("rotated q");
        }
        if(Input.GetKey(KeyCode.R))
        {
            ghostMode = true;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
    private void MoveUserCamera()
    {
        xRotation -= UserMouseInput.y * sentitivity;
        transform.Rotate(0f, UserMouseInput.x * sentitivity, 0f);
        UserCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
