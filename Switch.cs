using System;
using Tatacoa;
using System.Collections;
using System.Collections.Generic;

namespace Atoms
{
	public class Switch : AtomConditional 
	{
		public Atom whenTrue;
		public Atom whenFalse;

		public Switch (Func<bool> cond, Atom whenTrue, Atom whenFalse) : base (cond)
		{
			this.whenTrue = whenTrue;
			this.whenFalse = whenFalse;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enuTrue = whenTrue.GetEnumerator ();
			var enuFalse = whenFalse.GetEnumerator ();

			var active = true;

			while (active)
			{
				if (Cond())
				{
					active = enuTrue.MoveNext();
					yield return enuTrue.Current;
				}
				else
				{
					active = enuFalse.MoveNext();
					yield return enuFalse.Current;
				}
			}
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			throw new NotImplementedException ();
		}

		public static Switch _ (Func<bool> cond, Atom whenTrue, Atom whenFalse)
		{
			return new Switch (cond, whenTrue, whenFalse);
		}
	}
}
