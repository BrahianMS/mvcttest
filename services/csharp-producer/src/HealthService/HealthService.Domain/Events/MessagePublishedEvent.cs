namespace HealthService.Domain.Events;

public record MessagePublishedEvent(Guid Id, DateTime CreatedAt, string Message);