using StructureMap.AutoMocking;

namespace NullReference.Testing
{
    public class StructureMapAMC<TSubject>
        : IAutoMockingContainer<TSubject>
        where TSubject : class
    {
        readonly RhinoAutoMocker<TSubject> _rhinoAutoMocker;

        public RhinoAutoMocker<TSubject> RhinoAutoMocker
        {
            get { return _rhinoAutoMocker; }
        }

        public StructureMapAMC()
        {
            _rhinoAutoMocker =
                new RhinoAutoMocker<TSubject>(MockMode.AAA);
        }

        public TSubject Create()
        {
            return _rhinoAutoMocker.ClassUnderTest;
        }

        public TMock GetMock<TMock>()
            where TMock : class
        {
            return GetDependency<TMock>();
        }

        public TStub GetStub<TStub>()
            where TStub : class
        {
            return GetDependency<TStub>();
        }

        private TDependency GetDependency<TDependency>()
            where TDependency : class
        {
            return _rhinoAutoMocker.Get<TDependency>();
        }
    }
}