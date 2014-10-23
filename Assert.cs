//using UnityEngine;
//using System;
//using System.Collections;
//
//namespace Atoms {
//	public class Assert : Conditional {
//
//		Action OnFailure = Fn.DoNothing;
//		Action OnSuccess = Fn.DoNothing;
//
//		public Assert (Func<bool> Cond, Action OnFailure) : base (Cond)
//		{
//			this.OnFailure = OnFailure;
//		}
//		public Assert (Func<bool> Cond, Action OnFailure, Quantum prev, Quantum next) : base (Cond, prev, next) 
//		{
//			this.OnFailure = OnFailure;
//		}
//
//		public Assert (Func<bool> Cond, Action OnFailure, Action OnSuccess) : base (Cond)
//		{
//			this.OnFailure = OnFailure;
//			this.OnSuccess = OnSuccess;
//		}
//		public Assert (Func<bool> Cond, Action OnFailure, Action OnSuccess, Quantum prev, Quantum next) : base (Cond, prev, next) 
//		{
//			this.OnFailure = OnFailure;
//			this.OnSuccess = OnSuccess;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) {yield return _;}
//
//			AssertCond ();
//
//		}
//
//		public override Atom copyAtom {
//			get {
//				return new Assert (Cond, OnFailure, OnSuccess, prev, next);
//			}
//		}
//
//		void AssertCond ()
//		{
//			if (! Cond())
//			{
//				OnFailure();
//			}
//			else
//			{
//				OnSuccess();
//			}
//		}
//
//		public static Assert _ (Func<bool> Cond, Action OnFailure) {
//			return new Assert (Cond, OnFailure);
//		}
//
//		public static Assert _ (Func<bool> Cond, Action OnFailure, Action OnSuccess) {
//			return new Assert (Cond, OnFailure, OnSuccess);
//		}
//	}
//}
