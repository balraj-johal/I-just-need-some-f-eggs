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
    bool coolingDown = false;
    [SerializeField] bool isPlayerDashing = false;
    [SerializeField] float dashDistance = 4.0f;
    [SerializeField] float dashTime = 1.0f;
    [SerializeField] float dashDuration = 1.0f;
    [SerializeField] float dashCooldown = 2.0f;
    [SerializeField] float dashCooldownINIT = 2.0f;
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
    Vector2 currentDirMove = Vector2.zero;
    float velocityY = 0.0f;

    bool lockCursor = true;

    //CAMERA
    float FOV;
    float initFOV;
    [SerializeField] float fovChangeTo = 55f;
    [SerializeField] float fovChangeSpeed = 50f;
    [SerializeField] float fovChangeSpeedINIT = 50f;
    [SerializeField] float fovChangeDuration = 2f;

    //WEAPON CODE
    private string currentWeapon = "None";

    //allow good player referencing
    public static PlayerController instance {
        get; private set;
    }
    void OnEnable () {
        instance = this;
    } 	
    void OnDisable () {
        instance = null;
    }

    void Start() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller = GetComponent<CharacterController>();
        }
        FOV = Camera.main.fieldOfView;
        initFOV = FOV;
    }

    // Update is called once per frame
    void Update() {
        UpdateCamera();
        UpdateMovement();
    }

    void UpdateCamera() {
        //rotate player obj around y axis
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(Vector3.up * mouseDelta.x * senseX);
        
        //rotate around x axis
        cameraPitch -= mouseDelta.y * senseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -87f, 87f);
        playerCamera.localEulerAngles = cameraPitch * Vector3.right;

        //FOV
        Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, FOV, Time.deltaTime * fovChangeSpeed);
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
            if (!isPlayerDashing && !coolingDown) {
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
        } else if (coolingDown) {
            dashCooldown -= Time.deltaTime;
            if (dashCooldown <= 0) {
                coolingDown = false;
                dashCooldown = dashCooldownINIT;
            }
            // //print(dashInputDirection + " " + dashInputDirection.x);
            // // Vector2 currentDirDash = Vector2.SmoothDamp(currentDir, dashInputDirection*(dashCooldown/2f), ref currentVelocity, dashSmoothTime);
            // Vector3 velocityDashOver = dashCooldown * moveSpeed * (dashWorldF * dashInputDirection.y + dashWorldR * dashInputDirection.x) / 15.0f;
            // // currentDir = Vector2.SmoothDamp(currentDir, targetVec + dashInputDirection*(dashCooldown/100f), ref currentVelocity, smoothTime);
            currentDir = Vector2.SmoothDamp(currentDir, targetVec, ref currentVelocity, smoothTime);
            Vector3 velocityInputMove = moveSpeed * (transform.forward * currentDir.y + transform.right * currentDir.x) + Vector3.up * velocityY;
            controller.Move((velocityInputMove) * Time.deltaTime);
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
            coolingDown = true;
        } else {
            dashTime -= Time.deltaTime;
            currentDir = Vector2.SmoothDamp(currentDir, dashInputDirection, ref currentVelocity, dashSmoothTime); //dash movement
            Vector3 velocity = moveSpeed * (dashWorldF * currentDir.y + dashWorldR * currentDir.x) + Vector3.up * velocityY; //add vectors for strafe and forward & gravity
            controller.Move(velocity * Time.deltaTime);
        }
    }

    IEnumerator BlipFOV(float time) {
        SetFOV(fovChangeTo);
        fovChangeSpeed = fovChangeSpeedINIT;
        yield return new WaitForSeconds(time);
        SetFOV(initFOV);
        fovChangeSpeed = fovChangeSpeedINIT * 0.5f;
    }
    void SetFOV(float fovNew) {
        FOV = fovNew;
    }

    public void ChangeWeapon(string newWeapon) {
        currentWeapon = newWeapon;
    }

}
