using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


namespace VRFramework.Interactions
{
    //This is a component that the object will have some sort of interaction
    //This controls whether or not they can have certain interactions
    //and also the physics components attached to them
    
    //TODO: @Lara Context menu object to add to object to be interacted with.
    
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour
    {
        //While grab button is pressed, if false, press grab button again
        public bool canGrab = true;
        
        //Press grab button to hold and release
        public bool holdButtonToGrab = true;
        
        //Defines which controllers can grab the object
        public SteamVR_Input_Sources allowedGrabControllers = SteamVR_Input_Sources.Any;

        public bool canTouch = false;
        public SteamVR_Input_Sources allowedTouchControllers = SteamVR_Input_Sources.Any;
        
        public bool canUse = false;
        public SteamVR_Input_Sources allowedUseControllers = SteamVR_Input_Sources.Any;

        //This is the set of events will fire when this object is either picked up or dropped
        public InteractionEvent onGrabbed = new InteractionEvent();
        public InteractionEvent onReleased = new InteractionEvent();

        public Transform snapHandle;
        
        public Collider Collider {
            get { return collider; }
        }
        
        public Rigidbody Rigidbody {
            get { return rigidbody; }
        }
        
        private new Rigidbody rigidbody;
        private new Collider collider;
        
        // Start is called before the first frame update
        void Start() {
            rigidbody = gameObject.GetComponent<Rigidbody>();
            collider = gameObject.GetComponent<Collider>();
            
            if (collider == null)
                collider = gameObject.AddComponent<BoxCollider>();
        }

        //Called when the controller triggers grab action
        public void OnObjectGrabbed(InteractEventData _data) 
        {
            onGrabbed.Invoke(_data);
        }
        
        //Called when collider triggers the drop action
        public void OnObjectReleased(InteractEventData _data) 
        {
            onReleased.Invoke(_data);
        }

        // Update is called once per frame
        void Update() {

        }
    }
}