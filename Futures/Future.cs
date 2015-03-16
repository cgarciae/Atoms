using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Futures
{
	public interface Future<A>
	{
		
		Future<B> Then<B> (Func<A,B> f);
		Future<B> Then<B> (Func<B> f);
		Future<B> Then<B> (Func<A,Future<B>> f);
		Future<B> Then<B> (Func<Future<B>> f);	
		Future<A> Then (Action<A> f);
		Future<A> Then (Action f);
		
		//TODO: Future<A> CatchError<E> (Action<E> g);
		
		bool completed {get;}
		
	}
	
	public abstract class FutureBase<A> : Future<A>
	{
		public abstract Future<C> Return<C> (C value);
		
		public abstract Future<B> Then<B> (Func<A,Future<B>> f);
		
		public Future<B> Then<B> (Func<A,B> f)
		{
			return Then<B> ((A a) => Return (f(a)));
		}
		
		public Future<B> Then<B> (Func<B> f)
		{
			return Then ((A _) => Return (f()));
		}
		
		public Future<B> Then<B> (Func<Future<B>> f)
		{
			return Then ((A _) => f ());
		}
		
		public Future<A> Then (Action<A> f)
		{
			return Then (f.ToFunc());
		}
		
		public Future<A> Then (Action f)
		{
			return Then ((A _) => f());
		}
		
		public abstract bool completed {get;}
	}
	
	public interface Future
	{
		Future<A> Then<A> (Func<A> f);
		Future<A> Then<A> (Func<Future<A>> f);
		Future Then (Action f);
		
		bool completed {get;}
	}
	
	public abstract class FutureBase : Future
	{
		public abstract Future<A> Return<A> (A a);
		public abstract Future<A> Then<A> (Func<Future<A>> f);
		
		Future<A> Future.Then<A> (Func<A> f)
		{
			return Then<A> (() => Return (f()));
		}
		
		public abstract Future Then (Action f);
		
		public abstract bool completed {get;}
	}
	
	public class Completer<A> : FutureBase<A>
	{
		bool _completed = false;
		A _value;
		
		List<Action> actions = new List<Action>();
		
		public Completer () {}
		
		Completer (A value)
		{
			this._completed = true;
			this._value = value;
		}
		
		public override Future<C> Return<C> (C value)
		{
			return new Completer<C> (value);
		}

		public override Future<B> Then<B> (Func<A, Future<B>> f)
		{
			if (completed)
				return f (_value);
			
			var completer = new Completer<B> ();
			
			Action onComplete = () => {
			
				Future<B> future = f (_value);
				future.Then ((B b) => completer.Complete(b));
			};
			
			actions.Add (onComplete);
			
			return (Future<B>) completer;
		}

		public override bool completed {
			get {
				return _completed;
			}
		}
		
		public void Complete (A value)
		{
			
			this._value = value;
			actions.ForEach ((Action f) => f());
			this._completed = true;
		}
	}
	
}
