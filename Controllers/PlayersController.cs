using Microsoft.AspNetCore.Mvc;
using CardClashBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CardClashBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly PlayerContext _context;

    public PlayersController(PlayerContext context)
        => _context = context;

    // GET: api/Players/5
    [HttpGet("{email}/{pass}")]
    public async Task<ActionResult<bool>> GetPlayer(string? email, string? pass)
        => await _context.Players.AnyAsync(x => x.email == email && x.password == pass);


    // POST: api/Players
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Player>> PostPlayer(Player player)
    {
        // If the email already is registered, disregard it
        if (_context.Players.Any(x => x.email == player.email)) return BadRequest();

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
    }

    // DELETE: api/Players/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int? id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
