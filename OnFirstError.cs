using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public class OnFirstError : Atom {
		
		Func<Exception, Exception> f;
		
		public OnFirstError (Func<Exception, Exception> f)
		{
			this.f = f;
		}

		public OnFirstError (Func<Exception, Exception> f, Quantum prev, Quantum next) : base (prev, next)
		{
			this.f = f;
		}

		public override Atom copyAtom {
			get {
				return new OnFirstError (f, prev, next);
			}
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			foreach (var _ in Previous()) yield return _;

			firstBrokenQuantum.FMap (a => {
				try {
					a.ex = f (a.ex);
				}
				catch (Exception e) {
					ex = e;
				}
			});
			
			yield return null;
		}
		
		public static OnFirstError _ (Func<Exception,Exception> f) {
			return new OnFirstError (f);
		}
		
		public static OnFirstError _ (Action<Exception> f) {
			return new OnFirstError (f.ToFunc());
		}

		public static OnFirstError _ (Action f) {
			return new OnFirstError (f.ToFunc<Exception>());
		}
		
	}
}