using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Atoms {
	public abstract class Quantum : IEnumerable {

		public Exception ex;

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

		//Exception Handling
		public IEnumerable<Exception> exceptions {
			get {
				return GetQuanta ().FMap (q => q.ex).Filter (e => e != null);
			}
		}

		public Maybe<Exception> firstException {
			get {
				return exceptions.MaybeLast();
			}
		}

		public Maybe<Exception> lastException {
			get {
				return exceptions.MaybeHead();
			}
		}
	}

	public abstract class BoundQuantum : Quantum {
		public virtual Quantum prev { get; set; }
	}
}


