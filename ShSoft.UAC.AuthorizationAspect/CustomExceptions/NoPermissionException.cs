using System;

namespace ShSoft.UAC.AuthorizationAspect.CustomExceptions
{
    /// <summary>
    /// 无权限异常
    /// </summary>
    [Serializable]
    public class NoPermissionException : InvalidOperationException
    {

    }
}
