using System;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Config
{
    /// <summary>
    /// Sets the <see cref="ScreenOrientation"/> enum as a flagged enum.
    /// Used when choosing multiple orientations for disabling certain orientations for auto rotation.
    /// </summary>
    [Flags]
    public enum LockedScreenOrientation
    {
        Portrait = 1 << ScreenOrientation.Portrait,
        PortraitUpsideDown = 1 << ScreenOrientation.PortraitUpsideDown,
        LandscapeLeft = 1 << ScreenOrientation.LandscapeLeft,
        LandscapeRight = 1 << ScreenOrientation.LandscapeRight
    }
}