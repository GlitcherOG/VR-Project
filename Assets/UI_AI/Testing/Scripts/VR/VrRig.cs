using UnityEngine;
using VRFramework.Input;
using Valve.VR;
using VRFramework.Locomotion;

namespace VRFramework
{
    public class VrRig : MonoBehaviour
    {
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform leftController;
        [SerializeField] private Transform rightController;

        private VrController left;
        private VrController right;

        private static VrRig instance = null;

        public static Transform GetPlayArea()
        {
            return instance.transform;
        }

        /// <summary>
        /// Attempts to get the transoform for the paseed source that is being tracked by our system
        /// </summary>
        public static Transform GetTrackedTransform(SteamVR_Input_Sources _source) {
            switch (_source)
            {
                case SteamVR_Input_Sources.LeftHand: return instance.leftController;
                case SteamVR_Input_Sources.RightHand: return instance.rightController;
                case SteamVR_Input_Sources.LeftFoot:
                    break;
                case SteamVR_Input_Sources.RightFoot:
                    break;
                case SteamVR_Input_Sources.Head: return instance.camera;
            }

            return null;
        }

        /// <summary>
        /// Attempts to get the controller associated with passed in source
        /// </summary>
        /// <param name="_source">Source we are attempting to get</param>
        public static VrController GetController(SteamVR_Input_Sources _source) {
            if (_source == SteamVR_Input_Sources.LeftHand)
            {
                return instance.left;
            }
            else if (_source == SteamVR_Input_Sources.RightHand)
            {
                return instance.right;
            }

            //Source was not valid
            return null;
        }

        private void Awake() {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            left = leftController.GetComponent<VrController>();
            right = rightController.GetComponent<VrController>();
            
            //Set up controller components
            left.SetUP();
            right.SetUP();
            
            //Get the teleport component from vr rig GO
            Teleporter teleporter = gameObject.GetComponent<Teleporter>();
            if(teleporter != null)
                teleporter.Setup(left.Input, right.Input);
        }

        // Update is called once per frame
        void Update() {
            //Process the update loop for controllers
        left.Process();
        right.Process();
        }
    }
}
