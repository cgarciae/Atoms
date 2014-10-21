using System;
using System.Collections;

namespace Atoms {
	public class Assert : Conditional {

		string message;

		public Assert (string message, Func<bool> Cond) : base (Cond)
		{
			this.message = message;
		}
		public Assert (string message, Func<bool> Cond, Quantum prev, Quantum next) : base (Cond, prev, next) 
		{
			this.message = message;
		}

		internal override IEnumerable GetEnumerable ()
		{
			foreach (var _ in Previous()) {yield return _;}

			AssertCond (message, Cond ());

		}

		public override Atom copyAtom {
			get {
				return new Assert (message, Cond, prev, next);
			}
		}

		void AssertCond (string message, bool cond) {
			if (!cond) {
				throw new Exception	(message);
			}
		}

		public static Assert _ (string message, Func<bool> Cond) {
			return new Assert (message, Cond);
		}
	}
}
