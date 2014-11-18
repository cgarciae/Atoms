using System;
using System.Collections;

namespace Atoms {
	public class Wait : Atom {

		public Wait () {}

		internal override IEnumerable GetEnumerable ()
		{
			while (true)
				yield return null;
		}

		public static Wait _ () {
			return new Wait ();
		}
	}
}