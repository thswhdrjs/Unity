using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GuardianTest : MonoBehaviour
{
    Vector3[] boundaryPoints;
    Vector3 playAreaSize;

    bool originReady = false;
    bool configured = true;
    bool done = false;

    private void Start()
    {
        boundaryPoints = new Vector3[3];
        GetBoundary();
    }

    void CheckOriginMode()
    {
        TrackingOriginModeFlags tomFlags;
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

        if (subsystems.Count == 0)
            return;

        originReady = true;

        for (int i = 0; i < subsystems.Count; i++)
            subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
    }

    void GetPoints()
    {
        configured = OVRManager.boundary.GetConfigured();

        if (configured)
        {
            boundaryPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

            if (boundaryPoints == null || boundaryPoints.Length == 0)
                return;

            playAreaSize = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);
            done = true;
        }
    }

    void GetBoundary()
    {
        CheckOriginMode();

        if (!originReady)
            return;

        GetPoints();

        if (done)
        {
            for (int i = 0; i < boundaryPoints.Length; i++)
                print(i + " / " + boundaryPoints[i]);
        }
    }
}