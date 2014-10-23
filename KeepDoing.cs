//using System;
//using System.Collections;
//
//namespace Atoms {
//	public class KeepDoing : Atom {
//		
//		public Action f = Fn.DoNothing;
//		
//		public KeepDoing () {}
//		
//		public KeepDoing (Action f)
//		{
//			this.f = f;
//		}
//		
//		public KeepDoing (Action f, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.f = f;
//		}
//		
//		public override Atom copyAtom {
//			get {
//				return new KeepDoing (f, prev, next);
//			}
//		}
//		
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) yield return _;
//
//			while (true) {
//				try {
//					f ();
//				} catch (Exception e) {
//					ex = e;
//					break;
//				}
//				yield return null;
//			}
//		}
//		
//		public static KeepDoing _ (Action f) {
//			return new KeepDoing (f);
//		}
//		
//		public static KeepDoing<A> _<A> (Func<A> f) {
//			return new KeepDoing<A> (f);
//		}
//		
//	}
//	
//	public class KeepDoing<A> : Chain<A> {
//		
//		public Func<A> f;
//		
//		public KeepDoing (Func<A> f)
//		{
//			this.f = f;
//		}
//		
//		public KeepDoing (Func<A> f, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.f = f;
//		}
//		
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) {yield return _;}
//			
//			A a = default (A);
//
//			while (true) {
//				try
//				{
//					a = f();
//					
//					if (a == null)
//						throw new NullReferenceException("Function returned null reference");
//				}
//				catch (Exception e)
//				{
//					ex = e;
//					break;
//				}
//				
//				yield return a;
//			}
//		}
//		
//		
//		public override Chain<A> copyChain {
//			get {
//				return new KeepDoing<A> (f, prev, next);
//			}
//		}
//		
//		
//		
//	}
//}