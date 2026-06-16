using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FacturacionBarberia.Aplication.Helpers
{
    public class ValidationHelpers
    {
        public static bool EsCorreoValido(string? correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return true;

            try
            {
                var email = new MailAddress(correo);

                return email.Address.Equals(
                    correo.Trim(),
                    StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static bool EsTelefonoValido(string? telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return true;

            var numeros = Regex.Replace(
                telefono,
                @"\D",
                "");

            return numeros.Length >= 10
                   && numeros.Length <= 15;
        }

    }
}
