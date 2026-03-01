namespace Application.DTO_s.UserDetailsDto;

public record UserDetailsDto(Guid Id, string Username, Guid TelegramId, DateTime CreatedAt);