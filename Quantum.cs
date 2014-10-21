using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Quantum : IEnumerable {

	public Quantum prev;
	public Quantum next;

	public bool cancelPrevious = false;

	public Exception ex;
	
	internal  Quantum () {}
	
	internal Quantum (Quantum prev, Quantum next)
	{
		this.prev = prev;
		this.SetNext (next);
	}

	public abstract Quantum copyQuantum {get;}
	
	public IEnumerable<Quantum> previousQuanta {
		get {
			return this.IterateWhile (a => a != null, a => a.prev);
		}
	}
	
	public Quantum firstQuantum {
		get {
			return previousQuanta.Last();
		}
	}
	
	public IEnumerable<Quantum> brokenQuanta {
		get {
			return previousQuanta.Filter (a => a.ex != null);
		}
	}
	
	public Maybe<Quantum> firstBrokenQuantum {
		get {
			return brokenQuanta.MaybeLast();
		}
	}
	
	public Maybe<Quantum> lastBrokenQuantum {
		get {
			return brokenQuanta.MaybeHead();
		}
	}
	
	public IEnumerable<Exception> exceptions {
		get {
			return brokenQuanta.FMap (a => a.ex);
		}
	}
	
	public Maybe<Exception> firstException {
		get {
			return exceptions.MaybeLast();
		}
	}
	
	public Maybe<Exception> lastException {
		get {
			return exceptions.MaybeHead();
		}
	}
	
	internal bool valid {
		get {
			return ex != null;
		}
	}
	
	internal IEnumerable Previous () {
		if (prev != null && ! cancelPrevious) {
			foreach (var _ in prev) {
				yield return _;	
			}
		}
	}
	
	public IEnumerator GetEnumerator ()
	{
		return GetEnumerable ().GetEnumerator ();
	}
	
	internal abstract IEnumerable GetEnumerable ();

	Quantum ConnectTo (Quantum next) {

		var middle = next.firstQuantum;

		this.next = middle;
		middle.prev = this;

		return next;
	}

	public Quantum SetNext (Quantum newNext) {

		if (newNext == null)
			return this.next != null ? this.next : this;

		if (this.next == null)
		{
			(this) .ConnectTo (newNext);
			return newNext;
		}
		else 
		{
			var oldNext = this.next;

			(this) .ConnectTo (newNext) .ConnectTo (oldNext);

			return oldNext;
		}

	}

}
