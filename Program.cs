class Funciones
{
    public static string ConvertirBase(string numero, int baseInicial, int baseFinal)
    {
        if ((baseInicial != 2 && baseInicial != 8 && baseInicial != 10 && baseInicial != 16) ||
            (baseFinal != 2 && baseFinal != 8 && baseFinal != 10 && baseFinal != 16))
        {
            return "Base no válida.";
        }

        if (baseInicial != 10)
        {
            numero = ConvertirADecimal(numero, baseInicial);
        }

        if (baseFinal == 10)
        {
            return numero.ToString();
        }
        else
        {
            return ConvertirDesdeDecimal(numero, baseFinal);
        }
    }

    public static string ConvertirADecimal(string numero, int baseInicial)
    {
        int resultado = 0;
        int longitud = numero.Length;

        for (int i = 0; i < longitud; i++)
        {
            int digito = DigitoValor(numero[i]);
            resultado += digito * (int)Math.Pow(baseInicial, longitud - i - 1);
        }

        return resultado.ToString();
    }

    public static int DigitoValor(char digito)
    {
        if (char.IsDigit(digito))
        {
            return (int)char.GetNumericValue(digito);
        }
        else
        {
            return char.ToUpper(digito) - 'A' + 10;
        }
    }

    public static string ConvertirDesdeDecimal(string numero, int baseFinal)
    {
        int numeroDecimal = int.Parse(numero);
        string resultado = "";

        while (numeroDecimal > 0)
        {
            int residuo = numeroDecimal % baseFinal;

            if (residuo < 10)
            {
                resultado = residuo + resultado;
            }
            else
            {
                resultado = (char)(residuo - 10 + 'A') + resultado;
            }

            numeroDecimal /= baseFinal;
        }

        return resultado == "" ? "0" : resultado;
    }

    public static int ConvertirHexadecimalADecimal(string hexadecimal)
    {
        return int.Parse(hexadecimal, System.Globalization.NumberStyles.HexNumber);
    }
}

class Program
{
    static void Main()
    {
    COMIENZO:
        Console.WriteLine("Ingrese un número con su base (ejemplo: 111B) PARA CERRAR COLOCA 0");
        string NUMERO = Console.ReadLine();

        if (NUMERO == "0")
        {
            Console.WriteLine("Fin");
            return;
        }

        char[] charArray = NUMERO.ToCharArray();
        char lastSymbol = charArray[charArray.Length - 1];

        string inputNumero = NUMERO.Substring(0, NUMERO.Length - 1);
        string baseNumero = lastSymbol.ToString().ToUpper();

        int baseInicial;

        switch (baseNumero)
        {
            case "B":
                baseInicial = 2;
                break;
            case "O":
                baseInicial = 8;
                break;
            case "D":
                baseInicial = 10;
                break;
            case "H":
                baseInicial = 16;
                break;
            default:
                Console.WriteLine("Base no válida.");
                return;
        }

        string baseConvertirInput = Console.ReadLine().ToUpper();

        int baseConvertir;

        switch (baseConvertirInput)
        {
            case "B":
                baseConvertir = 2;
                break;
            case "O":
                baseConvertir = 8;
                break;
            case "D":
                baseConvertir = 10;
                break;
            case "H":
                baseConvertir = 16;
                break;
            default:
                Console.WriteLine("Base no válida.");
                return;
        }

        string numeroDecimal;

        if (inputNumero.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            inputNumero = inputNumero.Substring(2);
            numeroDecimal = Funciones.ConvertirHexadecimalADecimal(inputNumero).ToString();
        }
        else
        {
            numeroDecimal = inputNumero;
        }

        Console.WriteLine($"Número en base {baseConvertirInput}: {Funciones.ConvertirBase(numeroDecimal, baseInicial, baseConvertir)}");
        Console.ReadKey();
        Console.Clear();
        goto COMIENZO;
    }
}