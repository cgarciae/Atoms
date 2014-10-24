using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Atoms 
{ 
	public class Join : Atom 
	{
		Atom a;
		Atom b;

		public Join (Atom a, Atom b)
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

	public class Join<A> : Chain<A> 
	{
		public Atom a;
		public Chain<A> b;
		
		public Join (Atom a, Chain<A> b)
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

	public class SeqJoin<A> : Sequence<A>
	{
		public Sequence<A> a;
		public Sequence<A> b;
		
		public SeqJoin (Sequence<A> a, Sequence<A> b)
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

	public class BoundJoin : Atom 
	{
		public Atom a;
		public BoundAtom b;
		
		public BoundJoin (Atom a, BoundAtom b)
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

	public class BoundJoin<A> : Chain<A> 
	{
		public Atom a;
		public BoundChain<A> b;
		
		public BoundJoin (Atom a, BoundChain<A> b)
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

	public class BoundSeqJoin<A> : Sequence<A> 
	{
		public Sequence<A>  a;
		public BoundSequence<A> b;
		
		public BoundSeqJoin (Sequence<A> a, BoundSequence<A> b)
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

	public class BoundJoin<A,B> : Chain<B> {
		
		public Chain<A> a;
		public Bond<A,B> b;
		
		public BoundJoin (Chain<A> a, Bond<A, B> b)
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

	public class BoundSeqJoin<A,B> : Sequence<B> {
		
		public Sequence<A> a;
		public SeqBond<A,B> b;
		
		public BoundSeqJoin (Sequence<A> a, SeqBond<A, B> b)
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

