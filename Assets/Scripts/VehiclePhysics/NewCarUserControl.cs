using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

[RequireComponent(typeof (NewCarController))]
public class NewCarUserControl : MonoBehaviour
    {
        public float wheel;
        public float joystick;
        private NewCarController m_Car;

   

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<NewCarController>();
        }


        private void FixedUpdate()
        {
        // pass the input to the car!
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        float h = wheel;

        float v = joystick;

#if !MOBILE_INPUT
        float handbrake = Input.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
            
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }

