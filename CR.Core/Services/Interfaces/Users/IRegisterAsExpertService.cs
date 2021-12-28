namespace CR.Core.Services.Interfaces.Users
{
    public interface IRegisterAsExpertService
    {
        void Execute(long userId, string name, string lastName);
    }
}
