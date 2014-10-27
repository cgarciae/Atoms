using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms 
{
	public abstract class Sequence<A> : Chain<A>, Monad<A>, IEnumerable<Maybe<A>>
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

		public new abstract IEnumerator<Maybe<A>> GetEnumerator ();
	}

	public abstract class BoundedSequence<A> : BoundedChain<A>, Monad<A>, IEnumerable<Maybe<A>>
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

		public new abstract IEnumerator<Maybe<A>> GetEnumerator ();
	}
}
