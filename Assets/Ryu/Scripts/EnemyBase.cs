using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary> �t�@��(�G�l�~�[)�̋����̃N���X </summary>
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
        Debug.Log($"�t�@���̗v��:{ ServiceRequest} ");
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
            //���͂𔻒肷��

        }
    }

    private void FixedUpdate()
    {

        _timePassed += Time.deltaTime;

    }

    //���̃t�@���̗v���ƃA�N�V��������v�������̏���
    public void ServiceSuccess()
    {
        isBurst = true;
        Debug.Log("Yes!");
    }
    //���̃t�@���̗v���ƃA�N�V�������Ⴄ���̏���
    public void ServiceFail()
    {
        Debug.Log("Miss!");
    }
    //�A�N�V����������Ȃ��������̏���
    public void ServiceThrough()
    {
        Debug.Log("oh...");
        Destroy(this);
    }


    //�t�@����������Ԃ悤�Ɍ����鏈��
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
