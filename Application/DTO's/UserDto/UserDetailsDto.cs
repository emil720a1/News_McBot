namespace Application.DTO_s.UserDetailsDto;

public record UserDetailsDto(long Id, string Username, Guid TelegramId, DateTime CreatedAt);