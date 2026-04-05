# Microservice.BuildingBlocks

[![Framework](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Architecture](https://img.shields.io/badge/Architecture-DDD%20%2F%20CQRS%20%2F%20Event%20Sourcing%20%2F%20Clean%20Architecture-blue.svg)](#)

A foundational library for building **ASP.NET Web API** Microservices using **.NET 8**. This building block focuses on implementing **Domain-Driven Design (DDD)**, **Command Query Responsibility Segregation (CQRS)**, **Event Sourcing** and **Clean Architecture** patterns with a clean separation of concerns.

---

## 🏗 Solution Structure

The solution is divided into two specialized projects:

### 1. Microservice.BuildingBlocks.Domain

The core layer containing basic contracts and abstractions. It is designed to be highly portable with minimal dependencies.

- **Dependencies:** `MediatR.Contracts`
- **Key Components:** Aggregate Roots, Domain Events, Business Rules, Repositories, and Unit of Work.

### 2. Microservice.BuildingBlocks.CQRS

The implementation layer that provides the heavy lifting for DDD and CQRS patterns.

- **Dependencies:** `MediatR`, `FluentValidation`
- **Key Components:** Command/Query interfaces, Request identity, and core extensions.

---

## 📂 Technical Reference

### Project: .Domain

| Namespace          | Description                                                                                               |
| :----------------- | :-------------------------------------------------------------------------------------------------------- |
| **AggregateRoots** | Core DDD patterns: `AggregateRoot`, `IAggregateRoot`, and `EventSourcedAggregateRoot` for Event Sourcing. |
| **BusinessRules**  | Centralized validation via `IBusinessRule` and `BusinessRuleValidationException`.                         |
| **Events**         | Infrastructure for `DomainEvent`, `IDomainEventsDispatcher`, and `IEvent`.                                |
| **Entities**       | Standard `Entity` and `IEntity` implementations.                                                          |
| **ValueObjects**   | `ValueObject` base class for equality-by-value objects.                                                   |
| **Persistence**    | `IRepository` and `IUnitOfWork` for data consistency and abstraction.                                     |
| **Enumerations**   | `Enumeration` class for rich-domain constants.                                                            |

### Project: .CQRS

| Namespace      | Description                                                                              |
| :------------- | :--------------------------------------------------------------------------------------- |
| **Commands**   | Contains `ICommand`, `ICommandHandler`, and `CommandBase` for state-changing operations. |
| **Queries**    | Contains `IQuery`, `IQueryHandler`, and `QueryBase` for data retrieval.                  |
| **Requests**   | Managed via `IRequestIdentity` and `RequestBase` to track unique execution contexts.     |
| **Extensions** | `GenericTypeExtensions` for advanced reflection and type handling.                       |

---

## 🚀 Implementation Examples

### Defining an Aggregate Root with Business Rules

```csharp
public class Product : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public void UpdatePrice(decimal newPrice)
    {
        // Encapsulated Business Rule
        CheckRule(new PriceMustBePositiveRule(newPrice));

        Price = newPrice;
        AddDomainEvent(new ProductPriceUpdatedEvent(Id, newPrice));
    }
}
```

### Handling a Command

```csharp
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IRepository<Product> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var product = Product.Create(request.Name, request.Price);
        await _repository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(ct);
        return product.Id;
    }
}
```

## 🧰 Prerequisites

- **Runtime:** `.NET 8.0+`
- **Packages:**
  - `MediatR`
  - `MediatR.Contracts`
  - `FluentValidation`

## 📝 License

This project is licensed under the MIT License.
