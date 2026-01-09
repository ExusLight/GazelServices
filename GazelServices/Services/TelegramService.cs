using System.Text;
using System.Text.Json;
using GazelServices.Models;

namespace GazelServices.Services;

public class TelegramService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public TelegramService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task SendOrderAsync(OrderRequest order, CancellationToken ct = default)
    {
        var token = _config["Telegram:BotToken"];
        var chatId = _config["Telegram:ChatId"];

        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(chatId))
            throw new InvalidOperationException("Telegram config is missing (BotToken/chatId)");

        var text =
$@"🟡 Новая заявка
        📍 Погрузка: {order.FromCity}
        📦 Доставка: {order.ToCity}
        ⚖️ Вес: {order.Weight} кг
        📞 Телефон: {order.Phone}";

        var url = $"https://api.telegram.org/bot{token}/sendMessage";

        var payload = new
        {
            chat_id = chatId,
            text = text
        };

        using var content = new StringContent(
    JsonSerializer.Serialize(payload),
    Encoding.UTF8,
    "application/json"
);

        using var resp = await _http.PostAsync(url, content, ct);
        resp.EnsureSuccessStatusCode();
    }

}