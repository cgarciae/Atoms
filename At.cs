using UnityEngine;
using System.Collections;

namespace Atoms {
	public static partial class At {

		public static IEnumerable printAllTypes (this Quantum quantum) {
			foreach (var _ in quantum) yield return _;

			foreach (var q in quantum.previousQuanta) {
				Debug.Log (q.GetType());
			}
		}
	}
}
