using System;
using System.Collections;
using Tatacoa;

namespace Atoms {

	//If
	public static class If {

		public static Atom _ (Func<bool> cond, Atom thenAtom, Atom elseAtom = null)
		{
			return new LazyAtom (() => cond () ? thenAtom : (elseAtom != null ? elseAtom : Atom.DoNothing));
		}

		public static Chain<A> _<A> (Func<bool> cond, Chain<A> thenChain, Chain<A> elseChain)
		{
			return new LazyChain<A> (() => cond () ? thenChain : elseChain);
		}

		public static Sequence<A> _<A> (Func<bool> cond, Sequence<A> thenSeq, Sequence<A> elseSeq)
		{
			return new LazySeq<A> (() => cond () ? thenSeq : elseSeq);
		}
	}

	public abstract partial class Atom
	{
		public Atom If (Func<bool> cond, Atom elseAtom = null)
		{
			return Atoms.If._ (cond, this, elseAtom != null ? elseAtom : Atom.DoNothing);
		}
	}

	public abstract partial class Chain<A> 
	{
		public Chain<A> If (Func<bool> cond, Chain<A> elseChain)
		{
			return Atoms.If._ (cond, this, elseChain);
		}
	}

	public abstract partial class Sequence<A> 
	{
		public Sequence<A> If (Func<bool> cond, Sequence<A> elseSeq)
		{
			return Atoms.If._ (cond, this, elseSeq);
		}
	}

}
