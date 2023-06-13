using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReactor : MonoBehaviour, IListenable
{
    public void Listen(Transform trans)
    {
        // 소리가 나는 위치를 쳐다봄
        // transform.LookAt(trans.transform.position);
        StartCoroutine(LookatRoutine(trans));
    }

    IEnumerator LookatRoutine(Transform trans)
    {
        // 반복자 추가
        while (true)
        {
            // (플레이어 좌표) - (몬스터 좌표) => 플레이어가 몬스터를 바라보는 벡터 
            // 이 변환 과정을 거치지 않으면 몬스터가 플레이어와 평행한 벡터로 회전한다
            Vector3 pos = (trans.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);
            yield return null;
        }
    }


}
