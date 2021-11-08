using UnityEngine;
using VRFramework.Input;

namespace VRFramework.Extras
{
    public class CurvedUIRayProcessor : MonoBehaviour
    {
        public VrController leftController;
        public VrController rightController;
        
        [Tooltip("Determines which hand is being used on the UI.")] 
        public bool useRight = true;

        // Update is called once per frame
        void Update()
        {
            // (? = if), (: = else)
            VrControllerInput input = useRight ? rightController.Input : leftController.Input;
            CurvedUIInputModule.CustomControllerRay = new Ray(input.transform.position, input.transform.forward);
            CurvedUIInputModule.CustomControllerButtonState = input.IsInteractUIPressed;
        }
    }
}