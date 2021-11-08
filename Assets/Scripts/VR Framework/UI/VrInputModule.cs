using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using VRFramework.Input;
using UnityEngine.UI;

namespace VRFramework.UI
{
    [AddComponentMenu("VR Framework/UI/VR Input Module")]
    public class VrInputModule : BaseInputModule
    {
        public static VrInputModule instance = null;

        [SerializeField] private LayerMask layerMask;

        private Camera uiCamera;
        private PhysicsRaycaster raycaster;

        private Dictionary<VrControllerInput, UIControllerData> controllerData = new Dictionary<VrControllerInput, UIControllerData>();

        protected override void Awake()
        {
            base.Awake();

            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            
            //Create a new camera that will be used for raycasts
            uiCamera = new GameObject("UI Camera").AddComponent<Camera>();
            uiCamera.transform.SetParent(transform);

            //Add physics raycaster so that the pointer events are sent to 3D objects
            raycaster = uiCamera.gameObject.AddComponent<PhysicsRaycaster>();
            uiCamera.clearFlags = CameraClearFlags.Nothing;
            uiCamera.enabled = false;
            uiCamera.fieldOfView = 5;
            uiCamera.nearClipPlane = 0.01f;

            //Find all canvases in the scene and assign them to our custom camera
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                canvas.worldCamera = uiCamera;
            }
        }

        public void AddController(VrControllerInput _controller)
        {
            if (!controllerData.ContainsKey(_controller))
            {
                controllerData.Add(_controller, new UIControllerData());
            }
        }
        public void RemoveController(VrControllerInput _controller)
        {
            if (controllerData.ContainsKey(_controller))
            {
                controllerData.Remove(_controller);
            }
        }

        public void ClearSelection()
        {
            if (eventSystem.currentSelectedGameObject != null)
            {
                eventSystem.SetSelectedGameObject(null);
            }
        }

        protected void UpdateCameraPos(VrControllerInput _controller)
        {
            uiCamera.transform.position = _controller.transform.position;
            uiCamera.transform.rotation = _controller.transform.rotation;
        }

        private void Select(GameObject _go)
        {
            ClearSelection();

            if (ExecuteEvents.GetEventHandler<ISelectHandler>(_go) != null)
            {
                eventSystem.SetSelectedGameObject(_go);
            }
        }

        private void UpdateEventData(UIControllerData _data, VrControllerInput _input)
        {
            if (_data.eventData == null)
            {
                _data.eventData = new VrPointerEventData(eventSystem);
            }
            else
            {
                _data.eventData.Reset();
            }

            _data.eventData.controller = _input;
            _data.eventData.delta = Vector2.zero;
            _data.eventData.position = new Vector2(uiCamera.pixelWidth * 0.5f, uiCamera.pixelHeight * 0.5f);
        }

        private void Raycast(UIControllerData _data)
        {
            eventSystem.RaycastAll(_data.eventData, m_RaycastResultCache);
            _data.eventData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            m_RaycastResultCache.Clear();
        }
        
        public override void Process()
        {
            raycaster.eventMask = layerMask;

            foreach (KeyValuePair<VrControllerInput, UIControllerData> dataPair in controllerData)
            {
                //Copy values out of the pair for easier accessing
                VrControllerInput controllerInput = dataPair.Key;
                UIControllerData data = dataPair.Value;
                
                //Handle updating the camera pos, event data and raycasting
                UpdateCameraPos(controllerInput);
                UpdateEventData(data, controllerInput);
                Raycast(data);
                
                //Set the hit of the GUI element
                data.currentPoint = data.eventData.pointerCurrentRaycast.gameObject;
                
                //Handle the entering and exiting of UI elements
                HandlePointerExitAndEnter(data.eventData, data.currentPoint);
                
                //Handle the pointer down and up states
                if (controllerInput.IsInteractUIDown)
                {
                    ProcessPress(data, controllerInput);
                }

                if (controllerInput.IsInteractUIUp)
                {
                    ProcessRelease(data, controllerInput);
                }
                
                //Handle dragging and selecting
                ProcessDrag(data, controllerInput);
                ProcessSelect(data);
            }
        }

