
Feature: Verify generation report placed in input folder

Scenario Outline: Calculate "Total generation" value for each <generator> generator
  Given a generation report file is placed in input folder
  When I read the file from input folder
  And I calculate total generation value for each "<source>" generator with "<value>" valueFactor 
  Then the total generation value should match the "<source>" data from the output file

  Examples:
  |source           |value      |
  |Coal             |medium     |   
  |Gas              |medium     |
  |Wind[Onshore]    |high       |
  |Wind[Offshore]   |low        |

Scenario: Calculate "Actual heat rate" for each coal generator
  Given a generation report file is placed in input folder
  When I read the file from input folder
  And I calculate Actual Heat Rate for each fossil fuel generator
  Then the actual heat rate value should match the data from the output file 

Scenario: Calculate highest Daily Emissions for each day along with the emission value
  Given a generation report file is placed in input folder
  When I read the file from input folder
  And I calculate highest Daily Emissions for each day along with the emission value for each fossil fuel
  Then the calculated value should match the data from the output file   

