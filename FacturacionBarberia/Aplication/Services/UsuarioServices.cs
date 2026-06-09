using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using FacturacionBarberia.Infraestructure.PatronRepository.GenericRepository;
using FacturacionBarberia.Infraestructure.UnitOfWork;


namespace FacturacionBarberia.Aplication.Services
{
    public class UsuarioServices:IUsuario
    {
       private readonly IRepository<Usuario> _repository;
       private readonly IUnitOfWork _unitOfWork;

      public UsuarioServices(IRepository<Usuario> repository, IUnitOfWork unitOfWork)
      {
            _repository = repository;
            _unitOfWork = unitOfWork;
      }

        public async Task<Response<UsuarioResponse>> AgregarUsuario(UsuarioRequest request)
        {
            var response = new Response<UsuarioResponse>();
            var errors = new List<string>();

            try
            {
               

                if (string.IsNullOrWhiteSpace(request.Nombre))
                    errors.Add("El nombre es obligatorio.");

                if (string.IsNullOrWhiteSpace(request.UserName))
                    errors.Add("El nombre de usuario es obligatorio.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    errors.Add("La contraseña es obligatoria.");

                if (request.Password?.Length < 6)
                    errors.Add("La contraseña debe contener al menos 6 caracteres.");

                if (!Enum.IsDefined(typeof(RolEnum), request.Rol))
                    errors.Add("El rol seleccionado no es válido.");

                if (!Enum.IsDefined(typeof(EstadoEnum), request.Estado))
                    errors.Add("El estado seleccionado no es válido.");



                var existeUsuario = await _repository.GetAsync(
                    x => x.UserName == request.UserName);

                if (existeUsuario != null)
                    errors.Add($"El usuario '{request.UserName}' ya existe en el sistema.");

               

                if (errors.Any())
                {
                    response.Successful = false;
                    response.Message = "Se encontraron errores de validación.";
                    response.Errors = errors;

                    return response;
                }

                var usuario = new Usuario
                {
                    Nombre = request.Nombre.Trim(),
                    UserName = request.UserName.Trim(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Rol = request.Rol,
                    Estado = request.Estado
                };

                await _repository.AddAsync(usuario);
                await _unitOfWork.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario agregado exitosamente.";

                response.Data = new UsuarioResponse
                {
                    Id = usuario.UsuarioId,
                    Nombre = usuario.Nombre,
                    UserName = usuario.UserName,
                    Rol = usuario.Rol,
                    Estado = usuario.Estado,
                    FechaCreacion = usuario.FechaCreacion,
                    UsuarioCreacion = usuario.UsuarioCreacion,
                    EstaEliminado = usuario.EstaEliminado
                };

                return response;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al registrar el usuario.";
                response.Errors = new List<string>
        {
            ex.Message
        };

                return response;
            }
        }




    }
}
