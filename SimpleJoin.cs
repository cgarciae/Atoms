using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Atoms 
{ 
	public class SimpleJoin : SimpleAtom 
	{
		Atom a;
		SimpleAtom b;

		public SimpleJoin (Atom a, SimpleAtom b)
		{
			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			foreach (var _ in a) yield return _;

			foreach (var _ in b) yield return _;
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			foreach (var _ in b.GetQuanta()) yield return _;

			foreach (var _ in a.GetQuanta()) yield return _;
		}

		public override Quantum copy {
			get {
				return new SimpleJoin (a, b);
			}
		}
	}

	public class BoundJoin : SimpleAtom 
	{
		Atom a;
		BoundAtom b;
		
		public BoundJoin (Atom a, BoundAtom b)
		{
			b.prev = a;

			this.a = a;
			this.b = b;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			foreach (var _ in b) yield return _;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			foreach (var _ in b.GetQuanta()) yield return _;
		}
		
		public override Quantum copy {
			get {
				return new BoundJoin (a, b);
			}
		}
	}
}

