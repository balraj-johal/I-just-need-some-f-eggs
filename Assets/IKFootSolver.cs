using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public Transform legRoot;
    private Vector3 currentPosition;
    private Vector3 newPosition;
    private Vector3 oldPosition;
    public float stepDistance;
    public float stepHeight;
    public float speed;
    private float lerp;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        oldPosition = currentPosition; //?
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPosition;

        Ray ray = new Ray(legRoot.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10)) {
            if (Vector3.Distance(newPosition, info.point) > stepDistance) {
            // if (Vector3.Distance(transform.position, info.point) > stepDistance) {
                // currentPosition = info.point;
                print("STEP");
                lerp = 0f;
                newPosition = info.point;
                // print(newPosition);
            }
        }

        //smoothly interpolate if we need to step
        if (lerp < 1f) {
            // print("lerp: " + lerp);
            Vector3 pos = Vector3.Lerp(oldPosition, newPosition, lerp);
            //add vertical curve
            pos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = pos;
            lerp += Time.deltaTime * speed;
            // print("lerp end: " + lerp);
        } else {
            oldPosition = newPosition;
        }
    }
}
