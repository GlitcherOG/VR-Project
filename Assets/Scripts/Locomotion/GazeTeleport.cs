using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeTeleport : MonoBehaviour
{
    public Transform teleportPoint;

    public Transform rig;

    public bool isEnabled = true;
    public MeshCollider col;
    public MeshRenderer rend;

    public Image fade;

    public bool teleporting;
    float timer;
    Color alpha = new Color(0, 0, 0, 0);
    private void OnDrawGizmos()
    {
        BT.BaneGizmos.DrawArrow(teleportPoint.position, teleportPoint.forward);
    }

    public void Update()
    {
        if(teleporting)
        {
            timer += Time.deltaTime;
            fade.color = Color.Lerp(alpha, Color.black, timer / 4);
            if(Color.black == fade.color)
            {
                teleporting = false;
                TeleportToPoint();
                timer = 0;
            }
        }
        else
        {
            if(alpha != fade.color)
            {
                timer -= Time.deltaTime;
                fade.color = Color.Lerp(Color.black, alpha, timer / 4);
            }
            else
            {
                timer = 0;
            }
        }
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

    public void OnTeleport()
    {
        teleporting = true;
    }

    IEnumerator Teleport()
    {
        float t = 0;
        Color alpha = new Color(0, 0, 0, 0);
        while (t < 100)
        {
            Debug.Log(t);
            t += Time.deltaTime;
            print(Color.Lerp(alpha, Color.black, t / 100));
            fade.color = Color.Lerp(alpha, Color.black, t / 100);
        }
        print("Teleporting!");
        TeleportToPoint();
        t = 0;
        while (t < 100)
        {
            print("Fading out!");
            t += Time.deltaTime;
            fade.color = Color.Lerp(Color.black, alpha, t / 100);
        }

        yield return new WaitForSeconds(0);
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
