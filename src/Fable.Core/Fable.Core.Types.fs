namespace Fable.Core

open System

type CaseRules =
    | None = 0
    /// FooBar -> fooBar
    | LowerFirst = 1
    /// FooBar -> foo_bar
    | SnakeCase = 2
    /// FooBar -> FOO_BAR
    | SnakeCaseAllCaps = 3
    /// FooBar -> foo-bar
    | KebabCase = 4

/// Used on interfaces to mangle member names. This allows overloading and
/// prevents conflicts with other interfaces, but cannot be used to interact with JS.
[<AttributeUsage(AttributeTargets.Interface)>]
type MangleAttribute() =
    inherit Attribute()

/// Used on a class to attach all members, useful when you want to use the class from JS.
[<AttributeUsage(AttributeTargets.Class)>]
type AttachMembersAttribute() =
    inherit Attribute()

/// Used for erased union types and to ignore modules in JS compilation.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type EraseAttribute() =
    inherit Attribute()
    new (caseRules: CaseRules) = EraseAttribute()

/// The module, type, function... is globally accessible in JS.
/// More info: http://fable.io/docs/interacting.html#Import-attribute
type GlobalAttribute() =
    inherit Attribute()
    new (name: string) = GlobalAttribute()

/// References to the module, type, function... will be replaced by import statements.
/// Use `[<Import("default", "my-package")>] to import the default member.
/// Use `[<Import("*", "my-package")>] to import the whole package.
/// More info: http://fable.io/docs/interacting.html#Import-attribute
type ImportAttribute(selector: string, from: string) =
    inherit Attribute()

/// Takes the member name from the value it decorates
type ImportMemberAttribute(from: string) =
    inherit Attribute()

/// Same as `Import("default", "my-package")`
type ImportDefaultAttribute(from: string) =
    inherit Attribute()

/// Same as `Import("*", "my-package")`
type ImportAllAttribute(from: string) =
    inherit Attribute()

/// Function calls will be replaced by inlined JS code.
/// More info: http://fable.io/docs/interacting.html#emit-attribute
type EmitAttribute(macro: string, isStatement: bool) =
    inherit Attribute()
    new (macro: string) = EmitAttribute(macro, isStatement=false)

/// Same as `Emit("$0.methodName($1...)")`
type EmitMethodAttribute(methodName: string) =
    inherit Attribute()

/// Same as `Emit("new $0($1...)")`
type EmitConstructorAttribute() =
    inherit Attribute()

/// Same as `Emit("$0[$1]{{=$2}}")`
type EmitIndexerAttribute() =
    inherit Attribute()

/// Same as `Emit("$0.propertyName{{=$1}}")`
type EmitPropertyAttribute(propertyName: string) =
    inherit Attribute()

/// Compile union types as string literals.
/// More info: http://fable.io/docs/interacting.html#StringEnum-attribute
[<AttributeUsage(AttributeTargets.Class)>]
type StringEnumAttribute(caseRules: CaseRules) =
    inherit Attribute()
    new () = StringEnumAttribute(CaseRules.LowerFirst)

/// Used to spread the last argument. Mainly intended for `React.createElement` binding, not for general use.
[<AttributeUsage(AttributeTargets.Parameter)>]
type ParamListAttribute() =
    inherit Attribute()

type ParamSeqAttribute = ParamListAttribute

/// Converts arguments from the specified index on (0 by default) into an object.
/// IMPORTANT: This should be used only with native bindings.
[<AttributeUsage(AttributeTargets.Constructor ||| AttributeTargets.Method)>]
type ParamObjectAttribute(fromIndex: int) =
    inherit Attribute()
    new () = ParamObjectAttribute(0)

/// Experimental: Currently only intended for some specific libraries
[<AttributeUsage(AttributeTargets.Parameter)>]
type InjectAttribute() =
    inherit Attribute()

/// Intended for replacement types in Fable.Library
[<AttributeUsage(AttributeTargets.Class)>]
type NoOverloadSuffixAttribute() =
    inherit Attribute()

/// Can be used in an optional argument decorated with Inject attribute
/// to automatically pass generic information without inlining the method
type ITypeResolver<'T> =
    abstract ResolveType: unit -> Type

/// Erased union type to represent one of two possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U2<'a, 'b> =
    | Case1 of 'a
    | Case2 of 'b
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x

/// Erased union type to represent one of three possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U3<'a, 'b, 'c> =
    | Case1 of 'a
    | Case2 of 'b
    | Case3 of 'c
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x
    static member op_ErasedCast(x:'c) = Case3 x

/// Erased union type to represent one of four possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U4<'a, 'b, 'c, 'd> =
    | Case1 of 'a
    | Case2 of 'b
    | Case3 of 'c
    | Case4 of 'd
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x
    static member op_ErasedCast(x:'c) = Case3 x
    static member op_ErasedCast(x:'d) = Case4 x

/// Erased union type to represent one of five possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U5<'a, 'b, 'c, 'd, 'e> =
    | Case1 of 'a
    | Case2 of 'b
    | Case3 of 'c
    | Case4 of 'd
    | Case5 of 'e
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x
    static member op_ErasedCast(x:'c) = Case3 x
    static member op_ErasedCast(x:'d) = Case4 x
    static member op_ErasedCast(x:'e) = Case5 x

/// Erased union type to represent one of six possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U6<'a, 'b, 'c, 'd, 'e, 'f> =
    | Case1 of 'a
    | Case2 of 'b
    | Case3 of 'c
    | Case4 of 'd
    | Case5 of 'e
    | Case6 of 'f
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x
    static member op_ErasedCast(x:'c) = Case3 x
    static member op_ErasedCast(x:'d) = Case4 x
    static member op_ErasedCast(x:'e) = Case5 x
    static member op_ErasedCast(x:'f) = Case6 x

/// Erased union type to represent one of seven possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U7<'a, 'b, 'c, 'd, 'e, 'f, 'g> =
    | Case1 of 'a
    | Case2 of 'b
    | Case3 of 'c
    | Case4 of 'd
    | Case5 of 'e
    | Case6 of 'f
    | Case7 of 'g
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x
    static member op_ErasedCast(x:'c) = Case3 x
    static member op_ErasedCast(x:'d) = Case4 x
    static member op_ErasedCast(x:'e) = Case5 x
    static member op_ErasedCast(x:'f) = Case6 x
    static member op_ErasedCast(x:'g) = Case7 x

/// Erased union type to represent one of eight possible values.
/// More info: http://fable.io/docs/interacting.html#Erase-attribute
type [<Erase>] U8<'a, 'b, 'c, 'd, 'e, 'f, 'g, 'h> =
    | Case1 of 'a
    | Case2 of 'b
    | Case3 of 'c
    | Case4 of 'd
    | Case5 of 'e
    | Case6 of 'f
    | Case7 of 'g
    | Case8 of 'h
    static member op_ErasedCast(x:'a) = Case1 x
    static member op_ErasedCast(x:'b) = Case2 x
    static member op_ErasedCast(x:'c) = Case3 x
    static member op_ErasedCast(x:'d) = Case4 x
    static member op_ErasedCast(x:'e) = Case5 x
    static member op_ErasedCast(x:'f) = Case6 x
    static member op_ErasedCast(x:'g) = Case7 x
    static member op_ErasedCast(x:'h) = Case8 x
