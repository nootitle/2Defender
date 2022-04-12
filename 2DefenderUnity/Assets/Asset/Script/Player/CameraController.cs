using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera CameraObject;
    public float X;
    public float Y;
    public float W;
    public float H;
    public void setX(float value) { X += value; }
    private Vector3[] FrustumLine = new Vector3[4];

    public GameObject Target;

    private void Awake()
    {
        CameraObject = GetComponent<Camera>();
    }

    void Start()
    {
        Y = 0.5f;
        W = 1.0f;       
    }

    void Update()
    {       
        CameraObject.CalculateFrustumCorners(
            new Rect(X, Y, W, H),
            CameraObject.farClipPlane,
            Camera.MonoOrStereoscopicEye.Mono,
            FrustumLine);
        
        for (int i = 0; i < 4; ++i)
            Debug.DrawLine(transform.position, FrustumLine[i], Color.red);

        /*
        if(Input.GetMouseButton(0))
        {
            //Target.transform.eulerAngles += new Vector3(0.0f, Input.GetAxis("Mouse X") * 50.0f, 0.0f);
            CameraObject.transform.RotateAround(Target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 500.0f * Time.deltaTime);
            CameraObject.transform.LookAt(Target.transform);
            Target.transform.rotation = CameraObject.transform.rotation;
            //참고 함수 : Quaternion.LookRotation(Target.transform.position - this.transform.position);

            Vector3 CurrentRotate = transform.rotation.eulerAngles;

            //CurrentRotate.y += Input.GetAxis("Mouse X") * 50.0f;

            Quaternion CurrQu = Quaternion.Euler(CurrentRotate);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                CurrQu, 
                SmoothTime * CameraSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(Target.transform.forward),
                SmoothTime * CameraSpeed * Time.deltaTime);
        */
        /*
        Movement = Target.transform.position + transform.forward * -20.0f + Offset;
        transform.position = Vector3.Lerp(
            transform.position,
             Movement,
            SmoothTime * CameraSpeed * Time.deltaTime);      
        */
    }

    public Vector2 GetCameraEdge(int dir)
    {
        if (dir == 0)
        {
            return FrustumLine[3];
        }
        else if(dir == 1)
        {
            return FrustumLine[0];
        }
        else
        {
            return FrustumLine[3];
        }
    }
}
