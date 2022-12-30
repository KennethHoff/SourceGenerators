using JetBrains.Annotations;

namespace AnotherProject.Seremonibasen.Models.Abstractions;

[UsedImplicitly(ImplicitUseKindFlags.Assign | ImplicitUseKindFlags.Access | ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers | ImplicitUseTargetFlags.WithInheritors)]
public abstract class BasePocoObject : IBasePocoObject, IHasAccessToContent
{
    #region Interface implementations

    public Guid Id { get; set; }

    public DataStatus? Status { get; set; }

    public bool HasDeleteAccess { get; set; }
    public bool HasReadAccess { get; set; }
    public bool HasWriteAccess { get; set; }

    #endregion
}