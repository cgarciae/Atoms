using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms
{
	public class CatchError<E> : BoundedAtom where E : Exception {

		public Action<E> f;

		public CatchError (Action<E> f)
		{
			this.f = f;
		}


		internal override IEnumerable GetEnumerable ()
		{
			var enu = prev.GetEnumerator ();

			Func<bool> TryMoveNext = () => 
			{
				try {
					return enu.MoveNext();
				}
				catch (E e)
				{
					f (e);
					return false;
				}
			};

			while (TryMoveNext())
				yield return enu.Current;

		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;

			foreach (var q in prev.GetQuanta()) yield return q;
		}

		public static CatchError<E> _ (Action<E> f) 
		{
			return new CatchError<E> (f);
		}

		public static CatchErrorBindAtom<E> _ (Func<E,Atom> f) 
		{
			return new CatchErrorBindAtom<E> (f);
		}

		public static CatchErrorBindChain<E,A> _<A> (Func<E,Chain<A>> f) 
		{
			return new CatchErrorBindChain<E, A> (f);
		}

		public static CatchErrorBindChain<E,A> Map<A> (Func<E,A> f) 
		{
			Func<A,Chain<A>> g = a => Do._ (() => a);

			return new CatchErrorBindChain<E, A> ((g) .o (f));
		}
	}

	public class CatchErrorBindAtom<E> : BoundedAtom where E : Exception {

		public Func<E,Atom> f;
		
		bool failed = false;
		Atom atom;

		public CatchErrorBindAtom (Func<E, Atom> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enu = prev.GetEnumerator ();

			failed = false;
			atom = null;
			
			Func<bool> TryMoveNext = () => 
			{
				try 
				{
					return enu.MoveNext();
				}
				catch (E e)
				{
					atom = (Atom) f (e).copy;
					failed = true;
					return false;
				}
			};
			
			while (TryMoveNext())
				yield return enu.Current;

			if (failed)
				foreach (var _ in atom)
					yield return _;
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (failed)
				foreach (var q in atom.GetQuanta()) yield return q;

			yield return this;
			
			foreach (var q in prev.GetQuanta()) yield return q;
		}
	}

	public class CatchErrorBindChain<E,A> : BoundedChain<A> where E : Exception {
		
		public Func<E,Chain<A>> f;

		bool failed = false;
		Chain<A> chain;
		
		public CatchErrorBindChain (Func<E,Chain<A>> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enu = prev.GetEnumerator ();
			chain = null;
			failed = false;
			
			Func<bool> TryMoveNext = () => 
			{
				try {
					return enu.MoveNext();
				}
				catch (E e)
				{
					chain = f (e);
					failed = true;
					return false;
				}
			};
			
			while (TryMoveNext())
				yield return enu.Current;
			
			if (failed)
				foreach (var _ in chain)
					yield return _;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (failed)
				foreach (var q in chain.GetQuanta()) yield return q;
			
			yield return this;
			
			foreach (var q in prev.GetQuanta()) yield return q;
		}
	}
}