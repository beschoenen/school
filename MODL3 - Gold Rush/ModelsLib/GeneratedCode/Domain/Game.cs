﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Game
	{
		private PlayingGround PlayingGround
		{
			get;
			set;
		}

		public static int Score
		{
			get;
			set;
		}

		public static bool IsPlaying
		{
			get;
			set;
		}

		public virtual Timer Timer
		{
			get;
			set;
		}

		private void Revive()
		{
			throw new System.NotImplementedException();
		}

		public virtual void MoveSwitch(ConsoleKey id)
		{
			throw new System.NotImplementedException();
		}

		public virtual void TimerTick()
		{
			throw new System.NotImplementedException();
		}

	}
}
