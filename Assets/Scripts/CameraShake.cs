using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude) {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration) {
            float xDelta = Random.Range(-1f, 1f) * magnitude;
            xDelta = xDelta + originalPos.x;
            float yDelta = Random.Range(-1f, 1f) * magnitude;
            yDelta = yDelta + originalPos.y;
            transform.localPosition = new Vector3(xDelta, yDelta, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
            transform.localPosition = new Vector3(0f, 2f, 0f);
        }
    }

    public void ReturnCam() {
        transform.localPosition = new Vector3(0f, 2f, 0f);
    }
}
