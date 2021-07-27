using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public GameObject rootManager;
    IKManager manager;

    public Transform legRoot;
    private Vector3 currentPosition;
    private Vector3 newPosition;
    private Vector3 oldPosition;
    public float stepDistance;
    public float stepHeight;
    public float speed;
    private float lerp;

    public bool canMove = false;
    public bool moving = false;

    public float stepOffset = 0.7f;

    void Start()
    {
        // init positon of target
        currentPosition = transform.position;
        oldPosition = currentPosition;

        // get IK manager script ref
        manager = rootManager.GetComponent<IKManager>();
    }

    void Update()
    {
        // update position of target
        transform.position = currentPosition;

        if (canMove) {
            // RAYCAST down from leg root, compare distance from target, step if
            // distance > stepDistance
            Ray ray = new Ray(legRoot.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit info, 10)) { //if ray hits
                if (Vector3.Distance(newPosition, info.point) > stepDistance) {
                    lerp = 0f; //init position interpolation
                    newPosition = info.point + new Vector3(0, 0, -stepOffset); //set target position
                }
            }

            
            // smoothly interpolate if we need to step
            if (lerp < 1f) {
                moving = true;
                Vector3 pos = Vector3.Lerp(oldPosition, newPosition, lerp); // lerp
                pos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight; // add vertical curve
                currentPosition = pos; // update position

                lerp += Time.deltaTime * speed;
            } else {
                moving = false;
                oldPosition = newPosition;

                manager.ToggleMovingLeg();
            }
        }

    }
}
