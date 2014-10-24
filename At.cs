using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public static partial class At {

		//Start
		public static Coroutine Start (this IEnumerable e, MonoBehaviour m) 
		{
			return m.StartCoroutine (e.GetEnumerator ());
		}

		//ToEnumerable
		public static IEnumerable ToEnumerable (this IEnumerator e) {
			while (e.MoveNext())
				yield return e.Current;
		}

		public static IEnumerable<A> ToEnumerable<A> (this IEnumerator<A> e) {
			while (e.MoveNext())
				yield return e.Current;
		}

		//Map Compose
		public static Map<A,C> Next<A,B,C> (this Map<A,B> a, Map<B,C> b) 
		{
			return new Map<A, C> (Fn.Compose (b.f, a.f));
		} 

	}

	public interface IBound {
		Atom prev { get; set; }
	}
}
