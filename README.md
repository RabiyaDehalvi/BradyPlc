# BradyPlc Auomation Test

This repository contains the source code in C# that uses SpecFlow BDD to test the GenerationReport.xml as input data and assert against the GenerationOutput.xml using the calculations mentioned in the requirements document.

Please note, the output file provided for this contains the wrong data hence there is an amended output file GenerationOutput_CorrectValues.xml in the output directory.

```
Input File (GenerationReport.xml) --> Calculate --> Assert Calcualted values with GenerationOutput.xml
```

## Getting Started

To get this solution up and running, you will need to amend the filepaths specified in specFlow.json.

### Prerequisites

1. You will need Specflow extension and NUnit Test Adapter for VS 2019.
2. You have amended the file paths in specflow.json.
