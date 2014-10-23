using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Quantum : IEnumerable {

	public Exception ex;
	
	internal  Quantum () {}

	internal abstract IEnumerable GetEnumerable ();
	public abstract Quantum copy {get;}
	public abstract IEnumerable<Quantum> GetQuanta ();
	
	internal bool valid {
		get {
			return ex != null;
		}
	}
	
	public IEnumerator GetEnumerator ()
	{
		return GetEnumerable ().GetEnumerator ();
	}
	

}
