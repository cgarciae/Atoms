using System;
using System.Collections;

namespace Atoms 
{
	public abstract class AtomConditional : Atom 
	{
		public Func<bool> Cond;
		
		internal AtomConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
	}

	public abstract class ChainConditional<A> : Chain<A> 
	{
		public Func<bool> Cond;
		
		internal ChainConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
	}

	public abstract class BoundedAtomConditional : BoundedAtom 
	{
		public Func<bool> Cond;
		
		internal BoundedAtomConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
	}

	public abstract class BoundedChainConditional<A> : BoundedChain<A> 
	{
		public Func<bool> Cond;
		
		internal BoundedChainConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
	}

	public abstract class BoundedSeqConditional<A> : BoundedSequence<A> 
	{
		public Func<bool> Cond;
		
		internal BoundedSeqConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
	}
}
