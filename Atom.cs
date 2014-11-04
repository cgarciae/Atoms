using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms {
	public abstract partial class Atom : Quantum {

		public static Atom DoNothing = new Do (() => {});

		public Atom Then (Atom atom)
		{
			return new AtomJoinAtom (this.copy as Atom, atom.copy as Atom);
		}

		public Atom BoundedBy (BoundedAtom boundAtom)
		{
			return new AtomJoinBoundAtom (this.copy as Atom, boundAtom.copy as BoundedAtom);
		}

		public Atom Then (Func<Atom> f)
		{
			return Then (LazyAtom._ (f));
		}

		public Chain<A> Bind<A> (Chain<A> chain)
		{
			return new AtomJoinChain<A> (this.copy as Atom, chain.copy as Chain<A>);
		}

		public Chain<A> BoundedBy<A> (BoundedChain<A> chain)
		{
			return new AtomJoinBoundChain<A> (this.copy as Atom, chain.copy as BoundedChain<A>);
		}

		public Chain<A> Bind<A> (Func<Chain<A>> f)
		{
			return Bind (Atoms.LazyAtom._ (f));
		}

		public Atom Par (Atom other) 
		{
			return AtomParallelAtom._ (this, other);	
		}

		public Chain<A> Par<A> (Chain<A> other) 
		{
			return AtomParallelChain<A>._ (this, other);	
		}

		public static Atom operator + (Atom a, Atom b)
		{
			return a.Then (b);
		}
		public static Atom operator + (Atom a, BoundedAtom b)
		{
			return a.BoundedBy (b);
		}

		public static Atom operator * (int n, Atom atom) 
		{
			return atom.Replicate (n).FoldL1 ((sum, next) => sum + next);
		}

		public static Atom operator * (Atom atom, int n) 
		{
			return n * atom;
		}

		public static Atom operator | (Atom a, Atom b)
		{
			return a.Par (b);
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
