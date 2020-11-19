using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
   
    public GameObject levelPlane;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        // pos.x += Input.GetAxis("Horizontal") * .3f;
        pos.y += -scroll * 5f;
        
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        
        transform.position = pos + Camera.main.transform.right * (Input.GetAxis("Horizontal") * .3f) + forward * (Input.GetAxis("Vertical") * .3f);

    }
}