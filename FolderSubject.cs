using System;
using System.Linq;
using Machine.Specifications;

namespace NullReference.Testing
{
    public class FolderSubject : SubjectAttribute
    {
        public FolderSubject(Type subjectType) : base(GenerateSubject(subjectType))
        {
        }

        private static string GenerateSubject(Type type)
        {
            var trimmedNamespace = type.Namespace.Substring(type.Namespace.IndexOf("Tests") + 6);

            return trimmedNamespace.Replace("_", " ").Replace("."," - ");
        }
    }
}