using UnityEngine;
using Valve.VR;
using Vector2 = UnityEngine.Vector2;
using VRFramework.UI;

namespace VRFramework.Input
{
    public class VrControllerInput : MonoBehaviour
    {
        #region Properties
        
        //Contains the current state of the steam actions for non event based usage
        public bool IsGrabPressed { get; private set; } = false;
        public bool IsUsePressed { get; private set; } = false;
        public bool IsPointerPressed { get; private set; } = false;
        public bool IsInteractUIPressed { get; private set; } = false;
        public bool IsInteractUIDown { get { return interactUIAction.GetStateDown(controller.Source); } }
        public bool IsInteractUIUp { get { return interactUIAction.GetStateUp(controller.Source); } }
        public bool IsTeleportPressed { get; private set; } = false;
        public Vector2 TouchpadAxis { get; private set; } = Vector2.zero;

        #endregion

        [Header("Input Settings")]
        [Tooltip("This determines if we can interact with UI elements using this controller.")]
        public bool canInteractUI = true;
        
        [Header("Steam VR Input Actions")] 
        //Input actions steam will send when relevant buttons are sent
        //Has a unique set of values IE Bool contains boolean for when they are pressed
        // and vector 2 contains the vector2 relating to the touchpad/joystick
        [SerializeField] private SteamVR_Action_Boolean grabAction;
        [SerializeField] private SteamVR_Action_Boolean useAction;
        [SerializeField] private SteamVR_Action_Boolean teleportAction;
        [SerializeField] private SteamVR_Action_Boolean pointerAction;
        [SerializeField] private SteamVR_Action_Boolean interactUIAction;
        [SerializeField] private SteamVR_Action_Vector2 touchpadPosAction;

        [Header("Unity Input Actions")] 
        //These are the events we can listen to in Unity for when any of the InputActions of steam get fired
        public VrInputEvent onGrabPressed = new VrInputEvent(); 
        public VrInputEvent onGrabReleased = new VrInputEvent(); 
        
        public VrInputEvent onUsePressed = new VrInputEvent();
        public VrInputEvent onUseReleased = new VrInputEvent();
        
        public VrInputEvent onPointerPressed = new VrInputEvent();
        public VrInputEvent onPointerReleased = new VrInputEvent();
        
        public VrInputEvent onInteractUIPressed = new VrInputEvent();
        public VrInputEvent onInteractUIReleased = new VrInputEvent();
        
        public VrInputEvent onTeleportPressed = new VrInputEvent();
        public VrInputEvent onTeleportReleased = new VrInputEvent();
        
        public VrInputEvent onTouchpadPosChanged = new VrInputEvent();

        private VrController controller;

        
        #region Action Functions
        
        //GRAB
        public void OnGrabPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onGrabPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        public void OnGrabReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onGrabPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        //POINTER
        public void OnPointerPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onPointerPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        public void OnPointerReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onPointerReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        //LASER POINTER INTERACT UI
        public void OnInteractUIPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onPointerPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        public void OnInteractUIReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onPointerReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        //TELEPORT
        public void OnTeleportPressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onTeleportPressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        public void OnTeleportReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onTeleportReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        //USE
        public void OnUsePressed(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onUsePressed.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        public void OnUseReleased(SteamVR_Action_Boolean _data, SteamVR_Input_Sources _source) {
            onUseReleased.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, touchpadPosAction.axis));
        }
        
        //TOUCHPAD
        //Axis - current position
        //Delta - amount changed between calls
        public void OnTouchpadChanged(SteamVR_Action_Vector2 _data, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta) {
            onTouchpadPosChanged.Invoke(new VrInputActionData(controller, controller.Collider, controller.Rigidbody, _axis));
        }
        
        #endregion
        
        public void Setup(VrController _controller) {
            controller = _controller;
            
            grabAction.AddOnStateDownListener(OnGrabPressed, controller.Source);
            grabAction.AddOnStateUpListener(OnGrabReleased, controller.Source);
            
            useAction.AddOnStateDownListener(OnUsePressed, controller.Source);
            useAction.AddOnStateUpListener(OnUseReleased, controller.Source);
            
            pointerAction.AddOnStateDownListener(OnPointerPressed, controller.Source);
            pointerAction.AddOnStateUpListener(OnPointerReleased, controller.Source);
            
            interactUIAction.AddOnStateDownListener(OnInteractUIPressed, controller.Source);
            interactUIAction.AddOnStateUpListener(OnInteractUIReleased, controller.Source);
            
            teleportAction.AddOnStateDownListener(OnTeleportPressed, controller.Source);
            teleportAction.AddOnStateUpListener(OnTeleportReleased, controller.Source);
            
            touchpadPosAction.AddOnChangeListener(OnTouchpadChanged, controller.Source);
        }
        
        public void Process() {
            //Copy the current state of actions into corresponding values
            IsGrabPressed = grabAction.state;
            IsUsePressed = useAction.state;
            IsPointerPressed = pointerAction.state;
            IsTeleportPressed = teleportAction.state;
            IsInteractUIPressed = interactUIAction.state;
            TouchpadAxis = touchpadPosAction.axis;
            
            //Handle the state of if the controller can interact with the UI or not
            if (VrInputModule.instance != null)
            {
                if (canInteractUI)
                {
                    VrInputModule.instance.AddController(this);
                }
                else
                {
                    VrInputModule.instance.RemoveController(this);
                }
            }
        }
    }
}
