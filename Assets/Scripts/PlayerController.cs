using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float senseX = 3.5f;
    [SerializeField] float senseY = 3.5f;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float smoothTime = 0.2f;
    [SerializeField] float grav = -13.2f;

    //dash variables
    [SerializeField] bool isPlayerDashing = false;
    [SerializeField] float dashDistance = 4.0f;
    [SerializeField] float dashTime = 1.0f;
    [SerializeField] float dashDuration = 1.0f;
    [SerializeField] float dashCooldown = 2.0f;
    [SerializeField] float dashSmoothTime = 0.2f;
    [SerializeField] float dashFalloff = 0.2f;
    float dashSpeed;
    Vector2 dashInputDirection;
    Vector3 dashWorldF;
    Vector3 dashWorldR;

    float cameraPitch = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;
    float velocityY = 0.0f;

    bool lockCursor = true;
    // Start is called before the first frame update

    //CAMERA
    float FOV;
    float initFOV;
    [SerializeField] float fovChangeTo = 55f;
    [SerializeField] float fovChangeSpeed = 50f;
    [SerializeField] float fovChangeSpeedINIT = 50f;
    [SerializeField] float fovChangeDuration = 2f;



    void Start()
    {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller = GetComponent<CharacterController>();
        }
        FOV = Camera.main.fieldOfView;
        initFOV = FOV;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
        UpdateMovement();
    }

    void UpdateCamera()
    {
        //rotate player obj around y axis
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(Vector3.up * mouseDelta.x * senseX);
        
        //rotate around x axis
        cameraPitch -= mouseDelta.y * senseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -87f, 87f);
        playerCamera.localEulerAngles = cameraPitch * Vector3.right;

        //FOV
        Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, FOV, Time.deltaTime * fovChangeSpeed);
        print("fov: " + fovChangeSpeedINIT + " , fovchangespeed: " + fovChangeSpeed);
    }

    void UpdateMovement() {
        //GET INPUT DIRECTION
        Vector2 targetVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetVec.Normalize();

        //gravity influence
        if (controller.isGrounded) {
            velocityY = 0.0f;
        }
        velocityY += grav * Time.deltaTime;
 
        //DASH CHECK
        if (Input.GetKeyDown("space")) {
            if (!isPlayerDashing) {
                isPlayerDashing = true;
                dashInputDirection = targetVec * dashDistance;
                dashWorldF = transform.forward;
                dashWorldR = transform.right;
                StartCoroutine(BlipFOV(fovChangeDuration));
            }
        }

        //MOVE IT
        if (isPlayerDashing) {
            DashMove();
        } else {
        //smoothly transition movement
        currentDir = Vector2.SmoothDamp(currentDir, targetVec, ref currentVelocity, smoothTime);
        //add vectors for strafe and forward back to get total velocity
        Vector3 velocity = moveSpeed * (transform.forward * currentDir.y + transform.right * currentDir.x) + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        }

    }

    void DashMove() {
        if (dashTime <= 0) {
            isPlayerDashing = false;
            dashTime = dashDuration;
        } else {
            dashTime -= Time.deltaTime;
            //dash movement
            currentDir = Vector2.SmoothDamp(currentDir, dashInputDirection, ref currentVelocity, dashSmoothTime);
            //add vectors for strafe and forward back to get total velocity
            Vector3 velocity = moveSpeed * (dashWorldF * currentDir.y + dashWorldR * currentDir.x) + Vector3.up * velocityY;
            controller.Move(velocity * Time.deltaTime);
        }
    }
    void SetFOV(float fovNew) {
        FOV = fovNew;
    }
    IEnumerator BlipFOV(float time)
    {
        SetFOV(fovChangeTo);
        fovChangeSpeed = fovChangeSpeedINIT;
        yield return new WaitForSeconds(time);
        SetFOV(initFOV);
        fovChangeSpeed = fovChangeSpeedINIT * 0.5f;
    }
}
