//using UnityEngine;
//using System.Collections;
//
//namespace Atoms {
//	public class DetachJoin : Atom
//	{
//		public Atom first;
//		public Atom second;
//
//		public DetachJoin (Atom first, Atom second)
//		{
//			this.first = first;
//			this.second = second;;
//		}
//
//		public DetachJoin (Atom first, Atom second, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.first = first;
//			this.second = second;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) yield return _;
//
//			foreach (var _ in first + second) yield return _;
//		}
//
//		public override Atom copyAtom {
//			get {
//				return new DetachJoin (first, second, prev, next);
//			}
//		}
//
//		public static DetachJoin _ (Atom first, Atom second)
//		{
//			return new DetachJoin (first, second);	
//		}
//
//		public static DetachJoin<A> _<A> (Atom first, Chain<A> second)
//		{
//			return new DetachJoin<A> (first, second);	
//		}
//
//	}
//
//	public class DetachJoin<A> : Chain<A>
//	{
//		public Atom first;
//		public Chain<A> second;
//		
//		public DetachJoin (Atom first, Chain<A> second)
//		{
//			this.first = first;
//			this.second = second;
//		}
//		
//		public DetachJoin (Atom first, Chain<A> second, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.first = first;
//			this.second = second;
//		}
//		
//		internal override IEnumerable GetEnumerable ()
//		{
//			Debug.Log (prev);
//			foreach (var _ in Previous()) yield return _;
//			
//			foreach (var _ in first + second) yield return _;
//		}
//		
//		public override Chain<A> copyChain {
//			get {
//				return new DetachJoin<A> (first, second, prev, next);
//			}
//		}
//		
//	}
//}
