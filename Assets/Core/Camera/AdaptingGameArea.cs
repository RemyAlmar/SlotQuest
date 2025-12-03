using UnityEngine;

public class AdaptingGameArea : MonoBehaviour
{
    private Camera cam;

    [SerializeField, Min(0.1f)] private Vector2 gameArea = new(.5f, .5f);
    [SerializeField, Min(0.1f)] private float speed = 5f;
    public int Width => UnityEngine.Device.Screen.width;
    public int Height => UnityEngine.Device.Screen.height;
    public float ScreenRatio => (float)Width / (float)Height;
    public float HeightVisible => cam.orthographicSize * 2f;
    public float WidthVisible => HeightVisible * ScreenRatio;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        UpdateRatio();
    }

    private void UpdateRatio()
    {
        if (cam.orthographic)
            cam.orthographicSize = Mathf.Max(gameArea.x / (2 * ScreenRatio), gameArea.y * .5f);
        else
            FitPerspectiveCameraToArea();
    }
    private void FitPerspectiveCameraToArea()
    {
        float _aspect = cam.aspect;
        float _verticalFOV = cam.fieldOfView * Mathf.Deg2Rad;
        float _horizontalFOV = 2f * Mathf.Atan(Mathf.Tan(_verticalFOV * 0.5f) * _aspect);

        float _distHeight = (gameArea.y * 0.5f) / Mathf.Tan(_verticalFOV * 0.5f);
        float _distWidth = (gameArea.x * 0.5f) / Mathf.Tan(_horizontalFOV * 0.5f);

        float _distance = Mathf.Max(_distHeight, _distWidth);

        Vector3 _pos = cam.transform.position;
        _pos.z = -_distance;
        cam.transform.position = Vector3.Lerp(cam.transform.position, _pos, speed * Time.deltaTime);
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        UpdateRatio();
        DrawSquare(Vector2.one * .5f, ConvertWorldUnitToViewport(gameArea), true);
    }

    public Vector2 ConvertWorldUnitToViewport(Vector2 _unit) => new(_unit.x / WidthVisible, _unit.y / HeightVisible);

    void DrawSquare(Vector2 _center, Vector2 _size, bool inViewport = false, Color? _color = null)
    {
        Gizmos.color = _color ?? Color.yellow;

        if (inViewport)
        {
            _center = new((_center.x - .5f) * WidthVisible, (_center.y - .5f) * HeightVisible);
            _size = new((_size.x * .5f) * WidthVisible, (_size.y * .5f) * HeightVisible);
        }

        Vector2 _topLeft = new(_center.x - _size.x, _center.y + _size.y);
        Vector2 _topRight = _center + _size;
        Vector2 _bottomLeft = _center - _size;
        Vector2 _bottomRight = new(_center.x + _size.x, _center.y - _size.y);

        Gizmos.DrawLine(_topLeft, _topRight);
        Gizmos.DrawLine(_topRight, _bottomRight);
        Gizmos.DrawLine(_bottomRight, _bottomLeft);
        Gizmos.DrawLine(_bottomLeft, _topLeft);
    }
#endif
}
