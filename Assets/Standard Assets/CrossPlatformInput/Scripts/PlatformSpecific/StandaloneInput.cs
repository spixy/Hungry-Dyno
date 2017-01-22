using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput.PlatformSpecific
{
    public class StandaloneInput : VirtualInput
	{
		private void AddButton(string name)
		{
			// we have not registered this button yet so add it, happens in the constructor
			CrossPlatformInputManager.RegisterVirtualButton(new CrossPlatformInputManager.VirtualButton(name));
		}

		public override float GetAxis(string name, bool raw)
        {
            return raw ? Input.GetAxisRaw(name) : Input.GetAxis(name);
        }


        public override bool GetButton(string name)
        {
            return Input.GetButton(name);
        }


        public override bool GetButtonDown(string name)
		{
			if (Input.GetButtonDown(name))
			{
				return true;
			}

			if (!m_VirtualButtons.ContainsKey(name))
			{
				AddButton(name);
			}
			
			return m_VirtualButtons[name].GetButtonDown;

        }


        public override bool GetButtonUp(string name)
        {
            return Input.GetButtonUp(name);
        }


        public override void SetButtonDown(string name)
        {
			if (!m_VirtualButtons.ContainsKey(name))
			{
				AddButton(name);
			}
			m_VirtualButtons[name].Pressed();

			/*throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");*/
        }


        public override void SetButtonUp(string name)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override void SetAxisPositive(string name)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override void SetAxisNegative(string name)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override void SetAxisZero(string name)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override void SetAxis(string name, float value)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override Vector3 MousePosition()
        {
            return Input.mousePosition;
        }
    }
}