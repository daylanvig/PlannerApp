Feature: Register

@mytag
Scenario: Register - Green Path
	Given that a user is on the registration page
	When the user provides valid registration details
	Then they should be redirected to the login page
	And an account should be created for them

@mytag
Scenario: Register - Red Path
	Given that a user is on the registration page
	When the user provides invalid registration details
	Then an error message should be returned to them
	And an account should not be created