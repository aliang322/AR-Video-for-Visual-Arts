using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARToggleHandOff : MonoBehaviour
{
    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureTrigger click;

    [SerializeField]
    private Material[] arCubeMaterial;
    //[SerializeField]
    //private GameObject smallCube;

    private string handTag = "Player";
    private Renderer cubeRenderer;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        cubeRenderer = GetComponent<Renderer>();
        //cubeRenderer.sharedMaterial = arCubeMaterial[0];
        //cubeRenderer.material = arCubeMaterial[0];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other">The collider that stays</param>
    private void OnTriggerStay(Collider other)
    {
        MoveWhenGrab(other);
        RotateWhenHolding(other);
        ToggleHandWhenClicking(other);
    }

    /// <summary>
    /// If grab is performed while hand collider is in the cube.
    /// The cube will follow the hand.
    /// </summary>
    private void MoveWhenGrab(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
        {
            transform.parent = other.gameObject.transform;
        }

        else
        {
            transform.parent = null;
        }
    }

    /// <summary>
    /// If pinch is performed while hand collider is in the cube.
    /// The cube will start rotate.
    /// </summary>
    private void RotateWhenHolding(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
        }
    }

    /// <summary>
    /// If pick is performed while hand collider is in the cube.
    /// The cube will follow the hand.
    /// </summary>

    private void ToggleHandWhenClicking(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {
            //GameObject.Find("Example - ManoVisualization").GetComponent<ToggleVisualizationValues>().ToggleBoundingBox();
            //GameObject.Find("Example - Main Feature Menu/CategoryManager/Features/BoundingBox").GetComponent<UIIconBehavior>().isActive = false;
            //if (GameObject.Find("Hand Collider").GetComponent<HandCollider>().enabled == false)
            //{
            //    GameObject.Find("Hand Collider").GetComponent<HandCollider>().enabled = true;
            //}
            GameObject.Find("Hand Collider").GetComponent<HandCollider>().enabled = false;
        }
    }

    /// <summary>
    /// Vibrate when hand collider enters the cube.
    /// </summary>
    /// <param name="other">The collider that enters</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == handTag)
        {
            cubeRenderer.sharedMaterial = arCubeMaterial[1];
            Handheld.Vibrate();
        }
    }

    /// <summary>
    /// Change material when exit the cube
    /// </summary>
    /// <param name="other">The collider that exits</param>
    private void OnTriggerExit(Collider other)
    {
        cubeRenderer.sharedMaterial = arCubeMaterial[0];
    }
}