using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTeleport : MonoBehaviour
{
  public Transform teleportPoint;

  public Transform rig;

  public bool isEnabled = true;
  public MeshCollider col;
  public MeshRenderer rend;

  private void OnDrawGizmos()
  {
    BT.BaneGizmos.DrawArrow(teleportPoint.position, teleportPoint.forward);
  }

  private void Awake()
  {
    col = GetComponent<MeshCollider>();
    rend = GetComponent<MeshRenderer>();
  }

  public void TeleportToPoint()
  {
    if (!isEnabled) return;

    rig.position = teleportPoint.position;
    rig.rotation = teleportPoint.rotation;

    Transform cam = Camera.main.transform;
    cam.localEulerAngles = new Vector3(cam.localRotation.x, 0, cam.localRotation.z); // Resets rotation relative to rig
  }

  private void OnTriggerEnter(Collider other) => SetEnabled(false);
 
  private void OnTriggerExit(Collider other) => SetEnabled(true);
 
  public void SetEnabled(bool _state)
  {
    isEnabled = _state;

    col.enabled = _state; 
    rend.enabled = _state;
  }

}
