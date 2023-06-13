using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 인터페이스(속성)가 있는 경우에 때릴 수 있다는 식으로 구현
public interface IHittable
{
    public void TakeHit(int damage);
}
