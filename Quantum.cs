using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Atoms {
	public abstract class Quantum : IEnumerable {

		internal abstract IEnumerable GetEnumerable ();

		public Quantum copy {
			get{
				return MemberwiseClone() as Quantum;
			}
		}
		public abstract IEnumerable<Quantum> GetQuanta ();
		
		public IEnumerator GetEnumerator ()
		{
			return GetEnumerable ().GetEnumerator ();
		}
	}

	public abstract class BoundQuantum : Quantum {
		public virtual Quantum prev { get; set; }
	}
}


