using BradlyPlc.Test.Test.DTOs.InputGeneration;
using BradlyPlc.Test.Test.DTOs.OutputGeneration;
using BradlyPlc.Test.Test.DTOs.ReferenceData;
using System.Collections.Generic;
using System.Linq;

namespace BradlyPlc.Test.Test.Helpers
{
    public class EnergyCalculator
    {

        public Generator GetCoalTotalGenerationValue(CoalGenerator coalGenerator,
            decimal valueFactor)
        {
            Generator dailyGenerator = new Generator();
            dailyGenerator.Name = coalGenerator.Name;
            foreach (var day in coalGenerator.Generation.Day)
            {
                dailyGenerator.Total = dailyGenerator.Total
                    + day.Price * day.Energy * valueFactor;

            }

            dailyGenerator.Total = decimal.Round(dailyGenerator.Total, 9);
            return dailyGenerator;
        }

        public Generator GetGasTotalGenerationValue(GasGenerator gasGenerator,
            decimal valueFactor)
        {
            Generator dailyGenerator = new Generator();
            dailyGenerator.Name = gasGenerator.Name;
            foreach (var day in gasGenerator.Generation.Day)
            {
                dailyGenerator.Total = dailyGenerator.Total
                    + day.Price * day.Energy * valueFactor;

            }
            return dailyGenerator;
        }

        public Generator GetWindTotalGenerationValue(WindGenerator windGenerator,
            decimal valueFactor)
        {
            Generator dailyGenerator = new Generator();
            dailyGenerator.Name = windGenerator.Name;
            foreach (var day in windGenerator.Generation.Day)
            {
                dailyGenerator.Total = dailyGenerator.Total
                    + day.Price * day.Energy * valueFactor;

            }
            return dailyGenerator;
        }

        public IList<DTOs.OutputGeneration.Day> GetMaxEmissionsPerDay(Gas gas, Coal coal, EmissionsFactor emissionsFactor)
        {
            MaxEmissionGenerators maxEmissionGenerators = new MaxEmissionGenerators();
            maxEmissionGenerators.Day = new List<DTOs.OutputGeneration.Day>();

            foreach (var gasGenerator in gas.GasGenerator)
            {
                foreach (var inputday in gasGenerator.Generation.Day)
                {
                    DTOs.OutputGeneration.Day outputDay = new DTOs.OutputGeneration.Day();
                    outputDay.Date = inputday.Date;
                    outputDay.Name = gasGenerator.Name;
                    outputDay.Emission = inputday.Energy * gasGenerator.EmissionsRating * emissionsFactor.Medium;
                    maxEmissionGenerators.Day.Add(outputDay);
                }
            }

            foreach (var coalGenerator in coal.CoalGenerator)
            {
                foreach (var inputday in coalGenerator.Generation.Day)
                {
                    DTOs.OutputGeneration.Day outputDay = new DTOs.OutputGeneration.Day();
                    outputDay.Date = inputday.Date;
                    outputDay.Name = coalGenerator.Name;
                    outputDay.Emission = inputday.Energy * coalGenerator.EmissionsRating * emissionsFactor.High;
                    maxEmissionGenerators.Day.Add(outputDay);
                }
            }
            var result = maxEmissionGenerators.Day.GroupBy(x => x.Date)
                .SelectMany(group => group.Where(element => element.Emission == group.Max(obj => obj.Emission)));

            return result.ToList();
        }

        public IList<ActualHeatRates> GetActualHeatRates(Coal coal)
        {
            var actualHeatRates = new List<ActualHeatRates>();

            foreach (var coalGenerator in coal.CoalGenerator)
            {
                var actualHeatRate = new ActualHeatRates();
                actualHeatRate.HeatRate = coalGenerator.TotalHeatInput / coalGenerator.ActualNetGeneration;
                actualHeatRate.Name = coalGenerator.Name;
                actualHeatRates.Add(actualHeatRate);
            }
            return actualHeatRates;
        }
    }
}
