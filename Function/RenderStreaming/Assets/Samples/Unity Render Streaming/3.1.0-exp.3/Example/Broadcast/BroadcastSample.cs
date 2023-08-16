using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using UnityEngine.Android;

namespace Unity.RenderStreaming.Samples
{
    using InputSystem = UnityEngine.InputSystem.InputSystem;

    static class InputReceiverExtension
    {
        public static void SetInputRange(this InputReceiver reveiver, Vector2Int size)
        {
            reveiver.SetInputRange(size, new Rect(0, 0, Screen.width, Screen.height));
        }
    }

    class BroadcastSample : MonoBehaviour
    {
        [SerializeField] RenderStreaming renderStreaming;
        [SerializeField] InputReceiver inputReceiver;
        [SerializeField] VideoStreamSender videoStreamSender;
        GameObject dialog = null;

        private void Awake()
        {
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
                dialog = new GameObject();
            }
#endif

#if URS_USE_AR_FOUNDATION || PLATFORM_ANDROID
            InputSystem.RegisterLayout<UnityEngine.XR.ARSubsystems.HandheldARInputDevice>(
                matches: new InputDeviceMatcher()
                    .WithInterface(XRUtilities.InterfaceMatchAnyVersion)
                );
#endif
        }

        // Start is called before the first frame update
        void Start()
        {
            if (renderStreaming.runOnAwake)
                return;
            renderStreaming.Run(
                hardwareEncoder: RenderStreamingSettings.EnableHWCodec,
                signaling: RenderStreamingSettings.Signaling);

            inputReceiver.OnStartedChannel += OnStartedChannel;
        }

        void OnStartedChannel(string connectionId)
        {
            inputReceiver.SetInputRange(videoStreamSender.streamingSize);
            inputReceiver.SetEnableInputPositionCorrection(true);
        }
    }
}
