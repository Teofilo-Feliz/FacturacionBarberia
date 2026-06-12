using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using FacturacionBarberia.Infraestructure.Data;
using FacturacionBarberia.Infraestructure.PatronRepository.GenericRepository;
using FacturacionBarberia.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;


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
           

            try
            {

                if (string.IsNullOrWhiteSpace(request.Nombre))
                   response.Errors.Add("El nombre es obligatorio.");

                if (string.IsNullOrWhiteSpace(request.UserName))
                    response.Errors.Add("El nombre de usuario es obligatorio.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    response.Errors.Add("La contraseña es obligatoria.");

                if (request.Password?.Length < 6)
                    response.Errors.Add("La contraseña debe contener al menos 6 caracteres.");

                if (request.UserName.Length > 50)
                    response.Errors.Add("El usuario no puede tener mas de 50 caracteres");

                if (!Enum.IsDefined(typeof(RolEnum), request.Rol))
                    response.Errors.Add("El rol seleccionado no es válido.");

                if (!Enum.IsDefined(typeof(EstadoEnum), request.Estado))
                    response.Errors.Add("El estado seleccionado no es válido.");

                if (request.Estado == EstadoEnum.Inactivo)
                {
                    response.Successful = false;

                    response.Errors.Add(
                        "No se puede registrar un usuario en estado inactivo.");

                    return response;
                }



                var existeUsuario = await _repository.GetAsync(
                    x => x.UserName == request.UserName);

                if (existeUsuario != null)
                    response.Errors.Add($"El usuario '{request.UserName}' ya existe en el sistema.");

               

                if (response.ThereIsError)
                {
                    response.Successful = false;
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

        public async Task<Response<LoginResponse>> Login(LoginRequest request)
        {
            var response = new Response<LoginResponse>();
          

            try
            {

                if (request == null)
                {
                    response.Successful = false;
                    response.Errors.Add("Solicitud inválida.");

                    return response;
                }

                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    response.Errors.Add("Debe ingresar el usuario.");
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    response.Errors.Add("Debe ingresar la contraseña.");
                }
  

                if (response.ThereIsError)
                {
                    response.Successful = false;

                    return response;
                }

                var usuario = await _repository.GetAsync(
                    x => x.UserName == request.UserName);

                if (usuario == null)
                {
                    response.Successful = false;
                    response.Message = "Usuario o contraseña incorrectos.";

                    return response;
                }

                if (usuario.EstaEliminado)
                {
                    response.Successful = false;
                    response.Message =
                        "Usuario o contraseña incorrectos.";

                    return response;
                }

                if (usuario.Estado != EstadoEnum.Activo)
                {
                    response.Successful = false;
                    response.Message = "El usuario se encuentra inactivo.";

                    return response;
                }

                bool passwordValida = BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    usuario.PasswordHash);

                if (!passwordValida)
                {
                    response.Successful = false;
                    response.Message = "Usuario o contraseña incorrectos.";

                    return response;
                }

               

                response.Successful = true;
                response.Message = "Inicio de sesión exitoso.";

                response.Data = new LoginResponse
                {
                    UsuarioId = usuario.UsuarioId,
                    Nombre = usuario.Nombre,
                    UserName = usuario.UserName,
                    Rol = usuario.Rol,
                };

                return response;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al iniciar sesión.";

                response.Errors.Add(ex.Message);

                return response;
            }
        }

        public async Task<Response<ObtenerUsuarioResponse>> ObtenerUsuarios(ObtenerUsuarioRequest request)
        {
            var response = new Response<ObtenerUsuarioResponse>();

            try
            {
                var usuarios = await _repository
                    .GetAllAsync(x => !x.EstaEliminado);

                if (request.UsuarioId.HasValue)
                {
                    usuarios = usuarios
                        .Where(x => x.UsuarioId == request.UsuarioId)
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.UserName))
                {
                    usuarios = usuarios
                        .Where(x => x.UserName.Contains(request.UserName))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Nombre))
                {
                    usuarios = usuarios
                        .Where(x => x.Nombre.Contains(request.Nombre))
                        .ToList();
                }

                if (request.Estado.HasValue)
                {
                    usuarios = usuarios
                        .Where(x => x.Estado == request.Estado)
                        .ToList();
                }

                if (request.Rol.HasValue)
                {
                    usuarios = usuarios
                        .Where(x => x.Rol == request.Rol.Value)
                        .ToList();
                }


                response.Successful = true;

                response.DataList = usuarios.Select(x => new ObtenerUsuarioResponse
                {
                    UsuarioId = x.UsuarioId,
                    Nombre = x.Nombre,
                    UserName = x.UserName,
                    Rol = x.Rol,
                    Estado = x.Estado
                }).ToList();

                return response;
            }
            catch (Exception)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al obtener los usuarios.";
                return response;
            }
        }

        public async Task<Response<EditarUsuarioRequest>> ObtenerUsuarioEditar(int id)
        {
            var response = new Response<EditarUsuarioRequest>();

            try
            {
                var usuario = await _repository.GetByIdAsync(id);

                if (usuario == null)
                {
                    response.Successful = false;
                    response.Errors.Add("El usuario no fue encontrado.");

                    return response;
                }

                response.Successful = true;

                response.Data = new EditarUsuarioRequest
                {
                    UsuarioId = usuario.UsuarioId,
                    Nombre = usuario.Nombre,
                    Rol = usuario.Rol,
                    Estado = usuario.Estado
                };

                return response;
            }
            catch (Exception)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al obtener el usuario.";

                return response;
            }
        }

        public async Task<Response> EditarUsuario(
             EditarUsuarioRequest request)
        {
            var response = new Response();

            try
            {
                var usuario = await _repository
                    .GetByIdAsync(request.UsuarioId);

                if (usuario == null)
                {
                    response.Successful = false;

                    response.Errors.Add(
                        "El usuario no fue encontrado.");

                    return response;
                }

                usuario.Nombre = request.Nombre;
                usuario.Rol = request.Rol;
                usuario.Estado = request.Estado;

                _repository.Update(usuario);

                await _unitOfWork.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario actualizado correctamente.";

                return response;
            }
            catch (Exception)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al actualizar el usuario.";

                return response;
            }
        }


    }
}
