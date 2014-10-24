using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public abstract class Atom : Quantum {


		public static Atom operator + (Atom a, Atom b)
		{
			return new Join (a.copy as Atom, b.copy as Atom);
		}

		public static Atom operator + (Atom a, BoundAtom b)
		{
			return new BoundJoin (a.copy as Atom, b.copy as BoundAtom);
		}
	}

	public abstract class BoundAtom : Atom 
	{
		public Atom prev;
	}
}
