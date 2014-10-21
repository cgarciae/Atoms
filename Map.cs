using System;
using System.Collections;

namespace Atoms {
	public class Map<A,B> : Quantum, IChain<B> {
		
		public Func<A,B> f;
		
		public Map (Func<A, B> f)
		{
			this.f = f;
		}

		public Map (Func<A, B> f, Chain<A> c) : base (c, null)
		{
			this.f = f;
		}

		public Map (Func<A, B> f, Chain<A> c, Quantum next) : base (c, next)
		{
			this.f = f;
		}

		public Map<B,C> MakeMap<C> (Func<B,C> f) {
			return Map<B,C>._ (f);
		}
		
		public Map<B,B> MakeMap (Action<B> f) {
			return Map<B,B>._ (f);
		}

		public Bind<B,C> MakeBind<C> (Func<B,Chain<C>> f) {
			return Atoms.Bind._ (f);
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enu = Previous ().GetEnumerator ();

			while (enu.MoveNext())
				yield return enu.Current;


			B b = default (B);

			if (b != null) { 
				try
				{
					b = f ((A)enu.Current);
				} 
				catch (Exception e) 
				{
					ex = e;
				}
			}

			yield return b;
		}

		public Chain<B> MakeChain ()
		{
			var copy = copyQuantum;
			var chain = new Atomize<B> (copy as IChain<B>, copy);

			return chain;
		}
		
		public override Quantum copyQuantum {
			get {
				var copy = new Map<A,B> (f);
				copy.prev = prev;
				copy.next = next;
				
				return copy;
			}
		}

		public static Map<A,B> _ (Func<A,B> f) {
			return new Map<A, B> (f);
		}
		
		public static Map<A,A> _ (Action<A> f) {
			return new Map<A, A> (f.ToFunc());
		}

		public static Chain<B> operator % (Chain<A> c, Map<A,B> m)
		{
			return c.copyChain.FMap (m.f);
		}
		
	}
}