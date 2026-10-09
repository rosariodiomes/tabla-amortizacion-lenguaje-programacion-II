using Spectre.Console;

decimal montoPrestamo = AnsiConsole.Ask<decimal>("Monto del préstamo: ");
decimal tasaInteres = AnsiConsole.Ask<decimal>("Tasa de interés: ");
int plazoPrestamo = AnsiConsole.Ask<int>("Plazo del préstamo: ");

var tabla = new Table()
  .ShowRowSeparators()
  .RoundedBorder()
  .BorderColor(Color.Blue);

// Columnas
tabla.AddColumn("No. de cuota");
tabla.AddColumn("Pago de cuota");
tabla.AddColumn("Interés a pagar");
tabla.AddColumn("Abono a capital");
tabla.AddColumn("Saldo");

decimal tasaInteresMensual = tasaInteres / 12 / 100;

decimal numerador = (decimal)Math.Pow(1 + (double)tasaInteresMensual, plazoPrestamo);
decimal denominador = (decimal)Math.Pow(1 + (double)tasaInteresMensual, plazoPrestamo) - 1;

decimal cuotaFija = montoPrestamo * tasaInteresMensual * (numerador / denominador);

// Filas
for (int i = 1; i <= plazoPrestamo; i++)
{
  decimal interesPeriodo = montoPrestamo * tasaInteresMensual;
  decimal abonoCapital = cuotaFija - interesPeriodo;
  montoPrestamo = montoPrestamo - abonoCapital;

  tabla.AddRow(
    $"{i}",
    $"{cuotaFija:C2}",
    $"{interesPeriodo:C2}",
    $"{abonoCapital:C2}",
    $"{montoPrestamo:C2}"
  );
}

// Imprimo la tabla
AnsiConsole.Write(tabla);