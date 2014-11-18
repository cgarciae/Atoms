using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Atoms;

namespace Streams {
	public class Stream  {
		
		public HashSet<Action> dataHandlers = new HashSet<Action>();
		public HashSet<Action<Exception>> errorHandlers = new HashSet<Action<Exception>>();
		public HashSet<Action> doneHandlers = new HashSet<Action>();
		
		public bool done = false;
		
		public virtual void Broadcast ()
		{
			if (done)
				return;
			
			foreach (var f in dataHandlers.ToList())
			{
				f ();
			}	
		}
		
		public virtual void BroadcastError (Exception e)
		{
			if (done)
				return;
			
			foreach (var f in errorHandlers.ToList()) 
			{
				f (e);
			}
		}
		
		public virtual void BroadcastDone ()
		{
			if (done)
				return;
			
			foreach (var f in doneHandlers.ToList()) 
			{
				f ();
			}
			
			done = true;
		}
		
		public Stream OnData (Action f)
		{
			dataHandlers.Add (f);
			return this;
		}
		
		public Stream OnError (Action<Exception> f)
		{
			errorHandlers.Add (f);
			return this;
		}
		
		public Stream OnDone (Action f)
		{
			doneHandlers.Add (f);
			return this;
		}
		
		public Stream RemoveDataHandler (Action f)
		{
			dataHandlers.Remove (f);
			return this;
		}
		
		public Stream RemoveErrorHandler (Action<Exception> f)
		{
			errorHandlers.Remove (f);
			return this;
		}
		
		public Stream RemoveDoneHandler (Action f)
		{
			doneHandlers.Remove (f);
			return this;
		}
		
		public StreamPort Listen ()
		{
			return new StreamPort (this);	
		}

		public static Stream operator + (Stream s, Action f)
		{
			return s.OnData (f);
		}

		public static Stream operator - (Stream s, Action f)
		{
			return s.RemoveDataHandler (f);
		}

		public Atom First ()
		{
			bool broadcasted = false;
			OnData (() => broadcasted = true);
			return Wait._ ().While (() => ! broadcasted);
		}
	}

	public class StreamPort
	{
		public Action onData;
		public Action<Exception> onError;
		public Action onDone;
		
		public Stream stream;
		
		
		public StreamPort (Stream stream)
		{
			this.stream = stream;
		}
		
		public StreamPort OnData (Action f)
		{
			stream
				.RemoveDataHandler (onData)
				.OnData (f);
			
			onData = f;
			return this;
		}
		
		public StreamPort OnError (Action<Exception> f)
		{
			stream
				.RemoveErrorHandler (onError)
				.OnError (f);
			
			onError = f;
			return this;
		}
		
		public StreamPort OnDone (Action f)
		{
			stream
				.RemoveDoneHandler (onDone)
				.OnDone (f);
			
			onDone = f;
			return this;
		}
		
		public StreamPort Unsubscribe ()
		{
			stream
				.RemoveDataHandler (onData)
				.RemoveErrorHandler (onError)
				.RemoveDoneHandler (onDone);
			
			return this;
		}

		public static StreamPort operator + (StreamPort s, Action f)
		{
			return s.OnData (f);
		}
	}

	public class Stream<A>  {

		public HashSet<Action<A>> dataHandlers = new HashSet<Action<A>>();
		public HashSet<Action<Exception>> errorHandlers = new HashSet<Action<Exception>>();
		public HashSet<Action> doneHandlers = new HashSet<Action>();

		public bool done = false;

		public virtual void Broadcast (A a)
		{
			if (done)
				return;

			foreach (var f in dataHandlers.ToList())
			{
				f (a);
			}	
		}

		public virtual void BroadcastError (Exception e)
		{
			if (done)
				return;

			foreach (var f in errorHandlers.ToList()) 
			{
				f (e);
			}
		}

		public virtual void BroadcastDone ()
		{
			if (done)
				return;

			foreach (var f in doneHandlers.ToList()) 
			{
				f ();
			}

			done = true;
		}

		public Stream<A> OnData (Action<A> f)
		{
			dataHandlers.Add (f);
			return this;
		}

		public Stream<A> OnError (Action<Exception> f)
		{
			errorHandlers.Add (f);
			return this;
		}

		public Stream<A> OnDone (Action f)
		{
			doneHandlers.Add (f);
			return this;
		}

		public Stream<A> RemoveDataHandler (Action<A> f)
		{
			dataHandlers.Remove (f);
			return this;
		}

		public Stream<A> RemoveErrorHandler (Action<Exception> f)
		{
			errorHandlers.Remove (f);
			return this;
		}

		public Stream<A> RemoveDoneHandler (Action f)
		{
			doneHandlers.Remove (f);
			return this;
		}

		public MapStream<A,B> FMap<B> (Func<A,B> f)
		{
			return new MapStream<A,B> (f, this);
		}

		public StreamPort<A> Listen ()
		{
			return new StreamPort<A> (this);	
		}

		public static Stream<A> operator + (Stream<A> s, Action<A> f)
		{
			return s.OnData (f);
		}
		
		public static Stream<A> operator - (Stream<A> s, Action<A> f)
		{
			return s.RemoveDataHandler (f);
		}

		public Chain<A> First ()
		{
			A a = default (A);
			bool broadcasted = false;

			OnData ((val) => {
				broadcasted = true;
				a = val;
			});

			return KeepDoing._ (() => a).While (() => ! broadcasted);
		}
	}

	public class MapStream<A,B> : Stream<B> 
	{
		public Action<A> onData;

		public Stream<A> origin;

		public MapStream (Func<A, B> f, Stream<A> origin)
		{
			this.onData = a => this.Broadcast (f(a));
			this.origin = origin;

			origin.OnData (this.onData);
			origin.OnError (this.BroadcastError);
		}
		
		public void BreakStream ()
		{
			origin
				.RemoveDataHandler (onData)
				.RemoveErrorHandler (BroadcastError)
				.RemoveDoneHandler (BroadcastDone);

			BroadcastDone ();
		}
	}

	public class StreamPort<A>
	{
		public Action<A> onData;
		public Action<Exception> onError;
		public Action onDone;

		public Stream<A> stream;


		public StreamPort (Stream<A> stream)
		{
			this.stream = stream;
		}
		
		public StreamPort<A> OnData (Action<A> f)
		{
			stream
				.RemoveDataHandler (onData)
				.OnData (f);

			onData = f;
			return this;
		}

		public StreamPort<A> OnError (Action<Exception> f)
		{
			stream
				.RemoveErrorHandler (onError)
				.OnError (f);

			onError = f;
			return this;
		}

		public StreamPort<A> OnDone (Action f)
		{
			stream
				.RemoveDoneHandler (onDone)
				.OnDone (f);

			onDone = f;
			return this;
		}

		public StreamPort<A> Unsubscribe ()
		{
			stream
				.RemoveDataHandler (onData)
				.RemoveErrorHandler (onError)
				.RemoveDoneHandler (onDone);

			return this;
		}

		public static StreamPort<A> operator + (StreamPort<A> s, Action<A> f)
		{
			return s.OnData (f);
		}
	}
}
