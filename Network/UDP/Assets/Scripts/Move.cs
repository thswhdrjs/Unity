using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Move : MonoBehaviour
{
    public XRController controller;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position -= new Vector3(0.1f, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += new Vector3(0.1f, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += new Vector3(0, 0.1f, 0);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.position -= new Vector3(0, 0.1f, 0);

        // Touchpad/Joystick position
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {
            if (position.x < 0)
                transform.position -= new Vector3(0.1f, 0, 0);
            
            if (position.x > 0)
                transform.position += new Vector3(0.1f, 0, 0);

            if (position.y < 0)
                transform.position -= new Vector3(0, 0.1f, 0);

            if (position.y > 0)
                transform.position += new Vector3(0, 0.1f, 0);
        }

        Singleton.Instance.StopGame();
    }
}
