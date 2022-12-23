using UnityEngine;

/// <summary>ファンのイラストに合わせたボイスを再生する為のクラス</summary>
public class EnemyVoice : MonoBehaviour
{
    /// <summary>ボスの成功時ボイスのID</summary>
    const int BOOS_SUCCESS_VOICE_ID = 1;
    /// <summary>ボスの失敗時ボイスのID </summary>
    const int BOOS_FAIL_VOICE_ID = 8;

    /// <summary>成功・失敗時のIDを格納する為 </summary>
    const int VOICE_ID_SIZE = 2;

    /// <summary>通常ファンのイラストの種類を取得し、イラストに合ったボイスのIDを返す </summary>
    /// <param name="enemySprite">イラストの種類</param>
    /// <returns>サウンドID 0=成功 1=失敗</returns>
    public int[] GetNormalEnemyVoiceID(EnemyNomalSprites enemySprite)
    {
        var voiceID = new int[VOICE_ID_SIZE];

        switch (enemySprite)
        {
            case EnemyNomalSprites.Mine:
                voiceID[0] = 2;
                voiceID[1] = 9;
                break;
            case EnemyNomalSprites.JK:
                voiceID[0] = 3;
                voiceID[1] = 11;
                break;
            case EnemyNomalSprites.Glasses:
                voiceID[0] = 7;
                voiceID[1] = 12;
                break;
            case EnemyNomalSprites.Handsome:
                voiceID[0] = 4;
                voiceID[1] = 10;
                break;       
        }

        return voiceID;
    }

    /// <summary>ボスのボイスのIDを返す </summary>
    /// <returns>サウンドID 0=成功 1=失敗</returns>
    public int[] GetBossTimeVoiceID()
    {
          var voiceID = new int[VOICE_ID_SIZE] { BOOS_SUCCESS_VOICE_ID, BOOS_FAIL_VOICE_ID };

        return voiceID;
    }
}
