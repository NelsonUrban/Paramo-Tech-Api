namespace Sat.Recruitment.Custom.Validations.Abstract
{
    public interface ICustomValidation
    {
        decimal UserMoneyValidation(string userType, decimal money);
    }
}
