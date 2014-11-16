using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms
{
	public class Cycle : BoundedAtom {

		public Cycle ()
		{

		}
		
		internal override IEnumerable GetEnumerable ()
		{
			while (true)
				foreach (var _ in prev.copy as Atom)
					yield return _;
		}

		public static Cycle _ ()
		{
			return new Cycle ();
		}

		public static Cycle<A> _<A> ()
		{
			return new Cycle<A> ();
		}
	}

	public class Cycle<A> : BoundedSequence<A> {
		
		public Cycle ()
		{

		}
		
		public override IEnumerator<A> GetEnumerator ()
		{
			while (true)
				foreach (var _ in prev.copy as Sequence<A>)
					yield return _;
		}
	}

	public abstract partial class Atom
	{
		public Atom Cycle ()
		{
			return this.BoundedBy (Atoms.Cycle._ ());
		}
	}

	public abstract partial class Sequence<A>
	{
		public Atom Cycle ()
		{
			return this.BoundedBy (Atoms.Cycle._<A> ());
		}
	}
	
}
