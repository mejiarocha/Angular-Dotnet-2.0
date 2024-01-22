using AutoMapper;
using Backend_Mascotas.Models.DTO;

namespace Backend_Mascotas.Models.Profiles
{
	public class MascotaProfile: Profile
	{
		public MascotaProfile()
		{
			CreateMap<Mascota, MascotaDTO>();
			CreateMap<MascotaDTO, Mascota>();
		}
	}
}
