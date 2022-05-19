﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	//This character movement input class is an example of how to get input from a keyboard to control the character;
    public class CharacterKeyboardInput : CharacterInput
    {
		public static CharacterKeyboardInput instance;

		public string horizontalInputAxis = "Horizontal";
		public string verticalInputAxis = "Vertical";
		public KeyCode jumpKey = KeyCode.Space;
		public KeyCode skiKey = KeyCode.LeftShift;
		public KeyCode shootButton = KeyCode.Mouse0;
		public KeyCode reloadKey = KeyCode.R;

		//If this is enabled, Unity's internal input smoothing is bypassed;
		public bool useRawInput = true;

        private void Awake()
        {
			if (instance == null)
			{
				instance = this;
			}
        }

        public override float GetHorizontalMovementInput()
		{
			if(useRawInput)
				return Input.GetAxisRaw(horizontalInputAxis);
			else
				return Input.GetAxis(horizontalInputAxis);
		}

		public override float GetVerticalMovementInput()
		{
			if(useRawInput)
				return Input.GetAxisRaw(verticalInputAxis);
			else
				return Input.GetAxis(verticalInputAxis);
		}

		public override bool IsJumpKeyPressed()
		{
			return Input.GetKey(jumpKey);
		}

		public override bool IsSkiKeyPressed()
		{
			return Input.GetKey(skiKey);
		}

		public override bool IsShootButtonPressed() {
			return Input.GetKey(shootButton);
		}

		public override bool IsReloadKeyPressed()
		{
			return Input.GetKey(reloadKey);
		}
    }
}
