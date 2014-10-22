Atoms
=====

#Description

#Getting Started
1. Copy Atoms folder into your proyect.
2. Download or clone the Tatacao library from https://github.com/cgarciae/tatacoa into your proyect.

##Adding Tatacoa as a Submodule
If your proyect already has git repo you might want to add tatacoa as a submodule, to do this use these commands
````
git submodule add https://github.com/cgarciae/tatacoa.git /some/path/Atoms
git submodule update --init
```

#Baby Steps
**Repeat after me**
* Every `Quantum` is an `IEnumerable`.
* Every `Atom` is a `Quantum`.

**Reactions**
* `Atom` + `Atom` = `Atom`.
* `Atom` % `Atom` = `Atom`.

**Quiz**

1. `Atom` + `Quantum` = ???

#Basics
**Repeat after me**
* Every `Chain<A>` is an `Atom`.
* Every `Sequence<A>` is a `Chain<A>` and a `IEnumerable<A>`.

**Reactions**
* `Atom` + `Chain<A>` = `Chain<A>`.
* * `Atom` % `Atom` = `Atom`.
* `Atom` + `Sequence<A>` = `Chain<A>`.
* `Sequence<A>` + `Sequence<A>` = `Sequence<A>`.

**Quiz**

1. Is a `Chain<A>` an `IEnumerable`?
2. Is a `Chain<A>` an `IEnumerable<A>`?
3. `Chain<A>` + `Atom` = ???
4. `Chain<A>` + `Sequence<A>` = ???
5. Is addition associative?

#Intermediate
**Repeat after me**
* Every `Bond<A,B>` is an `Quantum` and a `IChain<B>`.
* Every `Map<A,B>` is a `Bond<A,B>`.
* Every `Bind<A,B>` is a `Bond<A,B>`.

**Reactions**
* `Chain<A>` * `Bond<A,B>` = `Chain<B>`.
* `Bond<A,B>` * `BondB,C>` = `Bond<A,C>`.

**Quiz**

1. `Chain<X>` * `Bond<X,Y>` = ???
2. `Atom` * `Bond<A,B>` = ???
3. `Chain<F>` * `Bond<T,F>` = ???
4. Does `Chain<A> * (Bond<A,B> * Bond<B,C>)` have the same type as `(Chain<A> * Bond<A,B>) * Bond<B,C>`?
