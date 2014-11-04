using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms {
	public class Map<A,B> : Bond<A,B> {
		
		public Func<A,B> f;
		
		public Map (Func<A, B> f)
		{
			this.f = f;
		}

		internal override IEnumerable GetEnumerable ()
		{
			return prev.copy.MapLast (f);
		}

		public Map<B,C> MakeMap<C> (Func<B,C> f)
		{
			return new Map<B, C> (f);	
		}

		public Map<B,B> MakeMap (Action<B> f)
		{
			return MakeMap (f.ToFunc());
		}

		public static Map<A,B> _ (Func<A,B> f)
		{
			return new Map<A, B> (f);	
		}
	}

	public class SeqMap<A,B> : SeqBond<A,B>, IEnumerable<B>
	{
		public Func<A,B> f;

		public SeqMap (Func<A, B> f)
		{
			this.f = f;
		}

		public SeqMap<B,C> MakeMap<C> (Func<B,C> f)
		{
			return new SeqMap<B, C> (f);	
		}
		
		public SeqMap<B,B> MakeMap (Action<B> f)
		{
			return MakeMap (f.ToFunc());
		}
		
		public SeqBind<B,C> MakeBind<C> (Func<B,Sequence<C>> f)
		{
			return new SeqBind<B, C> (f);	
		}

		internal override IEnumerable GetEnumerable ()
		{
			return GetEnumerator ().ToEnumerable ();
		}

		public override IEnumerator<B> GetEnumerator ()
		{
			return ((IEnumerable<A>)prev).FMap (f).GetEnumerator();
		}

		public static SeqMap<A,B> _ (Func<A,B> f)
		{
			return new SeqMap<A, B> (f);
		}
	}
}
//
//		public Map (Func<A, B> f, Chain<A> c) : base (c, null)
//		{
//			this.f = f;
//		}
//
//		public Map (Func<A, B> f, Chain<A> c, Quantum next) : base (c, next)
//		{
//			this.f = f;
//		}
//
//		public Map<B,C> MakeMap<C> (Func<B,C> f) {
//			return Map<B,C>._ (f);
//		}
//		
//		public Map<B,B> MakeMap (Action<B> f) {
//			return Map<B,B>._ (f.ToFunc());
//		}
//
//		public Bind<B,C> MakeBind<C> (Func<B,Chain<C>> f) {
//			return Atoms.Bind._ (f);
//		}
//		
//		internal override IEnumerable GetEnumerable ()
//		{
//			var enu = Previous ().GetEnumerator ();
//
//			while (enu.MoveNext())
//				yield return enu.Current;
//
//
//			var a = (A)enu.Current;
//			B b = default (B);
//
//			if (a != null) {
//				try
//				{
//					b = f (a);
//
//					if (b == null)
//						throw new Exception ("Function returned null reference");
//
//				} 
//				catch (Exception e) 
//				{
//					ex = e;
//				}
//			}
//
//			yield return b;
//		}
//		
//		public override Bond<A,B> copyBond {
//			get {
//				var copy = new Map<A,B> (f);
//				copy.prev = prev;
//				copy.next = next;
//				
//				return copy;
//			}
//		}
//
//		public static Map<A,B> _ (Func<A,B> f) {
//			return new Map<A, B> (f);
//		}
//
////		public static Chain<B> operator * (Chain<A> c, Map<A,B> m)
////		{
////			return c.copyChain.FMap (m.f);
////		}
//		
//	}
//}