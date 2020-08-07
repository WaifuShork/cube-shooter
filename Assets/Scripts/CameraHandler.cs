using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shork
{
    public class CameraHandler : MonoBehaviour
    {
        #region Transform Stats
        [Header("Transform Stats")]
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private Vector3 offsetVector = Vector3.zero;
        #endregion

        #region Camera Stats
        [Header("Camera Stats")]
        [SerializeField]
        private float rotSpeed = 10f;
        [SerializeField]
        private float mouseXModifier = 10f;
        [SerializeField]
        private float xMinClamped = 90;
        [SerializeField]
        private float xMaxClamped = 90;
        [SerializeField]
        private float cameraMoveSpeed = 2;
        [HideInInspector]
        public float mouseInputX;
        #endregion


        void Start()
        {
            mouseInputX = transform.rotation.eulerAngles.y;
        }

        // Executing physics based camera movement to reduce jitter and physics issues
        void FixedUpdate()
        {
            mouseInputX += Input.GetAxisRaw("Mouse X") * mouseXModifier;
            mouseInputX = Mathf.Repeat(mouseInputX, 360);

            RotateAround(mouseInputX);
        }

        // Handles rotational values and pivoting around the character because I'm fucking stupid and didn't separate it
        void RotateAround(float xRotation)
        {
            Vector3 targetPosition = playerTransform.position + playerTransform.rotation * offsetVector;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, xRotation, 0f), rotSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            playerTransform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        }
    }
}