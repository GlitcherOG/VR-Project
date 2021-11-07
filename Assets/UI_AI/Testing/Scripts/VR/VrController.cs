using UnityEngine;
using Valve.VR;
using VRFramework.Input;
using VRFramework.Interactions;
using VRFramework.Pointers;

namespace VRFramework
{
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(VrControllerInput))]
    public class VrController : MonoBehaviour
    {
        public VrControllerInput Input {
            get;
            private set;
        }

        public Pointer Pointer
        {
            get;
            private set;
        }

        public Collider Collider {
            get;
            private set;
        }

        public Rigidbody Rigidbody {
            get;
            private set;
        }

        public SteamVR_Behaviour_Pose Pose {
            get;
            private set;
        }

        public SteamVR_Input_Sources Source {
            get;
            private set;
        }

        public Vector3 Velocity {
            get;
            private set;
        }
        
        public Vector3 AngularVelocity {
            get;
            private set;
        }

        public GameObject controllerModel;
        

        public void SetUP() {
            Rigidbody = gameObject.GetComponent<Rigidbody>();
            
            //Attempt to get the collider component from the GO, if fail, add sphere collider
            Collider = gameObject.GetComponent<Collider>();
            if (Collider == null)
                Collider = gameObject.AddComponent<SphereCollider>();

            //Setup the collider and rigidbody in the way we need them
            Collider.isTrigger = true;
            Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Rigidbody.useGravity = false;
            
            //Get behaviour Pose and input source
            Pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            Source = Pose.inputSource;

            //Setup controller input component from go and set it up
            Input = gameObject.GetComponent<VrControllerInput>();
            Input.Setup(this);
            
            //Try and get the pointer component from the GO and set it up
            Pointer = gameObject.GetComponent<Pointer>();
            if(Pointer != null) 
                Pointer.Setup(Input);
            
            //If there is no controller model set, create a small cube
            if (controllerModel == null)
            {
                controllerModel = GameObject.CreatePrimitive(PrimitiveType.Cube);
                controllerModel.transform.SetParent(transform);
                controllerModel.transform.localScale = Vector3.one * 0.25f;
            }
            
            //Try and Get the GrabInteraction script and set it up if its found
            GrabInteraction grab = gameObject.GetComponent<GrabInteraction>();
            if(grab != null)
                grab.Setup(this);
        }

        public void Process() {
            Velocity = Pose.GetVelocity();
            AngularVelocity = Pose.GetAngularVelocity();
            
            Input.Process();
        }
    }
}
