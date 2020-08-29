Feature: Login

Scenario: Login - Green Path
	Given that a user is on the login page
	When the user enters valid details
	Then they should be redirected to a list of their planner items

Scenario: Login - Red Path
	Given that a user is on the login page
	When the user enter an invalid password
	Then they should see an error message which doesn't expose whether or not an account exists