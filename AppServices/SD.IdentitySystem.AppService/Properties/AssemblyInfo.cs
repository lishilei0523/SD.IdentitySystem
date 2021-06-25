using SD.Infrastructure.AOP.Aspects.ForAny;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTrademark("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1D1835C9-7D09-45BE-A592-520918EAF765")]


// AOP标签
#if NET461_OR_GREATER
[assembly: WCFServiceExceptionAspect]
#endif
#if NETSTANDARD2_0_OR_GREATER
[assembly: AppServiceExceptionAspect]
#endif
