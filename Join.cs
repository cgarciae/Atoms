using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms 
{ 
	public class AtomJoinAtom : Atom 
	{
		Atom a;
		Atom b;

		public AtomJoinAtom (Atom a, Atom b)
		{
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return a.Join (b);
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ().Join (a.GetQuanta ());
		}
	}

	public class AtomJoinChain<A> : Chain<A> 
	{
		public Atom a;
		public Chain<A> b;
		
		public AtomJoinChain (Atom a, Chain<A> b)
		{
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return a.Join (b);
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ().Join (a.GetQuanta ());
		}
	}

	public class SeqJoinSeq<A> : Sequence<A>
	{
		public Sequence<A> a;
		public Sequence<A> b;
		
		public SeqJoinSeq (Sequence<A> a, Sequence<A> b)
		{
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return a.Join (b);
		}

		public override IEnumerator<Maybe<A>> GetEnumerator ()
		{
			return a.Join (b).GetEnumerator();
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ().Join (a.GetQuanta ());
		}
	}

	public class AtomJoinBoundAtom : Atom 
	{
		public Atom a;
		public BoundedAtom b;
		
		public AtomJoinBoundAtom (Atom a, BoundedAtom b)
		{
			b.prev = a;
			
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return b;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}

	public class BoundedAtomJoinBoundedAtom : BoundedAtom 
	{
		public BoundedAtom a;
		public BoundedAtom b;

		public override Quantum prev {
			get {
				return a.prev;
			}
			set {
				a.prev = value;
			}
		}
		
		public BoundedAtomJoinBoundedAtom (BoundedAtom a, BoundedAtom b)
		{
			b.prev = a;
			
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return b;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}

	public class AtomJoinBoundChain<A> : Chain<A> 
	{
		public Atom a;
		public BoundedChain<A> b;
		
		public AtomJoinBoundChain (Atom a, BoundedChain<A> b)
		{
			b.prev = a;
			
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return b;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}

	public class SeqJoinBoundedSeq<A> : Sequence<A> 
	{
		public Sequence<A>  a;
		public BoundedSequence<A> b;
		
		public SeqJoinBoundedSeq (Sequence<A> a, BoundedSequence<A> b)
		{
			b.prev = a;
			
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return b;
		}

		public override IEnumerator<Maybe<A>> GetEnumerator ()
		{
			return b.GetEnumerator ();
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}

	public class ChainJoinBond<A,B> : Chain<B> {
		
		public Chain<A> a;
		public Bond<A,B> b;
		
		public ChainJoinBond (Chain<A> a, Bond<A, B> b)
		{
			b.prev = a;
			
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return b;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}

	public class SeqJoinSeqBond<A,B> : Sequence<B> {
		
		public Sequence<A> a;
		public SeqBond<A,B> b;
		
		public SeqJoinSeqBond (Sequence<A> a, SeqBond<A, B> b)
		{
			b.prev = a;
			
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return b;
		}

		public override IEnumerator<Maybe<B>> GetEnumerator ()
		{
			return b.GetEnumerator ();
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}

	//Parallels
	public class AtomParallelAtom : Atom
	{
		public Atom a;
		public Atom b;

		public AtomParallelAtom (Atom a, Atom b)
		{
			this.a = a;
			this.b = b;
		}

		internal override IEnumerable GetEnumerable ()
		{
			var enuA = a.GetEnumerator ();
			var enuB = b.GetEnumerator ();

			while (enuA.MoveNext() | enuB.MoveNext()) 
			{
				yield return null;	
			}
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			foreach (var q in b.GetQuanta()) yield return q;

			foreach (var q in a.GetQuanta()) yield return q;
		}

		public static AtomParallelAtom _ (Atom a, Atom b)
		{
			return new AtomParallelAtom (a, b);	
		}
	}

	public class AtomParallelChain<A> : Chain<A>
	{
		public Atom a;
		public Chain<A> b;
		
		public AtomParallelChain (Atom a, Chain<A> b)
		{
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enuA = a.GetEnumerator ();
			var enuB = b.GetEnumerator ();
			
			while (enuA.MoveNext() | enuB.MoveNext()) 
			{
				yield return null;
			}

			yield return enuB.Current;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			foreach (var q in b.GetQuanta()) yield return q;
			
			foreach (var q in a.GetQuanta()) yield return q;
		}
		
		public static AtomParallelChain<A> _ (Atom a, Chain<A> b)
		{
			return new AtomParallelChain<A> (a, b);	
		}
	}

	public class ChainParallelChain<A,B> : Chain<Tuple<A,B>>
	{
		public Chain<A> a;
		public Chain<B> b;
		
		public ChainParallelChain (Chain<A> a, Chain<B> b)
		{
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enuA = a.GetEnumerator ();
			var enuB = b.GetEnumerator ();
			
			while (enuA.MoveNext() | enuB.MoveNext()) 
			{
				yield return null;
			}
			
			var maybeA = (Maybe<A>)enuA.Current;
			var maybeB = (Maybe<B>)enuB.Current;

			yield return Fn.Tuple<A,B> () .up (maybeA) .Apply (maybeB);
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			foreach (var q in b.GetQuanta()) yield return q;
			
			foreach (var q in a.GetQuanta()) yield return q;
		}
		
		public static ChainParallelChain<A,B> _ (Chain<A> a, Chain<B> b)
		{
			return new ChainParallelChain<A,B> (a, b);	
		}
	}
}

