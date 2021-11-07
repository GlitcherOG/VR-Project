using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRFramework.Input;

namespace VRFramework.Interactions
{
    public abstract class Interaction : MonoBehaviour
    {
        public VrController controller;
        public VrControllerInput input;
        
        //Currently being interacted with.
        //Not set when no interaction is occurring.
        public InteractableObject interactingObject;
        public bool IsSetup { get; private set; } = false;
        
        //This is the function that is called when the controller is ready to begin interacting
        public virtual void Setup(VrController _controller) {
            controller = _controller;
            input = controller.Input;

            IsSetup = true;
        }

        //This function allows us to attach an object to the specified location giving us more control over how the object looks when grabbed
        protected void SnapObject(Transform _snapHandle) {
            Rigidbody attachPoint = controller.Rigidbody;
            if (_snapHandle == null)
            {
                interactingObject.transform.position = attachPoint.transform.position;
            }
            else
            {
                //This calculation allows us to orient the object along the forward axis of the snap handle
                interactingObject.transform.rotation =
                    attachPoint.transform.rotation * Quaternion.Euler(_snapHandle.localEulerAngles);

                //This calculation allows us to place the object in the correct position relative to the controller
                interactingObject.transform.position = attachPoint.transform.position -
                                                       (_snapHandle.position - interactingObject.transform.position);
            }
        }

        protected bool SetCollidingObject(Collider _collider) {
            //Check that there is an object already colliding with controller or no InteractableObject
            //script on the colliding object.
            InteractableObject interactable = _collider.GetComponent<InteractableObject>();
            if (interactingObject != null || interactable == null)
                return false;
            
            //Check that the interactableObject can actually be interacted with
            if (!CanInteract(interactable))
                return false;

            //We can interact with this object so store it
            interactingObject = interactable;
            return true;
        }
        
        protected abstract bool CanInteract(InteractableObject _interactable);

        //This function allows us to make the controller visible or not when interacting with something.
        protected void SetControllerVisibility(bool _visible) {
            controller.controllerModel.SetActive(_visible);
        }

        //Gives us an easy way to generate interaction data for events
        protected InteractEventData GenerateData(Collider _collider, InteractEventData.Interaction _interaction) 
        {
            return new InteractEventData(interactingObject, controller, _collider, _collider.GetComponent<Rigidbody>(),
                _interaction);
        }
        
        // TODO: Touching Interaction Script HINT!!!
        protected virtual void OnObjectTouched() {}
        protected virtual void OnObjectUntouched() {}

        private void OnTriggerEnter(Collider _other) 
        {
            if (SetCollidingObject(_other))
            {
                OnObjectTouched();
            }
        }

        private void OnTriggerExit(Collider _other) 
        {
            //If the object isn't set, no point in changing it
            if (interactingObject == null)
                return;

            interactingObject = null;
            OnObjectUntouched();
        }
        // TODO: Touching Interaction Script HINT ^ !!!

        private void OnTriggerStay(Collider _other) 
        {
            SetCollidingObject(_other);
        }
        
        //TODO: TOUCH AND USE INTERACTION CODE FOR ASSIGNMENT!!!!!!!!!!! ASAP ASAP ASAP!
    }
}