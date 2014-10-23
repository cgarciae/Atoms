//using UnityEngine;
//using System;
//using System.Collections;
//
//namespace Atoms {
//	public abstract class Conditional : Atom {
//
//		public Func<bool> Cond;
//
//		private Conditional (){}
//
//		internal Conditional (Func<bool> Cond) {
//			this.Cond = Cond;	
//		}
//
//		internal Conditional (Func<bool> Cond, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.Cond = Cond;
//		}
//
//		internal bool TryCond () {
//			try {
//				return Cond();
//			}
//			catch (Exception e) {
//				this.ex = e;
//				return false;
//			}
//		}
//
//	}
//
//	public class While : Conditional {
//
//		public While (Func<bool> Cond) : base (Cond) {}
//
//		public While (Func<bool> Cond, Quantum prev, Quantum next) : base (Cond, prev, next) {}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			var enu = Previous ().GetEnumerator ();
//
//			while (TryCond()) {
//				enu.MoveNext();
//				yield return enu.Current;
//			}
//		}
//
//		public override Atom copyAtom {
//			get {
//				return new While (Cond, prev, next);
//			}
//		}
//		
//		public static While _ (Func<bool> Cond) {
//			return new While (Cond);
//		}
//	}
//}