using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] Camera m_Camera;
    [SerializeField] Transform m_TrackingSpace;
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenHeightInUnits = 8.4f;
    [SerializeField] float paddleMinXClampNormal = -7.2f;
    [SerializeField] float paddleMaxXClampNormal = 7.2f;
    [SerializeField] float paddleMinYClampNormal = 4.4f;
    [SerializeField] float paddleMaxYClampNormal = 11.05f;


    [SerializeField] float paddleMinXClampNarrow = -7.2f;
    [SerializeField] float paddleMaxXClampNarrow = 7.2f;
    [SerializeField] float paddleMinYClampNarrow = 4.4f;
    [SerializeField] float paddleMaxYClampNarrow = 11.05f;
    [SerializeField] float paddleMinXClampExt = -7.2f;
    [SerializeField] float paddleMaxXClampExt = 7.2f;
    [SerializeField] float paddleMinYClampExt = 4.4f;
    [SerializeField] float paddleMaxYClampExt = 11.05f;

    [SerializeField] Missile missile;
    [SerializeField] LaserBeam laserBeam;
    public bool enableMovement = true;
    bool enableMissile = false;
    bool enableLaser = false;

    float paddleMinXClamp;
    float paddleMaxXClamp;
    float paddleMinYClamp;
    float paddleMaxYClamp;

    private void Start()
    {
        paddleMinXClamp = paddleMinXClampNormal;
        paddleMaxXClamp = paddleMaxXClampNormal;
        paddleMinYClamp = paddleMinYClampNormal;
        paddleMaxYClamp = paddleMaxYClampNormal;
    }


    public bool ControllerIsConnected
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers() & (OVRInput.Controller.LTrackedRemote | OVRInput.Controller.RTrackedRemote);
            return controller == OVRInput.Controller.LTrackedRemote || controller == OVRInput.Controller.RTrackedRemote;
        }
    }
    public OVRInput.Controller Controller
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();
            if ((controller & OVRInput.Controller.LTrackedRemote) == OVRInput.Controller.LTrackedRemote)
            {
                return OVRInput.Controller.LTrackedRemote;
            }
            else if ((controller & OVRInput.Controller.RTrackedRemote) == OVRInput.Controller.RTrackedRemote)
            {
                return OVRInput.Controller.RTrackedRemote;
            }
            return OVRInput.GetActiveController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 9;
        if (enableMovement)
        {
            //Check if Controller connected
            if (ControllerIsConnected)
            {
                //use controller orientation to control paddle
                Matrix4x4 localToWorld = m_TrackingSpace.localToWorldMatrix;
                Quaternion controllerOrientation = OVRInput.GetLocalControllerRotation(Controller);
                Vector3 localControllerPosition = OVRInput.GetLocalControllerPosition(Controller);
                Vector3 worldControllerPosition = localToWorld.MultiplyPoint(localControllerPosition);

                Ray ray = new Ray(worldControllerPosition, controllerOrientation * Vector3.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    Vector3 paddlePos = new Vector3(transform.position.x, transform.position.y, -6.5f);
                    paddlePos.x = Mathf.Clamp(hit.point.x, paddleMinXClamp, paddleMaxXClamp);
                    paddlePos.y = Mathf.Clamp(hit.point.y, paddleMinYClamp, paddleMaxYClamp);
                    transform.position = paddlePos;
                }
            }
            //Else Check if HMD connected
            else if (OVRManager.isHmdPresent)
            {

                //use gaze to control paddle
                Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    Vector3 paddlePos = new Vector3(transform.position.x, transform.position.y, -6.5f);
                    paddlePos.x = Mathf.Clamp(hit.point.x, paddleMinXClamp, paddleMaxXClamp);
                    paddlePos.y = Mathf.Clamp(hit.point.y, paddleMinYClamp, paddleMaxYClamp);
                    transform.position = paddlePos;
                }
            }
            //If no controller or HMD connected, use mouse to control paddle
            else
            {
                //Mouse Controls
                float mousePosInUnitsX = Input.mousePosition.x / Screen.width * screenWidthInUnits - 7.2f;
                float mousePosInUnitsY = Input.mousePosition.y / Screen.height * screenHeightInUnits + 2.4f;
                Vector3 paddlePos = new Vector3(transform.position.x, transform.position.y, -6.5f);
                paddlePos.x = Mathf.Clamp(mousePosInUnitsX, paddleMinXClamp, paddleMaxXClamp);
                paddlePos.y = Mathf.Clamp(mousePosInUnitsY, paddleMinYClamp, paddleMaxYClamp);
                transform.position = paddlePos;
            }
           
        }

        if (enableMissile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
                Instantiate(missile, new Vector3(paddlePos.x, paddlePos.y + 0.1f, 0f), Quaternion.identity);
            }
        }
        else if (enableLaser)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
                Instantiate(laserBeam, new Vector3(paddlePos.x + 0.6f, paddlePos.y + 0.4f, 0f), Quaternion.identity);
                Instantiate(laserBeam, new Vector3(paddlePos.x - 0.6f, paddlePos.y + 0.4f, 0f), Quaternion.identity);
            }
        }

    }

    public void ExtendPaddle()
    {
        //Change Paddle Size
        enableMissile = false;
        enableLaser = false;
        paddleMinXClamp = paddleMinXClampExt;
        paddleMaxXClamp = paddleMaxXClampExt;
        paddleMinYClamp = paddleMinYClampExt;
        paddleMaxYClamp = paddleMaxYClampExt;
    }

    public void NarrowPaddle()
    {
        //Change Paddle Size
        enableMissile = false;
        enableLaser = false;
        paddleMinXClamp = paddleMinXClampNarrow;
        paddleMaxXClamp = paddleMaxXClampNarrow;
        paddleMinYClamp = paddleMinYClampNarrow;
        paddleMaxYClamp = paddleMaxYClampNarrow;
    }

    public void MissilePaddle()
    {
        //Change Paddle Size
        enableMissile = true;
        enableLaser = false;
        paddleMinXClamp = paddleMinXClampNormal;
        paddleMaxXClamp = paddleMaxXClampNormal;
        paddleMinYClamp = paddleMinYClampNormal;
        paddleMaxYClamp = paddleMaxYClampNormal;
    }

    public void LaserPaddle()
    {
        //Change Paddle Size
        enableMissile = false;
        enableLaser = true;
        paddleMinXClamp = paddleMinXClampNormal;
        paddleMaxXClamp = paddleMaxXClampNormal;
        paddleMinYClamp = paddleMinYClampNormal;
        paddleMaxYClamp = paddleMaxYClampNormal;
    }

    public void CatchPaddle()
    {
        //Change Paddle Size
        enableMissile = false;
        enableLaser = false;
        paddleMinXClamp = paddleMinXClampNormal;
        paddleMaxXClamp = paddleMaxXClampNormal;
        paddleMinYClamp = paddleMinYClampNormal;
        paddleMaxYClamp = paddleMaxYClampNormal;
    }

    public void NormalPaddle()
    {
        //Change Paddle Size
        enableMissile = false;
        enableLaser = false;
        paddleMinXClamp = paddleMinXClampNormal;
        paddleMaxXClamp = paddleMaxXClampNormal;
        paddleMinYClamp = paddleMinYClampNormal;
        paddleMaxYClamp = paddleMaxYClampNormal;
    }
}
