using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRFramework.Input
{
    //Custom Unity Event that we can use for when an input action is activated 
    [System.Serializable]
    public class VrInputEvent : UnityEvent<VrInputActionData>{ }
    
    
    [System.Serializable]
    public class VrInputActionData
    {
        //The controller that the InputAction wsa fired on
        public VrController sender;

        //The collider that the InputAction wsa fired on
        public Collider collider;

        //The rigidbody that the InputAction wsa fired on
        public Rigidbody rigidbody;

        //The position of the touchpad that the InputAction wsa fired on
        public Vector2 touchpadPosition;

        //make constructor hotkey (alt enter, generate code, constructor, select inputs)
        public VrInputActionData(VrController _sender, Collider _collider, Rigidbody _rigidbody, Vector2 _touchpadPosition) 
        {
            this.sender = _sender;
            this.collider = _collider;
            this.rigidbody = _rigidbody;
            this.touchpadPosition = _touchpadPosition;
        }
    }
}
