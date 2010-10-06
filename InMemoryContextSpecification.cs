using System;
using FluentNHibernate.Cfg.Db;
using Machine.Specifications;
using NHibernate;
using NullReference.Data;
using NullReference.Data.Events;

namespace NullReference.Testing
{
    public abstract class InMemoryContextSpecification<TPersistenceSpec> where TPersistenceSpec : IPersistenceMappings, new()
    {
        protected static IRepository Repository;
        protected static ISession Session;

        Establish _context = () =>
            {

                var mappings = new TPersistenceSpec() as IPersistenceMappings;

                var config = mappings.Generate()
                    .Database(SQLiteConfiguration.Standard.InMemory().ShowSql()
                        .ProxyFactoryFactory(typeof(NHibernate.ByteCode.LinFu.ProxyFactoryFactory)))
                    .ExposeConfiguration(EventListenerRegistrations.Apply)
                    .BuildConfiguration();


                NHibernateHelper.Initialize(new SQLiteInMemorySessionFactory(config), new StaticStateHolder());
                NHibernateHelper.BeginTransaction();

                Session = NHibernateHelper.GetCurrentSession();
                Repository = new NHibernateRepository(Session);
            };
    }
}