using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    // Ссылка на основную камеру
    public static Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
}