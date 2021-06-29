namespace SellSpasibo.Core.Interfaces
{
    public interface IStringCrypt
    {
        /// <summary>
        /// Шифрование значение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Encrypt(string value);
        /// <summary>
        /// Дешифрование значения
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        string Decrypt(string hash);
    }
}