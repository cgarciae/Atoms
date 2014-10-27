using UnityEngine;
using System;
using System.Collections;
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