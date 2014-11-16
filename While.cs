using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms
{
	public class While : BoundedAtomConditional {

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

	public abstract partial class Atom
	{
		public Atom While (Func<bool> cond)
		{
			return this.BoundedBy (Atoms.While._(cond));
		}
	}
}