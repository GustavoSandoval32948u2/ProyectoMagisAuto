using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.Validators.Validaciones
{
    public static class ValidadorNitGuatemala
    {
        public static bool EsValido(string? nit)
        {
            if (string.IsNullOrWhiteSpace(nit))
                return false;

            // Admitimos espacios, puntos y guiones como separadores.
            string limpio = new string(
                nit.Trim()
                   .Where(c => !char.IsWhiteSpace(c)
                            && c != '.'
                            && c != '-')
                   .ToArray()
            ).ToUpperInvariant();

            // Base numérica de 6 a 9 dígitos + dígito verificador.
            if (limpio.Length < 7 || limpio.Length > 10)
                return false;

            string baseNit = limpio[..^1];
            char verificador = limpio[^1];

            if (!baseNit.All(char.IsDigit))
                return false;

            if (!char.IsDigit(verificador) && verificador != 'K')
                return false;

            int suma = 0;
            int factor = baseNit.Length + 1;

            foreach (char digito in baseNit)
            {
                suma += (digito - '0') * factor;
                factor--;
            }

            int resultado = 11 - (suma % 11);

            if (resultado == 11)
                resultado = 0;

            char esperado = resultado == 10
                ? 'K'
                : resultado.ToString()[0];

            return verificador == esperado;
        }
    }

}
