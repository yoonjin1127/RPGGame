using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 인터페이스를 가지고 있는 객체들은 소리에 반응한다

public interface IListenable
{
    public void Listen(Transform trans);
}
