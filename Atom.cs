using System;
using System.Collections;

namespace Atoms {
	public abstract class Atom : Quantum {
		
		internal  Atom () {}
		
		internal Atom (Quantum prev, Quantum next) : base (prev, next){}
		
		public override Quantum copyQuantum {	
			get {
				return copyAtom;
			}
		}
		
		public abstract Atom copyAtom { get; }
		
		public static Atom operator + (Atom a, Atom b) {
			var copyB = b.copyAtom;
			var copyA = a.copyAtom;

			return (copyA) .SetNext (copyB) as Atom;
		}
		
	}
}
