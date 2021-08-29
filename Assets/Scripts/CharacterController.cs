using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerInputSpace = default; // 输入空间，相对这个坐标做移动
    [SerializeField] 
    private Transform Target;
    [SerializeField, Range(0f, 100f)] 
    private float MaxSpeed = 10;
    [SerializeField, Range(0f, 100f)]
    private float MaxAcceleration = 10;
    [SerializeField, Range(0f, 1)]
    private float Bounciess = 0.5f;
    [SerializeField]
    private Rect AllowArea = new Rect(-30, -30, 30, 30);
    private Vector2 playerInput;
    private Vector3 velocity;
    private Vector3 desiredVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null){
            return;
        }
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        if (playerInput.Equals(Vector2.zero))        {
            return;
        }
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        if (PlayerInputSpace) // 相对于目标空间的移动
        {
            Vector3 forward = PlayerInputSpace.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = PlayerInputSpace.right;
            right.y = 0;
            right.Normalize();
            desiredVelocity = (forward * playerInput.y + right * playerInput.x) * MaxSpeed;
        }
        else // 相对世界的移动
        {
            desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * MaxSpeed;
        }
        // 使用加速度平滑移动效果
        float maxSpeedChange = MaxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 newPosition = Target.localPosition + velocity * Time.deltaTime;
        // 移动范围限制
        if (!AllowArea.Contains(new Vector2(newPosition.x, newPosition.z))){
            // newPosition.x = Mathf.Clamp(newPosition.x, AllowArea.xMin, AllowArea.xMax);
            // newPosition.z = Mathf.Clamp(newPosition.z, AllowArea.yMin, AllowArea.yMax);
            if (newPosition.x < AllowArea.xMin){
                newPosition.x = AllowArea.xMin;
                velocity.x = -velocity.x * Bounciess;
            }
            if (newPosition.x > AllowArea.xMax){
                newPosition.x = AllowArea.xMax;
                velocity.x = -velocity.x * Bounciess;
            }
            if (newPosition.z < AllowArea.yMin){
                newPosition.z = AllowArea.yMin;
                velocity.z = -velocity.z * Bounciess;
            }
            if (newPosition.z > AllowArea.yMax){
                newPosition.z = AllowArea.yMax;
                velocity.z = -velocity.z * Bounciess;
            }
        }
        Target.localPosition = newPosition;
    }
}
