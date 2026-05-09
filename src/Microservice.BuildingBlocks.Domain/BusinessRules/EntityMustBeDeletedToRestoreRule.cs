namespace Microservice.BuildingBlocks.Domain.BusinessRules;

public class EntityMustBeDeletedToRestoreRule(bool isDeleted) : IBusinessRule
{
    public bool BrokenWhen => !isDeleted;

    public string Message => "The entity must be deleted before it can be restored.";
}
