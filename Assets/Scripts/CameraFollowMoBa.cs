using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMoBa : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 10,5);
    [SerializeField] private float MinDistance = 5;
    [SerializeField] private float MaxDistance = 20;
    [SerializeField] private float RotateSpeed = 10f;
    [SerializeField] private float ScrollSpeed = 10f;
    [SerializeField] private float FollowSpeed = 10;
    private float distance = 10;
    private bool isRotating;
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        camera.position = target.position + offset;
        camera.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = Vector3.Lerp(targetPosition,target.position, FollowSpeed * Time.deltaTime);
        camera.position = targetPosition + offset;
        Scale();
        Rotate();
    }
    
    //缩放
    private void Scale()
    {
        distance = offset.magnitude;
        //向前滑动拉近 向后滑动拉远
        distance -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        distance = Mathf.Clamp(distance, MinDistance, MaxDistance);
        offset = offset.normalized * distance;
    }
    //左右上下移动
    private void Rotate()
    {
        //鼠标右键按下可以旋转视野
        if (Input.GetMouseButtonDown(1))
            isRotating = true;
        if (Input.GetMouseButtonUp(1))
            isRotating = false;

        if (isRotating)
        {
            Vector3 originalPosition = camera.position;
            Quaternion originalRotation = camera.rotation;
            camera.RotateAround(targetPosition, target.up, RotateSpeed * Input.GetAxis("Mouse X"));
            camera.RotateAround(targetPosition, camera.right, -RotateSpeed * Input.GetAxis("Mouse Y"));
            float x = camera.eulerAngles.x;
            //旋转的范围为10度到80度
            if (x < 10 || x > 80)
            {
                camera.position = originalPosition;
                camera.rotation = originalRotation;
            }

        }

        offset = camera.position - targetPosition;
    }
}
