using System;
using UnityEngine;
using System.Collections;
using System.Linq;

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

	public class WaitNIterations : Atom
	{
		public int n;

		public WaitNIterations (int n)
		{
			this.n = n;
		}

		internal override IEnumerable GetEnumerable ()
		{
			foreach (var _ in Enumerable.Range (0, n))
				yield return null;
		}

		public static WaitNIterations _ (int n)
		{
			return new WaitNIterations (n);
		}
	}

	public class WaitFor : Atom
	{
		public float t;

		public WaitFor (float t)
		{
			this.t = t;
		}

		internal override IEnumerable GetEnumerable ()
		{
			var time = Time.time;

			while (Time.time < time + t)
				yield return null;
		}

		public static WaitFor _ (float t)
		{
			return new WaitFor (t);
		}
	}

	public abstract class Delay : Atom
	{

		public static Atom _ (Atom atom)
		{
			return Atoms.WaitNIterations._ (1).Then (atom);
		}

		public static Chain<A> _<A> (Chain<A> chain)
		{
			return Atoms.WaitNIterations._ (1).Then (chain);
		}

		public static Atom _ (Action f)
		{
			return Atoms.WaitNIterations._ (1).Do (f);
		}

		public static Chain<A> _<A> (Func<A> f)
		{
			return Atoms.WaitNIterations._ (1).Do (f);
		}
	}

	public abstract partial class Atom
	{
		public Atom Delay (Atom atom)
		{
			return this.Then (Atoms.Delay._ (atom));
		}
		
		public Chain<A> Delay<A> (Chain<A> chain)
		{
			return this.Then (Atoms.Delay._ (chain));
		}
		
		public Atom Delay (Action f)
		{
			return this.Then (Atoms.Delay._ (f));
		}
		
		public Chain<A> Delay<A> (Func<A> f)
		{
			return this.Then (Atoms.Delay._(f));
		}

		public Atom Wait ()
		{
			return this.Then (Atoms.Wait._());
		}

		public Atom WaitNIterations (int iterations)
		{
			return this.Then (Atoms.WaitNIterations._(iterations));
		}

		public Atom WaitFor (float seconds)
		{
			return this.Then (Atoms.WaitFor._(seconds));
		}
	}
}