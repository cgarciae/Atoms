using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public abstract class Atom : Quantum {

		public Atom BoundBy (BoundedAtom boundAtom)
		{
			return new AtomJoinBoundAtom (this.copy as Atom, boundAtom.copy as BoundedAtom);
		}

		public Chain<A> Then<A> (Chain<A> chain)
		{
			return new AtomJoinChain<A> (this.copy as Atom, chain.copy as Chain<A>);
		}

		public Atom Then (Atom atom)
		{
			return new AtomJoinAtom (this.copy as Atom, atom.copy as Atom);
		}

		public static Atom operator + (Atom a, Atom b)
		{
			return a.Then (b);
		}
		public static Atom operator + (Atom a, BoundedAtom b)
		{
			return a.BoundBy (b);
		}
	}

	public abstract class BoundedAtom : BoundQuantum
	{
		public BoundedAtom BoundBy (BoundedAtom boundedAtom)
		{
			return new BoundedAtomJoinBoundedAtom (this.copy as BoundedAtom, boundedAtom.copy as BoundedAtom);
		}

		public static BoundedAtom operator + (BoundedAtom a, BoundedAtom b)
		{
			return a.BoundBy (b);
		}
	}
}
