using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public class Atomize : Atom {

		public IEnumerable e;

		public Atomize (IEnumerable e)
		{
			this.e = e;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return e;
		}
	}

	public class Atomize<A> : Chain<A> 
	{	
		public IEnumerable e;
		
		public Atomize (IEnumerable e)
		{
			this.e = e;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return e;
		}
	}

	public class AtomizeSeq<A> : Sequence<A> 
	{	
		public IEnumerable<A> e;
		
		public AtomizeSeq (IEnumerable<A> e)
		{
			this.e = e;
		}
		
		public override IEnumerator<A> GetEnumerator ()
		{
			return e.GetEnumerator ();
		}
	}
}