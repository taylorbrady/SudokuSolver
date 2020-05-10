using System;
using System.Linq;
using System.Numerics;
using Ardalis.GuardClauses;

namespace sudokusolver.Converters
{
    public static class Converter
    {
        public static int[][] From(string body, string delimiter)
        {
            Guard.Against.NullOrWhiteSpace(body, nameof(body));

            string nl = null;
            if (!String.IsNullOrEmpty(delimiter))
            {
                if (!body.Contains(delimiter))
                {
                    throw new ArgumentException("the supplied body does not contain the identified delimiter");
                }
                else
                {
                    nl = delimiter;
                }
            }
            else
            {
                if (body.Contains("\r\n")) nl = "\r\n";
                else if (body.Contains("\n")) nl = "\n";
                else throw new ArgumentException("no delimiter was provided and neither CRLF no LF were found"); 

            }

            var stringRows = body.Split(nl, StringSplitOptions.RemoveEmptyEntries);
            var rows = stringRows.Select(r =>
            {
                if (! BigInteger.TryParse(r, out _)) throw new FormatException($"'{r}' may contain non-numeric values");

                return r.ToCharArray()
                    .Select(c => c - '0')
                    .ToArray();
            }).ToArray();

            return rows;
        }
    }
}