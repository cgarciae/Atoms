using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tatacoa;

namespace Atoms {
	public abstract class Quantum : IEnumerable {

		internal abstract IEnumerable GetEnumerable ();

		public Quantum copy {
			get{
				return MemberwiseClone() as Quantum;
			}
		}
		public virtual IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;
		}
		
		public IEnumerator GetEnumerator ()
		{
			return GetEnumerable ().GetEnumerator ();
		}
	}

	public abstract class BoundQuantum : Quantum {
		public virtual Quantum prev { get; set; }

		public override IEnumerable<Quantum> GetQuanta ()
		{
			return Fn.AppendL (this, prev.copy.GetQuanta());
		}
	}
}


