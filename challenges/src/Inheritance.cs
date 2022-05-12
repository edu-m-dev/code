namespace Challenges;

public class A
{
}

public class B : A
{
}

public class Base
{
    public virtual void Foo()
    { }
}

public class Bar : Base
{
    public override sealed void Foo()
    { }
}

public static class Inheritance
{
    public static void CreateInstances()
    {
        A a = new A();
        A b = new B();
    }

    public static void CreateVirtualThenOverrideWithSealed()
    {
        Base _base = new Base();
        Bar bar = new Bar();
    }
}
