using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

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

		//MapLast
		public static IEnumerable MapLast<A,B> (this IEnumerable e, Func<A,B> f)
		{
			var enu = e.GetEnumerator();
			
			while (enu.MoveNext())
				yield return enu.Current;
			
			yield return f ((A) enu.Current);
		}

		//Atomize
		public static Atom Atomize (this IEnumerable e)
		{
			return new Atoms.Atomize (e);
		}

		public static Chain<A> Atomize<A> (this IEnumerable e)
		{
			return new Atoms.Atomize<A> (e);
		}

		public static Sequence<A> Atomize<A> (this IEnumerable<A> e)
		{
			return new Atoms.AtomizeSeq<A> (e);
		}

	}

	public interface IBound {
		Atom prev { get; set; }
	}	

	public abstract partial class Sequence<A> 
	{
		public Sequence<A> Replicate (int n)
		{
			return Fn.Replicate (this, n).FoldL1 ((sum, next) => sum.Then (next));
		}
	}

	public abstract partial class Chain<A> 
	{
		public Chain<A> Replicate (int n)
		{
			return Fn.Replicate (this, n).FoldL1<Chain<A>> ((sum, next) => sum.Then (next));
		}
	}

	public abstract partial class Atom
	{
		public Atom Replicate (int n)
		{
			return Fn.Replicate (this, n).FoldL1<Atom> ((sum, next) => sum + next);
		}
	}
}

