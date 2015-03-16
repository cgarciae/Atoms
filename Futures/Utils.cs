using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Futures
{
	public static class Utils
	{
		public static Func<A,A> ToFunc<A> (this Action<A> f)
		{
			return (A a) => {
				f (a);
				return a;
			};
		}
		
		public static Action<A> ToAction<A,B> (this Func<A,B> f)
		{
			return (A a) => {
				f (a);
			};
		}
		
		public static Func<A,C> Compose<A,B,C> (Func<B,C> f, Func<A,B> g)
		{
			return (A a) => f (g (a));
		}
	}
}
