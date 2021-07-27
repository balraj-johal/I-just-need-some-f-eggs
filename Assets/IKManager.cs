using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    public GameObject leftTarget;
    public GameObject rightTarget;

    IKFootSolver leftSolver;
    IKFootSolver rightSolver;

    string whichLegMoving;

    public float testMoveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        leftSolver = leftTarget.GetComponent<IKFootSolver>();
        rightSolver = rightTarget.GetComponent<IKFootSolver>();

        whichLegMoving = "left";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0.0f, 0f, -testMoveSpeed);
        if (whichLegMoving == "left") {
            leftSolver.canMove = true;
            rightSolver.canMove = false;
        } else {
            rightSolver.canMove = true;
            leftSolver.canMove = false;
        }
    }

    public void ToggleMovingLeg()
    {
        if (whichLegMoving == "left") {
            whichLegMoving = "right";
        } else {
            whichLegMoving = "left";
        }

    }
}