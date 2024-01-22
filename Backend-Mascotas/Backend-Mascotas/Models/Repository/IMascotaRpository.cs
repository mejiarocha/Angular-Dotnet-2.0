namespace Backend_Mascotas.Models.Repository
{
	public interface IMascotaRpository
	{
		Task<List<Mascota>> GetListMascotas();
		Task<Mascota> GetMascota(int id);
		Task DeleteMascota(Mascota mascota);
		
		Task<Mascota> AddMascota(Mascota mascota);

		Task UpdateMascota(Mascota mascota);
		
	}
}
