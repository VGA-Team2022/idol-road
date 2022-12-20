using UnityEngine;

/// <summary>通常ファンのイラストに合わせたボイスを再生する為のクラス</summary>
public class NomalEnemyVoice : MonoBehaviour
{
    /// <summary>成功・失敗時のIDを格納する為 </summary>
    const int VOICE_ID_SIZE = 2;

    /// <summary>通常ファンのイラストの種類を取得し、イラストに合ったボイスのIDを返す </summary>
    /// <param name="enemySprite">イラストの種類</param>
    /// <returns>サウンドID 0=成功 1=失敗</returns>
    public int[] GetVoiceID(EnemyNomalSprites enemySprite)
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
}
