using UnityEngine;
using System.Collections;
using System;


namespace Atoms
{
	class LoadWWW : Chain<WWW>
	{

		public WWW www;
		public float percent;
		public Action<float> onProgress;

		public LoadWWW (WWW www, float percent = 1f, Action<float> onProgress = null)
		{
			this.www = www;
			this.percent = percent;
			this.onProgress = onProgress != null ? onProgress : (float n) => {};
		}


		internal override IEnumerable GetEnumerable ()
		{
			var actual = www.progress;

			while (percent >= 1f ? www.isDone : www.progress < percent)
			{
				if (www.error != null)
				{
					throw new Exception (www.error);
				}
				else if (actual != www.progress)
				{
					actual = www.progress;
					onProgress (www.progress);
				}

				yield return null;
			}

			yield return www;
		}

	}

	class LoadLocal<A> : Chain<A> where A : UnityEngine.Object
	{
		public string path;
		public Action<float> onProgress;

		public LoadLocal (string path, Action<float> onProgress = null)
		{
			this.path = path;
			this.onProgress = onProgress != null ? onProgress : (float n) => {};
		}

		internal override IEnumerable GetEnumerable ()
		{
			Debug.Log ("Local Load");

			var req = Resources.LoadAsync<A> (path);

			while (! req.isDone)
			{
				onProgress (req.progress);
				yield return null;
			}

			yield return (A) req.asset;
		}
	}
}
