using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb; // 角色的刚体组件
    Animator animator; // 角色的动画组件
    AudioSource audioSource; // 音频源组件
    public GameObject blood; // 血液特效对象
    // public AudioClip running; // 跑步音效
    public AudioClip jumping; // 跳跃音效
    public AudioClip killing; // 踩怪音效
    public ParticleSystem playerPS; // 粒子特效系统
    public float PlayerSpeed = 5f; // 角色移动速度
    [Range(1, 10)]
    public float jumpSpeed = 5f; // 跳跃速度
    public bool isGrounded; // 是否在地面上
    public bool isCeiling; // 是否碰到天花板
    public Transform ceilingCheck; // 天花板检查点
    public Transform groundCheck; // 地面检查点
    public LayerMask ground; // 地面层
    public float fallAddition = 3.5f; // 下落时的额外重力
    public float jumpAddition = 1.5f; // 跳跃时的额外重力
    private float moveX; // 水平移动量
    private bool _facingRight; // 角色朝向
    private bool moveJump; // 是否尝试跳跃
    private bool jumpHold; // 是否按住跳跃键
    private bool isJump; // 是否正在跳跃
    private float jumpCount = 0; // 跳跃计数
    private Transform killPoint; // 杀敌点
    private enum PlayerState { idle, run, jump, fall, hit }; // 角色状态枚举

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取刚体组件
        animator = GetComponent<Animator>(); // 获取动画组件
        audioSource = GetComponent<AudioSource>(); // 获取音频组件
        killPoint = transform.Find("killPoint"); // 查找杀敌点
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 处理移动
        moveJump = Input.GetButtonDown("Jump"); // 检测跳跃按键按下
        jumpHold = Input.GetButton("Jump"); // 检测跳跃按键按住

        if (moveJump && jumpCount > 0)
        {
            isJump = true; // 设置跳跃状态
            PPS(); // 播放粒子特效
        }
    }
    // ����
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground); // 检查是否在地面上
        isCeiling = Physics2D.OverlapCircle(ceilingCheck.position, 0.1f, ground); // 检查是否碰到天花板
        Move(); // 处理移动
        Jump(); // 处理跳跃
        PlayerAnim(); // 更新动画
        Enemy(); // 处理敌人检测
    }

    // 移动
    private void Move()
    {
        moveX = Input.GetAxis("Horizontal"); // 获取水平输入
        rb.velocity = new Vector2(moveX * PlayerSpeed, rb.velocity.y); // 设置刚体速度

        if (_facingRight && moveX > 0)
        {
            Flip(); // 向左翻转
        }
        else if (!_facingRight && moveX < 0)
        {
            Flip(); // 向右翻转
        }
    }

    // 角色翻转
    private void Flip()
    {
        // audioSource.PlayOneShot(running); // 播放跑步音效
        PPS(); // 播放粒子特效
        _facingRight = !_facingRight; // 切换朝向
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale; // 翻转角色

    }

    private void Jump()
    {
        if (isGrounded)
        {
            jumpCount = 2; // 如果在地面上，重置跳跃次数
        }

        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); // 施加向上的冲力
            audioSource.PlayOneShot(jumping); // 播放跳跃音效
            jumpCount--; // 减少跳跃次数
            isJump = false; // 重置跳跃状态
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallAddition; // 下落时增加重力
        }
        else if (rb.velocity.y > 0 && !jumpHold)
        {
            rb.gravityScale = jumpAddition; // 跳跃时增加重力
        }
        else
        {
            rb.gravityScale = 1f; // 正常重力
        }
    }

    private void Enemy()
    {
        //Collider2D enemy =Physics2D.OverlapCircle(killPoint.position,0.15f,LayerMask.GetMask("Enemy"));//1、原点的位置2、半径3、图层
        // 1、原点的位置 2、Vector2 长宽 3、旋转的角度 4、图层
        Collider2D enemy = Physics2D.OverlapBox(killPoint.position, new Vector3(0.3f, 0.3f, 0f), 0f, LayerMask.GetMask("Enemy")); // 检测敌人
        if (enemy != null)
        {
            audioSource.PlayOneShot(killing); // 播放跳跃音效
            Instantiate(blood, transform.position, Quaternion.identity); // 实例化血液特效
            Destroy(enemy.gameObject, 0.2f); // 销毁敌人
            rb.velocity = new Vector2(rb.velocity.x, 0f); // 停止角色的垂直速度
            rb.AddForce(new Vector2(0, 300f)); // 施加向上的冲力
        }
    }

    void PPS()
    {
        if (playerPS != null)
        {
            playerPS.Play(); // 播放粒子特效
        }
    }


    // 播放动画
    void PlayerAnim()
    {
        PlayerState states; // 定义角色状态
        if (Mathf.Abs(moveX) > 0)
        {
            states = PlayerState.run; // 移动时设置为跑步状态
        }
        else
        {
            states = PlayerState.idle; // 不移动时设置为空闲状态
        }

        if (rb.velocity.y > 0.1f)
        {
            states = PlayerState.jump; // 向上跳跃时设置为跳跃状态
        }
        else if (rb.velocity.y < -0.1f)
        {
            states = PlayerState.fall; // 向下落时设置为下落状态
        }

        if (isCeiling)
        {
            states = PlayerState.hit; // 碰到天花板时设置为受击状态
        }

        animator.SetInteger("state", ((int)states)); // 更新动画状态
    }
}
