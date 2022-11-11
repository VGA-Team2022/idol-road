using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary> ファン(エネミー)の挙動のクラス </summary>
public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    float _waitingTime = 2.0f;
    [SerializeField]
    bool isOrder = false;
    [SerializeField]
    bool isBurst = false;

    [SerializeField]
    private ServiceRequest _requestService;


    private float _timePassed = 0f;
    public ServiceRequest ServiceRequest
    {
        get { return _requestService; }
        set { _requestService = value; }
    }
    public bool IsOrder
    {
        get { return isOrder; }
        set { isOrder = value; }
    }

    void Start()
    {
        ServiceRequest = (ServiceRequest)Random.Range(1, 4);
        Debug.Log($"ファンの要求:{ ServiceRequest} ");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOrder&&_timePassed > _waitingTime)
        {
            ServiceThrough();
        }

        if (isBurst)
        {
            EnemyBurst();
        }

        if (isOrder)
        {
            //入力を判定する

        }
    }

    private void FixedUpdate()
    {

        _timePassed += Time.deltaTime;

    }

    //このファンの要求とアクションが一致した時の処理
    public void ServiceSuccess()
    {
        isBurst = true;
        Debug.Log("Yes!");
    }
    //このファンの要求とアクションが違う時の処理
    public void ServiceFail()
    {
        Debug.Log("Miss!");
    }
    //アクションがされなかった時の処理
    public void ServiceThrough()
    {
        Debug.Log("oh...");
        Destroy(this);
    }


    //ファンが吹っ飛ぶように見せる処理
    public void EnemyBurst()
    {
        transform.Rotate(new(0,0,1));
        transform.localScale = new(transform.localScale.x-0.1f, transform.localScale.y - 0.1f,0);
        //transform.position = ;
    }
}

public enum ServiceRequest
{
    None = 0,
    Pose = 1,
    Sign = 2,
    Wink = 3,
    Kiss = 4,
}
