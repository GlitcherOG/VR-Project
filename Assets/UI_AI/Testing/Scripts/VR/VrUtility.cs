using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace VRFramework
{
    public static class VrUtility
    {
        private static List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

        /// <summary> The variable that dictates is VR/AR are enabled or allows us to set the enabled state. </summary>
        public static bool XREnabled
        {
            get
            {
                //Get all connected XR Devices
                SubsystemManager.GetInstances(subsystems);
                
                //Loop through all XR devices
                foreach (XRInputSubsystem subsystem in subsystems)
                {
                    //Check if the XR Device is active
                    if (subsystem.running)
                    {
                        return true;
                    }
                }
                
                //No active XR device
                return false;
            }

            set
            {
                //Get all connected XR Devices
                SubsystemManager.GetInstances(subsystems);
                
                //Loop through all XR devices
                foreach (XRInputSubsystem subsystem in subsystems)
                {
                    //If we want to enable it
                    if (value)
                    {
                        subsystem.Start();
                    }
                    else
                    {
                        subsystem.Stop();
                    }
                }
                
            }
            
            
        }
        
    }
}
