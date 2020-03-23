using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 10;
    public float jumpForce=3;
    public float maxCameraEulerRotation = 20f;
    public float minCameraEulerRotation = -20f;
    [SerializeField]LayerMask groundDetectionMask;
    [SerializeField]Transform cameraAnchor;
    Rigidbody rigBody;
    Quaternion maxCameraAngle;
    Quaternion minCameraAngle;
    Vector3 moveVector;
    bool isInAir;
    bool paused;
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
        maxCameraAngle = Quaternion.Euler(maxCameraEulerRotation, 0, 0);
        minCameraAngle = Quaternion.Euler(minCameraEulerRotation, 0, 0);
        isInAir = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if(paused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
        }
        if (isInAir)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1.1f, groundDetectionMask))
            {
                isInAir = false;
            }
        }
        if (!paused)
        {
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))

                MoveCharacter();
            if (Input.GetAxis("Mouse X") != 0f)
            {
                RotatePlayer();
            }
            if (Input.GetAxis("Mouse Y") != 0f)
            {
                RotateCameraAnchor();
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
            {
                rigBody.AddForce(Vector3.up * 100 * jumpForce);
                isInAir = true;
            }
        }
        //Debug.DrawRay(transform.position, -transform.up * 1.1f, Color.red);
    }

    void MoveCharacter()
    {
        moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveVector += transform.forward;
        if (Input.GetKey(KeyCode.S))
            moveVector -= transform.forward;
        if (Input.GetKey(KeyCode.A))
            moveVector -= transform.right;
        if (Input.GetKey(KeyCode.D))
            moveVector += transform.right;

        rigBody.MovePosition(transform.position + moveVector.normalized * Time.deltaTime * moveSpeed);
    }
    void RotatePlayer()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0));
    }
    void RotateCameraAnchor()
    {
        cameraAnchor.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -rotationSpeed * Time.deltaTime, 0, 0));
        if (cameraAnchor.localRotation.x > maxCameraAngle.x)
            cameraAnchor.localRotation = maxCameraAngle;
        if (cameraAnchor.localRotation.x < minCameraAngle.x)
            cameraAnchor.localRotation = minCameraAngle;
    }
}
