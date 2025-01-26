using UnityEngine;
using System.Collections;
using DG.Tweening;  //DoTween library

namespace ameDevWithNey.Guns
{
    public class Guns_Rotator : MonoBehaviour
    {
        //THese two variables are used for the drag rotations
        public float rotationSpeed = 10f;
        Camera cam;

        //These two variables are used for the lerping of the weapon
        //to its original rotation
        //Vector3 originalRotation = new Vector3(0, 180, 0); //0, 60, 12
        public float lerpTime = 0.5f;
        public Vector3 punch;
        public float punchDuration;

        // Use Quaternion for storing the original rotation to avoid interpolation issues
        private Quaternion originalRotation;

        private void Start()
        {
            //Will find the camera and store it into the variable
            cam = FindObjectOfType<Camera>();

            // Use local rotation instead of global
            originalRotation = transform.localRotation; 
        }



        private void OnMouseDrag()
        {
            RotateWeapon();
        }

        private void OnMouseUp()
        {
            StartCoroutine(RotateToOriginal());
        }



        //This code to rotate the weapon comes from a Youtube video
        //https://www.youtube.com/watch?v=Jxz5wbv7z3k&t=67s (I just used the pc code)
        private void RotateWeapon()
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Instead of using camera's up vector, use the gun's local up vector
            Vector3 right = Vector3.Cross(Vector3.up, transform.position - cam.transform.position);
            Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);

            transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
        }

        IEnumerator RotateToOriginal()
        {
            // Kill any ongoing rotation tweens to prevent interference
            transform.DOKill();

            // Use Quaternion.Lerp to smoothly rotate back to the original rotation
            Tween myTween = transform.DOLocalRotate(originalRotation.eulerAngles, lerpTime);

            //Waits for the mentioned tween to finish before executing the next line
            yield return myTween.WaitForCompletion();

            //Punches the rotation at the end to make it feel better
            transform.DOPunchRotation(punch, punchDuration);
        }
    }
}


