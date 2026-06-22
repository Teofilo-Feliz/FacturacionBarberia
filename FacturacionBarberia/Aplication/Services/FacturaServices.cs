using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using FacturacionBarberia.Infraestructure.Audit;
using FacturacionBarberia.Infraestructure.PatronRepository.FacturaRepository;

using FacturacionBarberia.Infraestructure.UnitOfWork;

namespace FacturacionBarberia.Aplication.Services
{
    public class FacturaServices: IFactura
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService  _currentUserService;
        private readonly IRepository<Servicio> _servicesRepository;
        private readonly IRepository<Cliente> _clienteRepository;

        public FacturaServices (IFacturaRepository facturaRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService,
          IRepository<Servicio> servicesRepository, IRepository<Cliente> clienteRepository)
        {
            _facturaRepository = facturaRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _servicesRepository = servicesRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<Response<ObtenerDetalleFacturaResponse>> AgregarFacturas(FacturaRequest request)
        {
            var response = new Response<ObtenerDetalleFacturaResponse>();

            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);

                if(cliente == null)
                {
                    response.Successful = false;
                    response.Message = "El cliente no existe";
                    return response;
                }

                if (request.Detalles == null || !request.Detalles.Any())
                {
                    response.Successful = false;
                    response.Message = "Debe seleccionar al menos un servicio";
                    return response;
                }

                var usuarioId = _currentUserService.UsuarioId;

                if (!usuarioId.HasValue)
                {
                    response.Successful = false;
                    response.Message = "No se puede identificar el usuario autenticado";
                }

                var factura = new Factura
                {
                    ClienteId = request.ClienteId,
                    UsuarioId = usuarioId!.Value,
                    FechaFactura = DateTime.UtcNow,
                    FormaPago = request.FormaPago,
                    Observaciones = request.Observaciones,
                    EstadoFactura = EstadoFacturaEnum.Activa
                };

                decimal total = 0;

                foreach (var item in request.Detalles)
                {
                    var servicio = await _servicesRepository
                        .GetByIdAsync(item.ServicioId);

                    if (servicio == null)
                        continue;

                    var detalle = new DetalleFactura
                    {
                        ServicioId = servicio.ServicioId,
                        Servicio = servicio,
                        Precio = servicio.Precio,
                        Cantidad = item.Cantidad,
                    };

                    factura.Detalles.Add(detalle);
                    total += detalle.SubTotal;
                }

                if (!factura.Detalles.Any())
                {
                    response.Successful = false;
                    response.Message = "No se encontraron servicios válidos.";
                    return response;
                }

                factura.Total = total;
                await _facturaRepository.AddAsync(factura);

                await _unitOfWork.SaveChangesAsync();

                response.Data = new ObtenerDetalleFacturaResponse
                {
                    FacturaId = factura.FacturaId,
                    Cliente = cliente.Nombre,
                    Usuario = _currentUserService.UserName!,
                    FechaFactura = factura.FechaFactura,
                    Total = factura.Total,
                    Detalles = factura.Detalles.Select(x => new DetalleFacturaResponse
                    {
                        Servicio = x.Servicio?.Nombre ?? string.Empty,
                        Precio = x.Precio,
                        Cantidad = x.Cantidad,
                        SubTotal = x.SubTotal
                    }).ToList()
                };

                response.Successful = true;
                response.Message = "Factura creada correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message =
                    ex.ToString();
            }

            return response;
        }


        public async Task<Response<ObtenerFacturaResponse>> ObtenerFactura(ObtenerFacturaRequest request)
        {
            var response = new Response<ObtenerFacturaResponse>();

            try
            {
                var facturas = await _facturaRepository.GetAllAsync();

                if (request.FacturaId.HasValue)
                {
                    facturas = facturas
                        .Where(x => x.FacturaId == request.FacturaId)
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Cliente))
                {
                    facturas = facturas
                        .Where(x => x.Cliente != null &&
                                    x.Cliente.Nombre.Contains(
                                        request.Cliente,
                                        StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Usuario))
                {
                    facturas = facturas
                        .Where(x => x.Usuario != null &&
                                    x.Usuario.UserName.Contains(
                                    request.Usuario,
                                    StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (request.FechaDesde.HasValue)
                {
                    facturas = facturas
                        .Where(x => x.FechaFactura.Date >= request.FechaDesde.Value.Date)
                        .ToList();
                }

                if (request.FechaHasta.HasValue)
                {
                    facturas = facturas
                        .Where(x => x.FechaFactura.Date <= request.FechaHasta.Value.Date)
                        .ToList();
                }

                if (request.FormaPago.HasValue)
                {
                    facturas = facturas
                        .Where(x => x.FormaPago == request.FormaPago)
                        .ToList();
                }

                if (request.EstadoFactura.HasValue)
                {
                    facturas = facturas
                        .Where(x => x.EstadoFactura == request.EstadoFactura)
                        .ToList();
                }



                response.DataList = facturas.Select(x => new ObtenerFacturaResponse
                {
                    FacturaId = x.FacturaId,
                    Cliente = x.Cliente.Nombre,
                    Usuario = x.Usuario.UserName,
                    FechaFactura = x.FechaFactura,
                    Total = x.Total,
                    FormaPago = x.FormaPago,
                    EstadoFactura = x.EstadoFactura,
                });
                response.Successful = true;
                    


            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = ex.Message;
                
            }
            return response;


        }

        public async Task<Response<ObtenerDetalleFacturaResponse>> ObtenerFacturaDetalle(int facturaId)
        {
            var response = new Response<ObtenerDetalleFacturaResponse>();

            try
            {
                var facturasDetalles = await _facturaRepository.GetFacturaDetalleAsync(facturaId);

                if (facturasDetalles == null)
                {
                    response.Successful = false;
                    response.Message = "La factura no fue encontrada";
                    return response;
                }

                response.Data = new ObtenerDetalleFacturaResponse
                {
                    FacturaId = facturasDetalles.FacturaId,
                    Cliente = facturasDetalles.Cliente.Nombre,
                    Usuario = facturasDetalles.Usuario.UserName,
                    FechaFactura = facturasDetalles.FechaFactura,
                    Total = facturasDetalles.Total,
                    Estado = facturasDetalles.EstadoFactura,
                    Detalles = facturasDetalles.Detalles
                    .Select(x => new DetalleFacturaResponse
                    {
                        Servicio = x.Servicio.Nombre,
                        Precio = x.Precio,
                        Cantidad = x.Cantidad,
                        SubTotal = x.SubTotal
                    })
                    .ToList()

                   
                };

                response.Successful = true;

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = ex.Message;

                
            }
            return response;
        }

        public async Task<Response<bool>> AnularFactura(int facturaId)
        {
            var response = new Response<bool>();

            try
            {
                var factura = await _facturaRepository.GetByIdAsync(facturaId);

                if (factura == null)
                {
                    response.Successful = false;
                    response.Message = "La factura no existe";
                    return response;    
                }

                if (factura.EstadoFactura == EstadoFacturaEnum.Anulada)
                {
                    response.Successful = false;
                    response.Message = "La factura ya fue anulada";
                    return response;
                }

                factura.EstadoFactura = EstadoFacturaEnum.Anulada;

                await _unitOfWork.SaveChangesAsync();

                response.Successful = true;
                response.Message = "La factura fue anulada correctamente";




            }
            catch (Exception ex)
            {
                response.Successful= false;
                response.Message= ex.Message;
                
            }
            return response;
        }







    }
}
