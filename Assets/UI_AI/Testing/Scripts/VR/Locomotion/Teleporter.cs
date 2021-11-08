using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRFramework.Input;
using VRFramework.Pointers;

namespace VRFramework.Locomotion
{
    public class Teleporter : MonoBehaviour
    {
        public bool Enabled { get; set; } = true;
        [SerializeField] protected NavMeshData navMeshData;
        
        public void Setup(VrControllerInput _left, VrControllerInput _right)
        {
            _left.onTeleportPressed.AddListener(OnTeleportPressed);
            _right.onTeleportPressed.AddListener(OnTeleportPressed);
        }

        public void Teleport(Vector3 _position)
        {
            VrRig.GetPlayArea().position = _position;
        }

        private void OnTeleportPressed(VrInputActionData _data)
        {
            //Get the pointer from the controller activating this teleport input
            Pointer pointer = _data.sender.Pointer;

            if (ValidTeleportLocation(pointer.Endpoint))
            {
                //Get the play area
                VrRig.GetPlayArea().position = pointer.Endpoint;
            }
        }
        
        

        protected virtual bool ValidTeleportLocation(Vector3 _position)
        {
            if (!Enabled)
                return false;

            bool validNavMeshLocation = false;
            //Check nav mesh to see if we can teleport
            validNavMeshLocation = navMeshData == null || NavMesh.SamplePosition(_position, out NavMeshHit _,
                navMeshData.distanceLimit, navMeshData.validAreas);

            return validNavMeshLocation;
        }
    }
}