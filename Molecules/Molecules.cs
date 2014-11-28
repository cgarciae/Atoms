using System;
using System.Collections;
using Tatacoa;

namespace Atoms {

	//If
	public static class If {

		public static Atom _ (Func<bool> cond, Atom thenAtom, Atom elseAtom = null)
		{
			return new LazyAtom (() => cond () ? thenAtom : (elseAtom != null ? elseAtom : Atom.DoNothing));
		}

		public static Chain<A> _<A> (Func<bool> cond, Chain<A> thenChain, Chain<A> elseChain)
		{
			return new LazyChain<A> (() => cond () ? thenChain : elseChain);
		}

		public static Sequence<A> _<A> (Func<bool> cond, Sequence<A> thenSeq, Sequence<A> elseSeq)
		{
			return new LazySeq<A> (() => cond () ? thenSeq : elseSeq);
		}
	}

	public abstract partial class Atom
	{
		public Atom If (Func<bool> cond, Atom thenAtom, Atom elseAtom)
		{
			return this.Then (Atoms.If._ (cond, thenAtom, elseAtom));
		}
	}

	public abstract partial class Chain<A> 
	{
		public Chain<A> If (Func<bool> cond, Chain<A> thenChain, Chain<A> elseChain)
		{
			return this.Then (Atoms.If._ (cond, thenChain, elseChain));
		}
	}

	public abstract partial class Sequence<A> 
	{
		public Sequence<A> If (Func<bool> cond, Sequence<A> thenSeq, Sequence<A> elseSeq)
		{
			return this.Then (Atoms.If._ (cond, thenSeq, elseSeq));
		}
	}



	//WaitWhile
	public static class WaitWhile {

		public static Atom _ (Func<bool> cond)
		{
			return Wait._ ().While (cond);
		}
	}
	public abstract partial class Atom
	{
		public Atom WaitWhile (Func<bool> cond)
		{
			return this.Then (Atoms.WaitWhile._ (cond));
		}
	}


	//JSONRequest
	public static class JSONRequest
	{
		public static Chain<UnityEngine.WWW> _ (string url, string json)
		{
			Hashtable headers = new Hashtable ();
			headers.Add ("Content-Type", "application/json");
			headers.Add ("Cookie", "Our session cookie");
			
			byte[] pData = System.Text.Encoding.ASCII.GetBytes (json.ToCharArray ());
			
			UnityEngine.WWW www = null;

			return Do._ (() => www = new UnityEngine.WWW (url, pData, headers))
					.WaitWhile (() => ! www.isDone)
					.Do (() => www);
		}
	}
}
