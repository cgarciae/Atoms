using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;

namespace Atoms {

	public class Bind<A,B> : Bond<A,B>
	{	
		public Func<A,Chain<B>> f;
		Chain<B> chain;
		
		public Bind (Func<A,Chain<B>> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var enu = prev.GetEnumerator ();
			while (enu.MoveNext()) yield return enu.Current;
			
			
			var a = (A) enu.Current;
			
			chain = f (a);
			
			if (chain == null)
				throw new NullReferenceException ("Function returned null chain");
			
			chain = (Chain<B>)chain.copy;
			
			foreach (var _ in chain) yield return _;
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (chain != null)
				return chain.GetQuanta ().Join (Fn.AppendL (this, prev.GetQuanta ()));
			else
				return Fn.AppendL (this, prev.GetQuanta ());
		}
	}

	public class BindEach<A,B> : SeqBondChain<A,B>
	{	
		public Func<A,Chain<B>> f;
		Chain<B> chain;
		
		public BindEach (Func<A,Chain<B>> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var prevSeq = ((Sequence<A>)prev);
			
			foreach (var a in prevSeq)
			{
				chain = f (a);
				
				if (chain == null)
					throw new NullReferenceException ("Function returned null chain");
				
				foreach (var b in (Chain<B>)chain.copy) 
					yield return b;
			}
		}

		public static BindEach<A,B> _ (Func<A,Chain<B>> f)
		{
			return new BindEach<A, B> (f);
		}
	}

	public class BindEach<A> : SeqBondAtom<A>
	{	
		public Func<A,Atom> f;
		Atom atom;
		
		public BindEach (Func<A,Atom> f)
		{
			this.f = f;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			var prevSeq = ((Sequence<A>)prev);
			
			foreach (var a in prevSeq)
			{
				atom = f (a);
				
				if (atom == null)
					throw new NullReferenceException ("Function returned null atom");
				
				foreach (var b in (Atom)atom.copy) 
					yield return b;
			}
		}
		
		public static BindEach<A> _ (Func<A,Atom> f)
		{
			return new BindEach<A> (f);
		}
	}

	public class SeqBind<A,B> : SeqBond<A,B>
	{	
		public Func<A,Sequence<B>> f;
		Sequence<B> seq;
		
		public SeqBind (Func<A,Sequence<B>> f)
		{
			this.f = f;
		}
		
		public override IEnumerator<B> GetEnumerator ()
		{
			var prevSeq = ((Sequence<A>)prev);

			foreach (var a in prevSeq)
			{
				seq = f (a);
				
				if (seq == null)
					throw new NullReferenceException ("Function returned null chain");
				
				foreach (var b in (Sequence<B>)seq.copy) 
					yield return b;
			}
		}
		
		public override IEnumerable<Quantum> GetQuanta ()
		{
			if (seq != null)
				return seq.GetQuanta ().Join (Fn.AppendL (this, prev.GetQuanta ()));
			else
				return Fn.AppendL (this, prev.GetQuanta ());
		}

		public static SeqBind<A,B> _ (Func<A,Sequence<B>> f)
		{
			return new SeqBind<A, B> (f);
		}
	}
}
//
//		public Bind (Func<Atom> f)
//		{
//			this.f = f;
//		}
//		
//		public Bind (Func<Atom> f, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.f = f;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) yield return _;
//
//			Atom atom = null;
//			try
//			{
//				atom = f ().copyAtom;
//				atom.cancelPrevious = true;
//
//				(this). SetNext (atom);
//
//				if (atom == null)
//					throw new NullReferenceException("Function returned null Atom");
//			}
//			catch (Exception e)
//			{
//				ex = e;
//			}
//
//			if (atom != null)
//			{
//				foreach (var _ in atom) yield return _;		
//			}
//		}
//
//		public override Atom copyAtom {
//			get {
//				return new Bind (f, prev, next);
//			}
//		}
//
//		public static Bind _ (Func<Atom> f) {
//			return new Bind (f);
//		}
//
//		public static Bind<A> _<A> (Func<Chain<A>> f) {
//			return new Bind<A> (f);
//		}
//
//		public static Bind<A,B> _<A,B> (Func<A,Chain<B>> f) {
//			return new Bind<A,B> (f);
//		}
//
//	}
//
//	public class Bind<A> : Chain <A> {
//
//		public Func<Chain<A>> f;
//
//		public Bind (Func<Chain<A>> f)
//		{
//			this.f = f;
//		}
//
//		public Bind (Func<Chain<A>> f, Quantum prev, Quantum next) : base (prev, next)
//		{
//			this.f = f;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			foreach (var _ in Previous()) yield return _;
//			
//			Chain<A> chain = null;
//			try
//			{
//				chain = f ().copyChain;
//				chain.cancelPrevious = true;
//				
//				(this). SetNext (chain);
//				
//				if (chain == null)
//					throw new NullReferenceException("Function returned null Chain");
//			}
//			catch (Exception e)
//			{
//				ex = e;
//			}
//			
//			if (chain != null)
//			{
//				foreach (var _ in chain) yield return _;		
//			}
//		}
//
//		public override Chain<A> copyChain {
//			get {
//				return new Bind<A> (f, prev, next);
//			}
//		}
//		
//
//	}
//
//	public class Bind<A,B> : Bond<A,B> {
//		
//		public Func<A, Chain<B>> f;
//		
//		public Bind (Func<A, Chain<B>> f)
//		{
//			this.f = f;
//		}
//
//		public Bind (Func<A, Chain<B>> f, Chain<A> c) : base (c, null)
//		{
//			this.f = f;
//		}
//		
//		public Bind (Func<A, Chain<B>> f, Chain<A> c, Quantum next) : base (c, next)
//		{
//			this.f = f;
//		}
//
//		internal override IEnumerable GetEnumerable ()
//		{
//			var enu = Previous ().GetEnumerator ();
//			
//			while (enu.MoveNext())
//				yield return enu.Current;
//			
//			A a = (A)enu.Current;
//			Chain<B> chain = null;
//
//			if (a != null) { 
//				try
//				{
//					chain = f (a);
//
//					if (chain == null)
//						throw new Exception("Function f returned null chain");
//				} 
//				catch (Exception e) 
//				{
//					ex = e;
//				}
//
//				if (chain != null)
//					foreach (var _ in chain) yield return _;
//			}
//
//
//		}
//
//		public override Bond<A,B> copyBond {
//			get {
//				var copy = new Bind<A,B> (f);
//				copy.prev = prev;
//				copy.next = next;
//				
//				return copy;
//			}
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
////		public static Chain<B> operator * (Chain<A> c, Bind<A,B> b)
////		{
////			return c.copyChain.Bind (b.f);
////		}
//	}
//}
