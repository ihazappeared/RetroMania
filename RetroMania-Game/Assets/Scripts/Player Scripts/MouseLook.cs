using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private Quaternion m_CameraTargetRot1;
        private bool m_cursorIsLocked = true;

        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;

            m_CameraTargetRot = camera.localRotation;
            m_CameraTargetRot1 = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

            //if (Input.GetKey(KeyCode.A))
            //{
            //    m_CameraTargetRot = Quaternion.Euler(0.0f, rotZ, 0.0f);
            //    //camera.rotation = m_CameraTargetRot;
            //}
            //else if (Input.GetKey(KeyCode.D))
            //{
            //    m_CameraTargetRot = Quaternion.Euler(0.0f, rotZ, 0.0f);
            //    //camera.rotation = m_CameraTargetRot;
            //}
            //else
            //{
            //    m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
            //}
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            
            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
