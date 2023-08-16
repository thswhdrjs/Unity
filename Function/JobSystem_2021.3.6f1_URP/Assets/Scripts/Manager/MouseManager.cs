using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    private Vector3 swipeOriginPos;
    private Vector3 swipeCurrPos;

    private bool isSwipe;

    private void Update()
    {
        MouseRaycast();
    }

    // Mouse Raycast (UI ¼±ÅÃ ¾ÈµÊ)
    private void MouseRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f) && !EventSystem.current.IsPointerOverGameObject())
            MouseButton(hit, 0);
    }

    // Mouse Button Left = 0, Right = 1, Wheel = 2, else = 3 ~ 6
    private void MouseButton(RaycastHit hit, int mouseButton)
    {
        if (Input.GetMouseButtonDown(mouseButton))
        {
            if(hit.collider.gameObject)
            {

            }
        }
    }

    // Touch
    private void Touch()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {

        }
    }

    #region MouseWheel
    private void MouseWheel(RaycastHit hit)
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll == 0)
            return;

        bool isZoomIn = scroll >= 0.1f ? true : false;
        float fov = isZoomIn ? 20f : 60f;

        if (fov == Camera.main.GetComponent<Camera>().fieldOfView)
            return;

        Vector3 point = hit.point;
        point.z = point.z > -360 ? point.z + 360 : point.z;
        point.z *= 0.5f;

        Vector3 pos = isZoomIn ? new Vector3(point.x, Camera.main.transform.position.y, point.z) : Camera.main.transform.position;
        Zoom(pos, fov);
    }

    // X = 0, Up = 1, Down = 2
    private int MouseWheel()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput > 0)
            return 1;
        else if (wheelInput < 0)
            return 2;

        return 0;
    }
    #endregion

    #region Swipe
    private void Swipe()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 dist;

            if (touch.phase == TouchPhase.Began)
                swipeOriginPos = Input.mousePosition;

            if (touch.phase == TouchPhase.Moved)
            {
                swipeCurrPos = Input.mousePosition;
                dist = swipeCurrPos - swipeOriginPos;

                if (CheckSwipe(swipeOriginPos, swipeCurrPos, 1.5f))
                {
                    isSwipe = true;

                    if (dist.x > 0)
                    {
                        //¿ÞÂÊ
                    }
                    else
                    {
                        //¿À¸¥ÂÊ
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                swipeOriginPos = Vector2.zero;
                swipeCurrPos = Vector2.zero;
                isSwipe = false;
            }
        }
    }

    private bool CheckSwipe(Vector2 originPos, Vector2 currPos, float swipeDist)
    {
        if (isSwipe)
            return false;

        bool check;
        Vector2 dist = currPos - originPos;
        return check = dist.magnitude > swipeDist ? true : false;
    }
    #endregion

    private void RotateCamera(GameObject obj, int resolution, float speed)
    {
        float value = resolution * 0.5f;
        float leftValue = value - 300f;
        float rightValue = value + 300f;

        int flag = Input.mousePosition.x < leftValue ? 1 : -1;

        if (Input.mousePosition.x < leftValue || rightValue < Input.mousePosition.x)
            Camera.main.transform.RotateAround(obj.transform.position, transform.up, Time.deltaTime * flag * speed);
    }

    private void Zoom(Vector3 pos, float fov)
    {
        Camera.main.GetComponent<Camera>().fieldOfView = fov;
        Camera.main.transform.position = pos;
    }
}