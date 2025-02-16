using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Animator animator;
    Quaternion targetRotation;
    Rigidbody rb;

    public float moveSpeed = 3f; // 移動速度
    public float jumpForce = 5f; // ジャンプ力
    private bool isGrounded; // 地面にいるか判定
    
    void Awake()
    {
        TryGetComponent(out animator);
        rb = GetComponent<Rigidbody>(); // Rigidbody を取得
        targetRotation = transform.rotation;
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
       
        // カメラの向きを基準に移動方向を変換
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Y軸の影響を無視（水平移動のみにする）
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();
       
        Vector3 velocity = (cameraForward * vertical + cameraRight * horizontal).normalized;
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = 600 * Time.deltaTime;

        // 移動方向がある場合のみ回転
        if (velocity.magnitude > 0.1f)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            // 前進の処理
            transform.position += transform.forward * moveSpeed * speed * Time.deltaTime;
        }

        // ジャンプの処理
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //ジャンプアニメーション開始
            animator.SetBool("Jump", true);
            //地面の判定をfalse
            isGrounded = false;
        }

        // 移動速度をアニメーターに反映
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
    }

    void FixedUpdate()
    {
        CheckGrounded();
    }

    // 地面判定の判定
    void CheckGrounded()
    {
        RaycastHit hit;
        float rayDistance = 0.2f;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            if(!isGrounded)
            {
            isGrounded = true;
            animator.SetBool("Jump", false); // 着地時にジャンプアニメーションを解除
            }
        }
        else
        {
            isGrounded = false;
        }
    }
}
