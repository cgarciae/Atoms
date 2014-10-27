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
		
		internal bool TryCond () {
			try 
			{
				return Cond();
			}
			catch (Exception e) 
			{
				this.ex = e;
				return false;
			}
		}
	}

	public abstract class ChainConditional<A> : Chain<A> 
	{
		public Func<bool> Cond;
		
		internal ChainConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
		
		internal Maybe<bool> TryCond () {
			try 
			{
				return Fn.Just (Cond());
			}
			catch (Exception e) 
			{
				this.ex = e;
				return Fn.Nothing<bool>();
			}
		}
	}

	public abstract class SeqConditional<A> : Sequence<A> 
	{
		public Func<bool> Cond;
		
		internal SeqConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
		
		internal Maybe<bool> TryCond () {
			try 
			{
				return Fn.Just (Cond());
			}
			catch (Exception e) 
			{
				this.ex = e;
				return Fn.Nothing<bool>();
			}
		}
	}

	public abstract class BoundedAtomConditional : BoundedAtom 
	{
		public Func<bool> Cond;
		
		internal BoundedAtomConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
		
		internal bool TryCond () {
			try 
			{
				return Cond();
			}
			catch (Exception e) 
			{
				this.ex = e;
				return false;
			}
		}
	}

	public abstract class BoundedChainConditional<A> : BoundedChain<A> 
	{
		public Func<bool> Cond;
		
		internal BoundedChainConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
		
		internal bool TryCond () {
			try 
			{
				return Cond();
			}
			catch (Exception e) 
			{
				this.ex = e;
				return false;
			}
		}
	}

	public abstract class BoundedSeqConditional<A> : BoundedSequence<A> 
	{
		public Func<bool> Cond;
		
		internal BoundedSeqConditional (Func<bool> Cond)
		{
			this.Cond = Cond;
		}
		
		internal bool TryCond () {
			try 
			{
				return Cond();
			}
			catch (Exception e) 
			{
				this.ex = e;
				return false;
			}
		}
	}
}
