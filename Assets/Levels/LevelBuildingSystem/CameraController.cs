using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Vector2 Limit;
    public float MinZoom, MaxZoom;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.x += Input.GetAxis("Horizontal") * .3f;
        pos.y += -scroll * 5f;
        pos.z += Input.GetAxis("Vertical") * .3f;

        pos.y = Mathf.Clamp(pos.y, MinZoom, MaxZoom);

        /*Camera.main.fieldOfView -= scroll*20;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView,MinZoom,MaxZoom);*/

        pos.x = Mathf.Clamp(pos.x, -Limit.x, Limit.x);
        pos.z = Mathf.Clamp(pos.z, -Limit.y, Limit.y);
        transform.position = pos;
    }
}