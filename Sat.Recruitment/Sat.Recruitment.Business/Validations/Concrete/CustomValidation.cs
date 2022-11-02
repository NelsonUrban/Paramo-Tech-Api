using Sat.Recruitment.Custom.Validations.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Custom.Validations.Concrete
{
    public class CustomValidation : ICustomValidation
    {
        private Dictionary<string,decimal> keyValues = new Dictionary<string,decimal>();
      
        public CustomValidation()
        {
            keyValues.Add("NormalMoreThan100", Convert.ToDecimal(0.12));
            keyValues.Add("NormalLessThan100", Convert.ToDecimal(0.8));
            keyValues.Add("SuperUser", Convert.ToDecimal(0.20));
            keyValues.Add("Premium", Convert.ToDecimal(2));
        }

        public decimal UserMoneyValidation(string userType, decimal money)
        {
            switch (userType)
            {
                case "Normal":
                return (MoneyNormalCalculation(money));
                case "SuperUser":
                    return (MoneyCalculation(money, keyValues.Where(x => x.Key == "SuperUser").FirstOrDefault().Value));
                case "Premium":
                    return (MoneyCalculation(money, keyValues.Where(x => x.Key == "Premium").FirstOrDefault().Value));
                default:
                    return 0;                   
            }
        }

        private decimal CalculateGif(decimal money, decimal percentage)
        { 
            return money * percentage;
        }

        private decimal MoneyCalculation(decimal money, decimal percentage)
        {
            if (money < 100) return 0;

            var gif = CalculateGif(money, percentage);
            return money + gif;
        }
        private decimal MoneyNormalCalculation(decimal money)
        {
            if (money > 100)
            {
                var percentage = keyValues.Where(x => x.Key == "NormalMoreThan100").FirstOrDefault().Value;
                var gif = CalculateGif(money, percentage);
                return money + gif;
            }
            if (money < 100 && money > 10)
            {
                var percentage = keyValues.Where(x => x.Key == "NormalLessThan100").FirstOrDefault().Value;
                var gif = CalculateGif(money, percentage);
                return money + gif;
            }
            return 0;
        }
    }
}
