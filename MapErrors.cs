using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public class MapErrors : Atom {

		Func<Exception, Exception> f;

		public MapErrors (Func<Exception, Exception> f)
		{
			this.f = f;
		}

		public MapErrors (Func<Exception, Exception> f, Quantum prev, Quantum next) : base (prev, next)
		{
			this.f = f;
		}

		public override Atom copyAtom {
			get {
				return new MapErrors (f, prev, next);
			}
		}

		internal override IEnumerable GetEnumerable ()
		{
			foreach (var _ in Previous()) yield return _;

			brokenQuanta.FMap (a => {
				try {
					a.ex = f (a.ex);
				}
				catch (Exception e) {
					ex = e;
				}
			});

			yield return null;
		}

		public static MapErrors _ (Func<Exception,Exception> f) {
			return new MapErrors (f);
		}

		public static MapErrors _ (Action<Exception> f) {
			return new MapErrors (f.ToFunc());
		}

	}
}