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
        // ������ �� ���� �ִϸ��̼� Ʈ���� Ȱ��ȭ
        anim.SetTrigger("Attack");
    }

    public void OnAttack()
    {
        // Attack�� ������ ����
        Attack();
    }

    public void AttackTiming()
    {
        // 1. ���� �ȿ� �ִ���
        // �� ��ġ���� ������ŭ �浹������ ������
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            //2. �տ� �ִ��� (�浹ü ��ġ - ���� �÷��̾� ��ġ)
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            // �ڿ� ���� ��(Dot�� ������ �����ְ�, 90������ Ŭ �� cos���� �����̴�)
            // Deg2Rad�� ȣ������ ����� �������� ��ȯ����
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;
            // �տ� ���� ��
            else
            {
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable?.TakeHit(damage);
            }
        }
    }

    // �÷��̾� ���� �ð�ȭ
    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        // ���ʰ� �����ʿ� ���� ���⼺ ����
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.yellow);
        Debug.DrawRay(transform.position, leftDir * range, Color.yellow);

    }

    //  ������ ���� �����
    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        // ���� (x, y)�� (cos, sin)���� ��ġ���������� �̶��� z�� �����̱⿡ �ݴ�� ����
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
