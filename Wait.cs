//using System;
//using System.Collections;
//
//namespace Atoms {
//	public class Wait : Atom {
//
//		public Wait () {}
//
//		public Wait (Quantum prev, Quantum next) : base (prev, next) {}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) yield return _;
//
//			while (true)
//				yield return null;
//		}
//
//		public override Atom copyAtom {
//			get {
//				return new Wait (prev, next);
//			}
//		}
//
//		public static Wait _ () {
//			return new Wait ();
//		}
//	}
//}