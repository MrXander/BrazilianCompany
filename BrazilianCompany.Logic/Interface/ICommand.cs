#region usings

using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Logic.Interface
{
    public interface ICommand
    {
        void Execute(Context context);
        object GetState();
    }
}