using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Atoms {
	public class Assert : ChainConditional<bool> {

		string onFailure;
		Action onSuccess = Fn.DoNothing;

		public Assert (Func<bool> Cond, string onFailure) : base (Cond)
		{
			this.onFailure = onFailure;
		}

		public Assert (Func<bool> Cond, string onFailure, Action onSuccess) : base (Cond)
		{
			this.onFailure = onFailure;
			this.onSuccess = onSuccess;
		}

		internal override IEnumerable GetEnumerable ()
		{
			var pred = Cond();

			if (pred) 
				onSuccess();
			else
				Debug.Log(onFailure);

			yield return pred;
		}

		public override IEnumerable<Quantum> GetQuanta ()
		{
			yield return this;
		}

		public static Assert _ (Func<bool> Cond, string OnFailure) {
			return new Assert (Cond, OnFailure);
		}

		public static Assert _ (Func<bool> Cond, string OnFailure, Action OnSuccess) {
			return new Assert (Cond, OnFailure, OnSuccess);
		}
	}
}
