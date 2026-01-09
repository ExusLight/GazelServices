using Microsoft.AspNetCore.Mvc;
using GazelServices.Models;
using GazelServices.Services;

namespace YourApp.Controllers;

public class OrderController : Controller
{
    private readonly TelegramService _telegram;

    public OrderController(TelegramService telegram)
    {
        _telegram = telegram;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send([FromForm] OrderRequest model, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        return BadRequest(new {ok = false, error = "validation"});

        try
        {
            await _telegram.SendOrderAsync(model, ct);
            return Ok(new { ok = true });
        }
        catch
        {
            return StatusCode(500, new {ok = false, error = "tg_failed"});
        }
    }
}