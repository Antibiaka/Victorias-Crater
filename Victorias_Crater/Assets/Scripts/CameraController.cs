using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform playerBody, cameraTr, cameraTarget;

    private Vector3 locRotation,locCameraPos;

    private float cameraDist;
    public float mouseSens = 200f;
    public float speed = 3f;
    public float scrollSens = 2f;


    public float Y = 0.2f; 
    public float B = -0.2f;



    private void Start() {
        Cursor.lockState = CursorLockMode.Locked; // locklen while not in menu
    }
    // Start is called before the first frame update
    void Update() {
        locCameraPos = cameraTr.transform.position;

        cameraDist = Vector3.Distance(locCameraPos , cameraTarget.transform.position);

        ZoomInOut();
        
        
    }

    // Update is called once per frame
    void LateUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime * 1.3f;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        
        locRotation.x += mouseX;
        locRotation.y -= mouseY;
        locRotation.y = Mathf.Clamp(locRotation.y, 5f, 40f); //clamp the rotation to not allow flip  



        if (Input.GetKey(KeyCode.LeftAlt)) {
            cameraTarget.Rotate(Vector3.up * mouseX); //only rotate around x axis
            
        }
        else {
            cameraTarget.transform.localRotation = Quaternion.Euler(0f, 0f, 0f); //return camera to prev position before shift
            transform.localRotation = Quaternion.Euler(locRotation.y, 0f, 0f);  //updown rotations
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }




    void ZoomInOut() {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cameraDist > 4.5f) { //zoom in
        
            
            transform.Translate(0, B, 1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cameraDist < 13f) { //zooom out
                                                                           //cameraTr.transform.position = Quaternion.Euler(locCameraPos.x, locCameraPos.y, locCameraPos.z);
            
            transform.Translate(0, Y, -1);
        

        }
    }



}
