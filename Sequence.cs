using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms 
{
	public abstract partial class Sequence<A> : Chain<A>, Monad<A>, IEnumerable<A>
	{
		public Sequence<B> MapEach<B> (Func<A,B> f)
		{
			return new SeqJoinSeqBond<A,B> (this, SeqMap<A,B>._ (f));
		}

		public Sequence<B> BindEach<B> (Func<A,Sequence<B>> f)
		{
			return new SeqJoinSeqBond<A,B> (this, SeqBind<A,B>._ (f));
		}

		public Chain<B> BindEach<B> (Func<A,Chain<B>> f)
		{
			return new SeqJoinSeqBondChain<A,B> (this, BindEach<A,B>._ (f));
		}

		public Chain<B> BindEach<B> (SeqBondChain<A,B> bond)
		{
			return new SeqJoinSeqBondChain<A,B> (this, bond.copy as SeqBondChain<A,B>);
		}

		public Atom BindEach (Func<A,Atom> f)
		{
			return new SeqJoinSeqBondAtom<A> (this, Atoms.BindEach<A>._ (f));
		}
		
		public Atom BindEach (SeqBondAtom<A> bond)
		{
			return new SeqJoinSeqBondAtom<A> (this, bond.copy as SeqBondAtom<A>);
		}

		public Sequence<B> Then<B> (SeqBond<A,B> bond)
		{
			return new SeqJoinSeqBond<A,B> (this.copy as Sequence<A>, bond.copy as SeqBond<A,B>);
		}

		public Sequence<A> Then (Func<Sequence<A>> f)
		{
			return Then (LazySeq<A>._ (f));
		}

		Monad<B> Monad<A>.Bind<B> (Func<A, Monad<B>> f)
		{
			throw new NotImplementedException ();
		}

		public Sequence<A> Then (Sequence<A> seq)
		{
			return new SeqJoinSeq<A> (this.copy as Sequence<A>, seq.copy as Sequence<A>);
		}
		
		public Sequence<A> Then (BoundedSequence<A> boundedSeq)
		{
			return new SeqJoinBoundSeq<A> (this.copy as Sequence<A>, boundedSeq.copy as BoundedSequence<A>);
		}

		public new Functor<A> Pure (A value)
		{
			throw new NotImplementedException ();
		}

		new Functor<B> FMap<B> (Func<A, B> f)
		{
			throw new NotImplementedException ();
		}

		public new Functor<A> XMap (Func<Exception, Exception> f)
		{
			throw new NotImplementedException ();
		}

		public SeqMap<A,B> MakeMap<B> (Func<A,B> f)
		{
			return new SeqMap<A, B> (f);	
		}
		
		public SeqMap<A,A> MakeMap (Action<A> f)
		{
			return MakeMap (f.ToFunc());
		}
		
		public SeqBind<A,B> MakeBind<B> (Func<A,Sequence<B>> f)
		{
			return new SeqBind<A, B> (f);	
		}

		public new abstract IEnumerator<A> GetEnumerator ();

		internal override IEnumerable GetEnumerable ()
		{
			return GetEnumerator ().ToEnumerable ();
		}



		public static Sequence<A> operator + (Sequence<A> a, Sequence<A> b)
		{
			return a.Then (b);
		}

		public static Sequence<A> operator + (Sequence<A> a, BoundedSequence<A> b)
		{
			return a.Then (b);
		}

		public static Sequence<A> operator * (int n, Sequence<A> seq) 
		{
			return seq.Replicate (n);
		}
		
		public static Sequence<A> operator * (Sequence<A> seq, int n) 
		{
			return n * seq;
		}
	}

	public abstract class BoundedSequence<A> : BoundedChain<A>, Monad<A>, IEnumerable<A>
	{
		public new Monad<B> Then<B> (Func<A, Monad<B>> f)
		{
			throw new NotImplementedException ();
		}
		
		public new Functor<A> Pure (A value)
		{
			throw new NotImplementedException ();
		}
		
		public new Functor<B> Then<B> (Func<A, B> f)
		{
			throw new NotImplementedException ();
		}
		
		public new Functor<A> XMap (Func<Exception, Exception> f)
		{
			throw new NotImplementedException ();
		}

		internal override IEnumerable GetEnumerable ()
		{
			return GetEnumerator ().ToEnumerable ();
		}

		public new abstract IEnumerator<A> GetEnumerator ();
	}
}
