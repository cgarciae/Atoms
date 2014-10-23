using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public abstract class Atom : Quantum {
		
		internal  Atom () {}

	}

	public abstract class SimpleAtom : Atom 
	{
		public static SimpleAtom operator + (Atom a, SimpleAtom b)
		{
			return new SimpleJoin (a.copy as Atom, b.copy as SimpleAtom);
		}
	}

	public abstract class BoundAtom : Atom 
	{
		public Atom prev;

		public static SimpleAtom operator + (Atom a, BoundAtom b)
		{
			return new BoundJoin (a.copy as Atom, b.copy as BoundAtom);
		}
	}
}
