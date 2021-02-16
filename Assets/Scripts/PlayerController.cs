using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 3f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float m_jumpPower = 7f;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float m_isGroundedLength = 0.1f;

    Animator m_anim = null;
    Rigidbody m_rb = null;

    bool m_jumpJudge = true;
    bool m_greenPlatformRelocation = false;

    GameObject switchObject;
    Transform redPlatform;
    Transform bluePlatform;
    Vector3 reSpawnPoint;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        switchObject = GameObject.Find("JumpSwitchManager");
        redPlatform = switchObject.transform.Find("Red");
        bluePlatform = switchObject.transform.Find("Blue");
        reSpawnPoint = Vector3.up;
    }

    void Update()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力方向のベクトルを組み立てる
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

            // 入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);  // Slerp を使うのがポイント

            Vector3 velo = dir.normalized * m_movingSpeed; // 入力した方向に移動する
            velo.y = m_rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            m_rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
            m_anim.SetTrigger("Jump");
            JumpSwitchPlatform();
        }
    }
    /// <summary>
    /// Update の後に呼び出される。Update の結果を元に何かをしたい時に使う。
    /// </summary>
    void LateUpdate()
    {
        // 水平方向の速度を求めて Animator Controller のパラメーターに渡す
        Vector3 horizontalVelocity = m_rb.velocity;
        horizontalVelocity.y = 0;
        m_anim.SetFloat("Speed", horizontalVelocity.magnitude);
    }

    /// <summary>
    /// 地面に接触しているか判定する
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        Vector3 start = this.transform.position;   // start: オブジェクトの中心
        Vector3 end = start + Vector3.down * m_isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }
    /// <summary>
    /// プレイヤーのジャンプに対応してオンオフを切り替える足場の処理
    /// </summary>
    void JumpSwitchPlatform()
    {
        if (m_jumpJudge) //一回目は赤がオフ、青がオンになる
        {
            redPlatform.gameObject.SetActive(false);
            bluePlatform.gameObject.SetActive(true);
            m_jumpJudge = false;
        }
        else             //二回目は青がオフ、赤がオンになる
        {
            redPlatform.gameObject.SetActive(true);
            bluePlatform.gameObject.SetActive(false);
            m_jumpJudge = true;
        }
    }
    /// <summary>
    /// プレイヤーが何らかの理由で、初期地点に飛ばされる処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FallJudge" || other.gameObject.tag == "BlackObject")
        {
            m_jumpJudge = true;
            redPlatform.gameObject.SetActive(true);
            bluePlatform.gameObject.SetActive(false);
            transform.position = reSpawnPoint;
            m_greenPlatformRelocation = true;
        }
    }
    /// <summary>
    /// 緑色の足場を再配置する処理
    /// </summary>
    /// <returns></returns>
    public bool GreenPlatformRelocation()
    {
        if (m_greenPlatformRelocation)
        {
            m_greenPlatformRelocation = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
