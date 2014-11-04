using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms 
{
	public abstract partial class Sequence<A> : Chain<A>, Monad<A>, IEnumerable<A>
	{
		public Sequence<B> FMap<B> (Func<A,B> f)
		{
			return new SeqJoinSeqBond<A,B> (this, SeqMap<A,B>._ (f));
		}

		public Sequence<B> Bind<B> (Func<A,Sequence<B>> f)
		{
			return new SeqJoinSeqBond<A,B> (this, SeqBind<A,B>._ (f));
		}

		public Sequence<B> Bind<B> (SeqBond<A,B> bond)
		{
			return new SeqJoinSeqBond<A,B> (this.copy as Sequence<A>, bond.copy as SeqBond<A,B>);
		}

		public Sequence<A> Then<A> (Func<Sequence<A>> f)
		{
			return Then (LazySeq<A>._ (f));
		}

		public new Monad<B> Bind<B> (Func<A, Monad<B>> f)
		{
			throw new NotImplementedException ();
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

		public Sequence<A> Then (Sequence<A> seq)
		{
			return new SeqJoinSeq<A> (this.copy as Sequence<A>, seq.copy as Sequence<A>);
		}

		public Sequence<A> BoundBy (BoundedSequence<A> boundedSeq)
		{
			return new SeqJoinBoundSeq<A> (this.copy as Sequence<A>, boundedSeq.copy as BoundedSequence<A>);
		}

		public static Sequence<A> operator + (Sequence<A> a, Sequence<A> b)
		{
			return a.Then (b);
		}

		public static Sequence<A> operator + (Sequence<A> a, BoundedSequence<A> b)
		{
			return a.BoundBy (b);
		}

		public static Sequence<A> operator * (int n, Sequence<A> seq) 
		{
			return seq.CycleN (n);
		}
		
		public static Sequence<A> operator * (Sequence<A> seq, int n) 
		{
			return n * seq;
		}
	}

	public abstract class BoundedSequence<A> : BoundedChain<A>, Monad<A>, IEnumerable<A>
	{
		public new Monad<B> Bind<B> (Func<A, Monad<B>> f)
		{
			throw new NotImplementedException ();
		}
		
		public new Functor<A> Pure (A value)
		{
			throw new NotImplementedException ();
		}
		
		public new Functor<B> FMap<B> (Func<A, B> f)
		{
			throw new NotImplementedException ();
		}
		
		public new Functor<A> XMap (Func<Exception, Exception> f)
		{
			throw new NotImplementedException ();
		}

		public new abstract IEnumerator<A> GetEnumerator ();
	}
}
