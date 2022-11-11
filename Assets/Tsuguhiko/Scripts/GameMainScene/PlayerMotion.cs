using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Playerの動きを止めたり、再開させたりしてアクションをさせるサポートをするスクリプト
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    /// <summary>PlayerControllerの変数 </summary>
    PlayerController controller;

    /// <summary> PlayerControllerでシリアライズした値を格納させるための変数</summary>
    float _newScroolSpeed ;

    void Start()
    {
        // アサインされてるプレイヤースクリプトを持ってくる（PlayerController.csを動かすのに必須)
        controller = GetComponent<PlayerController>();

        // PlayerControllerにシリアライズした値を格納。
        _newScroolSpeed = controller.ScrollSpeed;
    }

    // 動きを止めたい時にこの関数を外部に持ってこれるようにメソッド構築
    public void StopMotion()
    {
        controller.ScrollSpeed = 0;
    }

    // 動きを再開たい時にこの関数を外部に持ってこれるようにメソッド構築
    public void ResumeMotion()
    {
        controller.ScrollSpeed = _newScroolSpeed;
    }
}
