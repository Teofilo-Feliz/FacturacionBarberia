using System.Security.Claims;

namespace FacturacionBarberia.Infraestructure.Audit
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UsuarioId
        {
            get
            {
                var claim = _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                    return null;

                return int.Parse(claim.Value);
            }
        }
    }
}
