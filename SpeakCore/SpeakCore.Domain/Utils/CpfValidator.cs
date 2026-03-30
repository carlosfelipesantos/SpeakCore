namespace SpeakCore.Domain.Utils
{
    public class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11 || !cpf.All(char.IsDigit)) return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp = cpf.Substring(0, 9);
            int sum = temp.Select((t, i) => int.Parse(t.ToString()) * mult1[i]).Sum();
            int resto = sum % 11;
            int dig1 = resto < 2 ? 0 : 11 - resto;

            temp += dig1;
            sum = temp.Select((t, i) => int.Parse(t.ToString()) * mult2[i]).Sum();
            resto = sum % 11;
            int dig2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{dig1}{dig2}");
        }
    }
}
