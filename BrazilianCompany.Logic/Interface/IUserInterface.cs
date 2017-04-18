namespace BrazilianCompany.Logic.Interface
{
    public interface IUserInterface
    {
        string ReadLine();
        void WriteLine(string format, params string[] args);
    }
}