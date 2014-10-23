using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public class Do : SimpleAtom {

		public Action f;

		public Do (Action f)
		{
			this.f = f;
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;
		}

		public override Quantum copy {
			get {
				return new Do (f);
			}
		}

		internal override IEnumerable GetEnumerable ()
		{
			try {
				f ();
			} catch (Exception e) {
				ex = e;
			}

			yield return null;
		}

		public static Do _ (Action f) {
			return new Do (f);
		}

//		public static Do<A> _<A> (Func<A> f) {
//			return new Do<A> (f);
//		}

	}

//	public class Do<A> : Chain<A> {
//
//		public Func<A> f;
//
//		public Do (Func<A> f)
//		{
//			this.f = f;
//		}
//
//		public Do (Func<A> f, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.f = f;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) {yield return _;}
//
//			A a = default (A);
//
//			try
//			{
//				a = f();
//
//				if (a == null)
//					throw new NullReferenceException("Function returned null reference");
//			}
//			catch (Exception e)
//			{
//				ex = e;
//			}
//
//			yield return a;
//		}
//		
//
//		public override Chain<A> copyChain {
//			get {
//				return new Do<A> (f, prev, next);
//			}
//		}
//	}
}