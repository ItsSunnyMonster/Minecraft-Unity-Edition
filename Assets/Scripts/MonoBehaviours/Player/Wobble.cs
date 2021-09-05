using UnityEngine;

public class Wobble : MonoBehaviour {
    public float amount = 10.0f;
    public float speed = 1.0f;
    private Vector3 lastPos;
    private float dist = 0.0f;
 
    private void Start () {
        lastPos = transform.position;
    }
     
    private void LateUpdate () {
        dist += (transform.position - lastPos).magnitude;
        lastPos = transform.position;
        var rotation = transform.localEulerAngles;
        rotation.z = Mathf.Sin (dist * speed) * amount;
        transform.localEulerAngles = rotation;
    }
}