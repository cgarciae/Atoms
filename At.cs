using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tatacoa;

namespace Atoms {
	public static partial class At {

		public static string URLPattern = @"([a-zA-Z]{4,})://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

		//Start
		public static Coroutine Start (this IEnumerable e, MonoBehaviour m) 
		{
			return m.StartCoroutine (e.GetEnumerator ());
		}

		//ToEnumerable
		public static IEnumerable ToEnumerable (this IEnumerator e) {
			while (e.MoveNext())
				yield return e.Current;
		}

		public static IEnumerable<A> ToEnumerable<A> (this IEnumerator<A> e) {
			while (e.MoveNext())
				yield return e.Current;
		}

		//Map Compose
		public static Map<A,C> Next<A,B,C> (this Map<A,B> a, Map<B,C> b) 
		{
			return new Map<A, C> (Fn.Compose (b.f, a.f));
		}

		//MapLast
		public static IEnumerable MapLast<A,B> (this IEnumerable e, Func<A,B> f)
		{
			var enu = e.GetEnumerator();
			
			while (enu.MoveNext())
				yield return enu.Current;
			
			yield return f ((A) enu.Current);
		}

		//Atomize
		public static Atom Atomize (this IEnumerable e)
		{
			return new Atoms.Atomize (e);
		}

		public static Chain<A> Atomize<A> (this IEnumerable e)
		{
			return new Atoms.Atomize<A> (e);
		}

		public static Sequence<A> Atomize<A> (this IEnumerable<A> e)
		{
			return new Atoms.AtomizeSeq<A> (e);
		}

		public static bool IsURL (string s)
		{
			return  Regex.IsMatch (s, URLPattern);
		}

		public static Chain<C> Load<A,B,C> (WWW www, Func<WWW,A> beforeFun, Func<A,B> yieldFun, Func<B,C> afterFun, float percent = 1f, Action<float> onProgress = null) where C : UnityEngine.Object {
			
			return IsURL (www.url) ?
				LoadWeb (www, beforeFun, yieldFun, afterFun, percent, onProgress) :
				new LoadLocal<C> (www.url, onProgress);
		}

		public static Chain<B> Load<A,B> (WWW www, Func<WWW,A> beforeFun, Func<A,B> yieldFun, float percent = 1f, Action<float> onProgress = null) where B : UnityEngine.Object
		{
			return Load (www, beforeFun, yieldFun, Fn.Id<B> (), percent, onProgress);
		}

		public static Chain<A> Load<A> (WWW www, Func<WWW,A> beforeFun, float percent = 1f, Action<float> onProgress = null) where A : UnityEngine.Object
		{
			return Load (www, beforeFun, Fn.Id<A> (), percent, onProgress);
		}

		public static Chain<WWW> Load<A> (WWW www, Action<WWW> beforeFun, float percent = 1f, Action<float> onProgress = null) where A : UnityEngine.Object
		{
			Func<WWW,A> f = (WWW _www) =>
			{
				beforeFun (_www);
				return default (A);
			};

			return At.Load (www, f, percent, onProgress)
				.Then<WWW> (() => www);
		}

		public static Chain<A> LoadWeb<A> (WWW www, Func<WWW,A> f, float percent = 1f, Action<float> onProgress = null)
		{
			return LoadWeb (www, f, Fn.Id<A> (), percent, onProgress);
		}

		public static Chain<B> LoadWeb<A,B> (WWW www, Func<WWW,A> f, Func<A,B> g, float percent = 1f, Action<float> onProgress = null)
		{
			return LoadWeb (www, f, g, Fn.Id<B> (), percent, onProgress);
		}

		public static Chain<C> LoadWeb<A,B,C> (WWW www, Func<WWW,A> f, Func<A,B> g, Func<B,C> h, float percent = 1f, Action<float> onProgress = null)
		{
			Debug.Log ("LOAD WEB");
			return new LoadWWW (www, percent, onProgress)
				.Map (f)
				.Map (g)
				.Map (h)
				.Then (DisposeWWW (www));
		}

		public static Action DisposeWWW (WWW www) {
			return () => {
				if (www.assetBundle)
					www.assetBundle.Unload (false);
				www.Dispose ();
				www = null;
			};
		}

		public static Chain<GameObject> RequestGameObject (String url, int version, Action<float> onProgress = null)
		{
			return LoadWeb
			(
				WWW.LoadFromCacheOrDownload (url, version),
				(WWW www) => www.assetBundle,
				assetBundle => assetBundle.LoadAsync (Help.GetAssetName (url), typeof(GameObject)),
				request => (GameObject)request.asset,
				1f,
				onProgress
			);
		}

		public static Chain<String> RequestString (String url, float percent = 1f, Action<float> onProgress = null)
		{
			return LoadWeb
			(
				new WWW (url),
				(WWW www) => www.text,
				percent,
				onProgress
			);
		}

		public static Chain<A> RequestDecoded<A> (String url, float percent = 1f, Action<float> onProgress = null)
		{
			return RequestString
			(
				url,
				percent,
				onProgress
			)
			.Then ((String s) => Help.Deserialize<A> (s));
		}

	}

	public interface IBound {
		Atom prev { get; set; }
	}	

	public abstract partial class Sequence<A> 
	{
		public Sequence<A> Replicate (int n)
		{
			return Fn.Replicate (this, n).FoldL1 ((sum, next) => sum.Then (next));
		}
	}

	public abstract partial class Chain<A> 
	{
		public Chain<A> Replicate (int n)
		{
			return Fn.Replicate (this, n).FoldL1<Chain<A>> ((sum, next) => sum.Then (next));
		}
	}

	public abstract partial class Atom
	{
		public Atom Replicate (int n)
		{
			return Fn.Replicate (this, n).FoldL1<Atom> ((sum, next) => sum + next);
		}
	}
}

