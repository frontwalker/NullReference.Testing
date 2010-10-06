using StructureMap.AutoMocking;

namespace NullReference.Testing
{
    public interface IAutoMockingContainer<TSubject>
        where TSubject : class
    {
        RhinoAutoMocker<TSubject> RhinoAutoMocker { get; }
        TSubject Create();
        TMock GetMock<TMock>() where TMock : class;
        TStub GetStub<TStub>() where TStub : class;
    }
}