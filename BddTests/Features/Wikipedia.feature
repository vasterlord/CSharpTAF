Feature: Wikipedia

@beforeScenario
Scenario: Perform wikipedia search
	Given the user navigates to wikipedia page
	When the user is looking for information on request Test Automation 
	Then the all found links should contain with /wiki/