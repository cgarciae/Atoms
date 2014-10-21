using System;
using System.Collections;

namespace Atoms {
	public class Atomize<A> : Chain<A> {

		IChain<A> chain;

		public Atomize (IChain<A> chain, Quantum prev) : base (prev, null)
		{
			this.chain = chain;
		}

		public Atomize (IChain<A> chain, Quantum prev, Quantum next) : base (prev, next)
		{
			this.chain = chain;
		}

		internal override IEnumerable GetEnumerable ()
		{
			return chain;
		}

		public override Chain<A> copyChain {
			get {
				return new Atomize<A> (chain, prev, next);
			}
		}

	}
}