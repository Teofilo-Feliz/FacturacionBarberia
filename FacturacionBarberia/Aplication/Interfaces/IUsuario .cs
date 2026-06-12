using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;

namespace FacturacionBarberia.Aplication.Interfaces
{
    public interface IUsuario
    {
        Task<Response<UsuarioResponse>> AgregarUsuario(UsuarioRequest request);
        Task<Response<LoginResponse>> Login(LoginRequest request);
        Task<Response<ObtenerUsuarioResponse>> ObtenerUsuarios(ObtenerUsuarioRequest request);
        Task<Response<EditarUsuarioRequest>> ObtenerUsuarioEditar(int id);
        Task<Response> EditarUsuario(EditarUsuarioRequest request);
    }  
}
