using UnityEngine;
using System.Collections;

namespace Atoms {
	public static partial class At {

		//Start
		public static Coroutine Start (this IEnumerable e, MonoBehaviour m) 
		{
			return m.StartCoroutine (e.GetEnumerator ());
		}

	}
}
