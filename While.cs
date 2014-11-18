using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms
{
	public class While : BoundedAtomConditional 
	{

		public While (Func<bool> Cond) : base (Cond) {}

		internal override IEnumerable GetEnumerable ()
		{
			var enu = prev.GetEnumerator ();

			while (Cond()) 
			{
				enu.MoveNext();
				yield return enu.Current;
			}

		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;

			foreach (var q in prev.GetQuanta()) yield return q;
		}
		
		public static While _ (Func<bool> Cond) {
			return new While (Cond);
		}
	}

	public class While<A> : BoundedSeqConditional<A>
	{
		
		public While (Func<bool> Cond) : base (Cond) {}
		
		public override IEnumerator<A> GetEnumerator ()
		{
			var enu = (prev as Sequence<A>).GetEnumerator ();
			
			while (Cond()) 
			{
				enu.MoveNext();
				yield return enu.Current;
			}
			
		}
		
		public static While<A> _ (Func<bool> Cond) {
			return new While<A> (Cond);
		}
	}

	public abstract partial class Atom
	{
		public Atom While (Func<bool> cond)
		{
			return this.Then (Atoms.While._(cond));
		}
	}

	public abstract partial class Sequence<A>
	{
		public Sequence<A> While (Func<bool> cond)
		{
			return this.Then (Atoms.While<A>._(cond));
		}
	}
}