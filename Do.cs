using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Atoms {
	public class Do : Atom {

		public Action f;

		public Do (Action f)
		{
			this.f = f;
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;
		}

		internal override IEnumerable GetEnumerable ()
		{
			f ();
			yield return null;
		}

		public static Do _ (Action f) {
			return new Do (f);
		}

		public static Do<A> _<A> (Func<A> f) {
			return new Do<A> (f);
		}

	}

	public class Do<A> : Sequence<A> 
	{
		public Func<A> f;

		public Do (Func<A> f)
		{
			this.f = f;
		}

		public override IEnumerator<A> GetEnumerator ()
		{	
			yield return f();
		}
	}

	public abstract partial class Atom
	{
		public Atom Do (Action f)
		{
			return this.Then (Atoms.Do._ (f));
		}

		public Chain<A> Do<A> (Func<A> f)
		{
			return this.Then (Atoms.Do._ (f));
		}
	}
}