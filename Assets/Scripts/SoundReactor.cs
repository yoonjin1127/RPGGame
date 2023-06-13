using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReactor : MonoBehaviour, IListenable
{
    public void Listen(Transform trans)
    {
        // �Ҹ��� ���� ��ġ�� �Ĵٺ�
        // transform.LookAt(trans.transform.position);
        StartCoroutine(LookatRoutine(trans));
    }

    IEnumerator LookatRoutine(Transform trans)
    {
        // �ݺ��� �߰�
        while (true)
        {
            // (�÷��̾� ��ǥ) - (���� ��ǥ) => �÷��̾ ���͸� �ٶ󺸴� ���� 
            // �� ��ȯ ������ ��ġ�� ������ ���Ͱ� �÷��̾�� ������ ���ͷ� ȸ���Ѵ�
            Vector3 pos = (trans.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);
            yield return null;
        }
    }


}