        private void ProcessPress(UIControllerData _data, VrControllerInput _input)
        {
            ClearSelection();
            
            //Set up the pointer pressing components
            _data.eventData.pressPosition = _data.eventData.position;
            _data.eventData.pointerPressRaycast = _data.eventData.pointerCurrentRaycast;
            _data.eventData.pointerPress = null;
            
            //Update the current press if the cursor is over an element
            if (_data.currentPoint != null)
            {
                //Assign the current pressed and the event data current to the hovered element
                _data.currentPressed = _data.currentPoint;
                _data.eventData.current = _data.currentPressed;
                
                //Get the pressed object from the event system - find the go in the hierarchy
                GameObject newPressed = ExecuteEvents.ExecuteHierarchy(_data.currentPressed, _data.eventData, ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerDownHandler);

                if (newPressed == null)
                {
                    newPressed = ExecuteEvents.ExecuteHierarchy(_data.currentPressed, _data.eventData,
                        ExecuteEvents.pointerClickHandler);
                    ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerClickHandler);
                    
                    //If the new pressed is set we can update our current pressed object to the new one
                    if (newPressed != null)
                    {
                        _data.currentPressed = newPressed;
                    }
                }
                else
                {
                    _data.currentPressed = newPressed;
                    
                    //Because of headset jitter, we are going to process click handlers at the same time as down handlers
                    //instead of the up handling like regular mouse input
                    ExecuteEvents.Execute(newPressed, _data.eventData, ExecuteEvents.pointerClickHandler);
                    ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerClickHandler);
                }
                
                //If the new pressed was found, set the selected object to it
                if (newPressed != null)
                {
                    _data.eventData.pointerPress = newPressed;
                    _data.currentPressed = newPressed;
                    Select(_data.currentPressed);
                }
                
                //Run the begin drag handler
                ExecuteEvents.Execute(_data.currentPressed, _data.eventData, ExecuteEvents.beginDragHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.beginDragHandler);
                
                //Assign the drag objects to the pressed ones
                _data.eventData.pointerDrag = _data.currentPressed;
                _data.currentDragging = _data.currentPressed;

            }
        }

        private void ProcessRelease(UIControllerData _data, VrControllerInput _input)
        {
            //Handle end dragging
            if (_data.currentDragging != null)
            {
                _data.eventData.current = _data.currentPressed;
                ExecuteEvents.Execute(_data.currentDragging, _data.eventData, ExecuteEvents.dragHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.dragHandler);
                
                //Handle dropping
                if (_data.currentPoint != null)
                {
                    ExecuteEvents.Execute(_data.currentPoint, _data.eventData, ExecuteEvents.dropHandler);
                }
                
                //Reset drag object
                _data.eventData.pointerDrag = null;
                _data.currentDragging = null;
            }
            
            //Handle pointer up
            if (_data.currentPoint != null)
            {
                _data.eventData.current = _data.currentPressed;
                //Execute the pointer up events on the selected object
                ExecuteEvents.Execute(_data.currentPressed, _data.eventData, ExecuteEvents.pointerUpHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.pointerUpHandler);
                
                //Reset the pressed objects
                _data.eventData.rawPointerPress = null;
                _data.eventData.pointerPress = null;
                _data.currentPressed = null;
            }
        }

        private void ProcessDrag(UIControllerData _data, VrControllerInput _input)
        {
            if (_data.currentDragging != null)
            {
                _data.eventData.current = _data.currentPressed;
                ExecuteEvents.Execute(_data.currentDragging, _data.eventData, ExecuteEvents.dragHandler);
                ExecuteEvents.Execute(_input.gameObject, _data.eventData, ExecuteEvents.dragHandler);
            }
        }
        
        //Update the selected element to allow keyboard focus
        private void ProcessSelect(UIControllerData _data)
        {
            if (eventSystem.currentSelectedGameObject != null)
            {
                _data.eventData.current = eventSystem.currentSelectedGameObject;
                ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, GetBaseEventData(),
                    ExecuteEvents.updateSelectedHandler);
            }
        }
    }
}