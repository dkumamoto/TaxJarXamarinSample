# interview_test_20220515

The keys are not committed:

In taxcalc/Services/TaxCalculators/CalculatorTaxJar.cs, please put in
the API key in the variable:

ServerKey (currently null)


The GUI supports only 2 inputs:

zip code for displaying the combined tax rate.

total order amount for displaying the calculated tax due for the order.

I've tested the solution on 

M1 Macbook Pro, macOS 12.3.1

XCode 13.3.1

VS Studio 2022 RC 2 (17.0 build 8904)


iOS: iPhone 13, iOS 15.4.1

Android: LG G8X ThinQ, Android 10


Note about unit test: Could not test CalculateTaxOfOrder method with bad data.
I keep getting error:
Unit test 'System.Collections.Generic.List`1[System.String]' could not be loaded.

And could not find a solution around it so I did not add it to the unit test.
