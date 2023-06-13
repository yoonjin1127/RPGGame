using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        // 공격할 때 공격 애니메이션 트리거 활성화
        anim.SetTrigger("Attack");
    }

    public void OnAttack()
    {
        // Attack을 누르면 공격
        Attack();
    }

    public void AttackTiming()
    {
        // 1. 범위 안에 있는지
        // 내 위치에서 범위만큼 충돌범위를 가져옴
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            //2. 앞에 있는지 (충돌체 위치 - 현재 플레이어 위치)
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            // 뒤에 있을 때(Dot은 내적을 구해주고, 90도보다 클 때 cos값은 음수이다)
            // Deg2Rad는 호도법을 사용해 라디안으로 변환해줌
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;
            // 앞에 있을 때
            else
            {
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable?.TakeHit(damage);
            }
        }
    }

    // 플레이어 선택 시각화
    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

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
