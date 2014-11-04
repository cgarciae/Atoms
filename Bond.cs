using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Atoms
{
	public abstract class Bond<A,B> : BoundQuantum
	{
		public static Chain<B> operator * (Chain<A> chain, Bond<A,B> bond) {
			return chain.Bind (bond);
		}
	}

	public abstract class SeqBond<A,B> : BoundQuantum, IEnumerable<B>
	{
		public abstract IEnumerator<B> GetEnumerator ();

		internal override IEnumerable GetEnumerable ()
		{
			return GetEnumerator ().ToEnumerable ();
		}

		public static Sequence<B> operator * (Sequence<A> seq, SeqBond<A,B> bond) {
			return seq.Bind (bond);
		}
	}
}

//using UnityEngine;
//using System.Collections;
//
//namespace Atoms
//{
//	public abstract class Bond<A,B> : Quantum, IChain<B>
//	{
//		public abstract Bond<A,B> copyBond { get;}
//
//		public Chain<B> MakeChain () {
//			var copy = copyBond;
//			var chain = new Atomize<B> (copy, copy);
//			
//			return chain;
//		}
//
//		internal Bond () {}
//		internal Bond (Quantum prev, Quantum next) : base (prev, next) {}
//
//		public override Quantum copy {
//			get {
//				return copyBond;
//			}
//		}
//
//		public static Chain<B> operator * (Chain<A> a, Bond<A,B> b)
//		{
//			return a.Link (b);
//		}
//	}
//}
