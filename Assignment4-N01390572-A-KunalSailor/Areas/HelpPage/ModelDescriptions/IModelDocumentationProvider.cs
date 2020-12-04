using System;
using System.Reflection;

namespace Assignment4_N01390572_A_KunalSailor.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}