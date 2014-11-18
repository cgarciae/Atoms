using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tatacoa;
using Streams;

namespace Atoms
{
	public class StateMachine<A> : Atom
	{
		public A initialState;
		A _state;
		public Dictionary<A,StateBehaviour<A>> map;

		public StateMachine (A initialState, params StateBehaviour<A>[] behaviours)
		{
			this.initialState = this._state = initialState;
			this.map = new Dictionary<A, StateBehaviour<A>> ();

			behaviours.ForEach (m => map.Add (m.key, m));
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			StateBehaviour<A> actual = map [initialState];
			StateBehaviour<A> nextBehaviour;
			IEnumerator enu = actual.enumerator;
			bool move;

			actual.onEnter.Broadcast ();

			while ((move = enu.MoveNext()) || actual.transitive) 
			{
				if (! move)
				{
					_state = actual.onFinish();
					actual = map [_state];
					enu = actual.enumerator;

					continue;
				}

				yield return enu.Current;

				_state = actual.transitionFunction (_state);
				nextBehaviour = map [_state];

				if (nextBehaviour != actual)
				{
					actual.onExit.Broadcast();
					nextBehaviour.onEnter.Broadcast();

					actual = nextBehaviour;
					enu = actual.enumerator;
				}
			}
		}

		public A state {get {return _state;}}

		public static StateMachine<A> _ (A initialState, params StateBehaviour<A>[] behaviours)
		{
			return new StateMachine<A> (initialState, behaviours);	
		}
	}

	public class StateBehaviour<A> : Atom
	{
		public A key;
		public Func<A, A> transitionFunction;
		public Func<A> onFinish;
		public Atom atom;
		public bool restartOnEnter;

		public Stream onEnter = new Stream();
		public Stream onExit = new Stream();

		public bool transitive = false;

		IEnumerator enu;
		public IEnumerator enumerator
		{
			get 
			{
				return restartOnEnter ? (atom.copy as Atom).GetEnumerator() : enu;
			}
		}

		public StateBehaviour (A key, Func<A, A> f, Atom atom, bool restartOnEnter = false)
		{
			this.key = key;
			this.atom = atom.copy as Atom;
			this.transitionFunction = f;
			this.restartOnEnter = restartOnEnter;

			enu = atom.GetEnumerator ();
		}

		public StateBehaviour (A key, Func<A, A> f, Atom atom, Func<A> onFinish, bool restartOnEnter = false) : this (key, f, atom, restartOnEnter)
		{
			this.transitive = true;
			this.onFinish = onFinish;
		}
		
		internal override IEnumerable GetEnumerable ()
		{
			return atom;
		}

		public static StateBehaviour<A> _ (A key, Func<A, A> transitionFunction, Atom atom, bool restartOnEnter = false)
		{
			return new StateBehaviour<A> (key, transitionFunction, atom, restartOnEnter);
		}

		public static StateBehaviour<A> _ (A key, Func<A, A> f, Atom atom, Func<A> onFinish, bool restartOnEnter = false)
		{
			return new StateBehaviour<A> (key, f, atom, onFinish, restartOnEnter);
		}
	}

	public class TerminalState<A> : StateBehaviour<A>
	{

		public TerminalState (A key) : base (key, Fn.Id<A> (), Atom.NullAtom)
		{

		}

		public static TerminalState<A> _ (A key)
		{
			return new TerminalState<A> (key);	
		}

	}

	public class AbsorvingState<A> : StateBehaviour<A>
	{
		
		public AbsorvingState (A key, Atom atom) : base (key, Fn.Id<A> (), atom)
		{
			
		}

		public static AbsorvingState<A> _ (A key, Atom atom)
		{
			return new AbsorvingState<A> (key, atom);	
		}
	}
}
