using Microsoft.EntityFrameworkCore;

namespace Backend_Mascotas.Models.Repository
{
	public class MascotaRepository: IMascotaRpository
	{
		private readonly AplicationDbContext _context;

		public MascotaRepository(AplicationDbContext context)
		{
			_context = context;

		}

		public async Task DeleteMascota(Mascota mascota)
		{
			_context.Mascota.Remove(mascota);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Mascota>> GetListMascotas()
		{
			return await _context.Mascota.ToListAsync();
		}

		public async Task<Mascota> GetMascota(int id)
		{
			return await _context.Mascota.FindAsync(id);
		}

		public async Task<Mascota> AddMascota(Mascota mascota)
		{
			_context.Add(mascota);
			await _context.SaveChangesAsync();
			return mascota;
		}

		public async Task UpdateMascota(Mascota mascota)
		{
			var mascotaItem = await _context.Mascota.FirstOrDefaultAsync(x => x.Id == mascota.Id);

			if (mascota != null)
			{
				mascotaItem.Nombre = mascota.Nombre;
				mascotaItem.Edad = mascota.Edad;
				mascotaItem.Raza = mascota.Raza;
				mascotaItem.Color = mascota.Color;
				mascotaItem.Peso = mascota.Peso;
			}

			await _context.SaveChangesAsync();
		}
	}
}
