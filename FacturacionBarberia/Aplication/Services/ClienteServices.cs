using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using FacturacionBarberia.Infraestructure.UnitOfWork;

namespace FacturacionBarberia.Aplication.Services
{
    public class ClienteServices: ICliente
    {
        private readonly IRepository<Cliente> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteServices(IRepository<Cliente> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<ClienteResponse>> AgregarCliente(ClienteRequest request)
        {
            var response = new Response<ClienteResponse>();

            try
            {
                if (request is null)
                {
                    response.Errors.Add(
                        "La información del cliente es requerida.");

                    return response;
                }

          
                request.Nombre = request.Nombre?.Trim()!;
                request.Telefono = request.Telefono?.Trim()!;
                request.Correo = request.Correo?.Trim().ToLower()!;

                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Errors.Add(
                        "El nombre del cliente es requerido.");
                }

                if (!ValidationHelpers.EsCorreoValido(
                    request.Correo))
                {
                    response.Errors.Add(
                        "El correo no es válido.");
                }

                if (!ValidationHelpers.EsTelefonoValido(
                    request.Telefono))
                {
                    response.Errors.Add(
                        "El número de teléfono no es válido.");
                }

                if (!string.IsNullOrWhiteSpace(request.Telefono))
                {
                    var existeTelefono = await _repository.AnyAsync(
                        x => x.Telefono == request.Telefono);

                    if (existeTelefono)
                    {
                        response.Errors.Add(
                            "El número de teléfono ya está registrado.");
                    }
                }

                if (!string.IsNullOrWhiteSpace(request.Correo))
                {
                    var existeCorreo = await _repository.AnyAsync(
                        x => x.Correo == request.Correo);

                    if (existeCorreo)
                    {
                        response.Errors.Add(
                            "El correo ya está registrado.");
                    }
                }

                if (response.ThereIsError)
                {
                    response.Successful = false;

                    return response;
                }

                var cliente = new Cliente
                {
                    Nombre = request.Nombre,
                    Telefono = request.Telefono,
                    Correo = request.Correo,
                    Estado = EstadoEnum.Activo,
                    FechaCreacion = DateTime.UtcNow
                };

                await _repository.AddAsync(cliente);

                await _unitOfWork.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Cliente agregado exitosamente.";

                response.Data = new ClienteResponse
                {
                    ClienteId = cliente.ClienteId,
                    Nombre = cliente.Nombre,
                    Telefono = cliente.Telefono,
                    Correo = cliente.Correo,
                    FechaCreacion = cliente.FechaCreacion,
                    Estado = cliente.Estado
                };

                return response;
            }
             catch (Exception ex)
             {
                response.Successful = false;
                response.Message =
                    "Ocurrió un error al agregar el cliente.";

                response.Errors.Add(ex.Message);

                return response;
             }
        }


        public async Task<Response<ObtenerClienteResponse>> ObtenerCliente (ObtenerClienteRequest request)
        {
            var response = new Response<ObtenerClienteResponse>();

            try
            {
                var clientes = await _repository.
                    GetAllAsync(x => !x.EstaEliminado);

                if (request.ClienteId.HasValue)
                {
                    clientes = clientes
                        .Where(x => x.ClienteId == request.ClienteId)
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Nombre))
                {
                    clientes = clientes
                        .Where(x => x.Nombre.Contains(request.Nombre))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Telefono))
                {
                    clientes = clientes
                        .Where(x => x.Telefono != null &&
                                    x.Telefono.Contains(request.Telefono))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Correo))
                {
                    clientes = clientes
                        .Where(x => x.Correo != null &&
                                    x.Correo.Contains(request.Correo))
                        .ToList();
                }

                if (request.Estado.HasValue)
                {
                    clientes = clientes
                        .Where(x => x.Estado == request.Estado)
                        .ToList();
                }

                response.Successful = true;
                response.DataList = clientes.Select(x => new ObtenerClienteResponse
                {
                    ClienteId = x.ClienteId,
                    Nombre = x.Nombre,
                    Telefono = x.Telefono,
                    Correo = x.Correo,
                    Estado = x.Estado,
                });
                return response;


            }
            catch (Exception )
            {
                response.Successful = false;
                response.Message = "Ocurrio un error al obtener los clientes ";
                return response;
                
            }

        }

        public async Task<Response<EditarClienteRequest>> ObtenerClienteEditar(int id)
        {
            var response = new Response<EditarClienteRequest>();

            try
            {
                var clientes = await _repository.GetByIdAsync(id);

                if (clientes == null)
                {
                    response.Successful = false;
                    response.Message = "Cliente no encontrado.";
                    return response;
                }
                
                response.Successful = true;

                response.Data = new EditarClienteRequest
                {
                    ClienteId = clientes.ClienteId,
                    Nombre = clientes.Nombre,
                    Telefono = clientes.Telefono,
                    Correo = clientes.Correo,
                    Estado = clientes.Estado
                };
                return response;


            }
            catch (Exception)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al obtener el cliente.";
                return response;

            }
        }

        public async Task<Response> EditarCliente(EditarClienteRequest request)
        {
            var response = new Response();
            try
            {
                var clientes = await _repository
                    .GetByIdAsync(request.ClienteId);

                if (clientes == null)
                {
                    response.Successful = false;
                    response.Message = "Cliente no encontrado.";
                    return response;
                }

               

                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Errors.Add(
                        "El nombre del cliente es requerido.");
                }

                if (!ValidationHelpers.EsCorreoValido(
                    request.Correo))
                {
                    response.Errors.Add(
                        "El correo no es válido.");
                }

                if (!ValidationHelpers.EsTelefonoValido(
                    request.Telefono))
                {
                    response.Errors.Add(
                        "El número de teléfono no es válido.");
                }

                if (!string.IsNullOrWhiteSpace(request.Telefono))
                {
                    var existeTelefono = await _repository.AnyAsync(
                        x => x.Telefono == request.Telefono);

                    if (existeTelefono)
                    {
                        response.Errors.Add(
                            "El número de teléfono ya está registrado.");
                    }
                }

                if (!string.IsNullOrWhiteSpace(request.Correo))
                {
                    var existeCorreo = await _repository.AnyAsync(
                        x => x.Correo == request.Correo);

                    if (existeCorreo)
                    {
                        response.Errors.Add(
                            "El correo ya está registrado.");
                    }
                }

                if (response.ThereIsError)
                {
                    response.Successful = false;

                    return response;
                }





                _repository.Update(clientes);
                await _unitOfWork.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Cliente actualizado exitosamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Ocurrió un error al actualizar el cliente.";
                response.Errors.Add(ex.Message);
                return response;

            }
        }



    }
}
