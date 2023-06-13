using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CursorLockMode startMode;

    private void Start()
    {
        Cursor.lockState = startMode;
    }

    public void SetCursorMode(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }
}
