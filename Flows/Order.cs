namespace Cleipnir.Flows.HelloWorld.Flows;

public record Order(string OrderId, Guid CustomerId, IEnumerable<Guid> ProductIds, decimal TotalPrice);