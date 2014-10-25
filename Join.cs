using UnityEngine;
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

		public override IEnumerator<A> GetEnumerator ()
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

		public override IEnumerator<A> GetEnumerator ()
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

		public override IEnumerator<B> GetEnumerator ()
		{
			return b.GetEnumerator ();
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			return b.GetQuanta ();
		}
	}
}

