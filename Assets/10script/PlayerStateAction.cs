using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using System;

namespace PlayerStateAction
{
	public class PlayerStateActionBase : FsmStateAction {

		protected PlayerState player_state;

		public override void OnEnter()
		{
			base.OnEnter();
			player_state = Owner.gameObject.GetComponent<PlayerState>();
		}
	}

	[ActionCategory("PlayerStateAction")]
	[HutongGames.PlayMaker.Tooltip("PlayerStateAction")]
	public class Idle : PlayerStateActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			player_state.joystick.onMoveStart.AddListener(HandleMoveStart);
			EasyTouch.On_SwipeStart += EasyTouch_On_SwipeStart;
			EasyTouch.On_SwipeStart2Fingers += EasyTouch_On_SwipeStart2Fingers; ;
		}

		private void EasyTouch_On_SwipeStart2Fingers(Gesture gesture)
		{
			Fsm.Event("ds");
		}

		private void EasyTouch_On_SwipeStart(Gesture gesture)
		{
			Fsm.Event("ss");
		}

		private void HandleMoveStart()
		{
			Fsm.Event("move");
		}

		public override void OnExit()
		{
			base.OnExit();
			player_state.joystick.onMoveStart.RemoveListener(HandleMoveStart);
			EasyTouch.On_SwipeStart -= EasyTouch_On_SwipeStart;

		}

	}


	[ActionCategory("PlayerStateAction")]
	[HutongGames.PlayMaker.Tooltip("PlayerStateAction")]
	public class Moving : PlayerStateActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			player_state.joystick.onMove.AddListener(HandleMove);
			player_state.joystick.onMoveEnd.AddListener(HandleMoveEnd);
		}

		public override void OnExit()
		{
			base.OnExit();
			player_state.joystick.onMove.RemoveListener(HandleMove);
			player_state.joystick.onMoveEnd.RemoveListener(HandleMoveEnd);
		}

		private void HandleMove(Vector2 arg0)
		{
			player_state.input_controller.inputHorizontal = arg0.x;
			player_state.input_controller.inputVertical = arg0.y;
		}
		private void HandleMoveEnd()
		{
			player_state.input_controller.inputHorizontal = 0.0f;
			player_state.input_controller.inputVertical = 0.0f;
			Finish();
		}


	}


}