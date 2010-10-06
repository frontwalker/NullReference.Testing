using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using StructureMap;

namespace NullReference.Testing
{
    public abstract class ContextSpecification<TSubjectUnderTest> where TSubjectUnderTest : class
    {
        protected static IAutoMockingContainer<TSubjectUnderTest> _autoMockingContainer;
        protected static TSubjectUnderTest SUT { get; set; }
        protected static Action<ConfigurationExpression> CustomSutRegistration;

        Establish context = () =>
                                {
                                    _autoMockingContainer = new StructureMapAMC<TSubjectUnderTest>();

                                    if (CustomSutRegistration != null)
                                        _autoMockingContainer.RhinoAutoMocker.Container
                                            .Configure(CustomSutRegistration);

                                    SUT = _autoMockingContainer.Create();
                                };

        Cleanup stuff = () =>
                            {
                                SUT = null;
                                _autoMockingContainer = null;
                            };

        protected static TDependency Dependency<TDependency>()
            where TDependency : class
        {
            return _autoMockingContainer.GetMock<TDependency>();
        }

        protected static TStub Stub<TStub>()
            where TStub : class
        {
            return _autoMockingContainer.GetStub<TStub>();
        }

        protected static IMethodOptions<Object> Stub<TStub>(Action<TStub> action)
            where TStub : class
        {
            return _autoMockingContainer.GetStub<TStub>().Stub(action);
        }
        
        protected static void AssertWasCalled<TStub>(Action<TStub> action) where TStub : class
        {
            Stub<TStub>().AssertWasCalled(action);
        }
        protected static void AssertWasCalled<TStub>(Action<TStub> action, Action<IMethodOptions<object>> methodOptions) where TStub : class
        {
            Stub<TStub>().AssertWasCalled(action, methodOptions);
        }

        protected static void Stub()
        {
            
        }
    }
}