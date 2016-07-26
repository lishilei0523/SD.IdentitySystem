using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.UAC.Repository.Implements
{
    /// <summary>
    /// 信息系统类别仓储实现
    /// </summary>
    public class InfoSystemKindRepository : EFRepositoryProvider<InfoSystemKind>, IInfoSystemKindRepository
    {

    }
}
