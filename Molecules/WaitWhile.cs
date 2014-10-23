//using System;
//using System.Collections;
//
//namespace Atoms {
//	public class WaitWhile : Conditional {
//
//		public WaitWhile (Func<bool> Cond) : base (Cond) {}
//
//		public WaitWhile (Func<bool> Cond, Quantum prev, Quantum next) : base (Cond, prev, next) {}
//		
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) yield return _;
//			
//			while (TryCond()) {
//				yield return null;
//			}
//		}
//		
//		public override Atom copyAtom {
//			get {
//				return new WaitWhile (Cond, prev, next);
//			}
//		}
//		
//		public static WaitWhile _ (Func<bool> Cond) {
//			return new WaitWhile (Cond);
//		}
//	}
//}
