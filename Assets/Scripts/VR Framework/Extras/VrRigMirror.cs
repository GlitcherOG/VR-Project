using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFramework.FinalIK
{
    public class VrRigMirror : MonoBehaviour
    {
        [System.Serializable]
        public class FakeTransformRef
        {
            public Transform reference;
            public Transform controlled;
            public Vector3 positionOffset;
            public Vector3 rotationOffset;

            /// <summary>
            /// Copying reference values plus offset.
            /// </summary>
            public void Apply()
            {
                controlled.position = reference.position + positionOffset;
                controlled.eulerAngles = reference.eulerAngles + rotationOffset;
            }
        }

        public FakeTransformRef leftHand;
        public FakeTransformRef rightHand;
        public FakeTransformRef headset;

        // Update is called once per frame
        void Update()
        {
            if(leftHand != null)
                leftHand.Apply();
            
            if(rightHand != null)
                rightHand.Apply();
            
            if(headset != null)
                headset.Apply();
        }
    }
}