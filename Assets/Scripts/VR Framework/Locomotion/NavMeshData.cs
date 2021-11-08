using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFramework.Locomotion
{
    public class NavMeshData : MonoBehaviour
    {
        [Tooltip("The max distance given point can be outside the nav mesh to be considered valid.")] 
        public float distanceLimit = 0.1f;

        [Tooltip("The parts of the navmesh that are considered valid.")]
        public int validAreas = -1;

    }
}

