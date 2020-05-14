using BradlyPlc.Test.Test.DTOs.InputGeneration;
using BradlyPlc.Test.Test.DTOs.OutputGeneration;
using BradlyPlc.Test.Test.DTOs.ReferenceData;
using BradlyPlc.Test.Test.Helpers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace BradlyPlc.Test.Test.StepDefinitions
{
    [Binding]
    public class GenerationReport
    {

        private readonly IConfigurationRoot _config;
        private readonly XmlDataSourceProcessor _xmlDataSourceProcessor;
        private readonly EnergyCalculator _energyCalculator;
        private InputGenerationReport _input;
        private GenerationOutput _generationOutput;
        private ReferenceData _referenceData;
        private Totals _calculatedTotalValue;
        private IList<DTOs.OutputGeneration.Day> _maxEmissionGeneratorsDays;
        private IList<ActualHeatRates> _heatRates;

        public GenerationReport(InputGenerationReport input)
        {
            _config = new ConfigurationBuilder().AddJsonFile("specflow.json").Build();
            _xmlDataSourceProcessor = new XmlDataSourceProcessor();
            _input = input;
            _generationOutput = new GenerationOutput();
            _referenceData = new ReferenceData();
            _calculatedTotalValue = new Totals();
            _energyCalculator = new EnergyCalculator();
            _maxEmissionGeneratorsDays = new List<DTOs.OutputGeneration.Day>();
            _heatRates = new List<ActualHeatRates>();
            _generationOutput = _xmlDataSourceProcessor.GetGenerationOutput();
        }

        [Given(@"a generation report file is placed in input folder")]
        public void GivenAGenerationReportFileIsPlacedInInputFolder()
        {
            //ScenarioContext.Current.Pending();

            //Presumed file is in place, but if required it can be checked as below.
            //if (!File.Exists(_config.GetSection("inputFile").Value))
            //    Assert.Fail();

        }

        [When(@"I read the file from input folder")]
        public void WhenIReadTheFileFromInputFolder()
        {
            _input = _xmlDataSourceProcessor.GetGenerationReport();
            _referenceData = _xmlDataSourceProcessor.GetReferenceData();
        }

        [When(@"I calculate total generation value for each ""(.*)"" generator with ""(.*)"" valueFactor")]
        public void WhenICalculateTotalGenerationValueForEachGeneratorWithValueFactor(string generatorType, string valueFactor)
        {
            _calculatedTotalValue.Generator = new List<Generator>();

            switch (generatorType)
            {
                case "Coal":
                    foreach (var coalGenerator in _input.Coal.CoalGenerator)
                    {
                        _calculatedTotalValue.Generator.Add(_energyCalculator.GetCoalTotalGenerationValue(coalGenerator,
                            _referenceData.Factors.ValueFactor.Medium));
                    }
                    return;
                case "Gas":
                    foreach (var gasGenerator in _input.Gas.GasGenerator)
                    {
                        _calculatedTotalValue.Generator.Add(_energyCalculator.GetGasTotalGenerationValue(gasGenerator,
                            _referenceData.Factors.ValueFactor.Medium));
                    }
                    return;
                case "Wind[Onshore]":
                    foreach (var windGenerator in _input.Wind.WindGenerator.Where(a => a.Name.StartsWith("Wind[Onshore]")))
                    {
                        _calculatedTotalValue.Generator.Add(_energyCalculator.GetWindTotalGenerationValue(windGenerator,
                            _referenceData.Factors.ValueFactor.High));
                    }
                    return;
                case "Wind[Offshore]":
                    foreach (var windGenerator in _input.Wind.WindGenerator.Where(a => a.Name.StartsWith("Wind[Offshore]")))
                    {
                        _calculatedTotalValue.Generator.Add(_energyCalculator.GetWindTotalGenerationValue(windGenerator,
                            _referenceData.Factors.ValueFactor.Low));
                    }
                    return;
            }

        }
        [Then(@"the total generation value should match the ""(.*)"" data from the output file")]
        public void ThenTheTotalGenerationValueShouldMatchTheDataFromTheOutputFile(string generatorType)
        {
            var actualValue = _generationOutput.Totals.Generator.Where(a => a.Name.StartsWith(generatorType)).ToList();
            var expectedValue = _calculatedTotalValue.Generator;

            Assert.AreEqual(_calculatedTotalValue.Generator.Count, actualValue.Count());

            for (int index = 0; index < _calculatedTotalValue.Generator.Count; index++)
            {
                Assert.AreEqual(actualValue[index].Name, expectedValue[index].Name);
                Assert.AreEqual(actualValue[index].Total, expectedValue[index].Total);
            }

        }


        [When(@"I calculate Actual Heat Rate for each fossil fuel generator")]
        public void WhenICalculateActualHeatRateForEachCoalGenerator()
        {
            _heatRates = _energyCalculator.GetActualHeatRates(_input.Coal);
        }

        [Then(@"the actual heat rate value should match the data from the output file")]
        public void ThenTheActualHeatRateValueShouldMatchTheDataFromTheOutputFile()
        {
            var actualValue = _generationOutput.ActualHeatRates;
            var expectedValue = _heatRates;
            for (int index = 0; index < expectedValue.Count; index++)
            {
                Assert.AreEqual(expectedValue[index].HeatRate, actualValue[index].HeatRate);
                Assert.AreEqual(expectedValue[index].Name, actualValue[index].Name);
            }
        }

        [When(@"I calculate highest Daily Emissions for each day along with the emission value for each fossil fuel")]
        public void WhenICalculateHighestDailyEmissionsForEachDayAlongWithTheEmissionValueForEachFossilFuelGenerator()
        {
            _maxEmissionGeneratorsDays = _energyCalculator.GetMaxEmissionsPerDay(_input.Gas,
                        _input.Coal, _referenceData.Factors.EmissionsFactor);
        }


        [Then(@"the calculated value should match the data from the output file")]
        public void ThenTheCalculatedValueShouldMatchTheDataFromTheOutputFile()
        {
            var actualValue = _generationOutput.MaxEmissionGenerators.Day;
            var expectedValue = _maxEmissionGeneratorsDays;

            Assert.AreEqual(expectedValue.ToList().Count, actualValue.Count());

            for (int index = 0; index < _maxEmissionGeneratorsDays.Count; index++)
            {
                Assert.AreEqual(actualValue[index].Name, expectedValue[index].Name);
                Assert.AreEqual(actualValue[index].Emission, expectedValue[index].Emission);
                Assert.AreEqual(actualValue[index].Date, expectedValue[index].Date);
            }
        }

    }
}
