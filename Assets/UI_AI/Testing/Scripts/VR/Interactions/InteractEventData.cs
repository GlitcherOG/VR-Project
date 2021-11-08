using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Namespace hotkey ctr r + o when click on class name
namespace VRFramework.Interactions
{
    [System.Serializable]
    public class InteractionEvent : UnityEvent<InteractEventData>{ }

    [System.Serializable]
    public class InteractEventData
    {
        public enum Interaction
        {
            Touch,
            Grab
        }
        
        //Object being interacted with
        public InteractableObject interactable;

        //controller of interactable obj
        public VrController controller;

        //collider of interactable object
        public Collider collider;

        //RB of interactable object
        public Rigidbody rigidbody;

        public Interaction interaction;
        
        public InteractEventData(
            InteractableObject _interactable, 
            VrController _controller, 
            Collider _collider, 
            Rigidbody _rigidbody,
            Interaction _interaction) 
        {
            this.interactable = _interactable;
            this.controller = _controller;
            this.collider = _collider;
            this.rigidbody = _rigidbody;
            this.interaction = _interaction;
        }
    }
}