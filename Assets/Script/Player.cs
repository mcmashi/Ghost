using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    #region 定義

    //マウス感度
    public float MouseSensitivity = 0.01f;

    #endregion

    #region 公開

    [SerializeField]
    float Mspeed = 3.0f;
    [SerializeField]
    float Rspeed = 3.0f;


    #endregion

    #region 非公開

    CharacterController characterController;

    Camera cam;


    #endregion


    // Use this for initialization
    void Start () {

        //コンポーネント取得
        characterController = this.GetComponent<CharacterController>();
        cam = Camera.main;

        //初期化

	}
	
	// Update is called once per frame
	void Update () {
        //移動
        Move();

        //回転
        Rotate();
    }

    private void Move()
    {
        //入力受付
        float InputH = Input.GetAxis("Horizontal");
        float InputV = Input.GetAxis("Vertical");

        var camobj = cam.gameObject;

        var vec1 = Vector3.zero;
        var vec2 = Vector3.zero;
        var velocity = Vector3.zero;

        if (Mathf.Abs(InputH) <= 0.5f && Mathf.Abs(InputV) <= 0.5f)
        {
            characterController.Move(velocity);
            return;
        }

        //左右
        if (Mathf.Abs(InputH) != 0.0f)
        {

             vec1 = camobj.transform.right;

            vec1 = Vector3.Normalize(vec1);

            vec1 *= InputH;

        }

        //前方
        if (Mathf.Abs(InputV) != 0.0f)
        {

             vec2 = camobj.transform.forward;

            vec2 = Vector3.Normalize(vec2);

            vec2 *= InputV;

        }

        var vec3 = Vector3.Normalize(vec1 + vec2);

        vec3.y = 0.0f;

        velocity = vec3 * Mspeed * Time.deltaTime;

        characterController.Move(velocity);


    }

    private void Rotate()
    {
        //カメラなし
        if (cam == null) return;

        var camobj = cam.gameObject;

        var mdeltax = Input.GetAxis("Mouse X");

        var mdeltay = Input.GetAxis("Mouse Y");

        //前回との差が小さければ無視
        if (Mathf.Abs(mdeltax) <= 0.01f || Mathf.Abs(mdeltay) <= 0.01f) return;

        mdeltax *= MouseSensitivity;

        mdeltay *= MouseSensitivity;

        var localplr = this.transform.localEulerAngles;

        localplr.y += mdeltax;

        this.transform.localEulerAngles = localplr;

        //this.transform.Rotate(0.0f, mdeltax, 0.0f);

        var localcamr = camobj.transform.localEulerAngles;

        localcamr.x += -mdeltay;

        localcamr.z = 0.0f;

        localcamr.y = 0.0f;

        //クランプ
        Mathf.Clamp(localcamr.x, -80.0f, 80.0f);

        camobj.transform.localEulerAngles = localcamr;





    }

}

