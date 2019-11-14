using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    // Ссылка на главную камеру
    public static Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
}