//using System;
//using System.Collections;
//
//namespace Atoms {
//	public class Atomize<B> : Chain<B>, IChain<B> {
//
//		IEnumerable enu;
//
//		internal Atomize (IEnumerable enu, Quantum prev, Quantum next) : base (prev, next) {
//			this.enu = enu;
//		}
//
//		public Atomize (IChain<B> chain, Quantum prev) : base (prev, null)
//		{
//			this.enu = chain;
//		}
//
//		public Atomize (IChain<B> chain, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.enu = chain;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			return enu;
//		}
//
//		public override Chain<B> copyChain {
//			get {
//				return new Atomize<B> (enu, prev, next);
//			}
//		}
//
//	}
//}