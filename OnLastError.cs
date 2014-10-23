//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//namespace Atoms {
//	public class OnLastError : Atom {
//		
//		Func<Exception, Exception> f;
//		
//		public OnLastError (Func<Exception, Exception> f)
//		{
//			this.f = f;
//		}
//
//		public OnLastError (Func<Exception, Exception> f, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.f = f;
//		}
//
//		public override Atom copyAtom {
//			get {
//				return new OnLastError (f, prev, next);
//			}
//		}
//		
//		internal override IEnumerable GetEnumerable ()
//		{	
//			foreach (var _ in Previous()) yield return _;
//
//			lastBrokenQuantum.FMap (a => {
//				try {
//					a.ex = f (a.ex);
//				}
//				catch (Exception e) {
//					ex = e;
//				}
//			});
//			
//			yield return null;
//		}
//		
//		public static OnLastError _ (Func<Exception,Exception> f) {
//			return new OnLastError (f);
//		}
//		
//		public static OnLastError _ (Action<Exception> f) {
//			return new OnLastError (f.ToFunc());
//		}
//
//		public static OnLastError _ (Action f) {
//			return new OnLastError (f.ToFunc<Exception>());
//		}
//	}
//}