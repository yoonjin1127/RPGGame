using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� �������̽�(�Ӽ�)�� �ִ� ��쿡 ���� �� �ִٴ� ������ ����
public interface IHittable
{
    public void TakeHit(int damage);
}
