using Content.Shared._Sunrise.InteractionsPanel.Data.Components;
using Robust.Shared.Serialization;

namespace Content.Shared._Sunrise.InteractionsPanel.Data.Conditions;

[Serializable, NetSerializable, DataDefinition]
public sealed partial class ErpCondition : IAppearCondition
{
    [DataField]
    public bool CheckInitiator { get; private set; } = true;

    [DataField]
    public bool CheckTarget { get; private set; } = true;

    public bool IsMet(EntityUid initiator, EntityUid target, EntityManager entityManager)
    {
        if (CheckInitiator)
        {
            if (!HasErpRequired(initiator, entityManager))
                return false;
        }

        if (CheckTarget)
        {
            if (!HasErpRequired(target, entityManager))
                return false;
        }

        return true;
    }

    private bool HasErpRequired(EntityUid entity, EntityManager entityManager)
    {
        if (!entityManager.TryGetComponent<InteractionsComponent>(entity, out var interactions))
            return false;

        return interactions.Erp;
    }
}
