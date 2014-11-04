using System;
using Tatacoa;
using System.Collections;
using System.Collections.Generic;

namespace Atoms
{
	public class LazyAtom : Atom 
	{
		public Func<Atom> f;
		Atom atom;
		
		public LazyAtom (Func<Atom> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{	
			atom = f ();
			
			if (atom == null)
				throw new NullReferenceException ("Binding Function returned Null Atom");
			
			atom = atom.copy as Atom;
			
			foreach (var _ in atom) 
				yield return _;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (atom != null)
				yield return atom;
			
			yield return this;
		}
		
		public static LazyAtom _ (Func<Atom> f) 
		{
			return new LazyAtom (f);
		}
		
		public static LazyChain<A> _<A> (Func<Chain<A>> f) 
		{
			return new LazyChain<A> (f);
		}
		
		public static Bind<A,B> _<A,B> (Func<A,Chain<B>> f) 
		{
			return new Bind<A,B> (f);
		}
	}
	
	public class LazyChain<A> : Chain<A>
	{	
		public Func<Chain<A>> f;
		Chain<A> chain;
		
		public LazyChain (Func<Chain<A>> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{		
			chain = f ();
			
			if (chain == null)
				throw new NullReferenceException ("Function returned null reference");
			
			chain = (Chain<A>)chain.copy;
			
			foreach (var _ in chain) yield return _;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (chain != null)
				yield return chain;
			
			yield return this;
		}
	}
	
	public class LazySeq<A> : Sequence<A>
	{	
		public Func<Sequence<A>> f;
		Sequence<A> seq;
		
		public LazySeq (Func<Sequence<A>> f)
		{
			this.f = f;
		}
		
		public override IEnumerator<A> GetEnumerator ()
		{		
			seq = f ();
			
			if (seq == null)
				throw new NullReferenceException ("Function returned null reference");
			
			seq = (Sequence<A>)seq.copy;
			
			foreach (var a in seq) 
				yield return a;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (seq != null)
				foreach (var q in seq.GetQuanta())
					yield return q;
			
			yield return this;
		}
		
		public static LazySeq<A> _ (Func<Sequence<A>> f)
		{
			return new LazySeq<A> (f);
		}
	}
}
