using Backend_Mascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Mascotas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MascotaController : ControllerBase
	{
		private readonly AplicationDbContext _context;

		public MascotaController(AplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var listMascotas = await _context.Mascota.ToListAsync();
				return Ok(listMascotas);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var mascota = await _context.Mascota.FindAsync(id);
				if(mascota == null)
				{
					return NotFound();
				}
				return Ok(mascota);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var mascota = await _context.Mascota.FindAsync(id);

				if (mascota == null)
				{
					return NotFound(mascota);
				}

				_context.Mascota.Remove(mascota);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
