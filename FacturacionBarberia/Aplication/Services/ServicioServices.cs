using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using FacturacionBarberia.Infraestructure.UnitOfWork;

namespace FacturacionBarberia.Aplication.Services
{
    public class ServicioServices: IServicio
    {
        private readonly IRepository<Servicio> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ServicioServices(IRepository<Servicio> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<ServicioResponse>> AgregarServicio(ServicioRequest request)
        {
            var response = new Response<ServicioResponse>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Nombre))
                    response.Errors.Add("El nombre del servicio es obligatorio");

                if (request.Precio <=0 )
                    response.Errors.Add("El precio del servicio es obligatorio");

                if (request.Estado == EstadoEnum.Inactivo)
                {
                    response.Successful = false;

                    response.Errors.Add(
                        "No se puede registrar un servicio en estado inactivo.");

                    return response;
                }

                var existeServicio = await _repository.GetAsync(
                    x => x.Nombre == request.Nombre);
                if (existeServicio != null)
                    response.Errors.Add("El nombre del servicio ya esta registrado");

                if (response.ThereIsError)
                {
                    response.Successful = false;
                    return response;
                }

                var servicios = new Servicio
                {
                    Nombre = request.Nombre.Trim(),
                    Precio = request.Precio,
                    Estado = EstadoEnum.Activo
                };

                await _repository.AddAsync(servicios);
                await _unitOfWork.SaveChangesAsync();

                response.Successful = true;
                response.Message = "El servicio fue agregado exitosamente";

                response.Data = new ServicioResponse
                {
                    ServicioId = servicios.ServicioId,
                    Nombre = servicios.Nombre,
                    Precio = servicios.Precio,
                    Estado = servicios.Estado

                };
                return response;

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Ocurrio un error al agregar el servicio";
                response.Errors.Add(ex.Message);
                return response;


                
            }






        }




    }
}
