### Bank Application

## Changelog
- [0.0.2]
  - Extended methods and added Account management functionality.
    - Note that the ability to deposit/withdraw/show balance for designated accounts have been substituted with simply using the currently active account.
  - Expanded Account based on Abstract Account class to 3 subtypes. Added interest rates for each, as well as iteration on these in Bank class.
  

- [0.0.1]
  - Added Classes: Program, Bank and Account

    - Program Methods:
      - Program (mostly empty)
      - Run (Initiator)
      - Menu (Menu for further functionality and useability)
      - Select (Separate method called based on user-selection during Menu())

    - Bank Methods:
      - CreateAccount (establishes account, with name, and optionally with initial balance)
      - Deposit (deposits a decimal value to the accounts balance, and informs the user of the changes)
      - Withdraw (as above, but withdraws rather than deposits)
      - Balance (Shows the current balance of the account)

      - Bank Helper Methods:
        - _getAmount (sanitizes user input to try and avoid FormatExceptions due to user error)
        - _InitialBalanceCheck (lets the user dynamically (try) to add an initial balance upon account creation)

      - Account Methods:
        - Two Account constructors, one overrides with variable "balance" in addition to required "name"