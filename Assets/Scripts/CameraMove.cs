using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform _target;
    public Vector2 offset = new Vector2(2f, 1f);

    int prevPos;
    bool targetGoLeft;
    // Start is called before the first frame update
    void Start()
    {
        prevPos = Mathf.RoundToInt(_target.position.x);
        transform.position = new Vector3(_target.position.x + offset.x, _target.position.y + offset.y, transform.position.z);
    }

    private void LateUpdate()
    {
        int curPos = Mathf.RoundToInt(_target.position.x);
        if (curPos > prevPos)
        {
            targetGoLeft = false;
        }
        if(curPos < prevPos)
        {
            targetGoLeft = true;
        }
        prevPos = Mathf.RoundToInt(_target.position.x);
        Vector3 cameraTarget;
        cameraTarget = new Vector3(targetGoLeft ? _target.position.x - offset.x : _target.position.x + offset.x, _target.position.y + offset.y, transform.position.z);
        Vector3 currentPosition = Vector3.Lerp(transform.position, cameraTarget, 1.5f * Time.deltaTime);
        transform.position = currentPosition;
    }
}
