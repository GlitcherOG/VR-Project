using UnityEngine;
using VRFramework.Input;

namespace VRFramework.Pointers
{
    public class Pointer : MonoBehaviour
    {
        public bool Active { get; private set; } = false;
        public Vector3 Endpoint { get; private set; } = Vector3.zero;
        public VrController Controller { get; private set; }

        private new PointerRenderer renderer;
        
        public void Setup(VrControllerInput _input)
        {
            Controller = _input.GetComponent<VrController>();
            
            renderer = gameObject.GetComponent<PointerRenderer>();
            renderer.Setup(this);
            
            _input.onPointerPressed.AddListener(OnPointerPressed);
            _input.onPointerReleased.AddListener(OnPointerReleased);
        }

        // Update is called once per frame
        void Update()
        {
            //If we aren't active, don't run the method
            if (!Active)
                return;

            bool didHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);
            Endpoint = didHit ? hit.point : Vector3.zero; //short if statement
            renderer.Render(hit, didHit);
            renderer.SetValidState(didHit);
        }

        private void OnPointerPressed(VrInputActionData _data)
        {
            Active = true;
            renderer.SetVisibility(true);
        }
        
        private void OnPointerReleased(VrInputActionData _data)
        {
            Active = false;
            renderer.SetVisibility(false);
        }
    }
}
