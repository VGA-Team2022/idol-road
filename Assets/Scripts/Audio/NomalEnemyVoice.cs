using UnityEngine;

/// <summary>�ʏ�t�@���̃C���X�g�ɍ��킹���{�C�X���Đ�����ׂ̃N���X</summary>
public class NomalEnemyVoice : MonoBehaviour
{
    /// <summary>�����E���s����ID���i�[����� </summary>
    const int VOICE_ID_SIZE = 2;

    /// <summary>�ʏ�t�@���̃C���X�g�̎�ނ��擾���A�C���X�g�ɍ������{�C�X��ID��Ԃ� </summary>
    /// <param name="enemySprite">�C���X�g�̎��</param>
    /// <returns>�T�E���hID 0=���� 1=���s</returns>
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
