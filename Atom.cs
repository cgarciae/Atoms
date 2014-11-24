using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms {
	public abstract partial class Atom : Quantum {



		public Atom Then (Atom atom)
		{
			return new AtomJoinAtom (this.copy as Atom, atom.copy as Atom);
		}

		public Atom Then (BoundedAtom boundAtom)
		{
			return new AtomJoinBoundAtom (this.copy as Atom, boundAtom.copy as BoundedAtom);
		}

		public Atom Then (Func<Atom> f)
		{
			return Then (LazyAtom._ (f));
		}

		public Chain<A> Then<A> (Chain<A> chain)
		{
			return new AtomJoinChain<A> (this.copy as Atom, chain.copy as Chain<A>);
		}

		public Chain<A> Then<A> (BoundedChain<A> chain)
		{
			return new AtomJoinBoundChain<A> (this.copy as Atom, chain.copy as BoundedChain<A>);
		}

		public Chain<A> Then<A> (Func<Chain<A>> f)
		{
			return Then (LazyAtom._ (f));
		}

		public Atom Parallel (Atom other) 
		{
			return AtomParallelAtom._ (this, other);	
		}

		public Chain<A> Parallel<A> (Chain<A> other) 
		{
			return AtomParallelChain<A>._ (this, other);	
		}

		public static Atom operator + (Atom a, Atom b)
		{
			return a.Then (b);
		}
		public static Atom operator + (Atom a, BoundedAtom b)
		{
			return a.Then (b);
		}

		public static Atom operator * (int n, Atom atom) 
		{
			return atom.Replicate (n);
		}

		public static Atom operator * (Atom atom, int n) 
		{
			return n * atom;
		}

		public static Atom operator | (Atom a, Atom b)
		{
			return a.Parallel (b);
		}

		public static Atom DoNothing = new Do (() => {});
		public static Atom NullAtom = new Atomize (Null());

		static IEnumerable Null ()
		{
			yield break;	
		}
	}

	public abstract class BoundedAtom : BoundQuantum
	{
		public BoundedAtom Then (BoundedAtom boundedAtom)
		{
			return new BoundedAtomJoinBoundedAtom (this.copy as BoundedAtom, boundedAtom.copy as BoundedAtom);
		}

		public static BoundedAtom operator + (BoundedAtom a, BoundedAtom b)
		{
			return a.Then (b);
		}
	}
}
