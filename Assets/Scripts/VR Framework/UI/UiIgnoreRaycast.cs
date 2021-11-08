using UnityEngine;

namespace VRFramework.UI
{
    [AddComponentMenu("VR Framework/UI/Ignore Raycast Filter")]
    public class UiIgnoreRaycast : MonoBehaviour, ICanvasRaycastFilter
    {
        public bool IsRaycastLocationValid(Vector2 _sp, Camera _eventCamera)
        {
            return false;
        }
    }
}