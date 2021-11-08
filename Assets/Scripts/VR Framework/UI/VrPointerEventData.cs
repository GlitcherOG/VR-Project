using UnityEngine;
using UnityEngine.EventSystems;
using VRFramework.Input;

namespace VRFramework.UI
{
    public class VrPointerEventData : PointerEventData
    {
        public GameObject current;
        public VrControllerInput controller;
        
        public VrPointerEventData(EventSystem _system) : base(_system){ }

        public override void Reset()
        {
            current = null;
            controller = null;
            base.Reset();
        }
    }
}