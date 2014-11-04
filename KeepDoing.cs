using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Atoms {
	public class KeepDoing : Atom {
		
		public Action f;
		
		public KeepDoing (Action f)
		{
			this.f = f;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			while (true)
			{
				f ();
				yield return null;
			}
		}
		
		public static KeepDoing _ (Action f) {
			return new KeepDoing (f);
		}
		
		public static KeepDoing<A> _<A> (Func<A> f) {
			return new KeepDoing<A> (f);
		}
		
	}
	
	public class KeepDoing<A> : Sequence<A> 
	{
		public Func<A> f;
		
		public KeepDoing (Func<A> f)
		{
			this.f = f;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;
		}
		
		public override IEnumerator<A> GetEnumerator ()
		{	
			while (true)
				yield return f();
		}
	}
}