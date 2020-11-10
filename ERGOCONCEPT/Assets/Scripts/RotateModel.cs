using UnityEngine;
using UnityEngine.UI;

public class RotateModel : MonoBehaviour
{
    public RawImage Image;
    public GameObject Model;
    public Camera CameraUI;
    public float RotationSpeedX = 800;
    public float RotationSpeedY = 500;
    public int DistanceToMakeToRotate = 20;
    private bool canRotate = false;
    private bool ImageClicked = false;
    private Vector2 mousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CameraUI.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == Image.gameObject)
                {
                    ImageClicked = true;
                    mousePosition = Input.mousePosition;

                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ImageClicked = canRotate = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (canRotate && ImageClicked)
            {
                RotateMe();
            }
            else
            {

               float dist = Vector2.Distance(mousePosition, Input.mousePosition);
                if (dist > DistanceToMakeToRotate)
                {
                    canRotate = true;
                }
            }
        }
    }
    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }


    private void RotateMe()
    {
        float mx = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeedX;
        float my = Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSpeedY;

        Vector3 rot = Model.transform.rotation.eulerAngles + new Vector3(0f, mx, my);
        rot.x = ClampAngle(rot.x, -60f, 60f);
        rot.z = ClampAngle(rot.z, -70f, 9f);
        Model.transform.eulerAngles = rot;
    }
}