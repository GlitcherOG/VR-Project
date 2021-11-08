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

  private void Awake()
  {
    col = GetComponent<MeshCollider>();
    rend = GetComponent<MeshRenderer>();
  }

  public void TeleportToPoint()
  {
    if (!isEnabled) return;

    rig.position = teleportPoint.position;
  }

  private void OnTriggerEnter(Collider other)
  {
    SetEnabled(false);
  }

  private void OnTriggerExit(Collider other)
  {
    SetEnabled(true);
  }

  public void SetEnabled(bool _state)
  {
    print($"I am changing to {_state}!");
    isEnabled = _state;

    col.enabled = _state; 
    rend.enabled = _state;
  }

}
