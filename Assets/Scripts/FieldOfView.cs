using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField, Range(0f, 360f)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        // 1. 범위 안에 있는지
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (Collider collider in colliders)
        {
            // 2. 각도 안에 있는지 
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            // 3. 중간에 장애물이 없는지
            float distToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, dirTarget, distToTarget, obstacleMask))
                continue;

            // 위의 경우에 해당되지 않는다면 각도 안에 있고 장애물이 없는 것이므로 플레이어를 보는 게 가능하다
            Debug.DrawRay(transform.position, dirTarget * distToTarget, Color.red);
        }
    }
    // 몬스터 선택 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        // 왼쪽과 오른쪽에 따른 방향성 벡터
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.yellow);
        Debug.DrawRay(transform.position, leftDir * range, Color.yellow);

    }

    //  각도에 따른 기즈모
    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        // 원래 (x, y)는 (cos, sin)으로 동치가능하지만 이때는 z축 기준이기에 반대로 설정
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
